using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBConTest1
{
    public partial class Form8 : Form
    {
        private panelForm mainForm;
        public Form8(panelForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button1pay_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9(mainForm);
            mainForm.loadform(form9);
        }

        private void button2docpay_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10(mainForm);
            mainForm.loadform(form10);
        }

        private void button3test_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11(mainForm);
            mainForm.loadform(form11);
        }

        private void button4test_Click(object sender, EventArgs e)
        {
            Form12 form12 = new Form12(mainForm);
            mainForm.loadform(form12);
        }
    }
}
