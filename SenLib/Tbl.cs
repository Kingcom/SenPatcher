﻿using HyoutaPluginBase;
using HyoutaUtils;
using HyoutaUtils.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenLib {
	public class Tbl {
		public List<TblDefinition> Definitions;
		public List<TblEntry> Entries;

		public Tbl(DuplicatableStream stream, EndianUtils.Endianness e = EndianUtils.Endianness.LittleEndian) {
			ushort entryCount = stream.ReadUInt16(e);
			uint definitionCount = stream.ReadUInt32(e);
			List<TblDefinition> definitions = new List<TblDefinition>((int)definitionCount);
			for (uint i = 0; i < definitionCount; ++i) {
				var d = new TblDefinition();
				d.Name = stream.ReadUTF8Nullterm();
				d.Unknown = stream.ReadUInt32(e);
				definitions.Add(d);
			}

			List<TblEntry> entries = new List<TblEntry>(entryCount);
			for (int i = 0; i < entryCount; ++i) {
				var d = new TblEntry();
				d.Name = stream.ReadUTF8Nullterm();
				ushort count = GetLength(d.Name, stream, e);
				d.Data = stream.ReadBytes(count);
				entries.Add(d);
			}

			Definitions = definitions;
			Entries = entries;
		}

		private ushort GetLength(string name, DuplicatableStream stream, EndianUtils.Endianness e) {
			switch (name) {
				case "QSTitle": {
						// some of these have incorrect length fields in the official files, manually determine length here...
						stream.DiscardBytes(2);
						long p = stream.Position;
						stream.DiscardBytes(3);
						stream.ReadUTF8Nullterm();
						stream.ReadUTF8Nullterm();
						stream.DiscardBytes(13);
						ushort length = (ushort)(stream.Position - p);
						stream.Position = p;
						return length;
					}
				default:
					return stream.ReadUInt16(e);
			}
		}

		public void WriteToStream(Stream s, EndianUtils.Endianness e) {
			s.WriteUInt16((ushort)Entries.Count, e);
			s.WriteUInt32((uint)Definitions.Count, e);
			foreach (TblDefinition def in Definitions) {
				s.WriteUTF8Nullterm(def.Name);
				s.WriteUInt32(def.Unknown, e);
			}
			foreach (TblEntry entry in Entries) {
				s.WriteUTF8Nullterm(entry.Name);
				s.WriteUInt16((ushort)entry.Data.Length, e);
				s.Write(entry.Data);
			}
		}
	}

	public class TblDefinition {
		public string Name;
		public uint Unknown;

		public override string ToString() {
			return Name;
		}
	}

	public class TblEntry {
		public string Name;
		public byte[] Data;

		public override string ToString() {
			return Name;
		}
	}
}
