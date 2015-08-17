using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinApiWrapper.Enums;
using WinApiWrapper.Native.Methods;
using WinApiWrapper.Wrappers;

namespace WinApiWrapperTestClient
{
    public partial class TestClient : Form
    {
        public TestClient()
        {
            InitializeComponent();
        }

        private void TestClient_Load(object sender, EventArgs e)
        {
            var timer = new Timer
            {
                Interval = 20,
                Enabled = true
            };
            var mouse = new WinApiMouse();
            timer.Tick += (o, args) =>
            {
                mousePosition.BeginInvoke(new Action(() =>
                {
                    var position = mouse.Position;
                    var client = mouse.GetClientPosition(Handle);
                    mousePosition.Text = String.Format("X: {0}, Y: {1}", position.X, position.Y);
                    clientPosition.Text = String.Format("X: {0}, Y: {1}", client.X, client.Y);
                }));
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var windows = WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null);
            foreach (var window in windows)
            {
                listBox1.Items.Add(window.Title);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var windows =
                WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null).ToList();

            textbox1.Text = windows[index].Title;
            listBox2.Items.Clear();
            var childWindows = WinApiWindow.EnumChildWindows(windows[index].Hwnd);
            foreach (var childWindow in childWindows)
            {
                if (!String.IsNullOrWhiteSpace(childWindow.Title))
                {
                    listBox2.Items.Add(childWindow.Title);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var window = WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null).ToList()[index];
            MessageBox.Show(@"IsToolWindow: " + window.IsToolWindow);
            //button1.PerformClick();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var window = WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null).ToList()[index];
            window.IsToolWindow = !window.IsToolWindow;
            //button1.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var window = WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null).ToList()[index];
            window.Close();
            button1.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var window = WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null).ToList()[index];
            var currentWindow = new WinApiWindow(Handle)
            {
                Parent = window
            };
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var window = WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null).ToList()[index];
            window.Title = textbox1.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var window = WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null).ToList()[index];
            window.IsTopMost = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var window = WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null).ToList()[index];
            window.IsTopMost = false;
        }

        private void TestClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            User32.ShowCursor(true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var mouse = new WinApiMouse();
            mouse.IsVisible = !mouse.IsVisible;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var mouse = new WinApiMouse();
            mouse.PerformClick(WinApiMouseButton.Left, new Point(500, 550));
        }

    }
}
