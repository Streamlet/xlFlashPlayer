namespace Streamlet.xlFlashPlayer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel = new System.Windows.Forms.Panel();
            this.playPause = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.axShockwaveFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.LargeChange = 1;
            this.trackBar.Location = new System.Drawing.Point(72, 0);
            this.trackBar.Maximum = 1;
            this.trackBar.Minimum = 1;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(220, 42);
            this.trackBar.TabIndex = 4;
            this.trackBar.TickFrequency = 0;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar.Value = 1;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "*.swf";
            this.openFileDialog.Filter = "Flash 动画(*.swf)|*.swf";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.playPause);
            this.panel.Controls.Add(this.trackBar);
            this.panel.Controls.Add(this.stop);
            this.panel.Controls.Add(this.openFile);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(0, 249);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(292, 24);
            this.panel.TabIndex = 3;
            // 
            // playPause
            // 
            this.playPause.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.playPause.Enabled = false;
            this.playPause.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.playPause.Font = new System.Drawing.Font("Webdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.playPause.Location = new System.Drawing.Point(24, 0);
            this.playPause.Name = "playPause";
            this.playPause.Size = new System.Drawing.Size(24, 24);
            this.playPause.TabIndex = 2;
            this.playPause.Text = "4";
            this.playPause.UseVisualStyleBackColor = true;
            this.playPause.Click += new System.EventHandler(this.playPause_Click);
            // 
            // stop
            // 
            this.stop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.stop.Enabled = false;
            this.stop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.stop.Font = new System.Drawing.Font("Webdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.stop.Location = new System.Drawing.Point(48, 0);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(24, 24);
            this.stop.TabIndex = 3;
            this.stop.Text = "<";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // openFile
            // 
            this.openFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.openFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.openFile.Font = new System.Drawing.Font("Wingdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.openFile.Location = new System.Drawing.Point(0, 0);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(24, 24);
            this.openFile.TabIndex = 1;
            this.openFile.Text = "1";
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // axShockwaveFlash
            // 
            this.axShockwaveFlash.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axShockwaveFlash.Enabled = true;
            this.axShockwaveFlash.Location = new System.Drawing.Point(0, 0);
            this.axShockwaveFlash.Name = "axShockwaveFlash";
            this.axShockwaveFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash.OcxState")));
            this.axShockwaveFlash.Size = new System.Drawing.Size(292, 248);
            this.axShockwaveFlash.TabIndex = 0;
            this.axShockwaveFlash.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.axShockwaveFlash);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "xlFlashPlayer 1.0 beta1 - by Streamlet";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.Button playPause;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Timer timer;
    }
}

