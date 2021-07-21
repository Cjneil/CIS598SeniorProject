
namespace CodioToHugoConverter
{
    partial class FileSelection
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
            this.uxCodioSelectButton = new System.Windows.Forms.Button();
            this.uxHugoTargetButton = new System.Windows.Forms.Button();
            this.CodioSourceLabel = new System.Windows.Forms.Label();
            this.HugoTargetLabel = new System.Windows.Forms.Label();
            this.uxCreateHugoTextbookButton = new System.Windows.Forms.Button();
            this.uxHugoPath = new System.Windows.Forms.TextBox();
            this.uxCodioPath = new System.Windows.Forms.TextBox();
            this.uxResultLabel = new System.Windows.Forms.Label();
            this.uxConversionResultBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // uxCodioSelectButton
            // 
            this.uxCodioSelectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.uxCodioSelectButton.Location = new System.Drawing.Point(27, 31);
            this.uxCodioSelectButton.Name = "uxCodioSelectButton";
            this.uxCodioSelectButton.Size = new System.Drawing.Size(150, 43);
            this.uxCodioSelectButton.TabIndex = 0;
            this.uxCodioSelectButton.Text = "Select Codio Directory";
            this.uxCodioSelectButton.UseVisualStyleBackColor = true;
            this.uxCodioSelectButton.Click += new System.EventHandler(this.CodioSelectButton_Click);
            // 
            // uxHugoTargetButton
            // 
            this.uxHugoTargetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.uxHugoTargetButton.Location = new System.Drawing.Point(27, 123);
            this.uxHugoTargetButton.Name = "uxHugoTargetButton";
            this.uxHugoTargetButton.Size = new System.Drawing.Size(150, 43);
            this.uxHugoTargetButton.TabIndex = 1;
            this.uxHugoTargetButton.Text = "Select Target Hugo Directory";
            this.uxHugoTargetButton.UseVisualStyleBackColor = true;
            this.uxHugoTargetButton.Click += new System.EventHandler(this.HugoTargetButton_Click);
            // 
            // CodioSourceLabel
            // 
            this.CodioSourceLabel.AutoSize = true;
            this.CodioSourceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.CodioSourceLabel.Location = new System.Drawing.Point(183, 41);
            this.CodioSourceLabel.Name = "CodioSourceLabel";
            this.CodioSourceLabel.Size = new System.Drawing.Size(176, 20);
            this.CodioSourceLabel.TabIndex = 4;
            this.CodioSourceLabel.Text = "Selected Codio Source:";
            // 
            // HugoTargetLabel
            // 
            this.HugoTargetLabel.AutoSize = true;
            this.HugoTargetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.HugoTargetLabel.Location = new System.Drawing.Point(190, 133);
            this.HugoTargetLabel.Name = "HugoTargetLabel";
            this.HugoTargetLabel.Size = new System.Drawing.Size(169, 20);
            this.HugoTargetLabel.TabIndex = 5;
            this.HugoTargetLabel.Text = "Selected Hugo Target:";
            // 
            // uxCreateHugoTextbookButton
            // 
            this.uxCreateHugoTextbookButton.Enabled = false;
            this.uxCreateHugoTextbookButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uxCreateHugoTextbookButton.Location = new System.Drawing.Point(676, 255);
            this.uxCreateHugoTextbookButton.Name = "uxCreateHugoTextbookButton";
            this.uxCreateHugoTextbookButton.Size = new System.Drawing.Size(199, 43);
            this.uxCreateHugoTextbookButton.TabIndex = 6;
            this.uxCreateHugoTextbookButton.Text = "Create Hugo Textbook";
            this.uxCreateHugoTextbookButton.UseVisualStyleBackColor = true;
            this.uxCreateHugoTextbookButton.Click += new System.EventHandler(this.uxCreateHugoTextbookButton_Click);
            // 
            // uxHugoPath
            // 
            this.uxHugoPath.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.uxHugoPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uxHugoPath.Location = new System.Drawing.Point(365, 127);
            this.uxHugoPath.Multiline = true;
            this.uxHugoPath.Name = "uxHugoPath";
            this.uxHugoPath.ReadOnly = true;
            this.uxHugoPath.Size = new System.Drawing.Size(517, 61);
            this.uxHugoPath.TabIndex = 3;
            // 
            // uxCodioPath
            // 
            this.uxCodioPath.Cursor = System.Windows.Forms.Cursors.Default;
            this.uxCodioPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uxCodioPath.Location = new System.Drawing.Point(365, 35);
            this.uxCodioPath.Multiline = true;
            this.uxCodioPath.Name = "uxCodioPath";
            this.uxCodioPath.ReadOnly = true;
            this.uxCodioPath.Size = new System.Drawing.Size(517, 59);
            this.uxCodioPath.TabIndex = 2;
            // 
            // uxResultLabel
            // 
            this.uxResultLabel.AutoSize = true;
            this.uxResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxResultLabel.Location = new System.Drawing.Point(23, 202);
            this.uxResultLabel.Name = "uxResultLabel";
            this.uxResultLabel.Size = new System.Drawing.Size(168, 24);
            this.uxResultLabel.TabIndex = 8;
            this.uxResultLabel.Text = "Conversion Result:";
            // 
            // uxConversionResultBox
            // 
            this.uxConversionResultBox.Location = new System.Drawing.Point(27, 229);
            this.uxConversionResultBox.Multiline = true;
            this.uxConversionResultBox.Name = "uxConversionResultBox";
            this.uxConversionResultBox.Size = new System.Drawing.Size(615, 69);
            this.uxConversionResultBox.TabIndex = 9;
            // 
            // FileSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(910, 318);
            this.Controls.Add(this.uxConversionResultBox);
            this.Controls.Add(this.uxResultLabel);
            this.Controls.Add(this.uxCreateHugoTextbookButton);
            this.Controls.Add(this.HugoTargetLabel);
            this.Controls.Add(this.CodioSourceLabel);
            this.Controls.Add(this.uxHugoPath);
            this.Controls.Add(this.uxCodioPath);
            this.Controls.Add(this.uxHugoTargetButton);
            this.Controls.Add(this.uxCodioSelectButton);
            this.Name = "FileSelection";
            this.Text = "FileSelection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxCodioSelectButton;
        private System.Windows.Forms.Button uxHugoTargetButton;
        private System.Windows.Forms.Label CodioSourceLabel;
        private System.Windows.Forms.Label HugoTargetLabel;
        private System.Windows.Forms.Button uxCreateHugoTextbookButton;
        private System.Windows.Forms.TextBox uxHugoPath;
        private System.Windows.Forms.TextBox uxCodioPath;
        private System.Windows.Forms.Label uxResultLabel;
        private System.Windows.Forms.TextBox uxConversionResultBox;
    }
}