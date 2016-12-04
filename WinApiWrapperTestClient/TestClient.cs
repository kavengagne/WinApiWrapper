using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinApiWrapper.Enums;
using WinApiWrapper.Native.Enums;
using WinApiWrapper.Native.Methods;
using WinApiWrapper.Native.Structs;
using WinApiWrapper.Wrappers;

namespace WinApiWrapperTestClient
{
    public partial class TestClient : Form
    {
        private bool _clipped;

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
            timer.Tick += (o, args) =>
            {
                mousePosition.BeginInvoke(new Action(() =>
                {
                    var position = WinApiMouse.Position;
                    var client = WinApiMouse.GetClientPosition(Handle);
                    mousePosition.Text = String.Format("X: {0}, Y: {1}", position.X, position.Y);
                    clientPosition.Text = String.Format("X: {0}, Y: {1}", client.X, client.Y);
                }));
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var windows =
                WinApiWindow.EnumWindows(win => (win.IsDesktopWindow || win.IsToolWindow) && win.Title != null);
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
            WinApiMouse.IsVisible = !WinApiMouse.IsVisible;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //__mouse.UnregisterAllHooks();

            WinApiMouse.PerformClick(MouseButton.Left, new Point(500, 550));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //mouse.RegisterButtonHook(MouseButtonAction.Down, button => Debug.WriteLine("Down " + button));
            //__mouse.RegisterButtonHook(MouseButtonAction.Up, button => Debug.WriteLine("Up " + button));
            //__mouse.RegisterMoveHook(point => Debug.WriteLine("Move: " + point));
            //WinApiMouse.RegisterWheelHook(MouseWheelOrientation.Horizontal, delta => Debug.WriteLine("HWheel: " + delta));
            //mouse.RegisterWheelHook(MouseWheelOrientation.Vertical, i => Debug.WriteLine("VWheel: " + i));

            RECT rect;
            User32.GetClipCursor(out rect);
            clipCursor.Text = rect.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (_clipped)
            {
                _clipped = false;
                RECT clipZone = new RECT();
                User32.ClipCursor(ref clipZone);
            }
            else
            {
                _clipped = true;
                var clipZone = new RECT(Left, Top, Right, Bottom);
                User32.ClipCursor(ref clipZone);
            }
        }
    }
}
