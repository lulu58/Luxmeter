///================================================================================
/// Projekt           :	Client für Arduino-Luxmeter mit BH1750 (GY-30)
/// Dateibeschreibung : Numerische und Diagrammanzeige
/// Benutzer: Lulu
/// 02.07.2021 	initial 
/// 10.08.2021	add ScottPlot
///             (ScottPlot 3.1.6 is the last version supporting .NET Framework 4.5)
/// 16.08.2021	V1.1	add settings            
/// 28.09.2021	V1.2	add timer
///================================================================================
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using ScottPlot;
//using ScottPlot.UserControls;

namespace LuxmeterClient
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		const string PROG_NAME = "Luxmeter Client";
		const string PROG_VERSION = "V1.2";
		const string PROG_VENDOR = "visutronik GmbH";
		const string SERIALPORT = "COM3";
		
		Settings settings;
		SerialPort sp = new SerialPort();
		string[] ports;
		bool running = false;
		bool elapsed = false;
		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

		ScottPlot.Plot plt;
		Random rand = new Random();
		double[] data;

		//ScottPlot.Plottable signal;
		//bool busy = false;
        
		List<double> luxdata = new List<double>();
		
		#region ----- Mainform -----

		public MainForm()
		{
			InitializeComponent();
			this.Text = PROG_NAME + " " + PROG_VERSION;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MainForm_Load(object sender, EventArgs e)
		{
			string p = Settings.GetConfigPath(PROG_VENDOR, PROG_NAME, "settings.bin");
			settings = new Settings(p);
			if (!settings.Load()) {
				Output("Error loading settings!");
			}
			
			ports = SerialPort.GetPortNames();
			if (ports.Length == 0) {
				ShowStatus("No serial port found!");
			} else {
				foreach (string s in ports) {
					Output("Port found: " + s);
					
					//GetCommProps(s);
					Thread.Sleep(100);
				}
				Output("Stored port: " + settings.strSerialPort);
				Output("Stored interval [s]: " + settings.TimerSeconds);
			}
            timer.Tick += Timer_Tick;
			UpdateButtons(running);
			InitPlot();
		}

        private void Timer_Tick(object sender, EventArgs e)
        {
			elapsed = true;
			Debug.WriteLine("Timer_Tick");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Debug.WriteLine("MainForm_FormClosing");
			running = false;
			if ((sp != null) && sp.IsOpen)
			{
				Debug.WriteLine(" - close serial port");
				try 
				{
					//sp.DataReceived -= sp_DataReceived;
					sp.Close();
				} 
				catch (Exception ex) 
				{
					Debug.WriteLine(ex.Message);
				}
			}
			System.Threading.Thread.Sleep(500);
			Debug.WriteLine(" - save settings");
			settings.Save();
			Debug.WriteLine(" - close window");
		}

		#endregion

		#region ----- Buttons --------------------------
	
		/// <summary>
		/// Button: Programmende
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonQuit_Click(object sender, EventArgs e)
		{
			Debug.WriteLine("Button QUIT");
			this.Close();

		}
		
		/// <summary>
		/// Button: Start/Stopp der Messung
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonRun_Click(object sender, EventArgs e)
		{
			Debug.WriteLine("Button RUN");
			if (running) 
			{
				sp.Close();
				timer.Stop();
				running = false;
				Output("Measure stopped, port closed");
			} 
			else 
			{
				luxdata.Clear();
				running = OpenPort();
				if (settings.TimerSeconds > 0)
				{
					elapsed = true;	// erste Messung sofort!
					timer.Interval = settings.TimerSeconds * 1000;
					timer.Start();
				}
			}
			UpdateButtons(running);
		}
		
		/// <summary>
		/// Hardware-Konfiguration-Dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonConfig_Click(object sender, EventArgs e)
		{
			var conf = new ConfigForm(this.ports);
		
			conf.SerialPort = settings.strSerialPort;
			conf.TimerSeconds = settings.TimerSeconds;

			if (conf.ShowDialog() == DialogResult.OK)
			{
				settings.strSerialPort = conf.SerialPort;
				Output("New port selected: " + settings.strSerialPort);

				settings.TimerSeconds = conf.TimerSeconds;
				Output("New timer interval selected: " + settings.TimerSeconds);
			}
		}

		/// <summary>
		/// Programminfo-Dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void buttonInfo_Click(object sender, EventArgs e)
		{
			var info = new InfoForm(this);
			info.strProgName = PROG_NAME;
			info.strProgVer = PROG_VERSION;
			info.strInfo =
				"Arduino + GY30 based Luxmeter by Lulu\n\n" +
				"Credits:\n" +
			"ScottPlot - Scott W. Harden\n" +
			"  https://swharden.com/scottplot\n" +
			"and others...";
			
			info.ShowDialog();
		}

		#endregion
		
		#region ----- internal methods -----------------
		
		delegate void StatusCallback(string s);

		void ShowStatus(string msg)
		{
			if (labelStatus.InvokeRequired) {
				labelStatus.BeginInvoke(new StatusCallback(ShowStatus), msg);
			} else {
				this.labelStatus.Text = msg;
			}
		}
		
		delegate void OutputCallback(string s);
		
		void Output(string s)
		{
			if (listBox1.InvokeRequired) {
				listBox1.BeginInvoke(new OutputCallback(Output), s);
			} else {
				Debug.WriteLine(s);
				
				// am Ende einfügen und den letzten Eintrag sichtbar machen:
				listBox1.Items.Add(s);
				// Länge der Liste begrenzen - ältesten Eintrag löschen
				if (listBox1.Items.Count > 50)
					listBox1.Items.RemoveAt(0);
				// Cursor auf letzten Eintrag setzen
				listBox1.SelectedIndex = listBox1.Items.Count - 1;
				// und sofort neu zeichnen
				listBox1.Update();
			}
		}
		/*
                // am Ende einfügen und den letzten Eintrag sichtbar machen:
                lbxDiagnose.Items.Add(msg);
                if (lbxDiagnose.Items.Count > 50) lbxDiagnose.Items.RemoveAt(0);
                lbxDiagnose.SelectedIndex = lbxDiagnose.Items.Count - 1;

                // immer ersten Eintrag sichtbar machen, z.B. für Fehlerlog:
                //lbDiagnose.Items.Insert(0, msg);
                //if (lbDiagnose.Items.Count > 50) lbDiagnose.Items.RemoveAt(50);
                //lbDiagnose.SelectedIndex = -1;

                lbxDiagnose.Update();    // sofort neu zeichnen!
                Debug.WriteLine(msg);

		*/

		void UpdateButtons(bool run)
		{
			buttonQuit.Enabled = !run;
			buttonConfig.Enabled = !run;
			if (run) buttonRun.Text = "Stop"; else buttonRun.Text = "Start";
		}

		#endregion
		
		#region ----- COM-Port -------------------------

		private bool OpenPort()
		{
			bool bOk = true;
			try {
				sp.PortName = settings.strSerialPort;
				//Output("using " + settings.strSerialPort);
				sp.BaudRate = 115200;
				sp.ReadTimeout = 200;
				sp.DataReceived += sp_DataReceived;
				sp.PinChanged += sp_PinChanged;
				sp.ErrorReceived += sp_ErrorReceived;
				sp.Open();
				Output("Port opened, measure running");
			} catch (Exception ex) {
				//ShowStatus(ex.Message);
				Output(ex.Message);
				bOk = false;
			}
			return bOk;
		}
		
		/// <summary>
		/// Event: serielle Daten empfangen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try 
			{
				string s = sp.ReadLine();
				ShowStatus(s);
				
				string[] parts = s.Split(' ');
				double lux;
				if (Double.TryParse(parts[2].Replace('.', ','), out lux)) 
				{
					//Debug.WriteLine(lux);
					// graphical output
					if (settings.TimerSeconds > 0)
					{
						if (elapsed)
						{
							luxdata.Add(lux);
							UpdatePlot();
							elapsed = false;
						}
					}
					else
                    {
						luxdata.Add(lux);
						UpdatePlot();
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
		
		void sp_PinChanged(object sender, SerialPinChangedEventArgs e)
		{
			//throw new NotImplementedException();
			Output("sp_PinChanged!");
		}

		void sp_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
		{
			//throw new NotImplementedException();
			Output("sp_ErrorReceived!");
		}

		// Ermittlung der CommunicationProperties der seriellen Interfaces
		// https://stackoverflow.com/questions/1165692/how-to-programmatically-find-all-available-baudrates-in-c-sharp-serialport-clas
		// https://www.codeproject.com/Articles/75770/Basic-serial-port-listening-application
		
		[StructLayout(LayoutKind.Sequential)]
		struct COMMPROP
		{
			public short wPacketLength;
			public short wPacketVersion;
			public int dwServiceMask;
			public int dwReserved1;
			public int dwMaxTxQueue;
			public int dwMaxRxQueue;
			public int dwMaxBaud;
			public int dwProvSubType;
			public int dwProvCapabilities;
			public int dwSettableParams;
			public int dwSettableBaud;
			public short wSettableData;
			public short wSettableStopParity;
			public int dwCurrentTxQueue;
			public int dwCurrentRxQueue;
			public int dwProvSpec1;
			public int dwProvSpec2;
			public string wcProvChar;
		}

		[DllImport("kernel32.dll")]
		static extern bool GetCommProperties(IntPtr hFile, ref COMMPROP lpCommProp);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr CreateFile(string lpFileName, int dwDesiredAccess,
			int dwShareMode, IntPtr securityAttrs, int dwCreationDisposition,
			int dwFlagsAndAttributes, IntPtr hTemplateFile);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="portName"></param>
		public void GetCommProps(string portName)
		{
			COMMPROP commProp = new COMMPROP();
			IntPtr hFile = CreateFile(@"\\.\" + portName, 0, 0, IntPtr.Zero, 3, 0x80, IntPtr.Zero);
			if (GetCommProperties(hFile, ref commProp)) {
				Debug.WriteLine("dwSettableBaud = 0x" + commProp.dwSettableBaud.ToString("X4"));
				Debug.WriteLine("dwMaxBaud      = 0x" + commProp.dwMaxBaud.ToString("X4"));
				if (commProp.dwMaxBaud == 0x10000000)
					Debug.WriteLine(" = BAUD_USER");
			}
			//CloseFile(hFile);
		}


		public void DataAvailable(object sender, SerialDataReceivedEventArgs eargs)
		{
   			if (InvokeRequired)
       			Invoke(new Action<object, SerialDataReceivedEventArgs>(DataAvailable), new object[] {sender, eargs});
   			else
   			{
       			//Aufrufe zum Aktualisieren der Daten
   			}
		}
        
		#endregion
               
		#region ----- Plot -----------------------------
        
		/// <summary>
		/// Event: control loaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void formsPlot1_Load(object sender, EventArgs e)
		{
			// TODO: Implement formsPlot1_Load
			Debug.WriteLine("formsPlot1_Load");
			plt = this.formsPlot1.plt;
		}

		/// <summary>
		/// Ploteigenschaften festlegen
		/// </summary>
		private void InitPlot()
		{
			Debug.WriteLine("InitPlot");
			
			const int pointCount = 50;
			double[] dataXs = ScottPlot.DataGen.Consecutive(pointCount);
			double[] dataSin = ScottPlot.DataGen.Sin(pointCount);
			//formsPlot1.plt.PlotScatter(dataXs, dataSin);
			
			formsPlot1.plt.Title("Luxmeter");
			//plt.Title("Very Complicated Data");
			plt.YLabel("Lux");
			plt.XLabel("Measure");
			//plt.TightenLayout(padding: 20);
			//plt.TightenLayout();
			//plt.SaveFig("Plot1.png");
			//formsPlot1.Render();
			
//			data = ScottPlot.DataGen.RandomWalk(rand, 100);
//			signal = formsPlot1.plt.PlotSignalConst(data);
//			formsPlot1.plt.PlotSignalConst(data.Select(x => (int)x).ToArray());
//			formsPlot1.plt.Benchmark();
			formsPlot1.Render();
		}
		
		
		void formsPlot1_AxesChanged(object sender, EventArgs e)
		{
			// TODO: Implement formsPlot1_AxesChanged
			Debug.WriteLine("formsPlot1_AxesChanged");
		}

		const int MAX_VISIBLE_VALUES = 100;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="new_value"></param>
		void UpdatePlot()
		{
			// Debug.WriteLine("UpdatePlot");

			// Daten 
			int values = luxdata.Count;
			if (values > MAX_VISIBLE_VALUES) 
				luxdata.RemoveRange(0, values - MAX_VISIBLE_VALUES);
			
			formsPlot1.plt.Clear();
			data = luxdata.ToArray();

            if (data == null)
            {
                return;
            }
            else
            {
                if (formsPlot1.plt.GetPlottables().Count == 0)
                {
                    // plot the data
                    formsPlot1.plt.PlotSignalConst(data, markerSize: 0);
                    //formsPlot1.plt.PlotSignal(data, 0.001, markerSize: 0); // auch o.k.
                    //formsPlot1.plt.AxisAuto(0, .5); // wichtig!
                    //formsPlot1.plt.AxisAutoX();
                    //formsPlot1.plt.AxisAutoY();
                    formsPlot1.plt.Axis(0.0, (double) MAX_VISIBLE_VALUES, 0.0, 2500.0);
                }
            }
			//formsPlot1.plt.Benchmark();
            formsPlot1.Render();
            return;
		}
		
/*
 * === RESTERAMPE ===
 * 		
//                if (formsPlot1.plt.GetPlottables().Count > 0)
//                    formsPlot1.plt.Clear();

			//List<ScottPlot.Plottable> liste = formsPlot1.plt.GetPlottables();
			//Debug.WriteLine(liste[0].GetType());	// double[]
			//Debug.WriteLine(liste[1].GetType());	// int[]
			//ScottPlot.PlottableSignalConst`1[System.Double]
			//ScottPlot.PlottableSignalConst`1[System.Int32]

//			signal = formsPlot1.plt.PlotSignalConst(data);
//			int[] xs = data.Select(x => (int)x).ToArray();
//			formsPlot1.plt.PlotSignalConst(data.Select(x => (int)x).ToArray());

			// last update to make it look perfect and able to new run
			this.BeginInvoke((MethodInvoker)(() => {
				while (busy) Thread.Sleep(100);
				formsPlot1.Render(); // true, false);
			}));                

 */
		#endregion
		
	}
}
