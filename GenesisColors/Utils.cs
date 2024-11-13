using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisColors
{
	internal class Utils
	{

		static public void SetColorPicturebox(PictureBox pic, Color color)
		{
			pic.Image = new Bitmap(pic.Width, pic.Height);
			Graphics graphics = Graphics.FromImage(pic.Image);

			Brush brush = new SolidBrush(color);

			graphics.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, pic.Width, pic.Height));
		}

		static public uint ConvertARGBToGenesis(uint color)
		{
			uint a = (color & 0xF0000000) >> (24 + 4);
			uint r = (color & 0x00F00000) >> (16 + 4);
			uint g = (color & 0x0000F000) >> (8 + 4);
			uint b = (color & 0x000000F0) >> (0 + 4);

			uint mask = 0xeee;
			uint genesis_color = (((a << 12) | (b << 8) | (g << 4) | (r << 0)) & mask);

			return genesis_color;
		}

		static public uint ConvertGenesisToARGB(uint genesis_color)
		{
			uint r_genesis = (genesis_color & 0x00e) >> (1);
			uint g_genesis = (genesis_color & 0x0e0) >> (1 + 4);
			uint b_genesis = (genesis_color & 0xe00) >> (1 + 4 + 4);

			uint r_rgb8 = (r_genesis * 255 / 7);
			uint g_rgb8 = (g_genesis * 255 / 7);
			uint b_rgb8 = (b_genesis * 255 / 7);

			uint result_color = 0xff000000 | (r_rgb8 << 16) | (g_rgb8 << 8) | (b_rgb8);

			return result_color;
		}

		static public void SetEmptyColorPicturebox(PictureBox pic)
		{
			pic.Image = new Bitmap(pic.Width, pic.Height);
			Graphics graphics = Graphics.FromImage(pic.Image);
			Brush brush = new SolidBrush(Color.Transparent);

			graphics.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, pic.Width, pic.Height));
		}

	}
}
