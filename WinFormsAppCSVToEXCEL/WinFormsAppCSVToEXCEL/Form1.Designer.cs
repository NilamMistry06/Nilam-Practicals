namespace WinFormsAppCSVToEXCEL
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
            label1 = new Label();
            txtCSVFilePath = new TextBox();
            label2 = new Label();
            txtExcelFilePath = new TextBox();
            button1 = new Button();
            lblSuccessMsg = new Label();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 32);
            label1.Name = "label1";
            label1.Size = new Size(212, 20);
            label1.TabIndex = 0;
            label1.Text = "Enter CSV files directory path : ";
            // 
            // txtCSVFilePath
            // 
            txtCSVFilePath.Location = new Point(255, 27);
            txtCSVFilePath.Name = "txtCSVFilePath";
            txtCSVFilePath.Size = new Size(496, 27);
            txtCSVFilePath.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(39, 97);
            label2.Name = "label2";
            label2.Size = new Size(147, 20);
            label2.TabIndex = 2;
            label2.Text = "Enter Excel file path :";
            // 
            // txtExcelFilePath
            // 
            txtExcelFilePath.Location = new Point(203, 94);
            txtExcelFilePath.Name = "txtExcelFilePath";
            txtExcelFilePath.Size = new Size(496, 27);
            txtExcelFilePath.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(203, 157);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 4;
            button1.Text = "Export";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblSuccessMsg
            // 
            lblSuccessMsg.AutoSize = true;
            lblSuccessMsg.Location = new Point(203, 215);
            lblSuccessMsg.Name = "lblSuccessMsg";
            lblSuccessMsg.Size = new Size(176, 20);
            lblSuccessMsg.TabIndex = 5;
            lblSuccessMsg.Text = "File exported successfully";
            // 
            // button2
            // 
            button2.Location = new Point(316, 158);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 6;
            button2.Text = "Clear";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(lblSuccessMsg);
            Controls.Add(button1);
            Controls.Add(txtExcelFilePath);
            Controls.Add(label2);
            Controls.Add(txtCSVFilePath);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtCSVFilePath;
        private Label label2;
        private TextBox txtExcelFilePath;
        private Button button1;
        private Label lblSuccessMsg;
        private Button button2;
    }
}