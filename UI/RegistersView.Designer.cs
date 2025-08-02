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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.regsGridView = new System.Windows.Forms.DataGridView();
            this.regNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regValColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.numFormatCb = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.regsGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // regsGridView
            // 
            this.regsGridView.AllowUserToAddRows = false;
            this.regsGridView.AllowUserToDeleteRows = false;
            this.regsGridView.AllowUserToResizeRows = false;
            this.regsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.regsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.regsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.regsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.regNameColumn,
            this.regValColumn});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.regsGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.regsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.regsGridView.Location = new System.Drawing.Point(0, 26);
            this.regsGridView.MultiSelect = false;
            this.regsGridView.Name = "regsGridView";
            this.regsGridView.RowHeadersVisible = false;
            this.regsGridView.RowHeadersWidth = 51;
            this.regsGridView.RowTemplate.Height = 24;
            this.regsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.regsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.regsGridView.Size = new System.Drawing.Size(319, 441);
            this.regsGridView.TabIndex = 0;
            // 
            // regNameColumn
            // 
            this.regNameColumn.DataPropertyName = "Name";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.regNameColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.regNameColumn.HeaderText = "Name";
            this.regNameColumn.MinimumWidth = 6;
            this.regNameColumn.Name = "regNameColumn";
            this.regNameColumn.ReadOnly = true;
            this.regNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // regValColumn
            // 
            this.regValColumn.DataPropertyName = "Value";
            this.regValColumn.HeaderText = "Value";
            this.regValColumn.MinimumWidth = 6;
            this.regValColumn.Name = "regValColumn";
            this.regValColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numFormatCb,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(319, 26);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // numFormatCb
            // 
            this.numFormatCb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.numFormatCb.AutoSize = false;
            this.numFormatCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.numFormatCb.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numFormatCb.Items.AddRange(new object[] {
            "HEX",
            "DEC",
            "OCT",
            "BIN"});
            this.numFormatCb.Name = "numFormatCb";
            this.numFormatCb.Size = new System.Drawing.Size(80, 26);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 23);
            this.toolStripLabel1.Text = "Format:";
            // 
            // RegistersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 467);
            this.Controls.Add(this.regsGridView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RegistersView";
            this.ShowIcon = false;
            this.Text = "Registers";
            ((System.ComponentModel.ISupportInitialize)(this.regsGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView regsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn regNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regValColumn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox numFormatCb;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}
