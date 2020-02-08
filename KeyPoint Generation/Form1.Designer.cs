namespace KeyPoint_Generation
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.TitleBox = new System.Windows.Forms.TextBox();
            this.RunButton = new System.Windows.Forms.Button();
            this.OutputSelectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(1, 28);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(96, 36);
            this.SelectFileButton.TabIndex = 0;
            this.SelectFileButton.Text = "Select File";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // TitleBox
            // 
            this.TitleBox.Location = new System.Drawing.Point(1, 2);
            this.TitleBox.Name = "TitleBox";
            this.TitleBox.Size = new System.Drawing.Size(197, 20);
            this.TitleBox.TabIndex = 1;
            this.TitleBox.Text = "Select a .csv file to generate from.";
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(1, 70);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(197, 35);
            this.RunButton.TabIndex = 2;
            this.RunButton.Text = "Generate Points";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // OutputSelectButton
            // 
            this.OutputSelectButton.Location = new System.Drawing.Point(103, 30);
            this.OutputSelectButton.Name = "OutputSelectButton";
            this.OutputSelectButton.Size = new System.Drawing.Size(95, 34);
            this.OutputSelectButton.TabIndex = 3;
            this.OutputSelectButton.Text = "Select Output";
            this.OutputSelectButton.UseVisualStyleBackColor = true;
            this.OutputSelectButton.Click += new System.EventHandler(this.OutputSelectButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OutputSelectButton);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.TitleBox);
            this.Controls.Add(this.SelectFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.TextBox TitleBox;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button OutputSelectButton;
    }
}

