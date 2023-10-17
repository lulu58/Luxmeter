/*
 * Projekt:	OMS
 * Klasse:	Settings - Persistenz der Einstellungen
 * 19.04.2018	Lutz
 * 04.07.2021	Lutz Anpassung für LuxmeterClient
 */

using System;
using System.ComponentModel;			// wichtig für Benutzung PropertyGrid!
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LuxmeterClient
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	public class Settings
	{
		#region --------------------- constants -------------------------------------

		#endregion

		#region --------------------- properties -------------------------------------
		
		// TODO add settings Programm / Benutzer

		// --- Verzeichnisse und Dateien ---
/*		
		[DisplayName("Haupt"), Description("Hauptverzeichnis für programmeigene Daten"), Category("Dateien"), ReadOnly(true)]
		public string strAppDataFolder { get; set; }
		
		[DisplayName("Messdaten"), Description("Messdatenverzeichnis"), Category("Dateien"), ReadOnly(true)]
		public string strMeasFolder { get; set; }

		[DisplayName("Parameter"), Description("Parameter-Datenverzeichnis"), Category("Dateien"), ReadOnly(true)]
		public string strParamFolder { get; set; }
		
		[DisplayName("Log"), Description("Log-Verzeichnis"), Category("Dateien"), ReadOnly(true)]
		public string strLogFolder { get; set; }
		
		[DisplayName("Support"), Description("Support-Verzeichnis"), Category("Dateien"), ReadOnly(true)]
		public string strSupportFolder { get; set; }

		[DisplayName("Dokumentation"), Description("Dokumentation"), Category("Dateien"), ReadOnly(true)]
		public string strDocumentation { get; set; }

		[DisplayName("Fernwartung"), Description("Fernwartung"), Category("Dateien"), ReadOnly(true)]
		public string strRemoteCtrl { get; set; }
*/
		[DisplayName("COM-Port"), Description("Serial port"), Category("Hardware"), ReadOnly(false)]
		public string strSerialPort { get; set; }

		public Int32 TimerSeconds { get; set; } = 5;


		// --- Window size and location ---

		[DisplayName("Vollbildmodus"), Description("Vollbildmodus An/Aus"), Category("Allgemein"), ReadOnly(true)]
		public bool bFullscreen	{ get; set;	}
		
		[DisplayName("Fenstergröße X"), Description("WindowSizeX"), Category("Allgemein"), ReadOnly(true)]
		public int nWindowSizeX	{ get; set;	}
		[DisplayName("Fenstergröße Y"), Description("WindowSizeY"), Category("Allgemein"), ReadOnly(true)]
		public int nWindowSizeY	{ get; set;	}
		
		[DisplayName("Position X"), Description("WindowPositionX"), Category("Allgemein"), ReadOnly(true)]
		public int nWindowLocX	{ get; set;	}
		[DisplayName("Position Y"), Description("WindowPositionY"), Category("Allgemein"), ReadOnly(true)]
		public int nWindowLocY	{ get; set;	}
		
		#endregion ------------------ properties -------------------------------------

		#region --- vars --------

		private string _strPath = "";
		public string strMessage = "";

		#endregion --- vars --------
		
		
		/// <summary>
		/// Erzeugt Pfad für Config-Datei:
		/// </summary>
		public static string GetConfigPath(string strVendor, string strAppName, string strFile)
		{
			Debug.WriteLine("Settings.GetConfigPath(" + strVendor + ", " + strAppName + ", " + strFile +")");
			
			string folder = "";
			string path = "";

			// Win32:
			
			folder = Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
			// ggf. LocalApplicationData
			folder = Path.Combine(folder, strVendor);
			folder = Path.Combine(folder, strAppName);

			if (!Directory.Exists(folder))
			{
				try
				{
					DirectoryInfo di = Directory.CreateDirectory(folder);
					Debug.WriteLine(di.FullName + " created.");
				}
				catch (Exception ex)
				{
					Debug.WriteLine("ERROR: " + ex.Message);
				}
			}

			path = Path.Combine(folder, strFile);	// SETTINGS_FILE);
			
			// mit LocalApplicationData
			// XP:		"C:\Dokumente und Einstellungen\Lulu\Anwendungsdaten\Visutronik GmbH\OMS\..."
			// Vista:	"C:\Users\Lutz\AppData\Roaming\Visutronik GmbH\OMS\..."
			// Win10: 	"C:\Users\Lulu\AppData\Roaming\Visutronik GmbH\OMS\..."
			// mit CommonApplicationData
			// Win7, 10 "C:\ProgramData\Visutronik GmbH\OMS\oms.config.xml";
			return path;
		}


		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="path">Dateipfad für Einstellungsdatei</param>
		public Settings(string path)
		{
			_strPath = path;
			Debug.WriteLine("Settings(): settingspath = " + _strPath);
			
			//strAppDataFolder = STR_APPDATAFOLDER;
			//Default(strAppDataFolder);
		}
		
		
		/// <summary>
		/// Laden der Einstellungen aus Datei
		/// Wird eine Einstellung nicht gefunden, wird der Defaultwert verwendet!
		/// </summary>
		/// <returns>true bei Erfolg</returns>
		public bool Load()
		{
			Debug.WriteLine("--- Settings.Load() " + _strPath + " ---");

			Visutronik.XmlAppSettings xmlset = null;
			bool result = true;

			try
			{
				xmlset = new Visutronik.XmlAppSettings(_strPath, false);
/*
				this.strAppDataFolder   = xmlset.Read("Hauptverzeichnis", 		this.strAppDataFolder);
				this.strMeasFolder		= Path.Combine(strAppDataFolder, Constants.MEAS_PATH);
				this.strMeasFolder		= xmlset.Read("Messdatenverzeichnis", 	this.strMeasFolder);
				this.strParamFolder 	= Path.Combine(strAppDataFolder, Constants.PARA_PATH);
				this.strParamFolder		= xmlset.Read("Parameterverzeichnis", 	this.strParamFolder);
				this.strLogFolder 		= Path.Combine(strAppDataFolder, Constants.LOG_PATH);
				this.strLogFolder		= xmlset.Read("Logverzeichnis", 		this.strLogFolder);
				this.strSupportFolder	= Path.Combine(strAppDataFolder, Constants.SUPP_PATH);
				this.strSupportFolder	= xmlset.Read("Supportverzeichnis", 	this.strSupportFolder);
				this.strDocumentation	= Path.Combine(strSupportFolder, Constants.DOKU_FILE);
				this.strDocumentation	= xmlset.Read("Dokumentation", 			this.strDocumentation);
				this.strRemoteCtrl		= Path.Combine(strSupportFolder, Constants.TV_FILE);
				this.strRemoteCtrl		= xmlset.Read("Fernwartung", 			this.strRemoteCtrl);
*/
				this.strSerialPort		= xmlset.Read("Serialport", 			this.strSerialPort);
				this.TimerSeconds       = xmlset.Read("TimerSeconds",			this.TimerSeconds);

				//				this.strImagePath		= xmlset.Read("Bildverzeichnis", this.strImagePath);
				//				this.strDataPath		= xmlset.Read("Datenverzeichnis", this.strDataPath);
				//				this.strLastImage		= xmlset.Read("Bilddatei", this.strLastImage);
				//				this.bSaveJpgImage		= xmlset.Read("JPEG-Format", this.bSaveJpgImage);
				//              this.bSaveWithOverlay   = xmlset.Read("Overlaybild", this.bSaveWithOverlay);
				//				this.nGamma				= xmlset.Read("Gamma", this.nGamma);
				//				this.bGammaEin			= xmlset.Read("GammaEin", this.bGammaEin);
				//              this.bBildkontrolleEin = xmlset.Read("BildkontrolleEin", this.bBildkontrolleEin);
				//              this.nGain              = xmlset.Read("Gain", this.nGain);
				//
				//				// zoomabhängige Einstellungen:
				//				string s;
				//				for (int i=0; i < MAX_ZOOMSTUFEN; i++)
				//				{
				//					s = "Zoom-Schritt" + i.ToString();
				//					this.strZoom[i] = xmlset.Read(s, this.strZoom[i]);
				//
				//					s = "Belichtung" + i.ToString();
				//					this.dExposure[i] = xmlset.Read(s, this.dExposure[i]);
				//				}
				//
				//				// Messung
				//				this.nLastZoomStep		= xmlset.Read("Zoomschritt", this.nLastZoomStep);
				//				this.dRefLength			= xmlset.Read("Referenzlänge", this.dRefLength);
				//				this.nMessModus 		= xmlset.Read("Messmodus", this.nMessModus);
				//				this.nSearchLines		= xmlset.Read("Suchlinien", this.nSearchLines);
				//              this.dMinSlope          = xmlset.Read("MinAnstieg", this.dMinSlope);
				//              this.bGeoDiagEin        = xmlset.Read("GeoDiagnose", this.bGeoDiagEin);

				// Window
				this.bFullscreen 		= xmlset.Read("Fullscreen", this.bFullscreen);
				this.nWindowSizeX 		= xmlset.Read("WindowSizeX", this.nWindowSizeX);
				this.nWindowSizeY 		= xmlset.Read("WindowSizeY", this.nWindowSizeY);
				this.nWindowLocX 		= xmlset.Read("WindowPosX", this.nWindowLocX);
				this.nWindowLocY 		= xmlset.Read("WindowPosY", this.nWindowLocY);
			}
			catch (System.IO.FileNotFoundException fnfex)
			{
				this.strMessage = fnfex.Message;
				result = false;
			}
			catch (Exception ex)
			{
				this.strMessage = ex.Message;
				result = false;
			}
			return result;
		}
		
		/// <summary>
		/// Speichern der Einstellungen in Datei
		/// </summary>
		/// <returns></returns>
		public bool Save()
		{
			Debug.WriteLine("--- Settings.Save() " + _strPath + " ---");
			bool result = true;
			Visutronik.XmlAppSettings xmlset = null;

			try
			{
				xmlset = new Visutronik.XmlAppSettings(_strPath, false);
/*				
				xmlset.Write("Hauptverzeichnis", 		this.strAppDataFolder);
				xmlset.Write("Messdatenverzeichnis", 	this.strMeasFolder);
				xmlset.Write("Parameterverzeichnis", 	this.strParamFolder);
				xmlset.Write("Logverzeichnis", 			this.strLogFolder);
				xmlset.Write("Supportverzeichnis", 		this.strSupportFolder);
				xmlset.Write("Dokumentation", 			this.strDocumentation);
				xmlset.Write("Fernwartung", 			this.strRemoteCtrl);
*/
				xmlset.Write("Serialport", 			this.strSerialPort);
				xmlset.Write("TimerSeconds",		this.TimerSeconds);

				//				xmlset.Write("Bildverzeichnis", this.strImagePath);
				//				xmlset.Write("Datenverzeichnis", this.strDataPath);
				//				xmlset.Write("Bilddatei", this.strLastImage);
				//				xmlset.Write("JPEG-Format", this.bSaveJpgImage.ToString());
				//                xmlset.Write("Overlaybild", this.bSaveWithOverlay.ToString());
				//
				//				xmlset.Write("Belichtungszeit", this.dExpTime);
				//				xmlset.Write("Gamma", this.nGamma);
				//				xmlset.Write("GammaEin", this.bGammaEin);
				//                xmlset.Write("BildkontrolleEin", this.bBildkontrolleEin);
				//                xmlset.Write("Gain", this.nGain);
				//
				//				string s;
				//				for (int i=0; i < MAX_ZOOMSTUFEN; i++)
				//				{
				//					s = "Zoom-Schritt" + i.ToString();
				//					xmlset.Write(s, this.strZoom[i]);
				//					s = "Belichtung" + i.ToString();
				//					xmlset.Write(s, this.dExposure[i]);
				//				}
				//
				//				xmlset.Write("Zoomschritt", this.nLastZoomStep);
				//				xmlset.Write("Referenzlänge", this.dRefLength);
				//				xmlset.Write("Messmodus", this.nMessModus);
				//				xmlset.Write("Suchlinien", this.nSearchLines);
				//                xmlset.Write("MinAnstieg", this.dMinSlope);
				//                xmlset.Write("GeoDiagnose", this.bGeoDiagEin);

				xmlset.Write("Fullscreen", this.bFullscreen);
				xmlset.Write("WindowSizeX", this.nWindowSizeX);
				xmlset.Write("WindowSizeY", this.nWindowSizeY);
				xmlset.Write("WindowPosX", this.nWindowLocX);
				xmlset.Write("WindowPosY", this.nWindowLocY);

				xmlset.Save();
			}
			catch (System.IO.FileNotFoundException fnfex)
			{
				this.strMessage = fnfex.Message;
				result = false;
			}
			catch (Exception ex)
			{
				this.strMessage = ex.Message;
				result = false;
			}
			return result;
		}

		/// <summary>
		/// Setzt Default-Einstellungen
		/// </summary>
		public void Default(string mainfolder)
		{
			Debug.WriteLine("--- Set default settings ---");
/*			
			if (Methods.IsNullOrEmpty(mainfolder))
			{
				//strAppDataFolder = Application.StartupPath;
				// TODO Problem, wenn Zugriffsrechte fehlen, dann lieber in "Dokumente"
				strAppDataFolder  = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
			else
				this.strAppDataFolder = mainfolder;
			
			this.strMeasFolder		= Path.Combine(strAppDataFolder, Constants.MEAS_PATH);
			this.strParamFolder 	= Path.Combine(strAppDataFolder, Constants.PARA_PATH);
			this.strLogFolder 		= Path.Combine(strAppDataFolder, Constants.LOG_PATH);
			this.strSupportFolder	= Path.Combine(strAppDataFolder, Constants.SUPP_PATH);
			this.strDocumentation	= Path.Combine(strSupportFolder, Constants.DOKU_FILE);
			this.strRemoteCtrl		= Path.Combine(strSupportFolder, Constants.TV_FILE);
*/
			this.strSerialPort		= "COM1";
			this.TimerSeconds = 5;

			// Screen
			this.bFullscreen  = false;
			this.nWindowSizeX = 1200;
			this.nWindowSizeY = 800;
			this.nWindowLocX = 100;
			this.nWindowLocY = 10;
		}
		
		
		/// <summary>
		/// Programmeinstellungen im Debugfenster ausgeben:
		/// </summary>
		public void DebugSettings()
		{
			var sb = new StringBuilder();
			sb.AppendLine("--- DEBUG Benutzereinstellungen ---");
/*			
			sb.AppendLine("Verzeichnisse und Dateien:");
			sb.AppendLine("ProgDatenverzeichnis = " + this.strAppDataFolder);
			sb.AppendLine("Messdatenverzeichnis = " +	this.strMeasFolder);
			sb.AppendLine("Parameterverzeichnis = " + this.strParamFolder);
			sb.AppendLine("Logverzeichnis       = " + this.strLogFolder);
			sb.AppendLine("Supportverzeichnis   = " +	this.strSupportFolder);
			sb.AppendLine("Dokumentation        = " + this.strDocumentation);
			sb.AppendLine("Fernwartung          = " + this.strRemoteCtrl);
*/
			sb.AppendLine("Serialport = " +	this.strSerialPort);

/*
			sb.AppendLine("Ansicht:");
			sb.AppendLine("Fullscreen           = " + this.bFullscreen);
			sb.AppendLine("WindowSizeX          = " + this.nWindowSizeX);
			sb.AppendLine("WindowSizeY          = " + this.nWindowSizeY);
			sb.AppendLine("WindowPosX           = " + this.nWindowLocX);
			sb.AppendLine("WindowPosY           = " + this.nWindowLocY);
			sb.AppendLine("");
*/			
			sb.AppendLine("--------------------------------");
			
			Debug.WriteLine(sb.ToString());
		}


/*		
		/// <summary>
		/// Klassenvariablen als String ausgeben
		/// </summary>
		/// <returns>unleserlichen string</returns>
		public override string ToString()
		{
			return string.Format("[Settings StrPath={0}, StrMessage={1}, StrAppDataFolder={2}, StrMeasFolder={3}, StrParamFolder={4}, "
			                     + "StrLogFolder={5}, StrSupportFolder={6}, StrDocumentation={7}, StrRemoteCtrl={8}, "
			                     + "BFullscreen={10}, NWindowSizeX={11}, NWindowSizeY={12}, NWindowLocX={13}, NWindowLocY={14}"
			                     + "]",
			                     _strPath, strMessage, strAppDataFolder, strMeasFolder, strParamFolder, 
			                     strLogFolder, strSupportFolder, strDocumentation, strRemoteCtrl, 
			                     bFullscreen, nWindowSizeX, nWindowSizeY, nWindowLocX, nWindowLocY);
		}
*/		
	}
}
