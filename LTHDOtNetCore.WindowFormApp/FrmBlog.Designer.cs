namespace LTHDOtNetCore.WindowFormApp
{
    partial class FrmBlog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AuthorInput = new TextBox();
            ContentInput = new TextBox();
            TitleInput = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            BlogSubmitBtn = new Button();
            CancelBtn = new Button();
            SuspendLayout();
            // 
            // AuthorInput
            // 
            AuthorInput.Location = new Point(57, 117);
            AuthorInput.Name = "AuthorInput";
            AuthorInput.Size = new Size(203, 23);
            AuthorInput.TabIndex = 1;
            // 
            // ContentInput
            // 
            ContentInput.Location = new Point(57, 191);
            ContentInput.Multiline = true;
            ContentInput.Name = "ContentInput";
            ContentInput.Size = new Size(203, 121);
            ContentInput.TabIndex = 2;
            // 
            // TitleInput
            // 
            TitleInput.Location = new Point(57, 52);
            TitleInput.Name = "TitleInput";
            TitleInput.Size = new Size(203, 23);
            TitleInput.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(57, 173);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 4;
            label1.Text = "Content";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(57, 99);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 5;
            label2.Text = "Author";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(57, 34);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 6;
            label3.Text = "Title";
            // 
            // BlogSubmitBtn
            // 
            BlogSubmitBtn.FlatStyle = FlatStyle.Flat;
            BlogSubmitBtn.Location = new Point(138, 331);
            BlogSubmitBtn.Name = "BlogSubmitBtn";
            BlogSubmitBtn.Size = new Size(75, 23);
            BlogSubmitBtn.TabIndex = 7;
            BlogSubmitBtn.Text = "&Submit";
            BlogSubmitBtn.UseVisualStyleBackColor = true;
            BlogSubmitBtn.Click += BlogSubmitBtn_Click;
            // 
            // CancelBtn
            // 
            CancelBtn.Location = new Point(57, 331);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 8;
            CancelBtn.Text = "&Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // FrmBlog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(CancelBtn);
            Controls.Add(BlogSubmitBtn);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TitleInput);
            Controls.Add(ContentInput);
            Controls.Add(AuthorInput);
            Name = "FrmBlog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Blog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox AuthorInput;
        private TextBox ContentInput;
        private TextBox TitleInput;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button BlogSubmitBtn;
        private Button CancelBtn;
    }
}
