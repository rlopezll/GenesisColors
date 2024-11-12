using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GenesisColors
{
	public partial class Form1 : Form
	{
		String LastFolderASEFile = "";
		List<PictureBox> ASEPalette = new List<PictureBox>();
		List<PictureBox> ASEPaletteDuplicated = new List<PictureBox>();
		List<PictureBox> GenesisPalette = new List<PictureBox>();
		List<PictureBox> GenesisPaletteDuplicated = new List<PictureBox>();

		List<Color> DuplicatedColor = new List<Color>();

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			KeyEventHandler handle = new KeyEventHandler(textBox_KeyDown);
			textBox_ColorARGB.KeyDown += handle;
			textBox_ColorGenesis.KeyDown += handle;
			textBox_ColorGenesisDecimal.KeyDown += handle;
			SetColorARGB(Color.White);

			ASEPalette.Clear();
			ASEPalette.Add(pictureBox1);
			ASEPalette.Add(pictureBox2);
			ASEPalette.Add(pictureBox3);
			ASEPalette.Add(pictureBox4);
			ASEPalette.Add(pictureBox5);
			ASEPalette.Add(pictureBox6);
			ASEPalette.Add(pictureBox7);
			ASEPalette.Add(pictureBox8);
			ASEPalette.Add(pictureBox9);
			ASEPalette.Add(pictureBox10);
			ASEPalette.Add(pictureBox11);
			ASEPalette.Add(pictureBox12);
			ASEPalette.Add(pictureBox13);
			ASEPalette.Add(pictureBox14);
			ASEPalette.Add(pictureBox15);
			ASEPalette.Add(pictureBox16);

			GenesisPalette.Clear();
			GenesisPalette.Add(pictureBox17);
			GenesisPalette.Add(pictureBox18);
			GenesisPalette.Add(pictureBox19);
			GenesisPalette.Add(pictureBox20);
			GenesisPalette.Add(pictureBox21);
			GenesisPalette.Add(pictureBox22);
			GenesisPalette.Add(pictureBox23);
			GenesisPalette.Add(pictureBox24);
			GenesisPalette.Add(pictureBox25);
			GenesisPalette.Add(pictureBox26);
			GenesisPalette.Add(pictureBox27);
			GenesisPalette.Add(pictureBox28);
			GenesisPalette.Add(pictureBox29);
			GenesisPalette.Add(pictureBox30);
			GenesisPalette.Add(pictureBox31);
			GenesisPalette.Add(pictureBox32);

			ASEPaletteDuplicated.Clear();
			ASEPaletteDuplicated.Add(pictureBox33);
			ASEPaletteDuplicated.Add(pictureBox34);
			ASEPaletteDuplicated.Add(pictureBox35);
			ASEPaletteDuplicated.Add(pictureBox36);
			ASEPaletteDuplicated.Add(pictureBox37);
			ASEPaletteDuplicated.Add(pictureBox38);
			ASEPaletteDuplicated.Add(pictureBox39);
			ASEPaletteDuplicated.Add(pictureBox40);
			ASEPaletteDuplicated.Add(pictureBox41);
			ASEPaletteDuplicated.Add(pictureBox42);
			ASEPaletteDuplicated.Add(pictureBox43);
			ASEPaletteDuplicated.Add(pictureBox44);
			ASEPaletteDuplicated.Add(pictureBox45);
			ASEPaletteDuplicated.Add(pictureBox46);
			ASEPaletteDuplicated.Add(pictureBox47);
			ASEPaletteDuplicated.Add(pictureBox48);

			GenesisPaletteDuplicated.Clear();
			GenesisPaletteDuplicated.Add(pictureBox49);
			GenesisPaletteDuplicated.Add(pictureBox50);
			GenesisPaletteDuplicated.Add(pictureBox51);
			GenesisPaletteDuplicated.Add(pictureBox52);
			GenesisPaletteDuplicated.Add(pictureBox53);
			GenesisPaletteDuplicated.Add(pictureBox54);
			GenesisPaletteDuplicated.Add(pictureBox55);
			GenesisPaletteDuplicated.Add(pictureBox56);
			GenesisPaletteDuplicated.Add(pictureBox57);
			GenesisPaletteDuplicated.Add(pictureBox58);
			GenesisPaletteDuplicated.Add(pictureBox59);
			GenesisPaletteDuplicated.Add(pictureBox60);
			GenesisPaletteDuplicated.Add(pictureBox61);
			GenesisPaletteDuplicated.Add(pictureBox62);
			GenesisPaletteDuplicated.Add(pictureBox63);
			GenesisPaletteDuplicated.Add(pictureBox64);

			DuplicatedColor.Clear();
			DuplicatedColor.Add(Color.Red);
			DuplicatedColor.Add(Color.Green);
			DuplicatedColor.Add(Color.Blue);
			DuplicatedColor.Add(Color.Yellow);
			DuplicatedColor.Add(Color.Violet);
			DuplicatedColor.Add(Color.Pink);
			DuplicatedColor.Add(Color.Orange);
			DuplicatedColor.Add(Color.LightBlue);

			for (int i = 0;i< ASEPalette.Count;i++)
			{
				ASEPalette[i].MouseClick += ASEPalettePictureBox_MouseClick;
			}
			for (int i = 0; i < GenesisPalette.Count; i++)
			{
				GenesisPalette[i].MouseClick += GenesisPalettePictureBox_MouseClick;
			}
		}

		private void ASEPalettePictureBox_MouseClick(object? sender, MouseEventArgs e)
		{
			if(sender is PictureBox)
			{
				PictureBox picture = (PictureBox)sender;
				textBox_PaletteASEValue.Text = picture.DataContext.ToString();
			}
		}

		private void GenesisPalettePictureBox_MouseClick(object? sender, MouseEventArgs e)
		{
			if (sender is PictureBox)
			{
				PictureBox picture = (PictureBox)sender;
				textBox_PaletteGenesisValue.Text = picture.DataContext.ToString();
			}
		}

		private void SetColorPicturebox(PictureBox pic, Color color)
		{
			pic.Image = new Bitmap(pic.Width, pic.Height);
			Graphics graphics = Graphics.FromImage(pic.Image);

			Brush brush = new SolidBrush(color);

			graphics.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, pic.Width, pic.Height));

		}

		private void SetColorARGB(Color color)
		{
			SetColorPicturebox(pictureBox_RGB8, color);

			uint icolor = (uint)color.ToArgb();
			uint genesis_color = ConvertARGBToGenesis(icolor);
			uint genesis_color_converted = ConvertGenesisToARGB(genesis_color);
			textBox_ColorGenesis.Text = "0x" + genesis_color.ToString();
			textBox_ColorGenesisDecimal.Text = genesis_color.ToString();
			Color c = Color.FromArgb((int)genesis_color_converted);
			SetColorPicturebox(pictureBox_Genesis, c);
		}

		private void SetColorGenesis(uint genesis_color)
		{
			uint color_converted = ConvertGenesisToARGB(genesis_color);
			Color acolor = Color.FromArgb((int)color_converted);
			Color color = Color.FromArgb(255, acolor);

			SetColorPicturebox(pictureBox_RGB8, color);
			SetColorPicturebox(pictureBox_Genesis, color);

			textBox_ColorGenesis.Text = "0x" + genesis_color.ToString("X2");
			textBox_ColorGenesisDecimal.Text = genesis_color.ToString();
			textBox_ColorARGB.Text = "0x" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
		}

		private uint ConvertARGBToGenesis(uint color)
		{
			uint a = (color & 0xF0000000) >> (24 + 4);
			uint r = (color & 0x00F00000) >> (16 + 4);
			uint g = (color & 0x0000F000) >> (8 + 4);
			uint b = (color & 0x000000F0) >> (0 + 4);

			uint mask = 0xeee;
			uint genesis_color = (((a << 12) | (b << 8) | (g << 4) | (r << 0)) & mask);

			return genesis_color;
		}

		private uint ConvertGenesisToARGB(uint genesis_color)
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

		private void pictureBox_GenesisPalette_Click(object sender, EventArgs e)
		{
			MouseEventArgs rato = e as MouseEventArgs;
			Bitmap b = ((Bitmap)pictureBox_GenesisPalette.Image);
			int x = rato.X * b.Width / pictureBox_GenesisPalette.ClientSize.Width;
			int y = rato.Y * b.Height / pictureBox_GenesisPalette.ClientSize.Height;
			Color c = b.GetPixel(x, y);
			SetColorARGB(c);
			textBox_ColorARGB.Text = "0x" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
		}

		private void textBox_ColorARGB_TextChanged(object sender, EventArgs e)
		{

		}

		void textBox_KeyDown(object? sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (sender == textBox_ColorARGB)
				{
					int value = Convert.ToInt32(textBox_ColorARGB.Text, 16);
					Color acolor = Color.FromArgb(value);
					Color color = Color.FromArgb(255, acolor);
					SetColorARGB(color);
				}
				else if (sender == textBox_ColorGenesisDecimal)
				{
					uint value = 0;
					if (uint.TryParse(textBox_ColorGenesisDecimal.Text, out value))
					{
						SetColorGenesis((uint)value);
					}
				}
				else if (sender == textBox_ColorGenesis)
				{
					uint value = (uint)Convert.ToInt32(textBox_ColorGenesis.Text, 16);
					SetColorGenesis((uint)value);
				}
			}
		}

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (LastFolderASEFile == "")
			{
				LastFolderASEFile = AppDomain.CurrentDomain.BaseDirectory;
			}

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "ASEprite format|*.ase";
			dialog.CheckFileExists = true;
			dialog.InitialDirectory = LastFolderASEFile;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				LastFolderASEFile = Path.GetFullPath(dialog.FileName);
				ProccessASEFile(LastFolderASEFile);
				button_RefreshPalette.Enabled = true;
			}
		}

		private void button_RefreshPalette_Click(object sender, EventArgs e)
		{
			ProccessASEFile(LastFolderASEFile);
		}

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

		private void ProccessASEFile(string filename)
		{
			if (!File.Exists(filename))
				return;

			byte[] fileBytes = File.ReadAllBytes(filename);
			int totalColors = (int)fileBytes[0x20];
			int positionColor = 0xAA;
			int currColorIdx = 0;

			Dictionary<uint, ColorSlot> ASEColorsDict = new Dictionary<uint, ColorSlot>();
			Dictionary<uint, ColorSlot> genesisColorsDict = new Dictionary<uint, ColorSlot>();
			int currDuplicatedASEColorIdx = 0;
			int currDuplicatedGenesisColorIdx = 0;

			while (currColorIdx < totalColors && positionColor<fileBytes.Length && currColorIdx < ASEPalette.Count)
			{
				positionColor += 2;
				int r = (int)fileBytes[positionColor+0];
				int g = (int)fileBytes[positionColor+1];
				int b = (int)fileBytes[positionColor+2];
				int a = (int)fileBytes[positionColor+3];
				Color currColor = Color.FromArgb(a, r, g, b);
				SetColorPicturebox(ASEPalette[currColorIdx], currColor);

				String tooltipText = "0x" + currColor.R.ToString("X2") + currColor.G.ToString("X2") + currColor.B.ToString("X2");
				ASEPalette[currColorIdx].DataContext = tooltipText;

				uint icolor = (uint)currColor.ToArgb();
				uint genesis_color = ConvertARGBToGenesis(icolor);
				uint genesis_color_converted = ConvertGenesisToARGB(genesis_color);
				Color c = Color.FromArgb((int)genesis_color_converted);
				SetColorPicturebox(GenesisPalette[currColorIdx], c);
				tooltipText = "0x" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
				GenesisPalette[currColorIdx].DataContext = tooltipText;

				//Check duplicates
				if(ASEColorsDict.ContainsKey(icolor))
				{
					ColorSlot slot = ASEColorsDict[icolor];
					if(slot.duplicateColorIdx < 0)
					{
						slot.duplicateColorIdx = currDuplicatedASEColorIdx;
						ASEColorsDict[icolor] = slot;
						++currDuplicatedASEColorIdx;
						if(slot.firstSlot >=0 && slot.firstSlot < ASEPaletteDuplicated.Count)
						{
							SetColorPicturebox(ASEPaletteDuplicated[slot.firstSlot], DuplicatedColor[slot.duplicateColorIdx]);
						}
					}
					if (slot.duplicateColorIdx < DuplicatedColor.Count)
						SetColorPicturebox(ASEPaletteDuplicated[currColorIdx], DuplicatedColor[slot.duplicateColorIdx]);
					else
						SetColorPicturebox(ASEPaletteDuplicated[currColorIdx], Color.Black);
				}
				else
				{
					ColorSlot slot = new ColorSlot();
					slot.firstSlot = currColorIdx;
					ASEColorsDict.Add(icolor, slot);
				}

				if (genesisColorsDict.ContainsKey(genesis_color))
				{
					ColorSlot slot = genesisColorsDict[genesis_color];
					if (slot.duplicateColorIdx < 0)
					{
						slot.duplicateColorIdx = currDuplicatedGenesisColorIdx;
						genesisColorsDict[genesis_color] = slot;
						++currDuplicatedGenesisColorIdx;
						if (slot.firstSlot >= 0 && slot.firstSlot < GenesisPaletteDuplicated.Count)
						{
							SetColorPicturebox(GenesisPaletteDuplicated[slot.firstSlot], DuplicatedColor[slot.duplicateColorIdx]);
						}
					}
					if (slot.duplicateColorIdx < DuplicatedColor.Count)
						SetColorPicturebox(GenesisPaletteDuplicated[currColorIdx], DuplicatedColor[slot.duplicateColorIdx]);
					else
						SetColorPicturebox(GenesisPaletteDuplicated[currColorIdx], Color.Black);
				}
				else
				{
					ColorSlot slot = new ColorSlot();
					slot.firstSlot = currColorIdx;
					genesisColorsDict.Add(genesis_color, slot);
				}

				positionColor += 4;
				currColorIdx++;
			}
		}
	}
}
