///================================================================================
/// Projekt           :
/// Dateibeschreibung :
/// Benutzer: Lulu
/// Datum: 02.07.2021
///================================================================================
 
namespace LuxmeterClient
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button buttonQuit;
		private System.Windows.Forms.Button buttonRun;
		private System.Windows.Forms.Button buttonConfig;
		private System.Windows.Forms.Label labelStatus;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button buttonInfo;
		private ScottPlot.FormsPlot formsPlot1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.buttonQuit = new System.Windows.Forms.Button();
			this.buttonRun = new System.Windows.Forms.Button();
			this.buttonConfig = new System.Windows.Forms.Button();
			this.labelStatus = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.buttonInfo = new System.Windows.Forms.Button();
			this.formsPlot1 = new ScottPlot.FormsPlot();
			this.SuspendLayout();
			// 
			// buttonQuit
			// 
			this.buttonQuit.Location = new System.Drawing.Point(484, 405);
			this.buttonQuit.Name = "buttonQuit";
			this.buttonQuit.Size = new System.Drawing.Size(75, 23);
			this.buttonQuit.TabIndex = 0;
			this.buttonQuit.Text = "Quit";
			this.buttonQuit.UseVisualStyleBackColor = true;
			this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
			// 
			// buttonRun
			// 
			this.buttonRun.Location = new System.Drawing.Point(484, 312);
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.Size = new System.Drawing.Size(75, 23);
			this.buttonRun.TabIndex = 1;
			this.buttonRun.Text = "Run";
			this.buttonRun.UseVisualStyleBackColor = true;
			this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
			// 
			// buttonConfig
			// 
			this.buttonConfig.Location = new System.Drawing.Point(484, 341);
			this.buttonConfig.Name = "buttonConfig";
			this.buttonConfig.Size = new System.Drawing.Size(75, 23);
			this.buttonConfig.TabIndex = 2;
			this.buttonConfig.Text = "Config";
			this.buttonConfig.UseVisualStyleBackColor = true;
			this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
			// 
			// labelStatus
			// 
			this.labelStatus.BackColor = System.Drawing.SystemColors.ControlLight;
			this.labelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelStatus.Location = new System.Drawing.Point(12, 405);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(449, 23);
			this.labelStatus.TabIndex = 3;
			this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(12, 311);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(449, 82);
			this.listBox1.TabIndex = 4;
			// 
			// buttonInfo
			// 
			this.buttonInfo.Location = new System.Drawing.Point(484, 370);
			this.buttonInfo.Name = "buttonInfo";
			this.buttonInfo.Size = new System.Drawing.Size(75, 23);
			this.buttonInfo.TabIndex = 5;
			this.buttonInfo.Text = "Info";
			this.buttonInfo.UseVisualStyleBackColor = true;
			this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
			// 
			// formsPlot1
			// 
			this.formsPlot1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.formsPlot1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.formsPlot1.Location = new System.Drawing.Point(12, 12);
			this.formsPlot1.Name = "formsPlot1";
			this.formsPlot1.Size = new System.Drawing.Size(547, 276);
			this.formsPlot1.TabIndex = 6;
			this.formsPlot1.AxesChanged += new System.EventHandler(this.formsPlot1_AxesChanged);
			this.formsPlot1.Load += new System.EventHandler(this.formsPlot1_Load);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(571, 437);
			this.Controls.Add(this.formsPlot1);
			this.Controls.Add(this.buttonInfo);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.labelStatus);
			this.Controls.Add(this.buttonConfig);
			this.Controls.Add(this.buttonRun);
			this.Controls.Add(this.buttonQuit);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "LuxmeterClient";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
	}
}
