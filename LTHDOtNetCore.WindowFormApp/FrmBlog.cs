using LTHDOtNetCore.Shared;
using LTHDOtNetCore.WindowFormApp.Models;
using LTHDOtNetCore.WindowFormApp.Queries;

namespace LTHDOtNetCore.WindowFormApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;

        public FrmBlog()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        private void BlogSubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(TitleInput.Text.Trim()) ||
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
    }
}
