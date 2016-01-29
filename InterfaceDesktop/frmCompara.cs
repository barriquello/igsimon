using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InterfaceDesktop
{
    public partial class frmCompara : Form
    {
        public static List<RegistroDB> Registros = new List<RegistroDB>();
        public frmCompara()
        {
            InitializeComponent();
        }

        private void tmrRelogio_Tick(object sender, EventArgs e)
        {
            lblRelogio.Text = DateTime.Now.ToString();
            lblStatus.Text = string.Format("Memória utilizada {0}", System.Diagnostics.Process.GetCurrentProcess().PagedMemorySize64);
        }
    }
}
