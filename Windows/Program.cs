///================================================================================
/// Projekt           :
/// Dateibeschreibung :
/// Benutzer: Lulu
/// Datum: 02.07.2021
///================================================================================
 
using System;
using System.Windows.Forms;

namespace LuxmeterClient
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
	}
}
