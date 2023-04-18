using System.ComponentModel;

namespace MuOp2023
{
    partial class SelectPractice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.practice_01_02 = new System.Windows.Forms.Button();
            this.practice_03_04 = new System.Windows.Forms.Button();
            this.practice_05_06 = new System.Windows.Forms.Button();
            this.practice_07_08 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // practice_01_02
            // 
            this.practice_01_02.Location = new System.Drawing.Point(28, 28);
            this.practice_01_02.Name = "practice_01_02";
            this.practice_01_02.Size = new System.Drawing.Size(147, 31);
            this.practice_01_02.TabIndex = 0;
            this.practice_01_02.Text = "Практика 01-02";
            this.practice_01_02.UseVisualStyleBackColor = true;
            this.practice_01_02.Click += new System.EventHandler(this.practice_01_02_Click);
            // 
            // practice_03_04
            // 
            this.practice_03_04.Location = new System.Drawing.Point(28, 65);
            this.practice_03_04.Name = "practice_03_04";
            this.practice_03_04.Size = new System.Drawing.Size(147, 31);
            this.practice_03_04.TabIndex = 1;
            this.practice_03_04.Text = "Практика 03-04";
            this.practice_03_04.UseVisualStyleBackColor = true;
            this.practice_03_04.Click += new System.EventHandler(this.practice_03_04_Click);
            // 
            // practice_05_06
            // 
            this.practice_05_06.Location = new System.Drawing.Point(28, 102);
            this.practice_05_06.Name = "practice_05_06";
            this.practice_05_06.Size = new System.Drawing.Size(147, 31);
            this.practice_05_06.TabIndex = 2;
            this.practice_05_06.Text = "Практика 05-06";
            this.practice_05_06.UseVisualStyleBackColor = true;
            this.practice_05_06.Click += new System.EventHandler(this.practice_05_06_Click);
            // 
            // practice_07_08
            // 
            this.practice_07_08.Location = new System.Drawing.Point(28, 139);
            this.practice_07_08.Name = "practice_07_08";
            this.practice_07_08.Size = new System.Drawing.Size(147, 31);
            this.practice_07_08.TabIndex = 3;
            this.practice_07_08.Text = "Практика 07-08";
            this.practice_07_08.UseVisualStyleBackColor = true;
            this.practice_07_08.Click += new System.EventHandler(this.practice_07_08_Click);
            // 
            // SelectPractice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.practice_01_02);
            this.Controls.Add(this.practice_03_04);
            this.Controls.Add(this.practice_05_06);
            this.Controls.Add(this.practice_07_08);
            this.Name = "SelectPractice";
            this.Text = "Выберите практику";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button practice_01_02;
        private System.Windows.Forms.Button practice_03_04;
        private System.Windows.Forms.Button practice_05_06;
        private System.Windows.Forms.Button practice_07_08;

        #endregion
    }
}