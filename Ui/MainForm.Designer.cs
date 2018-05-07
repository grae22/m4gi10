namespace m4gi10.Ui
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.uiMusicFolder = new System.Windows.Forms.TextBox();
      this.uiScanMusicFolder = new System.Windows.Forms.Button();
      this.uiFiles = new System.Windows.Forms.CheckedListBox();
      this.uiGo = new System.Windows.Forms.Button();
      this.uiOutputFolder = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(67, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Music folder:";
      // 
      // uiMusicFolder
      // 
      this.uiMusicFolder.Location = new System.Drawing.Point(85, 16);
      this.uiMusicFolder.Name = "uiMusicFolder";
      this.uiMusicFolder.Size = new System.Drawing.Size(160, 20);
      this.uiMusicFolder.TabIndex = 1;
      // 
      // uiScanMusicFolder
      // 
      this.uiScanMusicFolder.Location = new System.Drawing.Point(251, 14);
      this.uiScanMusicFolder.Name = "uiScanMusicFolder";
      this.uiScanMusicFolder.Size = new System.Drawing.Size(75, 23);
      this.uiScanMusicFolder.TabIndex = 2;
      this.uiScanMusicFolder.Text = "Scan";
      this.uiScanMusicFolder.UseVisualStyleBackColor = true;
      this.uiScanMusicFolder.Click += new System.EventHandler(this.uiScanMusicFolder_Click);
      // 
      // uiFiles
      // 
      this.uiFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.uiFiles.CheckOnClick = true;
      this.uiFiles.FormattingEnabled = true;
      this.uiFiles.Location = new System.Drawing.Point(15, 53);
      this.uiFiles.Name = "uiFiles";
      this.uiFiles.Size = new System.Drawing.Size(800, 424);
      this.uiFiles.TabIndex = 3;
      // 
      // uiGo
      // 
      this.uiGo.Location = new System.Drawing.Point(740, 14);
      this.uiGo.Name = "uiGo";
      this.uiGo.Size = new System.Drawing.Size(75, 23);
      this.uiGo.TabIndex = 6;
      this.uiGo.Text = "Go!";
      this.uiGo.UseVisualStyleBackColor = true;
      this.uiGo.Click += new System.EventHandler(this.uiGo_Click);
      // 
      // uiOutputFolder
      // 
      this.uiOutputFolder.Location = new System.Drawing.Point(574, 16);
      this.uiOutputFolder.Name = "uiOutputFolder";
      this.uiOutputFolder.Size = new System.Drawing.Size(160, 20);
      this.uiOutputFolder.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(497, 19);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(71, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Output folder:";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(827, 491);
      this.Controls.Add(this.uiGo);
      this.Controls.Add(this.uiOutputFolder);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.uiFiles);
      this.Controls.Add(this.uiScanMusicFolder);
      this.Controls.Add(this.uiMusicFolder);
      this.Controls.Add(this.label1);
      this.Name = "MainForm";
      this.Text = "m4gi10";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox uiMusicFolder;
    private System.Windows.Forms.Button uiScanMusicFolder;
    private System.Windows.Forms.CheckedListBox uiFiles;
    private System.Windows.Forms.Button uiGo;
    private System.Windows.Forms.TextBox uiOutputFolder;
    private System.Windows.Forms.Label label2;
  }
}