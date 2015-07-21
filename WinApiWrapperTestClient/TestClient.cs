using System;
using System.Linq;
using System.Windows.Forms;
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

    }
}
