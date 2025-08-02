namespace ATmegaSim.UI
{
    partial class MemoryView
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
            this.firmTextBox = new ATmegaSim.UI.ReadOnlyRichTextBox();
            this.SuspendLayout();
            // 
            // firmTextBox
            // 
            this.firmTextBox.DetectUrls = false;
            this.firmTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firmTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.firmTextBox.Location = new System.Drawing.Point(0, 0);
            this.firmTextBox.Name = "firmTextBox";
            this.firmTextBox.ReadOnly = true;
            this.firmTextBox.ShortcutsEnabled = false;
            this.firmTextBox.Size = new System.Drawing.Size(659, 546);
            this.firmTextBox.TabIndex = 1;
            this.firmTextBox.TabStop = false;
            this.firmTextBox.Text = "";
            this.firmTextBox.WordWrap = false;
            // 
            // MemoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 546);
            this.Controls.Add(this.firmTextBox);
            this.Name = "MemoryView";
            this.ShowIcon = false;
            this.Text = "Memory";
            this.ResumeLayout(false);

        }

        #endregion

        private ReadOnlyRichTextBox firmTextBox;
    }
}
