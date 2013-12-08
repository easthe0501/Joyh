namespace Host
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnTopMost = new System.Windows.Forms.ToolStripButton();
            this.tbtnClear = new System.Windows.Forms.ToolStripButton();
            this.tbtnReloadCfg = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pgPerfomance = new System.Windows.Forms.PropertyGrid();
            this.txtConsole = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnTopMost,
            this.tbtnClear,
            this.tbtnReloadCfg});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(864, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnTopMost
            // 
            this.tbtnTopMost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnTopMost.Image = ((System.Drawing.Image)(resources.GetObject("tbtnTopMost.Image")));
            this.tbtnTopMost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnTopMost.Name = "tbtnTopMost";
            this.tbtnTopMost.Size = new System.Drawing.Size(23, 22);
            this.tbtnTopMost.Text = "窗口置顶";
            this.tbtnTopMost.Click += new System.EventHandler(this.tbtnTopMost_Click);
            // 
            // tbtnClear
            // 
            this.tbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tbtnClear.Image")));
            this.tbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnClear.Name = "tbtnClear";
            this.tbtnClear.Size = new System.Drawing.Size(23, 22);
            this.tbtnClear.Text = "清空屏幕";
            this.tbtnClear.Click += new System.EventHandler(this.tbtnClear_Click);
            // 
            // tbtnReloadCfg
            // 
            this.tbtnReloadCfg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnReloadCfg.Image = ((System.Drawing.Image)(resources.GetObject("tbtnReloadCfg.Image")));
            this.tbtnReloadCfg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnReloadCfg.Name = "tbtnReloadCfg";
            this.tbtnReloadCfg.Size = new System.Drawing.Size(23, 22);
            this.tbtnReloadCfg.Text = "刷新配置";
            this.tbtnReloadCfg.Click += new System.EventHandler(this.tbtnReloadCfg_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 500);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(864, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel1.Text = "就绪";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pgPerfomance);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtConsole);
            this.splitContainer1.Size = new System.Drawing.Size(864, 475);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.TabIndex = 2;
            // 
            // pgPerfomance
            // 
            this.pgPerfomance.CommandsDisabledLinkColor = System.Drawing.Color.Black;
            this.pgPerfomance.CommandsForeColor = System.Drawing.Color.Black;
            this.pgPerfomance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgPerfomance.HelpVisible = false;
            this.pgPerfomance.Location = new System.Drawing.Point(0, 0);
            this.pgPerfomance.Name = "pgPerfomance";
            this.pgPerfomance.Size = new System.Drawing.Size(284, 473);
            this.pgPerfomance.TabIndex = 0;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.ForeColor = System.Drawing.Color.White;
            this.txtConsole.Location = new System.Drawing.Point(0, 0);
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.Size = new System.Drawing.Size(572, 473);
            this.txtConsole.TabIndex = 0;
            this.txtConsole.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 522);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Action服务器宿主";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbtnTopMost;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid pgPerfomance;
        private System.Windows.Forms.RichTextBox txtConsole;
        private System.Windows.Forms.ToolStripButton tbtnClear;
        private System.Windows.Forms.ToolStripButton tbtnReloadCfg;
    }
}

