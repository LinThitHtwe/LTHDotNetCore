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

        private void dgvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            var blogId = Convert.ToInt32(dgvTable.Rows[e.RowIndex].Cells["colId"].Value);

            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {

            }

            if (e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                var dialogResult = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes) return;

                DeleteBlog(blogId);
            }
        }

        private void DeleteBlog(int id)
        {
            int result = _dapperService.Execute(BlogQuery.DeleteBlog, new { id });
            string message = result > 0 ? "Successfully Deleted" : "Delete Fail.";
            MessageBox.Show(message);
            GetBlogList();
        }
    }
}