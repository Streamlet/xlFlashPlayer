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
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitControls();
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

            try
            {
                SwfParser sp = new SwfParser();
                if (!sp.ParseSwfHeader(openFileDialog.FileName))
                {
                    MessageBox.Show("您所选的文件不是一个有效的 Flash 文件。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (sp.IsCompressed)
                {
                    MessageBox.Show("这是个压缩的 Flash 文件，无法获取宽度和高度。请自行调整窗口大小以便观看。", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.Width = sp.Width + 8;
                    this.Height = sp.Height + 52;
                    this.CenterToScreen();
                }

                axShockwaveFlash.Movie = openFileDialog.FileName;
                trackBar.Minimum = 1;
                trackBar.Maximum = axShockwaveFlash.TotalFrames;
                trackBar.LargeChange = trackBar.Maximum / 10;
                playPause.Text = ";";   //暂停符号
                axShockwaveFlash.Loop = false;
                axShockwaveFlash.Play();
                timer.Enabled = true;
                playPause.Enabled = true;
                stop.Enabled = true;
                trackBar.Enabled = true;
                isPlaying = true;
            }
            catch
            {
                MessageBox.Show("读取或播放文件失败。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
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
            axShockwaveFlash.GotoFrame(trackBar.Value);

            if (isPlaying)
            {
                axShockwaveFlash.Play();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            trackBar.Value = axShockwaveFlash.FrameNum;

            if (!axShockwaveFlash.Playing)
            {
                Stop();
            }
        }
    }
}
