﻿using HyoutaUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenLib.Sen2 {
	public class Sen2ExecutablePatchState {
		public RegionHelper Region50a = null;
		public RegionHelper Region50b = null;
		public RegionHelper Region60 = null;
		public RegionHelper Region51 = null;
		public RegionHelper Region41 = null;
		public RegionHelper Region32 = null;
		public RegionHelper RegionD = null;

		public void InitCodeSpaceIfNeeded(Stream binary, Sen2ExecutablePatchInterface patchInfo) {
			if (Region50a != null) {
				// already initialized
				return;
			}

			var mapper = new Sen2Mapper();
			var c = patchInfo.GetCodeSpaceLocations();

			// make some free space to actually put our expanded code in
			// note that there's a few more functions around here that the same logic could be applied to for even more space if we need it
			// (though I think I found the biggest blocks already, hard to tell)
			using (var b = new BranchHelper4Byte(binary, mapper)) {
				// inject a jmp 0x4219cb, as that target has the same function tail as this one, so we can get us 0x50 bytes of free space
				long source = c.Region50aAddress;
				binary.Position = (long)mapper.MapRamToRom((ulong)source);
				b.SetTarget(c.Region50aTarget);
				b.WriteJump5Byte(0xe9);
				Region50a = new RegionHelper((uint)mapper.MapRomToRam((ulong)binary.Position), c.Region50aLength, "Region50a");
				for (uint i = 0; i < c.Region50aLength; ++i) {
					binary.WriteUInt8(0xcc);
				}
			}
			using (var b = new BranchHelper4Byte(binary, mapper)) {
				// same here
				long source = c.Region50bAddress;
				binary.Position = (long)mapper.MapRamToRom((ulong)source);
				b.SetTarget(c.Region50bTarget);
				b.WriteJump5Byte(0xe9);
				Region50b = new RegionHelper((uint)mapper.MapRomToRam((ulong)binary.Position), c.Region50bLength, "Region50b");
				for (uint i = 0; i < c.Region50bLength; ++i) {
					binary.WriteUInt8(0xcc);
				}
			}
			using (var b = new BranchHelper4Byte(binary, mapper)) {
				// same logic, different target, this gives us 0x60 bytes
				long source = c.Region60Address;
				binary.Position = (long)mapper.MapRamToRom((ulong)source);
				b.SetTarget(c.Region60Target);
				b.WriteJump5Byte(0xe9);
				Region60 = new RegionHelper((uint)mapper.MapRomToRam((ulong)binary.Position), c.Region60Length, "Region60");
				for (uint i = 0; i < c.Region60Length; ++i) {
					binary.WriteUInt8(0xcc);
				}
			}
			using (var b = new BranchHelper4Byte(binary, mapper)) {
				// and one more for 0x51 bytes
				long source = c.Region51Address;
				binary.Position = (long)mapper.MapRamToRom((ulong)source);
				b.SetTarget(c.Region51Target);
				b.WriteJump5Byte(0xe9);
				Region51 = new RegionHelper((uint)mapper.MapRomToRam((ulong)binary.Position), c.Region51Length, "Region51");
				for (uint i = 0; i < c.Region51Length; ++i) {
					binary.WriteUInt8(0xcc);
				}
			}
			using (var b = new BranchHelper4Byte(binary, mapper)) {
				long source = c.Region41Address;
				binary.Position = (long)mapper.MapRamToRom((ulong)source);
				b.SetTarget(c.Region41Target);
				b.WriteJump5Byte(0xe9);
				Region41 = new RegionHelper((uint)mapper.MapRomToRam((ulong)binary.Position), c.Region41Length, "Region41");
				for (uint i = 0; i < c.Region41Length; ++i) {
					binary.WriteUInt8(0xcc);
				}
			}
			using (var b = new BranchHelper4Byte(binary, mapper)) {
				long source = c.Region32Address;
				binary.Position = (long)mapper.MapRamToRom((ulong)source);
				b.SetTarget(c.Region32Target);
				b.WriteJump5Byte(0xe9);
				Region32 = new RegionHelper((uint)mapper.MapRomToRam((ulong)binary.Position), c.Region32Length, "Region32");
				for (uint i = 0; i < c.Region32Length; ++i) {
					binary.WriteUInt8(0xcc);
				}
			}
			{
				// this one is just a between-function padding, but good enough for a small snippet
				RegionD = new RegionHelper(c.RegionDAddress, c.RegionDLength, "RegionD");
			}
		}

		public void PrintStatistics() {
			Console.WriteLine();
			Region50a.PrintStatistics();
			Region50b.PrintStatistics();
			Region60.PrintStatistics();
			Region51.PrintStatistics();
			Region41.PrintStatistics();
			Region32.PrintStatistics();
			RegionD.PrintStatistics();
		}
	}
}
