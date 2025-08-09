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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.memZoneCb = new System.Windows.Forms.ToolStripComboBox();
            this.firmTextBox = new ATmegaSim.UI.ReadOnlyRichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.memZoneCb});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(659, 26);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 23);
            this.toolStripLabel1.Text = "Zone:";
            // 
            // memZoneCb
            // 
            this.memZoneCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.memZoneCb.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.memZoneCb.Items.AddRange(new object[] {
            "CODE",
            "DATA"});
            this.memZoneCb.Name = "memZoneCb";
            this.memZoneCb.Size = new System.Drawing.Size(121, 26);
            this.memZoneCb.SelectedIndexChanged += new System.EventHandler(this.memZoneCb_SelectedIndexChanged);
            // 
            // firmTextBox
            // 
            this.firmTextBox.DetectUrls = false;
            this.firmTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firmTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.firmTextBox.Location = new System.Drawing.Point(0, 26);
            this.firmTextBox.Name = "firmTextBox";
            this.firmTextBox.ReadOnly = true;
            this.firmTextBox.ShortcutsEnabled = false;
            this.firmTextBox.Size = new System.Drawing.Size(659, 520);
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
            this.Controls.Add(this.toolStrip1);
            this.Name = "MemoryView";
            this.ShowIcon = false;
            this.Text = "Memory";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReadOnlyRichTextBox firmTextBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox memZoneCb;
    }
}
