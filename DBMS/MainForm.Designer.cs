namespace DBMS
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridFilteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paggingViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commonViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexedViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gridFilteringToolStripMenuItem,
            this.paggingToolStripMenuItem,
            this.paggingViewToolStripMenuItem,
            this.fToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(832, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gridFilteringToolStripMenuItem
            // 
            this.gridFilteringToolStripMenuItem.Name = "gridFilteringToolStripMenuItem";
            this.gridFilteringToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.gridFilteringToolStripMenuItem.Text = "In Memory Filtering";
            this.gridFilteringToolStripMenuItem.Click += new System.EventHandler(this.gridFilteringToolStripMenuItem_Click);
            // 
            // paggingToolStripMenuItem
            // 
            this.paggingToolStripMenuItem.Name = "paggingToolStripMenuItem";
            this.paggingToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.paggingToolStripMenuItem.Text = "Pagging";
            // 
            // paggingViewToolStripMenuItem
            // 
            this.paggingViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commonViewToolStripMenuItem,
            this.indexedViewToolStripMenuItem});
            this.paggingViewToolStripMenuItem.Name = "paggingViewToolStripMenuItem";
            this.paggingViewToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.paggingViewToolStripMenuItem.Text = "Pagging View";
            // 
            // fToolStripMenuItem
            // 
            this.fToolStripMenuItem.Name = "fToolStripMenuItem";
            this.fToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.fToolStripMenuItem.Text = "One Table SQL";
            // 
            // commonViewToolStripMenuItem
            // 
            this.commonViewToolStripMenuItem.Name = "commonViewToolStripMenuItem";
            this.commonViewToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.commonViewToolStripMenuItem.Text = "Common View";
            // 
            // indexedViewToolStripMenuItem
            // 
            this.indexedViewToolStripMenuItem.Name = "indexedViewToolStripMenuItem";
            this.indexedViewToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.indexedViewToolStripMenuItem.Text = "Indexed View";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 529);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridFilteringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paggingViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commonViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexedViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fToolStripMenuItem;
    }
}

