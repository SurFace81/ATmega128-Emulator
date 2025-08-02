namespace ATmegaSim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.firmPathText = new System.Windows.Forms.TextBox();
            this.loadFirmBtn = new System.Windows.Forms.Button();
            this.openFirmDlg = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.firmViewer = new ATmegaSim.UI.HexViewer();
            this.runBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // firmPathText
            // 
            this.firmPathText.Location = new System.Drawing.Point(45, 26);
            this.firmPathText.Name = "firmPathText";
            this.firmPathText.Size = new System.Drawing.Size(301, 22);
            this.firmPathText.TabIndex = 0;
            // 
            // loadFirmBtn
            // 
            this.loadFirmBtn.Location = new System.Drawing.Point(363, 26);
            this.loadFirmBtn.Name = "loadFirmBtn";
            this.loadFirmBtn.Size = new System.Drawing.Size(75, 23);
            this.loadFirmBtn.TabIndex = 1;
            this.loadFirmBtn.Text = "Firm";
            this.loadFirmBtn.UseVisualStyleBackColor = true;
            this.loadFirmBtn.Click += new System.EventHandler(this.loadFirmBtn_Click);
            // 
            // openFirmDlg
            // 
            this.openFirmDlg.Filter = "HEX files|*.hex|TXT files|*.txt";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(459, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "PC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // firmViewer
            // 
            this.firmViewer.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firmViewer.Location = new System.Drawing.Point(0, 143);
            this.firmViewer.Margin = new System.Windows.Forms.Padding(4);
            this.firmViewer.Name = "firmViewer";
            this.firmViewer.Size = new System.Drawing.Size(811, 407);
            this.firmViewer.TabIndex = 2;
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(363, 72);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(75, 23);
            this.runBtn.TabIndex = 4;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(459, 72);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 5;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 550);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.firmViewer);
            this.Controls.Add(this.loadFirmBtn);
            this.Controls.Add(this.firmPathText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox firmPathText;
        private System.Windows.Forms.Button loadFirmBtn;
        private System.Windows.Forms.OpenFileDialog openFirmDlg;
        private UI.HexViewer firmViewer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Button stopBtn;
    }
}

