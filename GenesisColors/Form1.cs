using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GenesisColors
{
	public partial class Form1 : Form
	{
		String LastFolderASEFile = "";
		String LastFolderCRAMFile = "";

		Palette ASEPalette = new Palette();
		Palette GenesisPalette = new Palette();
		Palette CRAMFullPalettes = new Palette();

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

			RegisterASEPalette();
			RegisterGenesisPalette();
			RegisterCRAMPalettes();
		}

		private void SetColorARGB(Color color)
		{
			Utils.SetColorPicturebox(pictureBox_RGB8, color);

			uint icolor = (uint)color.ToArgb();
			uint genesis_color = Utils.ConvertARGBToGenesis(icolor);
			uint genesis_color_converted = Utils.ConvertGenesisToARGB(genesis_color);
			textBox_ColorGenesis.Text = "0x" + genesis_color.ToString("X2");
			textBox_ColorGenesisDecimal.Text = genesis_color.ToString();
			Color c = Color.FromArgb((int)genesis_color_converted);
			textBox_ColorGenesisConverted.Text = "0x" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
			Utils.SetColorPicturebox(pictureBox_Genesis, c);
		}

		private void SetColorGenesis(uint genesis_color)
		{
			uint color_converted = Utils.ConvertGenesisToARGB(genesis_color);
			Color acolor = Color.FromArgb((int)color_converted);
			Color color = Color.FromArgb(255, acolor);

			Utils.SetColorPicturebox(pictureBox_RGB8, color);
			Utils.SetColorPicturebox(pictureBox_Genesis, color);

			textBox_ColorGenesis.Text = "0x" + genesis_color.ToString("X2");
			textBox_ColorGenesisDecimal.Text = genesis_color.ToString();
			textBox_ColorARGB.Text = "0x" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
			textBox_ColorGenesisConverted.Text = "0x" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
		}

		private void pictureBox_GenesisPalette_Click(object sender, EventArgs e)
		{
			MouseEventArgs? rato = e as MouseEventArgs;
			if (rato != null)
			{
				Bitmap b = ((Bitmap)pictureBox_GenesisPalette.Image);
				int x = rato.X * b.Width / pictureBox_GenesisPalette.ClientSize.Width;
				int y = rato.Y * b.Height / pictureBox_GenesisPalette.ClientSize.Height;
				Color c = b.GetPixel(x, y);
				SetColorARGB(c);
				textBox_ColorARGB.Text = "0x" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
			}
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

		private void button_LoadPalette_Click(object sender, EventArgs e)
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
				ASEPaletteImporter.ProccessASEFile(LastFolderASEFile, ASEPalette, GenesisPalette);
				button_RefreshPalette.Enabled = true;
			}
		}

		private void button_RefreshPalette_Click(object sender, EventArgs e)
		{
			ASEPaletteImporter.ProccessASEFile(LastFolderASEFile, ASEPalette, GenesisPalette);
		}

		private void button_LoadCRAMFile_Click(object sender, EventArgs e)
		{
			if (LastFolderCRAMFile == "")
			{
				LastFolderCRAMFile = AppDomain.CurrentDomain.BaseDirectory;
			}

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "CRAM format|*.bin;*.ram|All files|*.*";
			dialog.CheckFileExists = true;
			dialog.InitialDirectory = LastFolderCRAMFile;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				LastFolderCRAMFile = Path.GetFullPath(dialog.FileName);
				CRAMImporter.ProccessCRAMFile(LastFolderCRAMFile, CRAMFullPalettes, checkBox_SwapBytes.Checked);
				button_RefreshCRAMFile.Enabled = true;
			}
		}

		private void button_RefreshCRAMFile_Click(object sender, EventArgs e)
		{
			CRAMImporter.ProccessCRAMFile(LastFolderCRAMFile, CRAMFullPalettes, checkBox_SwapBytes.Checked);
		}

		private void checkBox_SwapBytes_CheckedChanged(object sender, EventArgs e)
		{
			CRAMImporter.ProccessCRAMFile(LastFolderCRAMFile, CRAMFullPalettes, checkBox_SwapBytes.Checked);
		}


		private void RegisterGenesisPalette()
		{
			List<PictureBox> GenesisPaletteList = new List<PictureBox>();
			GenesisPaletteList.Add(pictureBox17);
			GenesisPaletteList.Add(pictureBox18);
			GenesisPaletteList.Add(pictureBox19);
			GenesisPaletteList.Add(pictureBox20);
			GenesisPaletteList.Add(pictureBox21);
			GenesisPaletteList.Add(pictureBox22);
			GenesisPaletteList.Add(pictureBox23);
			GenesisPaletteList.Add(pictureBox24);
			GenesisPaletteList.Add(pictureBox25);
			GenesisPaletteList.Add(pictureBox26);
			GenesisPaletteList.Add(pictureBox27);
			GenesisPaletteList.Add(pictureBox28);
			GenesisPaletteList.Add(pictureBox29);
			GenesisPaletteList.Add(pictureBox30);
			GenesisPaletteList.Add(pictureBox31);
			GenesisPaletteList.Add(pictureBox32);

			List<PictureBox> GenesisPaletteDuplicatedList = new List<PictureBox>();
			GenesisPaletteDuplicatedList.Add(pictureBox49);
			GenesisPaletteDuplicatedList.Add(pictureBox50);
			GenesisPaletteDuplicatedList.Add(pictureBox51);
			GenesisPaletteDuplicatedList.Add(pictureBox52);
			GenesisPaletteDuplicatedList.Add(pictureBox53);
			GenesisPaletteDuplicatedList.Add(pictureBox54);
			GenesisPaletteDuplicatedList.Add(pictureBox55);
			GenesisPaletteDuplicatedList.Add(pictureBox56);
			GenesisPaletteDuplicatedList.Add(pictureBox57);
			GenesisPaletteDuplicatedList.Add(pictureBox58);
			GenesisPaletteDuplicatedList.Add(pictureBox59);
			GenesisPaletteDuplicatedList.Add(pictureBox60);
			GenesisPaletteDuplicatedList.Add(pictureBox61);
			GenesisPaletteDuplicatedList.Add(pictureBox62);
			GenesisPaletteDuplicatedList.Add(pictureBox63);
			GenesisPaletteDuplicatedList.Add(pictureBox64);

			GenesisPalette.RegisterPictureBoxPalette(GenesisPaletteList);
			GenesisPalette.RegisterTextBoxColorValue(textBox_PaletteGenesisValue);
			GenesisPalette.RegisterPictureBoxDuplicatedIndicators(GenesisPaletteDuplicatedList);
		}

		private void RegisterASEPalette()
		{
			List<PictureBox> ASEPaletteList = new List<PictureBox>();
			ASEPaletteList.Add(pictureBox1);
			ASEPaletteList.Add(pictureBox2);
			ASEPaletteList.Add(pictureBox3);
			ASEPaletteList.Add(pictureBox4);
			ASEPaletteList.Add(pictureBox5);
			ASEPaletteList.Add(pictureBox6);
			ASEPaletteList.Add(pictureBox7);
			ASEPaletteList.Add(pictureBox8);
			ASEPaletteList.Add(pictureBox9);
			ASEPaletteList.Add(pictureBox10);
			ASEPaletteList.Add(pictureBox11);
			ASEPaletteList.Add(pictureBox12);
			ASEPaletteList.Add(pictureBox13);
			ASEPaletteList.Add(pictureBox14);
			ASEPaletteList.Add(pictureBox15);
			ASEPaletteList.Add(pictureBox16);

			List<PictureBox> ASEPaletteDuplicatedList = new List<PictureBox>();
			ASEPaletteDuplicatedList.Add(pictureBox33);
			ASEPaletteDuplicatedList.Add(pictureBox34);
			ASEPaletteDuplicatedList.Add(pictureBox35);
			ASEPaletteDuplicatedList.Add(pictureBox36);
			ASEPaletteDuplicatedList.Add(pictureBox37);
			ASEPaletteDuplicatedList.Add(pictureBox38);
			ASEPaletteDuplicatedList.Add(pictureBox39);
			ASEPaletteDuplicatedList.Add(pictureBox40);
			ASEPaletteDuplicatedList.Add(pictureBox41);
			ASEPaletteDuplicatedList.Add(pictureBox42);
			ASEPaletteDuplicatedList.Add(pictureBox43);
			ASEPaletteDuplicatedList.Add(pictureBox44);
			ASEPaletteDuplicatedList.Add(pictureBox45);
			ASEPaletteDuplicatedList.Add(pictureBox46);
			ASEPaletteDuplicatedList.Add(pictureBox47);
			ASEPaletteDuplicatedList.Add(pictureBox48);

			ASEPalette.RegisterPictureBoxPalette(ASEPaletteList);
			ASEPalette.RegisterTextBoxColorValue(textBox_PaletteASEValue);
			ASEPalette.RegisterPictureBoxDuplicatedIndicators(ASEPaletteDuplicatedList);
		}

		private void RegisterCRAMPalettes()
		{
			List<PictureBox> CRAMPaletteList = new List<PictureBox>();
			CRAMPaletteList.Add(pictureBox96);
			CRAMPaletteList.Add(pictureBox95);
			CRAMPaletteList.Add(pictureBox93);
			CRAMPaletteList.Add(pictureBox94);
			CRAMPaletteList.Add(pictureBox89);
			CRAMPaletteList.Add(pictureBox90);
			CRAMPaletteList.Add(pictureBox91);
			CRAMPaletteList.Add(pictureBox92);
			CRAMPaletteList.Add(pictureBox81);
			CRAMPaletteList.Add(pictureBox82);
			CRAMPaletteList.Add(pictureBox83);
			CRAMPaletteList.Add(pictureBox84);
			CRAMPaletteList.Add(pictureBox85);
			CRAMPaletteList.Add(pictureBox86);
			CRAMPaletteList.Add(pictureBox87);
			CRAMPaletteList.Add(pictureBox88);

			CRAMPaletteList.Add(pictureBox128);
			CRAMPaletteList.Add(pictureBox127);
			CRAMPaletteList.Add(pictureBox125);
			CRAMPaletteList.Add(pictureBox126);
			CRAMPaletteList.Add(pictureBox121);
			CRAMPaletteList.Add(pictureBox122);
			CRAMPaletteList.Add(pictureBox123);
			CRAMPaletteList.Add(pictureBox124);
			CRAMPaletteList.Add(pictureBox113);
			CRAMPaletteList.Add(pictureBox114);
			CRAMPaletteList.Add(pictureBox115);
			CRAMPaletteList.Add(pictureBox116);
			CRAMPaletteList.Add(pictureBox117);
			CRAMPaletteList.Add(pictureBox118);
			CRAMPaletteList.Add(pictureBox119);
			CRAMPaletteList.Add(pictureBox120);

			CRAMPaletteList.Add(pictureBox192);
			CRAMPaletteList.Add(pictureBox191);
			CRAMPaletteList.Add(pictureBox189);
			CRAMPaletteList.Add(pictureBox190);
			CRAMPaletteList.Add(pictureBox185);
			CRAMPaletteList.Add(pictureBox186);
			CRAMPaletteList.Add(pictureBox187);
			CRAMPaletteList.Add(pictureBox188);
			CRAMPaletteList.Add(pictureBox177);
			CRAMPaletteList.Add(pictureBox178);
			CRAMPaletteList.Add(pictureBox179);
			CRAMPaletteList.Add(pictureBox180);
			CRAMPaletteList.Add(pictureBox181);
			CRAMPaletteList.Add(pictureBox182);
			CRAMPaletteList.Add(pictureBox183);
			CRAMPaletteList.Add(pictureBox184);

			CRAMPaletteList.Add(pictureBox160);
			CRAMPaletteList.Add(pictureBox159);
			CRAMPaletteList.Add(pictureBox157);
			CRAMPaletteList.Add(pictureBox158);
			CRAMPaletteList.Add(pictureBox153);
			CRAMPaletteList.Add(pictureBox154);
			CRAMPaletteList.Add(pictureBox155);
			CRAMPaletteList.Add(pictureBox156);
			CRAMPaletteList.Add(pictureBox145);
			CRAMPaletteList.Add(pictureBox146);
			CRAMPaletteList.Add(pictureBox147);
			CRAMPaletteList.Add(pictureBox148);
			CRAMPaletteList.Add(pictureBox149);
			CRAMPaletteList.Add(pictureBox150);
			CRAMPaletteList.Add(pictureBox151);
			CRAMPaletteList.Add(pictureBox152);

			List<PictureBox> CRAMPaletteDuplicatedList = new List<PictureBox>();
			CRAMPaletteDuplicatedList.Add(pictureBox65);
			CRAMPaletteDuplicatedList.Add(pictureBox66);
			CRAMPaletteDuplicatedList.Add(pictureBox67);
			CRAMPaletteDuplicatedList.Add(pictureBox68);
			CRAMPaletteDuplicatedList.Add(pictureBox69);
			CRAMPaletteDuplicatedList.Add(pictureBox70);
			CRAMPaletteDuplicatedList.Add(pictureBox71);
			CRAMPaletteDuplicatedList.Add(pictureBox72);
			CRAMPaletteDuplicatedList.Add(pictureBox73);
			CRAMPaletteDuplicatedList.Add(pictureBox74);
			CRAMPaletteDuplicatedList.Add(pictureBox75);
			CRAMPaletteDuplicatedList.Add(pictureBox76);
			CRAMPaletteDuplicatedList.Add(pictureBox77);
			CRAMPaletteDuplicatedList.Add(pictureBox78);
			CRAMPaletteDuplicatedList.Add(pictureBox79);
			CRAMPaletteDuplicatedList.Add(pictureBox80);

			CRAMPaletteDuplicatedList.Add(pictureBox97);
			CRAMPaletteDuplicatedList.Add(pictureBox98);
			CRAMPaletteDuplicatedList.Add(pictureBox99);
			CRAMPaletteDuplicatedList.Add(pictureBox100);
			CRAMPaletteDuplicatedList.Add(pictureBox101);
			CRAMPaletteDuplicatedList.Add(pictureBox102);
			CRAMPaletteDuplicatedList.Add(pictureBox103);
			CRAMPaletteDuplicatedList.Add(pictureBox104);
			CRAMPaletteDuplicatedList.Add(pictureBox105);
			CRAMPaletteDuplicatedList.Add(pictureBox106);
			CRAMPaletteDuplicatedList.Add(pictureBox107);
			CRAMPaletteDuplicatedList.Add(pictureBox108);
			CRAMPaletteDuplicatedList.Add(pictureBox109);
			CRAMPaletteDuplicatedList.Add(pictureBox110);
			CRAMPaletteDuplicatedList.Add(pictureBox111);
			CRAMPaletteDuplicatedList.Add(pictureBox112);

			CRAMPaletteDuplicatedList.Add(pictureBox161);
			CRAMPaletteDuplicatedList.Add(pictureBox162);
			CRAMPaletteDuplicatedList.Add(pictureBox163);
			CRAMPaletteDuplicatedList.Add(pictureBox164);
			CRAMPaletteDuplicatedList.Add(pictureBox165);
			CRAMPaletteDuplicatedList.Add(pictureBox166);
			CRAMPaletteDuplicatedList.Add(pictureBox167);
			CRAMPaletteDuplicatedList.Add(pictureBox168);
			CRAMPaletteDuplicatedList.Add(pictureBox169);
			CRAMPaletteDuplicatedList.Add(pictureBox170);
			CRAMPaletteDuplicatedList.Add(pictureBox171);
			CRAMPaletteDuplicatedList.Add(pictureBox172);
			CRAMPaletteDuplicatedList.Add(pictureBox173);
			CRAMPaletteDuplicatedList.Add(pictureBox174);
			CRAMPaletteDuplicatedList.Add(pictureBox175);
			CRAMPaletteDuplicatedList.Add(pictureBox176);

			CRAMPaletteDuplicatedList.Add(pictureBox129);
			CRAMPaletteDuplicatedList.Add(pictureBox130);
			CRAMPaletteDuplicatedList.Add(pictureBox131);
			CRAMPaletteDuplicatedList.Add(pictureBox132);
			CRAMPaletteDuplicatedList.Add(pictureBox133);
			CRAMPaletteDuplicatedList.Add(pictureBox134);
			CRAMPaletteDuplicatedList.Add(pictureBox135);
			CRAMPaletteDuplicatedList.Add(pictureBox136);
			CRAMPaletteDuplicatedList.Add(pictureBox137);
			CRAMPaletteDuplicatedList.Add(pictureBox138);
			CRAMPaletteDuplicatedList.Add(pictureBox139);
			CRAMPaletteDuplicatedList.Add(pictureBox140);
			CRAMPaletteDuplicatedList.Add(pictureBox141);
			CRAMPaletteDuplicatedList.Add(pictureBox142);
			CRAMPaletteDuplicatedList.Add(pictureBox143);
			CRAMPaletteDuplicatedList.Add(pictureBox144);

			CRAMFullPalettes.RegisterPictureBoxPalette(CRAMPaletteList);
			CRAMFullPalettes.RegisterTextBoxColorValue(textBox_CRAMColor_Value);
			CRAMFullPalettes.RegisterPictureBoxDuplicatedIndicators(CRAMPaletteDuplicatedList);
		}

	}
}
