/*
 * 26.03.2018 CHRI	
 * 04.07.2021 LUTZ
 * https://docs.microsoft.com/de-de/dotnet/framework/resources/retrieving-resources-in-desktop-apps
 */
 
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection; 		// embedded resources
using System.Resources;
using System.Diagnostics;
//using Visutronik;				// SystemTools(), ...

namespace LuxmeterClient
{
	/// <summary>
	/// Description of InfoForm.
	/// </summary>
	public partial class InfoForm : Form
	{
		/// <summary>
		/// Genauer Programm-Name
		/// </summary>
		public string strProgName { get; set; }	//= "strProgName";

		/// <summary>
		/// Programmversion
		/// </summary>
		public string strProgVer { get; set; }	//= "strProgVer";
		
		public string strProgCopyright { get; set; } //= "strProgCopyright";

		public string strInfo { get; set; }
		
		
		MainForm _parent = null;
		
		public InfoForm(MainForm parentForm)
		{
			InitializeComponent();
			_parent = parentForm;
		}

		/// <summary>
		/// Info-Dialog wird geladen... 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void InfoFormLoad(object sender, EventArgs e)
		{
			Debug.WriteLine("InfoFormLoad() sender = " + sender);
			
			this.labelProgName.Text = strProgName;
			this.labelProgVer.Text = strProgVer;
			
			// Bilder aus lokalen Resources.resx holen
			// wobei "Standardnamespace" meist der Projektname ist
			//ResourceManager rm = new ResourceManager("OMS", Assembly.GetExecutingAssembly());
			
			// Bitmap anhand seines Namens (ohne Dateinamenerweiterung!) aus der Resource holen
			//Bitmap bmpLogo = rm.GetObject("BmpTest") as Bitmap;
			//pictureBox1.Image = bmpLogo;
			
			try
			{
				Assembly _assembly = Assembly.GetExecutingAssembly();
				Debug.WriteLine(_assembly);
				string[] names = _assembly.GetManifestResourceNames();
				foreach (string name in names)
				{
					Debug.WriteLine("Embedded ManifestResource: " + name);
				}
				
				/*
				LuxmeterClient, Version=1.0.7856.21386, Culture=neutral, PublicKeyToken=null
				Embedded ManifestResource: LuxmeterClient.InfoForm.resources
				Embedded ManifestResource: LuxmeterClient.MainForm.resources
				Embedded ManifestResource: LuxmeterClient.Resources.Logo-visutronik-32.ico
				Embedded ManifestResource: LuxmeterClient.Resources.Logo_Visutronik_300_200.bmp
				 */
				
				Stream _imageStream = _assembly.GetManifestResourceStream("LuxmeterClient.Resources.Logo_Visutronik_300_200.bmp");
				//Stream _imageStream = _assembly.GetManifestResourceStream("LOGO_VISUTRONIK_300_200"); // LOGO_VISUTRONIK
				pictureBox1.Image = new Bitmap(_imageStream);
				
				//_textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("MyNamespace.MyTextFile.txt"));
	
				//pictureBox1.Image = Properties.Resource.LOGO_VISUTRONIK_300_200;

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				MessageBox.Show("Error accessing resources!");
			}
	
			labelInfo.Text = strInfo;
		}
		
		
	}
}
