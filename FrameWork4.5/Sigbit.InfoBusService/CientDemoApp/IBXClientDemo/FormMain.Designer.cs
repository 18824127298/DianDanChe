namespace IBXClientDemo
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dNSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDNSTestClient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.vCodeBreakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestVCodeBreak = new System.Windows.Forms.ToolStripMenuItem();
            this.voiceRegIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestVoiceReg = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dNSToolStripMenuItem,
            this.vCodeBreakToolStripMenuItem,
            this.voiceRegIToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(589, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dNSToolStripMenuItem
            // 
            this.dNSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDNSTestClient,
            this.toolStripMenuItem1,
            this.mnuClose});
            this.dNSToolStripMenuItem.Name = "dNSToolStripMenuItem";
            this.dNSToolStripMenuItem.Size = new System.Drawing.Size(63, 21);
            this.dNSToolStripMenuItem.Text = "DNS(&D)";
            // 
            // mnuDNSTestClient
            // 
            this.mnuDNSTestClient.Name = "mnuDNSTestClient";
            this.mnuDNSTestClient.Size = new System.Drawing.Size(191, 22);
            this.mnuDNSTestClient.Text = "Test DNS Client(&C)...";
            this.mnuDNSTestClient.Click += new System.EventHandler(this.mnuDNSTestClient_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuClose
            // 
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(191, 22);
            this.mnuClose.Text = "Close(&X)";
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // vCodeBreakToolStripMenuItem
            // 
            this.vCodeBreakToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTestVCodeBreak});
            this.vCodeBreakToolStripMenuItem.Name = "vCodeBreakToolStripMenuItem";
            this.vCodeBreakToolStripMenuItem.Size = new System.Drawing.Size(109, 21);
            this.vCodeBreakToolStripMenuItem.Text = "VCodeBreak(&V)";
            // 
            // mnuTestVCodeBreak
            // 
            this.mnuTestVCodeBreak.Name = "mnuTestVCodeBreak";
            this.mnuTestVCodeBreak.Size = new System.Drawing.Size(197, 22);
            this.mnuTestVCodeBreak.Text = "Test VCode Break(&V)";
            this.mnuTestVCodeBreak.Click += new System.EventHandler(this.mnuTestVCodeBreak_Click);
            // 
            // voiceRegIToolStripMenuItem
            // 
            this.voiceRegIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTestVoiceReg});
            this.voiceRegIToolStripMenuItem.Name = "voiceRegIToolStripMenuItem";
            this.voiceRegIToolStripMenuItem.Size = new System.Drawing.Size(87, 21);
            this.voiceRegIToolStripMenuItem.Text = "VoiceReg(&I)";
            // 
            // mnuTestVoiceReg
            // 
            this.mnuTestVoiceReg.Name = "mnuTestVoiceReg";
            this.mnuTestVoiceReg.Size = new System.Drawing.Size(218, 22);
            this.mnuTestVoiceReg.Text = "Test Voice Regnoition(&T)";
            this.mnuTestVoiceReg.Click += new System.EventHandler(this.mnuTestVoiceReg_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 381);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "IBX Client Demo";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dNSToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem mnuDNSTestClient;
        private System.Windows.Forms.ToolStripMenuItem vCodeBreakToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTestVCodeBreak;
        private System.Windows.Forms.ToolStripMenuItem voiceRegIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTestVoiceReg;
    }
}

