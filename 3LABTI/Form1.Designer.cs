namespace _3LABTI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox msgForCoding;
        private System.Windows.Forms.Button codingBtn;
        private System.Windows.Forms.Button decodingBtn;
        private System.Windows.Forms.TextBox encodedCodeBox;
        private System.Windows.Forms.TextBox lengthBox;
        private System.Windows.Forms.TextBox decodedMsgBox;
        private System.Windows.Forms.DataGridView probabilityGrid;
        private System.Windows.Forms.DataGridView encodingStepsGrid;
        private System.Windows.Forms.DataGridView decodingStepsGrid;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.msgForCoding = new System.Windows.Forms.TextBox();
            this.codingBtn = new System.Windows.Forms.Button();
            this.decodingBtn = new System.Windows.Forms.Button();
            this.encodedCodeBox = new System.Windows.Forms.TextBox();
            this.lengthBox = new System.Windows.Forms.TextBox();
            this.decodedMsgBox = new System.Windows.Forms.TextBox();
            this.probabilityGrid = new System.Windows.Forms.DataGridView();
            this.encodingStepsGrid = new System.Windows.Forms.DataGridView();
            this.decodingStepsGrid = new System.Windows.Forms.DataGridView();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.clearBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.probabilityGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.encodingStepsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decodingStepsGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // msgForCoding
            // 
            this.msgForCoding.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.msgForCoding.Location = new System.Drawing.Point(15, 45);
            this.msgForCoding.Name = "msgForCoding";
            this.msgForCoding.Size = new System.Drawing.Size(242, 26);
            this.msgForCoding.TabIndex = 0;
            this.msgForCoding.TextChanged += new System.EventHandler(this.msgForCoding_TextChanged);
            // 
            // codingBtn
            // 
            this.codingBtn.BackColor = System.Drawing.Color.White;
            this.codingBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.codingBtn.ForeColor = System.Drawing.Color.Black;
            this.codingBtn.Location = new System.Drawing.Point(15, 85);
            this.codingBtn.Name = "codingBtn";
            this.codingBtn.Size = new System.Drawing.Size(118, 32);
            this.codingBtn.TabIndex = 1;
            this.codingBtn.Text = "Кодировать";
            this.codingBtn.UseVisualStyleBackColor = false;
            this.codingBtn.Click += new System.EventHandler(this.codingBtn_Click);
            // 
            // decodingBtn
            // 
            this.decodingBtn.BackColor = System.Drawing.Color.White;
            this.decodingBtn.Enabled = false;
            this.decodingBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.decodingBtn.ForeColor = System.Drawing.Color.Black;
            this.decodingBtn.Location = new System.Drawing.Point(15, 125);
            this.decodingBtn.Name = "decodingBtn";
            this.decodingBtn.Size = new System.Drawing.Size(273, 40);
            this.decodingBtn.TabIndex = 2;
            this.decodingBtn.Text = "Декодировать";
            this.decodingBtn.UseVisualStyleBackColor = false;
            this.decodingBtn.Click += new System.EventHandler(this.decodingBtn_Click);
            // 
            // encodedCodeBox
            // 
            this.encodedCodeBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.encodedCodeBox.Location = new System.Drawing.Point(15, 45);
            this.encodedCodeBox.Name = "encodedCodeBox";
            this.encodedCodeBox.ReadOnly = true;
            this.encodedCodeBox.Size = new System.Drawing.Size(200, 23);
            this.encodedCodeBox.TabIndex = 3;
            // 
            // lengthBox
            // 
            this.lengthBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lengthBox.Location = new System.Drawing.Point(230, 45);
            this.lengthBox.Name = "lengthBox";
            this.lengthBox.Size = new System.Drawing.Size(58, 23);
            this.lengthBox.TabIndex = 4;
            // 
            // decodedMsgBox
            // 
            this.decodedMsgBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.decodedMsgBox.Location = new System.Drawing.Point(15, 90);
            this.decodedMsgBox.Name = "decodedMsgBox";
            this.decodedMsgBox.ReadOnly = true;
            this.decodedMsgBox.Size = new System.Drawing.Size(273, 26);
            this.decodedMsgBox.TabIndex = 5;
            // 
            // probabilityGrid
            // 
            this.probabilityGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.probabilityGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.probabilityGrid.Location = new System.Drawing.Point(3, 3);
            this.probabilityGrid.Name = "probabilityGrid";
            this.probabilityGrid.ReadOnly = true;
            this.probabilityGrid.Size = new System.Drawing.Size(622, 203);
            this.probabilityGrid.TabIndex = 6;
            // 
            // encodingStepsGrid
            // 
            this.encodingStepsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.encodingStepsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.encodingStepsGrid.Location = new System.Drawing.Point(3, 3);
            this.encodingStepsGrid.Name = "encodingStepsGrid";
            this.encodingStepsGrid.ReadOnly = true;
            this.encodingStepsGrid.Size = new System.Drawing.Size(622, 232);
            this.encodingStepsGrid.TabIndex = 7;
            // 
            // decodingStepsGrid
            // 
            this.decodingStepsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.decodingStepsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.decodingStepsGrid.Location = new System.Drawing.Point(3, 3);
            this.decodingStepsGrid.Name = "decodingStepsGrid";
            this.decodingStepsGrid.ReadOnly = true;
            this.decodingStepsGrid.Size = new System.Drawing.Size(622, 232);
            this.decodingStepsGrid.TabIndex = 8;
            // 
            // outputBox
            // 
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outputBox.Location = new System.Drawing.Point(314, 25);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(632, 165);
            this.outputBox.TabIndex = 9;
            this.outputBox.Text = "";
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = System.Drawing.Color.White;
            this.clearBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearBtn.ForeColor = System.Drawing.Color.Black;
            this.clearBtn.Location = new System.Drawing.Point(139, 85);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(118, 32);
            this.clearBtn.TabIndex = 10;
            this.clearBtn.Text = "Очистить";
            this.clearBtn.UseVisualStyleBackColor = false;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Сообщение для кодирования:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Закодированный код:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(227, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Длина:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Декодированное сообщение:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(315, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "Детальный результат:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(315, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(241, 18);
            this.label6.TabIndex = 16;
            this.label6.Text = "Таблицы шагов кодирования:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.msgForCoding);
            this.groupBox1.Controls.Add(this.codingBtn);
            this.groupBox1.Controls.Add(this.clearBtn);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 145);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "КОДИРОВАНИЕ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.encodedCodeBox);
            this.groupBox2.Controls.Add(this.lengthBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.decodedMsgBox);
            this.groupBox2.Controls.Add(this.decodingBtn);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(12, 271);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 180);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ДЕКОДИРОВАНИЕ";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(314, 221);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(636, 237);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.probabilityGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(628, 209);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Вероятности";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.encodingStepsGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(628, 238);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Шаги кодирования";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.decodingStepsGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(628, 238);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Шаги декодирования";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 477);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.outputBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Арифметическое кодирование - Лабораторная работа №3";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.probabilityGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.encodingStepsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decodingStepsGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}