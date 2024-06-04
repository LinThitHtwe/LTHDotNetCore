using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTHDOtNetCore.WindowFormApp
{
    public partial class FrmMainMenu : Form
    {
        public FrmMainMenu()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmBlog frmBlog = new();
            frmBlog.ShowDialog();
        }

        private void blogsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlogsList frmBlogsList = new();
            frmBlogsList.ShowDialog();
        }
    }
}
