using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisColors
{
	internal class CRAMImporter
	{
		public static void ProccessCRAMFile(string filename, Palette palette, bool swapBytes)
		{
			if (!File.Exists(filename))
				return;
			
			int count = 16 * 4; // 4 palettes x 16 colors
			if (palette.Count < count)
				return;

			palette.ResetValues();

			byte[] fileBytes = File.ReadAllBytes(filename);
			if (fileBytes.Length < count*2) // 2 bytes per color
			{
				MessageBox.Show("File hasn't a valid format!");
			}
			else
			{
				for(int currColorIdx=0;currColorIdx<count;currColorIdx++) {

					uint value0 = fileBytes[currColorIdx * 2];
					uint value1 = (uint)(fileBytes[currColorIdx * 2 + 1] << 8);
					if(swapBytes)
					{
						value0 = (uint)(fileBytes[currColorIdx * 2] << 8);
						value1 = fileBytes[currColorIdx * 2 + 1];
					}
					uint genesis_color = value0 ^ value1;
					uint genesis_color_converted = Utils.ConvertGenesisToARGB(genesis_color);
					Color c = Color.FromArgb((int)genesis_color_converted);
					palette.SetColor(currColorIdx, c);
					palette.CheckDuplicates(currColorIdx, genesis_color);
				}

			}
		}
	}
}
