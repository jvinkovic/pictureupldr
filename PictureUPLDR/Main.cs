using System;
using System.Windows.Forms;

namespace PictureUPLDR
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var c in MdiChildren)
            {
                if (c is Upload)
                {
                    c.BringToFront();
                    return;
                }
            }

            var upl = new Upload();
            upl.MdiParent = this;
            upl.Show();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var c in MdiChildren)
            {
                if (c is View)
                {
                    c.BringToFront();
                    return;
                }
            }

            var v = new View();
            v.MdiParent = this;
            v.Show();
        }
    }
}