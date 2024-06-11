using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DBConTest1
{
    public partial class panelForm : Form
    {
        public panelForm()
        {
            InitializeComponent();
            LoadDefaultForm();
        }

        private void LoadDefaultForm()
        {
            loadform(new Form2());
        }
        public void loadform(object Form)
        {
            if (this.panelmain.Controls.Count > 0)
                this.panelmain.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelmain.Controls.Add(f);
            this.panelmain.Tag = f;
            f.Show();
        }

        public void NavigateToDashboardForm()
        {
            Form8 dashboardForm = new Form8(this);
            loadform(dashboardForm);
        }

        private void button1appo_Click(object sender, EventArgs e)
        {
            loadform(new Form2());
        }

        private void button2petient_Click(object sender, EventArgs e)
        {
            loadform(new Form3());
        }

        private void button3docto_Click(object sender, EventArgs e)
        {
            loadform(new Form4());
        }

        private void button4dep_Click(object sender, EventArgs e)
        {
            loadform(new Form5());
        }

        private void button5serv_Click(object sender, EventArgs e)
        {
            loadform(new Form7());
        }

        private void button6pay_Click(object sender, EventArgs e)
        {
            loadform(new Form6());
        }

        private void button7dash_Click(object sender, EventArgs e)
        {
            Form8 dashboardForm = new Form8(this);
            loadform(dashboardForm);
        }

        private void button8x_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button9m_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void button10min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 8);
            }
        }
    }
}
