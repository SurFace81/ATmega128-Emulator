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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pinaLbl = new System.Windows.Forms.Label();
            this.ddraLbl = new System.Windows.Forms.Label();
            this.portaLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "PINA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "DDRA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "PORTA";
            // 
            // pinaLbl
            // 
            this.pinaLbl.Location = new System.Drawing.Point(303, 81);
            this.pinaLbl.Name = "pinaLbl";
            this.pinaLbl.Size = new System.Drawing.Size(100, 23);
            this.pinaLbl.TabIndex = 3;
            // 
            // ddraLbl
            // 
            this.ddraLbl.Location = new System.Drawing.Point(303, 113);
            this.ddraLbl.Name = "ddraLbl";
            this.ddraLbl.Size = new System.Drawing.Size(100, 23);
            this.ddraLbl.TabIndex = 4;
            // 
            // portaLbl
            // 
            this.portaLbl.Location = new System.Drawing.Point(303, 153);
            this.portaLbl.Name = "portaLbl";
            this.portaLbl.Size = new System.Drawing.Size(100, 23);
            this.portaLbl.TabIndex = 5;
            // 
            // PortsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 418);
            this.Controls.Add(this.portaLbl);
            this.Controls.Add(this.ddraLbl);
            this.Controls.Add(this.pinaLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PortsView";
            this.ShowIcon = false;
            this.Text = "Ports";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pinaLbl;
        private System.Windows.Forms.Label ddraLbl;
        private System.Windows.Forms.Label portaLbl;
    }
}
