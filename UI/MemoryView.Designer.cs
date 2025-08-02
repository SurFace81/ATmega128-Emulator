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
            this.hexViewer = new ATmegaSim.UI.HexViewer();
            this.SuspendLayout();
            // 
            // hexViewer
            // 
            this.hexViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexViewer.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hexViewer.Location = new System.Drawing.Point(0, 0);
            this.hexViewer.Margin = new System.Windows.Forms.Padding(4);
            this.hexViewer.Name = "hexViewer";
            this.hexViewer.Size = new System.Drawing.Size(659, 546);
            this.hexViewer.TabIndex = 0;
            // 
            // MemoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 546);
            this.Controls.Add(this.hexViewer);
            this.Name = "MemoryView";
            this.Text = "Memory";
            this.ResumeLayout(false);

        }

        #endregion

        private HexViewer hexViewer;
    }
}
