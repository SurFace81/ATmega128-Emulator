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
            this.openFirmDlg = new System.Windows.Forms.OpenFileDialog();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.registersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disasmMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runBtn = new System.Windows.Forms.ToolStripButton();
            this.stopBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.firmPathText = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFirmDlg
            // 
            this.openFirmDlg.Filter = "HEX files|*.hex|TXT files|*.txt";
            // 
            // dockPanel
            // 
            this.dockPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 31);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1134, 637);
            this.dockPanel.TabIndex = 7;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileMenuItem,
            this.closeMenuItem,
            this.exitMenuItem});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(54, 28);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.AutoSize = false;
            this.openFileMenuItem.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.ShortcutKeyDisplayString = "";
            this.openFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileMenuItem.Size = new System.Drawing.Size(224, 20);
            this.openFileMenuItem.Text = "Open";
            this.openFileMenuItem.Click += new System.EventHandler(this.openFileMenuItem_Click);
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.closeMenuItem.Size = new System.Drawing.Size(180, 26);
            this.closeMenuItem.Text = "Close";
            this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(180, 26);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registersMenuItem,
            this.memoryMenuItem,
            this.disasmMenuItem});
            this.toolStripDropDownButton2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(54, 28);
            this.toolStripDropDownButton2.Text = "View";
            // 
            // registersMenuItem
            // 
            this.registersMenuItem.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.registersMenuItem.Name = "registersMenuItem";
            this.registersMenuItem.Size = new System.Drawing.Size(166, 26);
            this.registersMenuItem.Text = "Registers";
            this.registersMenuItem.Click += new System.EventHandler(this.registersMenuItem_Click);
            // 
            // memoryMenuItem
            // 
            this.memoryMenuItem.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.memoryMenuItem.Name = "memoryMenuItem";
            this.memoryMenuItem.Size = new System.Drawing.Size(166, 26);
            this.memoryMenuItem.Text = "Memory";
            this.memoryMenuItem.Click += new System.EventHandler(this.memoryMenuItem_Click);
            // 
            // disasmMenuItem
            // 
            this.disasmMenuItem.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.disasmMenuItem.Name = "disasmMenuItem";
            this.disasmMenuItem.Size = new System.Drawing.Size(166, 26);
            this.disasmMenuItem.Text = "Disassembly";
            this.disasmMenuItem.Click += new System.EventHandler(this.disasmMenuItem_Click);
            // 
            // runBtn
            // 
            this.runBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runBtn.Image = ((System.Drawing.Image)(resources.GetObject("runBtn.Image")));
            this.runBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(29, 28);
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopBtn.Enabled = false;
            this.stopBtn.Image = ((System.Drawing.Image)(resources.GetObject("stopBtn.Image")));
            this.stopBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(29, 28);
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.runBtn,
            this.stopBtn,
            this.firmPathText});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1134, 31);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "mainToolStrip";
            // 
            // firmPathText
            // 
            this.firmPathText.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.firmPathText.AutoSize = false;
            this.firmPathText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firmPathText.Name = "firmPathText";
            this.firmPathText.Size = new System.Drawing.Size(500, 28);
            this.firmPathText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 668);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ATmega128";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFirmDlg;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem registersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disasmMenuItem;
        private System.Windows.Forms.ToolStripButton runBtn;
        private System.Windows.Forms.ToolStripButton stopBtn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel firmPathText;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
    }
}

