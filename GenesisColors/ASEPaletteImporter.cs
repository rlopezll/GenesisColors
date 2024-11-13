using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisColors
{
	internal class ASEPaletteImporter
	{

		public static void ProccessASEFile(string filename, Palette ASEPalette, Palette GenesisPalette)
		{
			if (!File.Exists(filename))
				return;

			ASEPalette.ResetValues();
			GenesisPalette.ResetValues();

			byte[] fileBytes = File.ReadAllBytes(filename);
			int totalColors = (int)fileBytes[0x20];
			int positionColor = 0xAA;
			int currColorIdx = 0;

			while (currColorIdx < totalColors && positionColor < fileBytes.Length && currColorIdx < ASEPalette.Count)
			{
				positionColor += 2;
				int r = (int)fileBytes[positionColor + 0];
				int g = (int)fileBytes[positionColor + 1];
				int b = (int)fileBytes[positionColor + 2];
				int a = (int)fileBytes[positionColor + 3];
				Color currColor = Color.FromArgb(a, r, g, b);
				ASEPalette.SetColor(currColorIdx, currColor);

				uint icolor = (uint)currColor.ToArgb();
				uint genesis_color = Utils.ConvertARGBToGenesis(icolor);
				uint genesis_color_converted = Utils.ConvertGenesisToARGB(genesis_color);
				Color c = Color.FromArgb((int)genesis_color_converted);
				GenesisPalette.SetColor(currColorIdx, c);

				ASEPalette.CheckDuplicates(currColorIdx, icolor);
				GenesisPalette.CheckDuplicates(currColorIdx, genesis_color);

				positionColor += 4;
				currColorIdx++;
			}
		}
	}
}
