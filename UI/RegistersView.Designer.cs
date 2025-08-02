namespace ATmegaSim.UI
{
    partial class RegistersView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.regsGrivView = new System.Windows.Forms.DataGridView();
            this.regNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regValColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.regsGrivView)).BeginInit();
            this.SuspendLayout();
            // 
            // regsGrivView
            // 
            this.regsGrivView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.regsGrivView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.regsGrivView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.regsGrivView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regsGrivView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.regNameColumn,
            this.regValColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.regsGrivView.DefaultCellStyle = dataGridViewCellStyle2;
            this.regsGrivView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regsGrivView.Location = new System.Drawing.Point(0, 0);
            this.regsGrivView.MultiSelect = false;
            this.regsGrivView.Name = "regsGrivView";
            this.regsGrivView.RowHeadersVisible = false;
            this.regsGrivView.RowHeadersWidth = 51;
            this.regsGrivView.RowTemplate.Height = 24;
            this.regsGrivView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.regsGrivView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.regsGrivView.Size = new System.Drawing.Size(319, 467);
            this.regsGrivView.TabIndex = 0;
            // 
            // regNameColumn
            // 
            this.regNameColumn.HeaderText = "Name";
            this.regNameColumn.MinimumWidth = 6;
            this.regNameColumn.Name = "regNameColumn";
            // 
            // regValColumn
            // 
            this.regValColumn.HeaderText = "Value";
            this.regValColumn.MinimumWidth = 6;
            this.regValColumn.Name = "regValColumn";
            // 
            // RegistersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 467);
            this.Controls.Add(this.regsGrivView);
            this.Name = "RegistersView";
            this.Text = "Registers";
            ((System.ComponentModel.ISupportInitialize)(this.regsGrivView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView regsGrivView;
        private System.Windows.Forms.DataGridViewTextBoxColumn regNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regValColumn;
    }
}
