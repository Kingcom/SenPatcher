﻿using HyoutaUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenPatcherCli {
	public class ScriptFunction {
		public string Name;
		public List<string> Ops;
	}

	public static class ScriptParser {
		public static List<ScriptFunction> Parse(Stream s, EndianUtils.Endianness? endian = null) {
			EndianUtils.Endianness e = endian.HasValue ? endian.Value : (s.PeekUInt32(EndianUtils.Endianness.LittleEndian) == 0x20 ? EndianUtils.Endianness.LittleEndian : EndianUtils.Endianness.BigEndian);
			uint headerLength = s.ReadUInt32(e);
			if (headerLength != 0x20) {
				throw new Exception("unexpected header length");
			}

			uint namePosition = s.ReadUInt32(e);
			uint functionOffsetsPosition = s.ReadUInt32(e);
			uint functionOffsetsLength = s.ReadUInt32(e);
			uint functionNameOffsetsPosition = s.ReadUInt32(e);
			uint functionCount = s.ReadUInt32(e); // seems redundant between length and count...?
			if (functionCount * 4 != functionOffsetsLength) {
				throw new Exception("inconsistency"); // maybe one of the two means something else then?
			}
			uint functionMetadataEnd = s.ReadUInt32(e);
			uint unknown = s.ReadUInt32(e);

			string name = s.ReadAsciiNulltermFromLocationAndReset(namePosition);
			s.Position = functionOffsetsPosition;
			uint[] functionPositions = new uint[functionCount];
			for (long i = 0; i < functionCount; ++i) {
				functionPositions[i] = s.ReadUInt32(e);
			}
			s.Position = functionNameOffsetsPosition;
			ushort[] functionNamePositions = new ushort[functionCount];
			for (long i = 0; i < functionCount; ++i) {
				functionNamePositions[i] = s.ReadUInt16(e);
			}
			string[] functionNames = new string[functionCount];
			for (long i = 0; i < functionCount; ++i) {
				s.Position = functionNamePositions[i];
				functionNames[i] = s.ReadAsciiNullterm();
			}


			List<ScriptFunction> funcs = new List<ScriptFunction>();
			for (long i = 0; i < functionCount; ++i) {
				s.Position = functionPositions[i];
				var ops = ParseFunction(s, (i + 1) == functionCount ? s.Length : functionPositions[i + 1]);
				funcs.Add(new ScriptFunction() { Name = functionNames[i], Ops = ops });
			}

			return funcs;
		}

		private static List<string> ParseFunction(Stream s, long end) {
			// actually parsing this is more work than i thought it would be, so just guess
			// this will have false positives but that's okay
			List<string> text = new List<string>();
			while (s.Position < end) {
				byte a = s.ReadUInt8();
				if (a == 0x1a) {
					long originalPosition = s.Position;
					try {
						ushort speaker = s.ReadUInt16();
						List<byte> sb = new List<byte>();
						List<byte> contentbytes = new List<byte>();
						while (true) {
							byte next = s.ReadUInt8();
							if (next < 0x20) {
								if (next == 0) {
									break;
								} else if (next == 0x01) {
									sb.Add((byte)'{');
									sb.Add((byte)'n');
									sb.Add((byte)'}');
								} else if (next == 0x02) {
									sb.Add((byte)'{');
									sb.Add((byte)'f');
									sb.Add((byte)'}');
								} else if (next == 0x10) {
									s.ReadUInt16();
								} else if (next == 0x11 || next == 0x12) {
									s.ReadUInt32();
								}
							} else {
								//if (next == 0x23) {
								//	for (int i = 0; i < 2; ++i) {
								//		next = s.ReadUInt8();
								//		if (next == 0) {
								//			break;
								//		}
								//		// there's also some special case with 0x4b here, let's see if we can ignore this...
								//	}
								//} else {
								sb.Add(next);
								contentbytes.Add(next);
								//}
							}
						}
						if (LooksLikeValidString(contentbytes)) {
							string str = Encoding.UTF8.GetString(sb.ToArray());
							text.Add(str);
						}
						s.Position = originalPosition;
					} catch (Exception ex) {
						s.Position = originalPosition;
					}
				}
			}
			return text;
		}

		private static bool LooksLikeValidString(List<byte> contentbytes) {
			return contentbytes.Count >= 3;
		}

		/*
		private static List<string> ParseFunction(Stream s, long end) {
			List<string> ops = new List<string>();
			while (s.Position < end) {
				byte op = s.ReadUInt8();
				switch (op) {
					case 0x00: {
							// calls something else too
							ops.Add("command 0x00");
							break;
						}
					case 0x01: {
							ops.Add("return");
							break;
						}
					case 0x02: {
							byte flags = s.ReadUInt8(); // ?
							if (flags == 0x0B) {
								string func = s.ReadAsciiNullterm();
								ops.Add(string.Format("call local {0}", func));
							} else if (flags == 0x00) {
								string func = s.ReadAsciiNullterm();
								ops.Add(string.Format("call global {0}", func));
							} else if (flags == 0x00) {
								throw new Exception(string.Format("unknown flag {0:x2} in command 0x02", flags));
							}
							break;
						}
					case 0x03: {
							uint location = s.ReadUInt32();
							ops.Add(string.Format("jmp 0x{0:x8}", location));
							break;
						}
					case 0x05: {
							// this seems to be a *pretty damn wild* conditional jump statement
							// with a lot of options and multiple potential targets
							// we have to parse this in order to know how many bytes this statement consumes...
							HandleCommand05(s, ops);
							break;
						}
					case 0x09: {
							ops.Add("nop");
							break;
						}
					case 0x0a: {
							s.ReadUInt64();
							ops.Add("command 0x0a");
							break;
						}
					case 0x16: {
							s.ReadUInt8();
							ops.Add("command 0x16");
							break;
						}
					case 0x1e: {
							string a = s.ReadAsciiNullterm();
							string b = s.ReadAsciiNullterm();
							ops.Add(string.Format("call other script? ({0}) ({1})", a, b));
							break;
						}
					case 0x59: {
							s.ReadUInt16();
							ops.Add("command 0x59");
							break;
						}
					case 0x86: {
							byte subcommand = s.ReadUInt8();
							if (subcommand == 0x00) {
								s.ReadUInt32();
								ops.Add("command 0x8d/00");
							} else if (subcommand == 0x01) {
								s.ReadUInt32();
								ops.Add("command 0x8d/01");
							} else {
								ops.Add("command 0x8d/??");
							}
							break;
						}
					default:
						throw new Exception(string.Format("unknown command {0:x2}", op));
				}
			}

			return ops;
		}

		private static void HandleCommand05(Stream s, List<string> ops) {
			long extra_data = 0;
			while (true) {
				byte b = s.ReadUInt8();
				switch (b) {
					case 0x00:
						throw new Exception();
						break;
					case 0x01:
						goto exit_0x05;
					case 0x08:
						if (extra_data == 0) {
							extra_data = 1;
						} else {
							extra_data = 0;
						}
						break;
					case 0x1c:
						s.ReadUInt32(); // i think? this piece of code is very confusing
						break;
					case 0x1e:
						s.ReadUInt16();
						break;
					case 0x1f:
					case 0x20:
					case 0x23:
						s.ReadUInt8();
						break;
					case 0x21:
						s.ReadUInt16();
						s.ReadUInt8();
						break;
					default:
						break;
				}
			}
		exit_0x05:
			ops.Add(string.Format("command 0x05"));
		}
		*/
	}
}