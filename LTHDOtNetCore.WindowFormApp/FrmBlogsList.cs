using LTHDOtNetCore.Shared;
using LTHDOtNetCore.WindowFormApp.Models;
using LTHDOtNetCore.WindowFormApp.Queries;
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
    public partial class FrmBlogsList : Form
    {
        private readonly DapperService _dapperService;
        public FrmBlogsList()
        {
            InitializeComponent();
            dgvTable.AutoGenerateColumns = false;
            _dapperService = new DapperService(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        private void FrmBlogsList_Load(object sender, EventArgs e)
        {
            GetBlogList();
        }
        private void GetBlogList()
        {
            List<BlogModel> blogs = _dapperService.Query<BlogModel>(BlogQuery.BlogList);
            dgvTable.DataSource = blogs;
        }

        
    }
}
