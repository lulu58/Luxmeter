///================================================================================
/// Projekt           :
/// Dateibeschreibung :
/// Benutzer: Lulu
/// Datum: 05.07.2021
///================================================================================
 
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace LuxmeterClient
{
	/// <summary>
	/// Description of ConfigForm.
	/// </summary>
	public partial class ConfigForm : Form
	{
		// List of available ports;
		public string[] ComPorts { get; set; }

		// Zeitintervalle 1 s ... 5 min
		public string[] TimerIntervals { get; set; } = { "0", "1", "5", "10", "30", "60", "300" };

		// selected port:
		public string SerialPort { get; set; }

		public int TimerSeconds { get; set; } = 1;

		// ctor
		public ConfigForm(string[] ports)
		{
			InitializeComponent();

			if (ports.Length > 0)
			{
				cbPorts.Items.AddRange(ports);
			}

			cbTimer.Items.AddRange(TimerIntervals);
		}
		
		
		// Load event
		void ConfigForm_Load(object sender, EventArgs e)
		{
			cbPorts.SelectedText = SerialPort;
			cbTimer.SelectedItem = TimerSeconds.ToString();
		}

		// Change events
		void cbPorts_SelectedIndexChanged(object sender, EventArgs e)
		{
			SerialPort = cbPorts.SelectedItem.ToString();
			Debug.WriteLine("Port selected: " + SerialPort);
		}

		private void cbTimer_SelectedIndexChanged(object sender, EventArgs e)
		{
			TimerSeconds = Int32.Parse(cbTimer.SelectedItem.ToString());
			Debug.WriteLine("Timer interval selected: " + TimerSeconds);
		}

		// Button event	
		void buttonOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		
		// Button event	
		void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
