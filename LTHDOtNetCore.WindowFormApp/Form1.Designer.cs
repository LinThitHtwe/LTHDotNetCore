namespace LTHDOtNetCore.WindowFormApp
{
    partial class Form1
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
            clickMe1 = new Button();
            SuspendLayout();
            // 
            // clickMe1
            // 
            clickMe1.Location = new Point(390, 170);
            clickMe1.Name = "clickMe1";
            clickMe1.Size = new Size(75, 23);
            clickMe1.TabIndex = 0;
            clickMe1.Text = "Click Me";
            clickMe1.UseVisualStyleBackColor = true;
            clickMe1.Click += clickMe1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(clickMe1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button clickMe1;
    }
}
