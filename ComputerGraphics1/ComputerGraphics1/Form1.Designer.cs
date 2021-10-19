
namespace ComputerGraphics1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.interactivePictureBox = new System.Windows.Forms.PictureBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.lineTypeBox = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.interactivePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(904, 503);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // interactivePictureBox
            // 
            this.interactivePictureBox.Location = new System.Drawing.Point(12, 563);
            this.interactivePictureBox.Name = "interactivePictureBox";
            this.interactivePictureBox.Size = new System.Drawing.Size(904, 350);
            this.interactivePictureBox.TabIndex = 1;
            this.interactivePictureBox.TabStop = false;
            this.interactivePictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.interactivePictureBox_MouseClick);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(665, 919);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(251, 38);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // lineTypeBox
            // 
            this.lineTypeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lineTypeBox.FormattingEnabled = true;
            this.lineTypeBox.ItemHeight = 24;
            this.lineTypeBox.Items.AddRange(new object[] {
            "BresenhamLine",
            "DDALine"});
            this.lineTypeBox.Location = new System.Drawing.Point(12, 929);
            this.lineTypeBox.Name = "lineTypeBox";
            this.lineTypeBox.Size = new System.Drawing.Size(172, 28);
            this.lineTypeBox.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(353, 934);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 20);
            this.textBox1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 969);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lineTypeBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.interactivePictureBox);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.interactivePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.PictureBox interactivePictureBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ListBox lineTypeBox;
        private System.Windows.Forms.TextBox textBox1;
    }
}

