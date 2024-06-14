using LTHDOtNetCore.Shared;
using LTHDOtNetCore.WindowFormApp.Models;
using LTHDOtNetCore.WindowFormApp.Queries;

namespace LTHDOtNetCore.WindowFormApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;
        private readonly int _blogId;

        public FrmBlog()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        public FrmBlog(int id)
        {
            InitializeComponent();
            _blogId = id;
            _dapperService = new DapperService(ConnectionString.sqlConnectionStringBuilder.ConnectionString);

            var blog = _dapperService.QueryFirstOrDefault<BlogModel>(BlogQuery.GetBlogById,
                new { id = _blogId });

            TitleInput.Text = blog.Title;
            AuthorInput.Text = blog.Author;
            ContentInput.Text = blog.BlogContent;

            BlogSubmitBtn.Visible = false;
            btnUpdate.Visible = true;
        }

        private void BlogSubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TitleInput.Text.Trim()) ||
                   string.IsNullOrEmpty(AuthorInput.Text.Trim()) ||
                   string.IsNullOrEmpty(ContentInput.Text.Trim()))
                {
                    MessageBox.Show("All field require!");
                    return;
                }


                BlogModel blog = new()
                {
                    Title = TitleInput.Text.Trim(),
                    Author = AuthorInput.Text.Trim(),
                    BlogContent = ContentInput.Text.Trim()
                };
                int result = _dapperService.Execute(BlogQuery.BlogCreate, blog);
                string message = result > 0 ? "Successfully Saved." : "Save Failed!";
                var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, messageBoxIcon);
                if (result > 0)
                    ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            TitleInput.Clear();
            AuthorInput.Clear();
            ContentInput.Clear();

            TitleInput.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var blog = new BlogModel
                {
                    Id = _blogId,
                    Title = TitleInput.Text.Trim(),
                    Author = AuthorInput.Text.Trim(),
                    BlogContent = ContentInput.Text.Trim(),
                };

                int result = _dapperService.Execute(BlogQuery.UpdateBlog, blog);
                string message = result > 0 ? "Successfully Updated" : "Update Failed.";
                MessageBox.Show(message);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
