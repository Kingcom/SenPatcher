﻿using HyoutaUtils;
using HyoutaUtils.Streams;
using System;
using System.Collections.Generic;
using System.IO;

namespace SenLib.Sen1.FileFixes {
	public class ItemData {
		public ushort Idx;
		public ushort Usability;
		public string Flags;
		public byte ItemType;
		public byte Element;
		public byte Status_Breaker;
		public byte Range;
		public byte AttackArea;
		public byte Action1_PassiveEffect;
		public ushort Action1Value1_Art2;
		public ushort Action1Value2_Art3;
		public byte Action2_Rarity;
		public ushort Action2Value1_Art1;
		public ushort Action2Value2_ProcChance;
		public ushort STR;
		public ushort DEF;
		public ushort ATS;
		public ushort ADF;
		public ushort ACC;
		public ushort EVA;
		public ushort SPD;
		public ushort MOV;
		public ushort HP;
		public ushort EP;
		public uint Mira;
		public byte CarryLimit;
		public ushort SortOrder;
		public ushort Unknown1;
		public ushort Unknown2;
		public string Name;
		public string Desc;

		public static ushort[] ReadUInt16Array(Stream s, long count, EndianUtils.Endianness endianness = EndianUtils.Endianness.LittleEndian) {
			ushort[] data = new ushort[count];
			for (long i = 0; i < count; ++i) {
				data[i] = s.ReadUInt16(endianness);
			}
			return data;
		}

		public static void WriteUInt16Array(Stream s, ushort[] data, EndianUtils.Endianness endianness = EndianUtils.Endianness.LittleEndian) {
			foreach (ushort d in data) {
				s.WriteUInt16(d, endianness);
			}
		}

		public ItemData(byte[] data, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian, TextUtils.GameTextEncoding encoding = TextUtils.GameTextEncoding.UTF8) {
			var stream = new DuplicatableByteArrayStream(data);
			Idx = stream.ReadUInt16(endian);
			Usability = stream.ReadUInt16(endian);
			Flags = stream.ReadNulltermString(encoding);
			ItemType = stream.ReadUInt8();
			Element = stream.ReadUInt8();
			Status_Breaker = stream.ReadUInt8();
			Range = stream.ReadUInt8();
			AttackArea = stream.ReadUInt8();
			Action1_PassiveEffect = stream.ReadUInt8();
			Action1Value1_Art2 = stream.ReadUInt16(endian);
			Action1Value2_Art3 = stream.ReadUInt16(endian);
			Action2_Rarity = stream.ReadUInt8();
			Action2Value1_Art1 = stream.ReadUInt16(endian);
			Action2Value2_ProcChance = stream.ReadUInt16(endian);
			STR = stream.ReadUInt16(endian);
			DEF = stream.ReadUInt16(endian);
			ATS = stream.ReadUInt16(endian);
			ADF = stream.ReadUInt16(endian);
			ACC = stream.ReadUInt16(endian);
			EVA = stream.ReadUInt16(endian);
			SPD = stream.ReadUInt16(endian);
			MOV = stream.ReadUInt16(endian);
			HP = stream.ReadUInt16(endian);
			EP = stream.ReadUInt16(endian);
			Mira = stream.ReadUInt32(endian);
			CarryLimit = stream.ReadUInt8();
			SortOrder = stream.ReadUInt16(endian);
			Unknown1 = stream.ReadUInt16(endian);
			Unknown2 = stream.ReadUInt16(endian);
			Name = stream.ReadNulltermString(encoding);
			Desc = stream.ReadNulltermString(encoding);
		}

		public byte[] ToBinary(EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian, TextUtils.GameTextEncoding encoding = TextUtils.GameTextEncoding.UTF8) {
			MemoryStream ms = new MemoryStream();
			ms.WriteUInt16(Idx, endian);
			ms.WriteUInt16(Usability, endian);
			ms.WriteNulltermString(Flags, encoding);
			ms.WriteUInt8(ItemType);
			ms.WriteUInt8(Element);
			ms.WriteUInt8(Status_Breaker);
			ms.WriteUInt8(Range);
			ms.WriteUInt8(AttackArea);
			ms.WriteUInt8(Action1_PassiveEffect);
			ms.WriteUInt16(Action1Value1_Art2, endian);
			ms.WriteUInt16(Action1Value2_Art3, endian);
			ms.WriteUInt8(Action2_Rarity);
			ms.WriteUInt16(Action2Value1_Art1, endian);
			ms.WriteUInt16(Action2Value2_ProcChance, endian);
			ms.WriteUInt16(STR, endian);
			ms.WriteUInt16(DEF, endian);
			ms.WriteUInt16(ATS, endian);
			ms.WriteUInt16(ADF, endian);
			ms.WriteUInt16(ACC, endian);
			ms.WriteUInt16(EVA, endian);
			ms.WriteUInt16(SPD, endian);
			ms.WriteUInt16(MOV, endian);
			ms.WriteUInt16(HP, endian);
			ms.WriteUInt16(EP, endian);
			ms.WriteUInt32(Mira, endian);
			ms.WriteUInt8(CarryLimit);
			ms.WriteUInt16(SortOrder, endian);
			ms.WriteUInt16(Unknown1, endian);
			ms.WriteUInt16(Unknown2, endian);
			ms.WriteNulltermString(Name, encoding);
			ms.WriteNulltermString(Desc, encoding);
			return ms.CopyToByteArrayAndDispose();
		}

		public override string ToString() {
			return Name + " / " + Desc;
		}
	}

	public class text_dat_us_t_item_tbl : FileMod {
		public string GetDescription() {
			return "Series consistency fixes in EN item descriptions.";
		}

		public static string GetMagicClass(long value) {
			string c = "D";
			if (value >= 120) c = "C";
			if (value >= 135) c = "C+";
			if (value >= 170) c = "B";
			if (value >= 200) c = "B+";
			if (value >= 210) c = "A";
			if (value >= 240) c = "A+";
			if (value >= 255) c = "S";
			if (value >= 295) c = "S+";
			if (value >= 315) c = "SS";
			if (value >= 345) c = "SS+";
			if (value >= 370) c = "SSS";
			if (value >= 405) c = "SSS+";
			if (value >= 450) c = "4S";
			if (value >= 600) c = "5S";
			return c;
		}

		public static void PatchItemTbl(Tbl tbl) {
			//List<ItemData> items = new List<ItemData>();
			//foreach (TblEntry entry in tbl.Entries) {
			//	items.Add(new ItemData(entry.Data));
			//}

			{
				// stray letter
				int idx = 743;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.Remove(8, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			{
				// incorrect plural
				int idx = 105;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Name = item.Name.Remove(item.Name.Length - 1, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			{
				// missing stat
				int idx = 157;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.InsertSubstring(20, new ItemData(tbl.Entries[156].Data).Desc, 7, 6);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			{
				// missing equip limitation
				int idx = 159;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.InsertSubstring(14, new ItemData(tbl.Entries[168].Data).Desc, 27, 11);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			// stat formatting consistency
			{
				int idx = 363;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.InsertSubstring(11, item.Desc, 15, 2);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			foreach (int idx in new int[] { 143, 179, 217, 242, 250 }) {
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.Replace('\u3010', '[');
				item.Desc = item.Desc.Replace('\u3011', ']');
				item.Desc = item.Desc.Replace(' ', '/');
				tbl.Entries[idx].Data = item.ToBinary();
			}

			for (int i = 873; i < 953; ++i) {
				// stray spaces at the end of the item name
				int idx = i;
				var item = new ItemData(tbl.Entries[idx].Data);
				if (item.Name.EndsWith(" ")) {
					item.Name = item.Name.Substring(0, item.Name.Length - 1);
					tbl.Entries[idx].Data = item.ToBinary();
				}
			}

			// a bunch of 'easy to use' -> 'easy-to-use' and the like
			{
				int idx = 127;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(28, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(31, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 128;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(41, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(44, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 144;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(50, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(53, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 147;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(64, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(67, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 150;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(64, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(67, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 151;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(64, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(67, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 185;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(23, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(26, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 192;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(24, 1, " ", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(36, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(39, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 193;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(49, 1, " ", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(61, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(64, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 196;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(49, 1, " ", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(61, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(64, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 199;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(49, 1, " ", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(61, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(64, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 225;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(30, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(33, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 251;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(20, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(23, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 252;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(20, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(23, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 253;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(33, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(36, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 197;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(27, 1, "-", 0, 1);
				item.Desc = item.Desc.ReplaceSubstring(30, 1, "-", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 132;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(38, 1, " ", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 181;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(24, 1, " ", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			// capitalization consistency in Heat Up/R
			{
				int idx = 609;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(33, 9, new ItemData(tbl.Entries[21].Data).Desc, 7, 9);
				item.Desc = item.Desc.ReplaceSubstring(57, 9, new ItemData(tbl.Entries[21].Data).Desc, 7, 9);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 624;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(36, 9, new ItemData(tbl.Entries[21].Data).Desc, 7, 9);
				item.Desc = item.Desc.ReplaceSubstring(68, 9, new ItemData(tbl.Entries[21].Data).Desc, 7, 9);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			// capitalization consistency in Saintly Force/R
			{
				int idx = 728;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(36, 1, "t", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 740;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.ReplaceSubstring(39, 1, "t", 0, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			foreach (int idx in new int[] { 608, 609, 623, 624, 931 }) {
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = Sen2.FileFixes.text_dat_us_t_item_tbl.FixHpEpCpSpacing(item.Desc);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			// double space in Needle Shoot/R
			{
				int idx = 537;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.Remove(19, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 551;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.Remove(22, 1);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			// poorly-sounding description on Nightmare, sync to CS2's
			{
				int idx = 688;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.InsertSubstring(38, new ItemData(tbl.Entries[527].Data).Desc, 47, 8);
				item.Desc = item.Desc.ReplaceSubstring(46, 17, item.Name, 0, item.Name.Length);
				tbl.Entries[idx].Data = item.ToBinary();
			}

			// add turn count to Chrono Break/R
			{
				int idx = 672;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.InsertSubstring(33, new ItemData(tbl.Entries[671].Data).Desc, 41, 10);
				tbl.Entries[idx].Data = item.ToBinary();
			}
			{
				int idx = 686;
				var item = new ItemData(tbl.Entries[idx].Data);
				item.Desc = item.Desc.InsertSubstring(36, new ItemData(tbl.Entries[671].Data).Desc, 41, 10);
				tbl.Entries[idx].Data = item.ToBinary();
			}
		}

		public static void SyncItemMagicTbl(Tbl item_tbl, Tbl magic_tbl) {
			var magic = new Dictionary<ushort, MagicData>();
			foreach (var e in magic_tbl.Entries) {
				var m = new MagicData(e.Data);
				magic.Add(m.Idx, m);
			}

			foreach (TblEntry entry in item_tbl.Entries) {
				var item = new ItemData(entry.Data);
				if (item.ItemType == 0xaa) {
					bool isSingleArt = IsSingleArt(item) && magic[item.Action2Value1_Art1].Name == item.Name;

					// series consistency: add R to rare quartzes
					if (isSingleArt && item.Action2_Rarity == 1) {
						item.Name += " R";
					}

					//if (isSingleArt) {
					//	string itemdesc0 = item.Desc.Split('\n')[0].Replace("(R)", "").Replace("(SR)", "");
					//	string itemdesc1 = item.Desc.Split('\n')[1];
					//	if (itemdesc1.StartsWith("(")) {
					//		itemdesc1 = itemdesc1.Substring(itemdesc1.IndexOf(")") + 2);
					//	}
					//	string magicdesc0 = magic[item.Action2Value1_Art1].Desc.Split('\n')[0];
					//	string magicdesc1 = magic[item.Action2Value1_Art1].Desc.Split('\n')[1];
					//	if (itemdesc0 != magicdesc0) {
					//		Console.WriteLine();
					//		Console.WriteLine(item.Name);
					//		Console.WriteLine(itemdesc0);
					//		Console.WriteLine(magicdesc0);
					//		Console.WriteLine();
					//	}
					//	if (itemdesc1 != magicdesc1) {
					//		Console.WriteLine();
					//		Console.WriteLine(item.Name);
					//		Console.WriteLine(itemdesc1);
					//		Console.WriteLine(magicdesc1);
					//		Console.WriteLine();
					//	}
					//}

					// series consistency: add magic power class to description
					if (isSingleArt) {
						int where = item.Desc.IndexOf(']');
						if (where != -1) {
							var m = magic[item.Action2Value1_Art1];
							if (m.Effect1_Type == 0x01 || m.Effect1_Type == 0x02 || m.Effect1_Type == 0x70 || (m.Effect1_Type >= 0xd9 && m.Effect1_Type <= 0xdd) || m.Effect1_Type == 0xdf) {
								item.Desc = item.Desc.Insert(where, " - Class " + GetMagicClass(m.Effect1_Value1));
							}
						}
					}

					// series consistency: quartz that just boost a stat and nothing else should say Stat Boost
					if (IsStatusQuartz(item)) {
						item.Desc = item.Desc.Insert(5, "Stat Boost");
					}

					// some quartz have incorrect (R)/(SR) in description
					FixRarenessIdentifierInDescription(item);

					// series consistency: quartz that have passive effects should say Special
					if (IsSpecialQuartz(item)) {
						item.Desc = item.Desc.Insert(item.Action2_Rarity == 0 ? 5 : (item.Action2_Rarity == 1 ? 8 : 9), "Special");
					}

					entry.Data = item.ToBinary();
				}
			}

			// sync inconsistent descriptions across quartz/magic
			SyncEffect(item_tbl, 609, magic_tbl, 33, useMagic: false); // Heat Up
			SyncEffect(item_tbl, 624, magic_tbl, 33, useMagic: false); // Heat Up R
			SyncEffect(item_tbl, 685, magic_tbl, 48, useMagic: true); // Chrono Drive R
			SyncEffect(item_tbl, 673, magic_tbl, 50, useMagic: true); // Chrono Burst
			SyncEffect(item_tbl, 687, magic_tbl, 50, useMagic: true); // Chrono Burst R
			SyncEffect(item_tbl, 708, magic_tbl, 53, useMagic: true); // Cross Crusade R
			SyncEffect(item_tbl, 709, magic_tbl, 54, useMagic: true); // Altair Cannon R
			SyncEffect(item_tbl, 699, magic_tbl, 55, useMagic: true); // Fortuna
			SyncEffect(item_tbl, 710, magic_tbl, 55, useMagic: true); // Fortuna R
			SyncEffect(item_tbl, 706, magic_tbl, 51, useMagic: true); // Golden Sphere R
			SyncEffect(item_tbl, 707, magic_tbl, 52, useMagic: true); // Dark Matter R
			SyncEffect(item_tbl, 711, magic_tbl, 56, useMagic: true); // Shining R
			SyncEffect(item_tbl, 712, magic_tbl, 57, useMagic: true); // Seraphic Ring R
			SyncEffect(item_tbl, 725, magic_tbl, 60, useMagic: false); // Phantom Phobia
			SyncEffect(item_tbl, 737, magic_tbl, 60, useMagic: false); // Phantom Phobia R
			SyncEffect(item_tbl, 728, magic_tbl, 63, useMagic: false); // Saintly Force
			SyncEffect(item_tbl, 740, magic_tbl, 63, useMagic: false); // Saintly Force R
			SyncEffect(item_tbl, 729, magic_tbl, 64, useMagic: true); // Crescent Mirror
			SyncEffect(item_tbl, 741, magic_tbl, 64, useMagic: true); // Crescent Mirror R
			SyncDescription(item_tbl, 543, magic_tbl, 13, useMagic: false); // Crest
			SyncDescription(item_tbl, 543, magic_tbl, 13, useMagic: false); // Crest R
			SyncDescription(item_tbl, 557, magic_tbl, 14, useMagic: false); // La Crest
			SyncDescription(item_tbl, 558, magic_tbl, 14, useMagic: false); // La Crest R
			SyncDescription(item_tbl, 545, magic_tbl, 15, useMagic: true); // Adamantine Shield
			SyncDescription(item_tbl, 559, magic_tbl, 15, useMagic: true); // Adamantine Shield R
			SyncDescription(item_tbl, 605, magic_tbl, 29, useMagic: false); // Volcanic Rain
			SyncDescription(item_tbl, 620, magic_tbl, 29, useMagic: false); // Volcanic Rain R
			SyncDescription(item_tbl, 607, magic_tbl, 31, useMagic: true); // Purgatorial Flame
			SyncDescription(item_tbl, 622, magic_tbl, 31, useMagic: true); // Purgatorial Flame R
			SyncDescription(item_tbl, 609, magic_tbl, 33, useMagic: false); // Heat Up
			SyncDescription(item_tbl, 624, magic_tbl, 33, useMagic: false); // Heat Up R
			SyncDescription(item_tbl, 610, magic_tbl, 34, useMagic: true); // Forte
			SyncDescription(item_tbl, 625, magic_tbl, 34, useMagic: true); // Forte R
			SyncDescription(item_tbl, 611, magic_tbl, 35, useMagic: true); // La Forte
			SyncDescription(item_tbl, 626, magic_tbl, 35, useMagic: true); // La Forte R
			SyncDescription(item_tbl, 639, magic_tbl, 40, useMagic: true); // Ragna Vortex
			SyncDescription(item_tbl, 654, magic_tbl, 40, useMagic: true); // Ragna Vortex R
			SyncDescription(item_tbl, 667, magic_tbl, 44, useMagic: true); // Soul Blur
			SyncDescription(item_tbl, 681, magic_tbl, 44, useMagic: true); // Soul Blur R
			SyncDescription(item_tbl, 668, magic_tbl, 45, useMagic: true); // Demonic Scythe
			SyncDescription(item_tbl, 682, magic_tbl, 45, useMagic: true); // Demonic Scythe R
			SyncDescription(item_tbl, 669, magic_tbl, 46, useMagic: true); // Grim Butterfly
			SyncDescription(item_tbl, 683, magic_tbl, 46, useMagic: true); // Grim Butterfly R
			SyncDescription(item_tbl, 673, magic_tbl, 50, useMagic: true); // Chrono Burst
			SyncDescription(item_tbl, 687, magic_tbl, 50, useMagic: true); // Chrono Burst R
			SyncDescription(item_tbl, 697, magic_tbl, 53, useMagic: true); // Cross Crusade
			SyncDescription(item_tbl, 708, magic_tbl, 53, useMagic: true); // Cross Crusade R
			SyncDescription(item_tbl, 698, magic_tbl, 54, useMagic: false); // Altair Cannon
			SyncDescription(item_tbl, 709, magic_tbl, 54, useMagic: false); // Altair Cannon R
			SyncDescription(item_tbl, 699, magic_tbl, 55, useMagic: true); // Fortuna
			SyncDescription(item_tbl, 710, magic_tbl, 55, useMagic: true); // Fortuna R
			SyncDescription(item_tbl, 726, magic_tbl, 61, useMagic: true); // Claiomh Solarion
			SyncDescription(item_tbl, 738, magic_tbl, 61, useMagic: true); // Claiomh Solarion R
		}

		private static void SyncEffect(Tbl itemTbl, int itemId, Tbl magicTbl, int magicId, bool useMagic) {
			var itemEntry = itemTbl.Entries[itemId];
			var item = new ItemData(itemEntry.Data);
			var magicEntry = magicTbl.Entries[magicId];
			var magic = new MagicData(magicEntry.Data);
			int itemDescEnd = item.Desc.LastIndexOf('\n');
			int magicDescEnd = magic.Desc.LastIndexOf('\n');
			if (useMagic) {
				// copy magic to item
				string effect = magic.Desc.Substring(0, magicDescEnd);
				if (item.Desc[5] == '(') {
					int thing = 5;
					while (item.Desc[thing] != ')') { ++thing; }
					string rarity = item.Desc.Substring(5, thing - 4);
					effect = effect.Insert(5, rarity);
				}
				item.Desc = item.Desc.ReplaceSubstring(0, itemDescEnd, effect, 0, effect.Length);
				itemEntry.Data = item.ToBinary();
			} else {
				// copy item to magic
				string effect = item.Desc.Substring(0, itemDescEnd);
				if (effect[5] == '(') {
					int thing = 5;
					while (effect[thing] != ')') { ++thing; }
					effect = effect.Remove(5, thing - 4);
				}
				magic.Desc = magic.Desc.ReplaceSubstring(0, magicDescEnd, effect, 0, effect.Length);
				magicEntry.Data = magic.ToBinary();
			}
		}

		private static void SyncDescription(Tbl itemTbl, int itemId, Tbl magicTbl, int magicId, bool useMagic) {
			var itemEntry = itemTbl.Entries[itemId];
			var item = new ItemData(itemEntry.Data);
			var magicEntry = magicTbl.Entries[magicId];
			var magic = new MagicData(magicEntry.Data);
			int itemDescStart = item.Desc.LastIndexOf('\n') + 1;
			int magicDescStart = magic.Desc.LastIndexOf('\n') + 1;
			if (item.Desc[itemDescStart] == '(') {
				while (item.Desc[itemDescStart] != ')') { ++itemDescStart; }
				itemDescStart += 2;
			}
			if (useMagic) {
				item.Desc = item.Desc.ReplaceSubstring(itemDescStart, item.Desc.Length - itemDescStart, magic.Desc, magicDescStart, magic.Desc.Length - magicDescStart);
				itemEntry.Data = item.ToBinary();
			} else {
				magic.Desc = magic.Desc.ReplaceSubstring(magicDescStart, magic.Desc.Length - magicDescStart, item.Desc, itemDescStart, item.Desc.Length - itemDescStart);
				magicEntry.Data = magic.ToBinary();
			}
		}

		public IEnumerable<FileModResult> TryApply(FileStorage storage) {
			var s = storage.TryGetDuplicate(new HyoutaUtils.Checksum.SHA1(0xb64ec4d8b6204216ul, 0x6e97e60c57555203ul, 0x9c49c465u));
			if (s == null) {
				return null;
			}
			var tbl = new Tbl(s);
			PatchItemTbl(tbl);

			var magic_s = storage.TryGetDuplicate(new HyoutaUtils.Checksum.SHA1(0xd5f7bf4c4c575efdul, 0x5699e8bbd4040b81ul, 0x276a7284u));
			if (magic_s != null) {
				var magic_tbl = new Tbl(magic_s);
				text_dat_us_t_magic_tbl.PatchMagicTbl(magic_tbl);
				SyncItemMagicTbl(tbl, magic_tbl);
			}

			MemoryStream ms = new MemoryStream();
			tbl.WriteToStream(ms, EndianUtils.Endianness.LittleEndian);
			return new FileModResult[] { new FileModResult("data/text/dat_us/t_item.tbl", ms) };
		}

		public static void FixRarenessIdentifierInDescription(ItemData item) {
			// first identify what's already here
			byte identifiedRarity = 0;
			if (item.Desc[5] == '(' && item.Desc[6] == 'R' && item.Desc[7] == ')') {
				identifiedRarity = 1;
			} else if (item.Desc[5] == '(' && item.Desc[6] == 'S' && item.Desc[7] == 'R' && item.Desc[8] == ')') {
				identifiedRarity = 2;
			}

			// if it's different fix it up
			if (identifiedRarity != item.Action2_Rarity) {
				if (identifiedRarity != 0) {
					item.Desc = item.Desc.Remove(5, identifiedRarity == 1 ? 3 : 4);
				}
				if (item.Action2_Rarity == 1) {
					item.Desc = item.Desc.Insert(5, "(R)");
				} else if (item.Action2_Rarity == 2) {
					item.Desc = item.Desc.Insert(5, "(SR)");
				}
			}
		}

		public static bool IsSingleArt(ItemData item) {
			return item.Action2Value1_Art1 != 0xffff && item.Action1Value1_Art2 == 0xffff && item.Action1Value2_Art3 == 0xffff;
		}

		public static bool IsStatusQuartz(ItemData item) {
			if (item.Action2Value1_Art1 == 0xffff && item.Action1Value1_Art2 == 0xffff && item.Action1Value2_Art3 == 0xffff
				&& item.Status_Breaker == 0 && item.Range == 0 && item.AttackArea == 0 && item.Action2_Rarity == 0
				&& item.Action1_PassiveEffect == 0 && item.Action2Value2_ProcChance == 0) {
				int c = 0;
				c += (item.STR != 0 ? 1 : 0);
				c += (item.DEF != 0 ? 1 : 0);
				c += (item.ATS != 0 ? 1 : 0);
				c += (item.ADF != 0 ? 1 : 0);
				c += (item.ACC != 0 ? 1 : 0);
				c += (item.EVA != 0 ? 1 : 0);
				c += (item.SPD != 0 ? 1 : 0);
				c += (item.MOV != 0 ? 1 : 0);
				c += (item.HP != 0 ? 1 : 0);
				c += (item.EP != 0 ? 1 : 0);
				return c == 1;
			}
			return false;
		}

		public static bool IsSpecialQuartz(ItemData item) {
			if (item.Action2Value1_Art1 == 0xffff && item.Action1Value1_Art2 == 0xffff && item.Action1Value2_Art3 == 0xffff && item.Action1_PassiveEffect != 0) {
				return true;
			}
			if (item.Idx == 0x88c || item.Idx == 0x8ec) {
				return true;
			}
			return false;
		}

		public IEnumerable<FileModResult> TryRevert(FileStorage storage) {
			var s = storage.TryGetDuplicate(new HyoutaUtils.Checksum.SHA1(0xb64ec4d8b6204216ul, 0x6e97e60c57555203ul, 0x9c49c465u));
			if (s == null) {
				return null;
			}
			return new FileModResult[] { new FileModResult("data/text/dat_us/t_item.tbl", s) };
		}
	}
}
