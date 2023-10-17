/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Mitarbeiter
 * Datum: 26.03.2018
 * 
 * Version: 
 */
namespace LuxmeterClient
{
	partial class InfoForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelProgName;
		private System.Windows.Forms.Label labelInfo;
		private System.Windows.Forms.Label labelProgVer;
		private System.Windows.Forms.GroupBox groupBoxInfo;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelProgName = new System.Windows.Forms.Label();
			this.labelInfo = new System.Windows.Forms.Label();
			this.labelProgVer = new System.Windows.Forms.Label();
			this.groupBoxInfo = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBoxInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Location = new System.Drawing.Point(7, 19);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(300, 200);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.pictureBox1);
			this.groupBox1.Location = new System.Drawing.Point(319, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(313, 404);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Support";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(7, 230);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(300, 109);
			this.label1.TabIndex = 1;
			this.label1.Text = "visutronik GmbH\r\nRobert-Blum-Straße 5\r\n17033 Neubrandenburg\r\n\r\nTel.: 0395 558 423" +
	" 0\r\nwww.visutronik.de\r\n";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelProgName
			// 
			this.labelProgName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.labelProgName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelProgName.Location = new System.Drawing.Point(12, 12);
			this.labelProgName.Name = "labelProgName";
			this.labelProgName.Size = new System.Drawing.Size(268, 39);
			this.labelProgName.TabIndex = 2;
			this.labelProgName.Text = "labelProgName";
			this.labelProgName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelInfo
			// 
			this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left)));
			this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelInfo.Location = new System.Drawing.Point(7, 16);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(287, 237);
			this.labelInfo.TabIndex = 3;
			this.labelInfo.Text = "\r\n";
			// 
			// labelProgVer
			// 
			this.labelProgVer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.labelProgVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelProgVer.Location = new System.Drawing.Point(12, 51);
			this.labelProgVer.Name = "labelProgVer";
			this.labelProgVer.Size = new System.Drawing.Size(268, 32);
			this.labelProgVer.TabIndex = 5;
			this.labelProgVer.Text = "labelProgVersion";
			this.labelProgVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBoxInfo
			// 
			this.groupBoxInfo.Controls.Add(this.labelInfo);
			this.groupBoxInfo.Location = new System.Drawing.Point(12, 98);
			this.groupBoxInfo.Name = "groupBoxInfo";
			this.groupBoxInfo.Size = new System.Drawing.Size(300, 318);
			this.groupBoxInfo.TabIndex = 6;
			this.groupBoxInfo.TabStop = false;
			this.groupBoxInfo.Text = "Information";
			// 
			// InfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 428);
			this.Controls.Add(this.groupBoxInfo);
			this.Controls.Add(this.labelProgVer);
			this.Controls.Add(this.labelProgName);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InfoForm";
			this.Text = "Information & Support";
			this.Load += new System.EventHandler(this.InfoFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBoxInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
