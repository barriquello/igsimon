using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CriarRegistrosLocalhost
{
    public partial class UserControl1 : UserControl
    {
        public void text(string valor)
        {
            textBox1.Text = valor;
        }
        public string text()
        {
            return textBox1.Text;
        }
        public void caption(string valor)
        {
            label1.Text = valor;
        }
        public string caption()
        {
            return label1.Text;
        }

        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            for (int nn = 99; nn >=0; nn--)
                textBox1.Items.Add(nn);
        }
    }
}
