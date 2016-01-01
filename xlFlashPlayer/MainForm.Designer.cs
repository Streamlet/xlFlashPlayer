using System.Windows.Forms;

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
            this.totalTime = new System.Windows.Forms.Label();
            this.curTime = new System.Windows.Forms.Label();
            this.playPause = new System.Windows.Forms.Button();
            this.about = new System.Windows.Forms.Button();
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
            this.trackBar.BackColor = System.Drawing.Color.White;
            this.trackBar.LargeChange = 0;
            this.trackBar.Location = new System.Drawing.Point(72, 0);
            this.trackBar.Maximum = 1;
            this.trackBar.Minimum = 1;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(146, 42);
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
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Controls.Add(this.totalTime);
            this.panel.Controls.Add(this.curTime);
            this.panel.Controls.Add(this.playPause);
            this.panel.Controls.Add(this.trackBar);
            this.panel.Controls.Add(this.about);
            this.panel.Controls.Add(this.stop);
            this.panel.Controls.Add(this.openFile);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(0, 249);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(292, 24);
            this.panel.TabIndex = 3;
            // 
            // totalTime
            // 
            this.totalTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.totalTime.BackColor = System.Drawing.Color.White;
            this.totalTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.totalTime.ForeColor = System.Drawing.Color.SteelBlue;
            this.totalTime.Location = new System.Drawing.Point(215, 12);
            this.totalTime.Name = "totalTime";
            this.totalTime.Size = new System.Drawing.Size(53, 12);
            this.totalTime.TabIndex = 6;
            this.totalTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // curTime
            // 
            this.curTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.curTime.BackColor = System.Drawing.Color.White;
            this.curTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.curTime.ForeColor = System.Drawing.Color.SkyBlue;
            this.curTime.Location = new System.Drawing.Point(215, 0);
            this.curTime.Name = "curTime";
            this.curTime.Size = new System.Drawing.Size(53, 12);
            this.curTime.TabIndex = 5;
            this.curTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playPause
            // 
            this.playPause.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.playPause.BackColor = System.Drawing.SystemColors.Control;
            this.playPause.Enabled = false;
            this.playPause.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.playPause.Font = new System.Drawing.Font("Webdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.playPause.ForeColor = System.Drawing.Color.Black;
            this.playPause.Location = new System.Drawing.Point(24, 0);
            this.playPause.Name = "playPause";
            this.playPause.Size = new System.Drawing.Size(24, 24);
            this.playPause.TabIndex = 2;
            this.playPause.Text = "4";
            this.playPause.UseVisualStyleBackColor = false;
            this.playPause.Click += new System.EventHandler(this.playPause_Click);
            // 
            // about
            // 
            this.about.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.about.BackColor = System.Drawing.SystemColors.Control;
            this.about.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.about.Font = new System.Drawing.Font("Webdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.about.Location = new System.Drawing.Point(268, 0);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(24, 24);
            this.about.TabIndex = 5;
            this.about.Text = "i";
            this.about.UseVisualStyleBackColor = false;
            this.about.Click += new System.EventHandler(this.about_Click);
            // 
            // stop
            // 
            this.stop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.stop.BackColor = System.Drawing.SystemColors.Control;
            this.stop.Enabled = false;
            this.stop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.stop.Font = new System.Drawing.Font("Webdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.stop.Location = new System.Drawing.Point(48, 0);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(24, 24);
            this.stop.TabIndex = 3;
            this.stop.Text = "<";
            this.stop.UseVisualStyleBackColor = false;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // openFile
            // 
            this.openFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.openFile.BackColor = System.Drawing.SystemColors.Control;
            this.openFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.openFile.Font = new System.Drawing.Font("Wingdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.openFile.ForeColor = System.Drawing.Color.Black;
            this.openFile.Location = new System.Drawing.Point(0, 0);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(24, 24);
            this.openFile.TabIndex = 1;
            this.openFile.Text = "1";
            this.openFile.UseVisualStyleBackColor = false;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // timer
            // 
            this.timer.Interval = 500;
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
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.axShockwaveFlash);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "溪流 Flash 播放器 v1.0 beta3";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
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
        private System.Windows.Forms.Label totalTime;
        private System.Windows.Forms.Label curTime;
        private Button about;
    }
}

