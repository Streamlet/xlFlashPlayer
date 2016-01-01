using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Streamlet.xlFlashPlayer
{
    public partial class MainForm : Form
    {
        string preLoadFilePath = null;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string swfFilePath)
        {
            InitializeComponent();
        
            preLoadFilePath = swfFilePath;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitControls();

            if (preLoadFilePath != null)
            {
                if (LoadFile(preLoadFilePath))
                {
                    Play();
                }
                else
                {
                    this.Close();
                }
            }
        }


        private void InitControls()
        {
            openFile.Enabled = true;
            playPause.Enabled = false;
            playPause.Text = "4"; //播放符号
            stop.Enabled = false;
            trackBar.Enabled = false;
        }

        bool isPlaying = false;
        double frameRate = 0.0;

        void Play()
        {
            axShockwaveFlash.Play();
            playPause.Text = ";";   //暂停符号
            timer.Enabled = true;
            stop.Enabled = true;
            isPlaying = true;
            
        }

        void Pause()
        {
            axShockwaveFlash.Stop();
            playPause.Text = "4";   //播放符号
            timer.Enabled = false;
            isPlaying = false;
        }

        void Stop()
        {
            axShockwaveFlash.Stop();
            axShockwaveFlash.GotoFrame(1);
            playPause.Text = "4";   //播放符号
            trackBar.Value = 1;
            timer.Enabled = false;
            stop.Enabled = false;
            isPlaying = false;
        }

        void UpdateCurrentTime(int curFrame)
        {
            if (frameRate >= 0.1)
            {
                int seconds = (int)(curFrame / frameRate);
                curTime.Text = string.Format("{0:00}:{1:00}:{2:00}", seconds / 60 / 60, seconds / 60 % 60, seconds % 60);
            }
        }

        bool LoadFile(string swfFilePath)
        {
            try
            {
                SwfParser sp = new SwfParser();
                if (!sp.ParseSwfHeader(swfFilePath))
                {
                    MessageBox.Show("您所选的文件不是一个有效的 Flash 文件。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (sp.IsCompressed)
                {
                    totalTime.Text = "";
                    curTime.Text = "";
                    MessageBox.Show("这是个压缩的 Flash 文件，暂时无法获取宽度和高度。请自行调整窗口大小以便观看。", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    frameRate = sp.FrameRate;
                    int seconds = (int)(sp.FrameCount / sp.FrameRate);
                    totalTime.Text = string.Format("{0:00}:{1:00}:{2:00}", seconds / 60 / 60, seconds / 60 % 60, seconds % 60);
                    curTime.Text = "00:00:00";
                    this.Width = sp.Width + 8;
                    this.Height = sp.Height + 52;
                    this.CenterToScreen();
                }

                axShockwaveFlash.Movie = swfFilePath;
                trackBar.Minimum = 1;
                axShockwaveFlash.Loop = false;
                axShockwaveFlash.Play();

                trackBar.Maximum = axShockwaveFlash.TotalFrames;
                playPause.Enabled = true;
                trackBar.Enabled = true;

                return true;
            }
            catch
            {
                MessageBox.Show("读取或播放文件失败。", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (isPlaying)
            {
                Pause();
            }

            if (LoadFile(openFileDialog.FileName))
            {
                Play();
            }
        }

        private void playPause_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        private void stop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            bool isPlaying = this.isPlaying;

            Pause();

            axShockwaveFlash.GotoFrame(trackBar.Value);
            UpdateCurrentTime(trackBar.Value);

            if (isPlaying)
            {
                Play();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            trackBar.Value = axShockwaveFlash.FrameNum;

            UpdateCurrentTime(axShockwaveFlash.FrameNum);

            if (!axShockwaveFlash.Playing)
            {
                Stop();
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width < 184)
            {
                this.Width = 184;
            }

            if (this.Height < 60)
            {
                this.Height = 60;
            }
        }

        private void about_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "溪流 Flash 播放器 v1.0 beta3\r\n\r\n\r\n"
                    + "有任何建议意见欢迎来邮相告^_^\r\n\r\n"
                    + "http://www.streamlet.org/\r\n"
                    + "streamlet@outlook.com\r\n",
                "关于溪流 Flash 播放器",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
