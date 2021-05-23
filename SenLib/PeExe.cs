﻿using HyoutaPluginBase;
using HyoutaUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenLib {
	public class PeExe {
		private Coff Coff;
		private OptionalHeader OptionalHeader;
		private SectionHeader[] SectionHeaders;

		public PeExe(DuplicatableStream inputStream, EndianUtils.Endianness e) {
			DuplicatableStream s = inputStream.Duplicate();
			s.Position = 0x3c;
			uint sigOffset = s.ReadUInt32(e);
			s.Position = sigOffset;
			if (s.ReadUInt32(EndianUtils.Endianness.LittleEndian) != 0x4550) {
				throw new Exception("signature mismatch");
			}
			Coff coff = new Coff(s, e);
			long optionalHeaderStart = s.Position;
			OptionalHeader optionalHeader = new OptionalHeader(s, e, coff);
			s.Position = optionalHeaderStart + coff.SizeOfOptionalHeader;
			SectionHeader[] sectionHeaders = new SectionHeader[coff.NumberOfSections];
			for (int i = 0; i < coff.NumberOfSections; ++i) {
				sectionHeaders[i] = new SectionHeader(s, e);
			}

			this.Coff = coff;
			this.OptionalHeader = optionalHeader;
			this.SectionHeaders = sectionHeaders;
		}

		public IRomMapper CreateRomMapper() {
			List<GenericRomMapper.Region> parsedRegions = new List<GenericRomMapper.Region>();

			foreach (SectionHeader sh in SectionHeaders) {
				if ((sh.Characteristics & (SectionHeader.IMAGE_SCN_CNT_CODE | SectionHeader.IMAGE_SCN_CNT_INITIALIZED_DATA)) != 0) {
					parsedRegions.Add(new GenericRomMapper.Region(
						romStart: sh.PointerToRawData,
						ramStart: OptionalHeader.ImageBase + sh.VirtualAddress,
						length: sh.SizeOfRawData
					));
				}
			}

			return new GenericRomMapper(parsedRegions);
		}
	}

	public class Coff {
		public ushort Machine;
		public ushort NumberOfSections;
		public uint TimeDateStamp;
		public uint PointerToSymbolTable;
		public uint NumberOfSymbols;
		public ushort SizeOfOptionalHeader;
		public ushort Characteristics;

		public Coff(DuplicatableStream s, EndianUtils.Endianness e) {
			Machine = s.ReadUInt16(e);
			NumberOfSections = s.ReadUInt16(e);
			TimeDateStamp = s.ReadUInt32(e);
			PointerToSymbolTable = s.ReadUInt32(e);
			NumberOfSymbols = s.ReadUInt32(e);
			SizeOfOptionalHeader = s.ReadUInt16(e);
			Characteristics = s.ReadUInt16(e);
		}
	}

	public class OptionalHeader {
		public ushort Magic;
		public byte MajorLinkerVersion;
		public byte MinorLinkerVersion;
		public uint SizeOfCode;
		public uint SizeOfInitializedData;
		public uint SizeOfUninitializedData;
		public uint AddressOfEntryPoint;
		public uint BaseOfCode;
		public uint BaseOfData;

		public ulong ImageBase;
		public uint SectionAlignment;
		public uint FileAlignment;
		public ushort MajorOperatingSystemVersion;
		public ushort MinorOperatingSystemVersion;
		public ushort MajorImageVersion;
		public ushort MinorImageVersion;
		public ushort MajorSubsystemVersion;
		public ushort MinorSubsystemVersion;
		public uint Win32VersionValue;
		public uint SizeOfImage;
		public uint SizeOfHeaders;
		public uint CheckSum;
		public ushort Subsystem;
		public ushort DllCharacteristics;
		public ulong SizeOfStackReserve;
		public ulong SizeOfStackCommit;
		public ulong SizeOfHeapReserve;
		public ulong SizeOfHeapCommit;
		public uint LoaderFlags;
		public uint NumberOfRvaAndSizes;

		public struct ImageDataDirectory {
			public uint VirtualAddress;
			public uint Size;

			public ImageDataDirectory(DuplicatableStream s, EndianUtils.Endianness e) {
				VirtualAddress = s.ReadUInt32(e);
				Size = s.ReadUInt32(e);
			}
		}

		public ImageDataDirectory[] DataDirectories;
		public const int ImageDataDirectoryIndexExportTable = 0;
		public const int ImageDataDirectoryIndexImportTable = 1;
		public const int ImageDataDirectoryIndexResourceTable = 2;
		public const int ImageDataDirectoryIndexExceptionTable = 3;
		public const int ImageDataDirectoryIndexCertificateTable = 4;
		public const int ImageDataDirectoryIndexBaseRelocationTable = 5;
		public const int ImageDataDirectoryIndexDebug = 6;
		public const int ImageDataDirectoryIndexArchitecture = 7;
		public const int ImageDataDirectoryIndexGlobalPtr = 8;
		public const int ImageDataDirectoryIndexTLSTable = 9;
		public const int ImageDataDirectoryIndexLoadConfigTable = 10;
		public const int ImageDataDirectoryIndexBoundImport = 11;
		public const int ImageDataDirectoryIndexIAT = 12;
		public const int ImageDataDirectoryIndexDelayImportDescriptor = 13;
		public const int ImageDataDirectoryIndexCLRRuntimeHeader = 14;
		public const int ImageDataDirectoryIndexReserved = 15;

		public const ushort PE32_MAGIC = 0x10b;
		public const ushort PE32PLUS_MAGIC = 0x20b;

		public OptionalHeader(DuplicatableStream s, EndianUtils.Endianness e, Coff coff) {
			Magic = s.ReadUInt16(e);
			MajorLinkerVersion = s.ReadUInt8();
			MinorLinkerVersion = s.ReadUInt8();
			SizeOfCode = s.ReadUInt32(e);
			SizeOfInitializedData = s.ReadUInt32(e);
			SizeOfUninitializedData = s.ReadUInt32(e);
			AddressOfEntryPoint = s.ReadUInt32(e);
			BaseOfCode = s.ReadUInt32(e);
			BaseOfData = Magic == PE32_MAGIC ? s.ReadUInt32(e) : 0;

			var bits = Magic == PE32PLUS_MAGIC ? BitUtils.Bitness.B64 : BitUtils.Bitness.B32;
			ImageBase = s.ReadUInt(bits, e);
			SectionAlignment = s.ReadUInt32(e);
			FileAlignment = s.ReadUInt32(e);
			MajorOperatingSystemVersion = s.ReadUInt16(e);
			MinorOperatingSystemVersion = s.ReadUInt16(e);
			MajorImageVersion = s.ReadUInt16(e);
			MinorImageVersion = s.ReadUInt16(e);
			MajorSubsystemVersion = s.ReadUInt16(e);
			MinorSubsystemVersion = s.ReadUInt16(e);
			Win32VersionValue = s.ReadUInt32(e);
			SizeOfImage = s.ReadUInt32(e);
			SizeOfHeaders = s.ReadUInt32(e);
			CheckSum = s.ReadUInt32(e);
			Subsystem = s.ReadUInt16(e);
			DllCharacteristics = s.ReadUInt16(e);
			SizeOfStackReserve = s.ReadUInt(bits, e);
			SizeOfStackCommit = s.ReadUInt(bits, e);
			SizeOfHeapReserve = s.ReadUInt(bits, e);
			SizeOfHeapCommit = s.ReadUInt(bits, e);
			LoaderFlags = s.ReadUInt32(e);
			NumberOfRvaAndSizes = s.ReadUInt32(e);

			DataDirectories = new ImageDataDirectory[NumberOfRvaAndSizes];
			for (uint i = 0; i < NumberOfRvaAndSizes; ++i) {
				DataDirectories[i] = new ImageDataDirectory(s, e);
			}
		}
	}

	public class SectionHeader {
		public ulong Name;
		public uint VirtualSize;
		public uint VirtualAddress;
		public uint SizeOfRawData;
		public uint PointerToRawData;
		public uint PointerToRelocations;
		public uint PointerToLinenumbers;
		public ushort NumberOfRelocations;
		public ushort NumberOfLinenumbers;
		public uint Characteristics;

		public const uint IMAGE_SCN_CNT_CODE = 0x00000020u;
		public const uint IMAGE_SCN_CNT_INITIALIZED_DATA = 0x00000040u;
		public const uint IMAGE_SCN_CNT_UNINITIALIZED_DATA = 0x00000080u;
		public const uint IMAGE_SCN_LNK_NRELOC_OVFL = 0x01000000u;
		public const uint IMAGE_SCN_MEM_DISCARDABLE = 0x02000000u;
		public const uint IMAGE_SCN_MEM_NOT_CACHED = 0x04000000u;
		public const uint IMAGE_SCN_MEM_NOT_PAGED = 0x08000000u;
		public const uint IMAGE_SCN_MEM_SHARED = 0x10000000u;
		public const uint IMAGE_SCN_MEM_EXECUTE = 0x20000000u;
		public const uint IMAGE_SCN_MEM_READ = 0x40000000u;
		public const uint IMAGE_SCN_MEM_WRITE = 0x80000000u;

		public SectionHeader(DuplicatableStream s, EndianUtils.Endianness e) {
			Name = s.ReadUInt64(e);
			VirtualSize = s.ReadUInt32(e);
			VirtualAddress = s.ReadUInt32(e);
			SizeOfRawData = s.ReadUInt32(e);
			PointerToRawData = s.ReadUInt32(e);
			PointerToRelocations = s.ReadUInt32(e);
			PointerToLinenumbers = s.ReadUInt32(e);
			NumberOfRelocations = s.ReadUInt16(e);
			NumberOfLinenumbers = s.ReadUInt16(e);
			Characteristics = s.ReadUInt32(e);
		}
	}
}
