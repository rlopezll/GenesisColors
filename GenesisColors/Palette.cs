using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisColors
{
	internal class Palette
	{
		struct ColorSlot
		{
			public int duplicateColorIdx;
			public int firstSlot;

			public ColorSlot()
			{
				firstSlot = -1;
				duplicateColorIdx = -1;
			}
		}

		List<Color> DuplicatedColor = new List<Color>();
		List<PictureBox> PictureBox_Palette = new List<PictureBox>();
		List<PictureBox> PictureBox_DuplicatedIndicators = new List<PictureBox>();

		Dictionary<uint, ColorSlot> ColorsDict = new Dictionary<uint, ColorSlot>();

		TextBox? Textbox_ColorValue;

		PictureBox? lastPictureBoxSelected;

		int currDuplicatedColorIdx = 0;

		public Palette()
		{
			DuplicatedColor.Add(Color.Red);
			DuplicatedColor.Add(Color.Green);
			DuplicatedColor.Add(Color.Blue);
			DuplicatedColor.Add(Color.Yellow);
			DuplicatedColor.Add(Color.Violet);
			DuplicatedColor.Add(Color.Pink);
			DuplicatedColor.Add(Color.Orange);
			DuplicatedColor.Add(Color.LightBlue);

			DuplicatedColor.Add(Color.DarkRed);
			DuplicatedColor.Add(Color.DarkGreen);
			DuplicatedColor.Add(Color.DarkBlue);
			DuplicatedColor.Add(Color.LightYellow);
			DuplicatedColor.Add(Color.BlueViolet);
			DuplicatedColor.Add(Color.White);
			DuplicatedColor.Add(Color.DarkOrange);
			DuplicatedColor.Add(Color.LightSkyBlue);

			DuplicatedColor.Add(Color.Peru);
			DuplicatedColor.Add(Color.PapayaWhip);
			DuplicatedColor.Add(Color.Purple);
			DuplicatedColor.Add(Color.SeaGreen);
			DuplicatedColor.Add(Color.Brown);
			DuplicatedColor.Add(Color.SlateGray);
			DuplicatedColor.Add(Color.Salmon);
			DuplicatedColor.Add(Color.SaddleBrown);

			DuplicatedColor.Add(Color.BlueViolet);
			DuplicatedColor.Add(Color.Coral);
			DuplicatedColor.Add(Color.Cyan);
			DuplicatedColor.Add(Color.DarkOrchid);
			DuplicatedColor.Add(Color.DimGray);
			DuplicatedColor.Add(Color.Firebrick);
			DuplicatedColor.Add(Color.DodgerBlue);
		}

		public void RegisterPictureBoxPalette(List<PictureBox> _PictureBox_Palette)
		{
			PictureBox_Palette = _PictureBox_Palette;

			for (int i = 0; i < PictureBox_Palette.Count; i++)
			{
				PictureBox_Palette[i].MouseClick += PalettePictureBox_MouseClick;
				PictureBox_Palette[i].Paint += PictureBoxPalette_Paint;
			}
		}

		public void RegisterTextBoxColorValue(TextBox _Textbox_ColorValue)
		{
			Textbox_ColorValue = _Textbox_ColorValue;
		}

		public void RegisterPictureBoxDuplicatedIndicators(List<PictureBox> _PictureBox_DuplicatedIndicators)
		{
			PictureBox_DuplicatedIndicators = _PictureBox_DuplicatedIndicators;
		}

		public int Count
		{
			get
			{
				return PictureBox_Palette.Count;
			}
		}

		public void ResetValues()
		{
			lastPictureBoxSelected = null;
			if(Textbox_ColorValue is TextBox) {
				Textbox_ColorValue.Text = "";
			}
			for (int i = 0; i < PictureBox_Palette.Count; i++)
			{
				PictureBox_Palette[i].Tag = null;
			}

			ColorsDict.Clear();
			currDuplicatedColorIdx = 0;
		}

		private void PictureBoxPalette_Paint(object? sender, PaintEventArgs e)
		{
			if (sender is PictureBox)
			{
				PictureBox picture = (PictureBox)sender;
				if (picture.Tag != null)
				{
					ControlPaint.DrawBorder(e.Graphics, picture.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
				}
				else
				{
					ControlPaint.DrawBorder(e.Graphics, picture.ClientRectangle, Color.Transparent, ButtonBorderStyle.Solid);
				}
			}
		}

		private void PalettePictureBox_MouseClick(object? sender, MouseEventArgs e)
		{
			if (sender is PictureBox)
			{
				if (lastPictureBoxSelected != null)
				{
					lastPictureBoxSelected.Tag = null;
					lastPictureBoxSelected.Invalidate();
				}

				PictureBox picture = (PictureBox)sender;
				if(Textbox_ColorValue is TextBox)
				{
					if (picture.DataContext is null)
					{
						Textbox_ColorValue.Text = "";
					}
					else
					{
						Textbox_ColorValue.Text = picture.DataContext.ToString();
					}
				}
				picture.Tag = "Selected";
				picture.Invalidate();
				lastPictureBoxSelected = picture;
			}
		}

		public void SetColor(int pictureBoxIndex, Color color)
		{
			Utils.SetColorPicturebox(PictureBox_Palette[pictureBoxIndex], color);

			String tooltipText = "0x" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
			PictureBox_Palette[pictureBoxIndex].DataContext = tooltipText;
		}

		public void CheckDuplicates(int colorIdx, uint icolor)
		{
			if (ColorsDict.ContainsKey(icolor))
			{
				ColorSlot slot = ColorsDict[icolor];
				if (slot.duplicateColorIdx < 0)
				{
					slot.duplicateColorIdx = currDuplicatedColorIdx;
					ColorsDict[icolor] = slot;
					++currDuplicatedColorIdx;
					if (slot.firstSlot >= 0 && slot.firstSlot < PictureBox_DuplicatedIndicators.Count)
					{
						Color cDuplicated = slot.duplicateColorIdx < DuplicatedColor.Count ? DuplicatedColor[slot.duplicateColorIdx] : Color.Gold;
						Utils.SetColorPicturebox(PictureBox_DuplicatedIndicators[slot.firstSlot], cDuplicated);
					}
				}
				if (slot.duplicateColorIdx < DuplicatedColor.Count)
					Utils.SetColorPicturebox(PictureBox_DuplicatedIndicators[colorIdx], DuplicatedColor[slot.duplicateColorIdx]);
				else
					Utils.SetColorPicturebox(PictureBox_DuplicatedIndicators[colorIdx], Color.Black);
			}
			else
			{
				ColorSlot slot = new ColorSlot();
				slot.firstSlot = colorIdx;
				ColorsDict.Add(icolor, slot);
				Utils.SetEmptyColorPicturebox(PictureBox_DuplicatedIndicators[colorIdx]);
			}
		}
	}
}
