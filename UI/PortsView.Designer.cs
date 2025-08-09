namespace ATmegaSim.UI
{
    partial class PortsView
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
            this.pinsTable = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.portGControl = new ATmegaSim.UI.PortControl();
            this.portFControl = new ATmegaSim.UI.PortControl();
            this.portEControl = new ATmegaSim.UI.PortControl();
            this.portDControl = new ATmegaSim.UI.PortControl();
            this.portCControl = new ATmegaSim.UI.PortControl();
            this.portBControl = new ATmegaSim.UI.PortControl();
            this.portAControl = new ATmegaSim.UI.PortControl();
            this.pinsTable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pinsTable
            // 
            this.pinsTable.ColumnCount = 1;
            this.pinsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pinsTable.Controls.Add(this.portGControl, 0, 7);
            this.pinsTable.Controls.Add(this.portFControl, 0, 6);
            this.pinsTable.Controls.Add(this.portEControl, 0, 5);
            this.pinsTable.Controls.Add(this.portDControl, 0, 4);
            this.pinsTable.Controls.Add(this.portCControl, 0, 3);
            this.pinsTable.Controls.Add(this.portBControl, 0, 2);
            this.pinsTable.Controls.Add(this.portAControl, 0, 1);
            this.pinsTable.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.pinsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pinsTable.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pinsTable.Location = new System.Drawing.Point(0, 0);
            this.pinsTable.Name = "pinsTable";
            this.pinsTable.RowCount = 8;
            this.pinsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.pinsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.pinsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.pinsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.pinsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.pinsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.pinsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.pinsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.pinsTable.Size = new System.Drawing.Size(570, 294);
            this.pinsTable.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.label8, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 8, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 30);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(451, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 30);
            this.label8.TabIndex = 7;
            this.label8.Text = "1";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(395, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 30);
            this.label7.TabIndex = 6;
            this.label7.Text = "2";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(339, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 30);
            this.label6.TabIndex = 5;
            this.label6.Text = "3";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(283, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 30);
            this.label5.TabIndex = 4;
            this.label5.Text = "4";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(227, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 30);
            this.label4.TabIndex = 3;
            this.label4.Text = "5";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(171, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "6";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(115, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "7";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(507, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // portGControl
            // 
            this.portGControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portGControl.Location = new System.Drawing.Point(3, 255);
            this.portGControl.Name = "portGControl";
            this.portGControl.PinCount = 5;
            this.portGControl.PortName = "PortG";
            this.portGControl.Size = new System.Drawing.Size(564, 36);
            this.portGControl.TabIndex = 6;
            // 
            // portFControl
            // 
            this.portFControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portFControl.Location = new System.Drawing.Point(3, 219);
            this.portFControl.Name = "portFControl";
            this.portFControl.PinCount = 8;
            this.portFControl.PortName = "PortF";
            this.portFControl.Size = new System.Drawing.Size(564, 30);
            this.portFControl.TabIndex = 5;
            // 
            // portEControl
            // 
            this.portEControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portEControl.Location = new System.Drawing.Point(3, 183);
            this.portEControl.Name = "portEControl";
            this.portEControl.PinCount = 8;
            this.portEControl.PortName = "PortE";
            this.portEControl.Size = new System.Drawing.Size(564, 30);
            this.portEControl.TabIndex = 4;
            // 
            // portDControl
            // 
            this.portDControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portDControl.Location = new System.Drawing.Point(3, 147);
            this.portDControl.Name = "portDControl";
            this.portDControl.PinCount = 8;
            this.portDControl.PortName = "PortD";
            this.portDControl.Size = new System.Drawing.Size(564, 30);
            this.portDControl.TabIndex = 3;
            // 
            // portCControl
            // 
            this.portCControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portCControl.Location = new System.Drawing.Point(3, 111);
            this.portCControl.Name = "portCControl";
            this.portCControl.PinCount = 8;
            this.portCControl.PortName = "PortC";
            this.portCControl.Size = new System.Drawing.Size(564, 30);
            this.portCControl.TabIndex = 2;
            // 
            // portBControl
            // 
            this.portBControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portBControl.Location = new System.Drawing.Point(3, 75);
            this.portBControl.Name = "portBControl";
            this.portBControl.PinCount = 8;
            this.portBControl.PortName = "PortB";
            this.portBControl.Size = new System.Drawing.Size(564, 30);
            this.portBControl.TabIndex = 1;
            // 
            // portAControl
            // 
            this.portAControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portAControl.Location = new System.Drawing.Point(3, 39);
            this.portAControl.Name = "portAControl";
            this.portAControl.PinCount = 8;
            this.portAControl.PortName = "PortA";
            this.portAControl.Size = new System.Drawing.Size(564, 30);
            this.portAControl.TabIndex = 0;
            // 
            // PortsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 294);
            this.Controls.Add(this.pinsTable);
            this.Name = "PortsView";
            this.ShowIcon = false;
            this.Text = "Ports";
            this.pinsTable.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PortControl portAControl;
        private System.Windows.Forms.TableLayoutPanel pinsTable;
        private PortControl portGControl;
        private PortControl portFControl;
        private PortControl portEControl;
        private PortControl portDControl;
        private PortControl portCControl;
        private PortControl portBControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
