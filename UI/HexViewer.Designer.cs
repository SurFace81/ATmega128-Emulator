namespace ATmegaSim.UI
{
    partial class HexViewer
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.firmTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // firmTextBox
            // 
            this.firmTextBox.DetectUrls = false;
            this.firmTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firmTextBox.Location = new System.Drawing.Point(0, 0);
            this.firmTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.firmTextBox.Name = "firmTextBox";
            this.firmTextBox.ReadOnly = true;
            this.firmTextBox.Size = new System.Drawing.Size(442, 409);
            this.firmTextBox.TabIndex = 0;
            this.firmTextBox.Text = "";
            // 
            // HexViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.firmTextBox);
            this.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HexViewer";
            this.Size = new System.Drawing.Size(442, 409);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox firmTextBox;
    }
}
