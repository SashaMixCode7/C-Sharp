using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Microsoft.UI.Windowing;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Microsoft.UI;
using Windows.UI.WindowManagement;

namespace WinFormsApp_Bongo_Cat_Forms
{
    public partial class Form1 : Form
    {
        private int actionCount = 0;
        private Label countLabel;
        private int imageToggle = 0;

        // Изображения кота
        private Bitmap RightPaw;
        private Bitmap LeftPaw;
        private Bitmap raisedPaws;

        private Dictionary<Keys, Bitmap> imageKeys = new Dictionary<Keys, Bitmap>();
        private Keys lastPressedKey = Keys.None;

        private DispatcherTimer timer;
        private DispatcherTimer topmostTimer;
        private const double defaultTimeoutSeconds = 0.01;
        private double timeoutSeconds = defaultTimeoutSeconds;

        public Form1()
        {
            InitializeComponent();

            // Загрузка изображений из ресурсов
            RightPaw = Properties.Resources.cat_right ??
                             Properties.Resources.default_lap as Bitmap;
            LeftPaw = Properties.Resources.cat_left ??
                            Properties.Resources.default_lap as Bitmap;
            raisedPaws = Properties.Resources.cat_simple ??
                        Properties.Resources.default_lap as Bitmap;

            // Настройка начального состояния
            pictureBox1.Image = raisedPaws;

            // Инициализация таймеров
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(timeoutSeconds);
            timer.Tick += Timer_Tick;

            topmostTimer = new DispatcherTimer();
            topmostTimer.Interval = TimeSpan.FromSeconds(1);
        }

        private void HandleKeyPress()
        {
            timer.Stop();
            timer.Interval = TimeSpan.FromSeconds(timeoutSeconds);
            timer.Start();

            switch (imageToggle)
            {
                case 0:
                    pictureBox1.Image = RightPaw;
                    imageToggle = 1;
                    break;
                case 1:
                    pictureBox1.Image = LeftPaw;
                    imageToggle = 0;
                    break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pictureBox1.Image = raisedPaws;
            timer.Stop();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Исключаем системные клавиши
            if (keyData == Keys.Space || keyData == Keys.LWin ||
                keyData == Keys.RWin || keyData == Keys.Tab)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // Загружаем изображение для нажатой клавиши
            if (!imageKeys.ContainsKey(keyData))
            {
                string resourceName = $"lap_{keyData}";
                try
                {
                    Bitmap image = Properties.Resources.ResourceManager.GetObject(resourceName) as Bitmap;
                    if (image != null)
                    {
                        imageKeys[keyData] = image;
                    }
                }
                catch
                {
                    imageKeys[keyData] = Properties.Resources.default_lap as Bitmap;
                }
            }

            if (pictureBox1.Focused)
            {
                pictureBox1.Image = imageKeys[keyData];
                lastPressedKey = keyData;
            }

            UpdateCounter();
            System.Media.SystemSounds.Beep.Play();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UpdateCounter()
        {
            actionCount++;
            countLabel.Text = $"Действий: {actionCount}";
        }

        private void BongoCatForm_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateCounter();
            System.Media.SystemSounds.Beep.Play();
        }

        private void BongoCatForm_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateCounter();
            System.Media.SystemSounds.Beep.Play();
        }

        private void BongoCatForm_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateCounter();
            System.Media.SystemSounds.Beep.Play();

            Graphics g = this.CreateGraphics();
            g.FillEllipse(System.Drawing.Brushes.Red,
                         this.PointToClient(Cursor.Position).X - 10,
                         this.PointToClient(Cursor.Position).Y - 10,
                         20, 20);
            g.Dispose();
        }

    }
}
