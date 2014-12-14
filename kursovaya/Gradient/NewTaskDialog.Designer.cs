using System;
using OptLab.Func;

namespace OptLab.Gradient
{
    partial class NewTaskDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.richBox = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtSigma = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCoef = new System.Windows.Forms.TextBox();
            this.funCombo = new System.Windows.Forms.ComboBox();
            this.del = new System.Windows.Forms.Button();
            this.addG = new System.Windows.Forms.Button();
            this.addH = new System.Windows.Forms.Button();
            this.listF = new System.Windows.Forms.ListBox();
            this.richF = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtGammaF = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtGammaZ = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(75, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(132, 20);
            this.txtName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(304, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Безусловный поиск";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 45);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(184, 49);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Гадиентный поиск",
            "Поиск Хука-Дживса"});
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(184, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.richBox);
            this.groupBox2.Location = new System.Drawing.Point(15, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 128);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Целевая функция";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::OptLab.Properties.Resources.tick_4854;
            this.button1.Location = new System.Drawing.Point(254, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(124, 17);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(61, 17);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "(∂/∂y)Q";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(60, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(61, 17);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "(∂/∂x)Q";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(52, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Q(x,y)";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // richBox
            // 
            this.richBox.Location = new System.Drawing.Point(6, 41);
            this.richBox.Name = "richBox";
            this.richBox.Size = new System.Drawing.Size(271, 78);
            this.richBox.TabIndex = 0;
            this.richBox.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "x = ";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(130, 19);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(60, 20);
            this.txtY.TabIndex = 10;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(35, 19);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(60, 20);
            this.txtX.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtY);
            this.groupBox3.Controls.Add(this.txtX);
            this.groupBox3.Location = new System.Drawing.Point(304, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(196, 48);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Начальная точка";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(101, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "y = ";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(425, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(344, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtSigma
            // 
            this.txtSigma.Location = new System.Drawing.Point(247, 6);
            this.txtSigma.Name = "txtSigma";
            this.txtSigma.Size = new System.Drawing.Size(45, 20);
            this.txtSigma.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "σ = ";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtCoef);
            this.groupBox4.Controls.Add(this.funCombo);
            this.groupBox4.Controls.Add(this.del);
            this.groupBox4.Controls.Add(this.addG);
            this.groupBox4.Controls.Add(this.addH);
            this.groupBox4.Controls.Add(this.listF);
            this.groupBox4.Controls.Add(this.richF);
            this.groupBox4.Location = new System.Drawing.Point(15, 166);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(283, 97);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ограничения";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "f⁰ = ";
            // 
            // txtCoef
            // 
            this.txtCoef.Location = new System.Drawing.Point(37, 67);
            this.txtCoef.Name = "txtCoef";
            this.txtCoef.Size = new System.Drawing.Size(46, 20);
            this.txtCoef.TabIndex = 9;
            this.txtCoef.TextChanged += new System.EventHandler(this.txtCoef_TextChanged);
            // 
            // funCombo
            // 
            this.funCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.funCombo.FormattingEnabled = true;
            this.funCombo.Items.AddRange(new object[] {
            "f(x,y)",
            "(∂/∂x)f",
            "(∂/∂y)f"});
            this.funCombo.Location = new System.Drawing.Point(215, 67);
            this.funCombo.Name = "funCombo";
            this.funCombo.Size = new System.Drawing.Size(62, 21);
            this.funCombo.TabIndex = 8;
            this.funCombo.SelectedValueChanged += new System.EventHandler(this.funCombo_SelectedValueChanged);
            // 
            // del
            // 
            this.del.FlatAppearance.BorderSize = 0;
            this.del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.del.Image = global::OptLab.Properties.Resources.delete_7856;
            this.del.Location = new System.Drawing.Point(147, 65);
            this.del.Name = "del";
            this.del.Size = new System.Drawing.Size(23, 23);
            this.del.TabIndex = 7;
            this.del.UseVisualStyleBackColor = true;
            this.del.Click += new System.EventHandler(this.del_Click);
            // 
            // addG
            // 
            this.addG.FlatAppearance.BorderSize = 0;
            this.addG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addG.Image = global::OptLab.Properties.Resources.vector_add_1726;
            this.addG.Location = new System.Drawing.Point(118, 65);
            this.addG.Name = "addG";
            this.addG.Size = new System.Drawing.Size(23, 23);
            this.addG.TabIndex = 6;
            this.addG.UseVisualStyleBackColor = true;
            this.addG.Click += new System.EventHandler(this.addG_Click);
            // 
            // addH
            // 
            this.addH.FlatAppearance.BorderSize = 0;
            this.addH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addH.Image = global::OptLab.Properties.Resources.add_7615;
            this.addH.Location = new System.Drawing.Point(89, 65);
            this.addH.Name = "addH";
            this.addH.Size = new System.Drawing.Size(23, 23);
            this.addH.TabIndex = 5;
            this.addH.UseVisualStyleBackColor = true;
            this.addH.Click += new System.EventHandler(this.addH_Click);
            // 
            // listF
            // 
            this.listF.FormattingEnabled = true;
            this.listF.Location = new System.Drawing.Point(6, 19);
            this.listF.Name = "listF";
            this.listF.Size = new System.Drawing.Size(77, 43);
            this.listF.TabIndex = 1;
            this.listF.SelectedIndexChanged += new System.EventHandler(this.listF_SelectedIndexChanged);
            // 
            // richF
            // 
            this.richF.Location = new System.Drawing.Point(89, 19);
            this.richF.Name = "richF";
            this.richF.Size = new System.Drawing.Size(188, 40);
            this.richF.TabIndex = 0;
            this.richF.Text = "";
            this.richF.TextChanged += new System.EventHandler(this.richF_TextChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox2);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtGammaF);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txtGammaZ);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(304, 166);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(196, 68);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Коэффициент штрафа";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Constant",
            "Adaptive",
            "Linear",
            "Square"});
            this.comboBox2.Location = new System.Drawing.Point(109, 15);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(81, 21);
            this.comboBox2.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(109, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "γ(k+1) > γ(k)";
            // 
            // txtGammaF
            // 
            this.txtGammaF.Location = new System.Drawing.Point(39, 42);
            this.txtGammaF.Name = "txtGammaF";
            this.txtGammaF.Size = new System.Drawing.Size(64, 20);
            this.txtGammaF.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "γͫ = ";
            // 
            // txtGammaZ
            // 
            this.txtGammaZ.Location = new System.Drawing.Point(39, 16);
            this.txtGammaZ.Name = "txtGammaZ";
            this.txtGammaZ.Size = new System.Drawing.Size(64, 20);
            this.txtGammaZ.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "γ⁰ = ";
            // 
            // NewTaskDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 275);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSigma);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTaskDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новая задача оптимизации";
            this.Load += new System.EventHandler(this.NewTaskDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RichTextBox richBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtSigma;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtGammaZ;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtGammaF;
        private System.Windows.Forms.ListBox listF;
        private System.Windows.Forms.Button del;
        private System.Windows.Forms.Button addG;
        private System.Windows.Forms.Button addH;
        private System.Windows.Forms.RichTextBox richF;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCoef;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox funCombo;

        private Penalty.Type getType()
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0: return Penalty.Type.Constant;
                case 1: return Penalty.Type.Adaptive;
                case 2: return Penalty.Type.Linear;
                case 3: return Penalty.Type.Square;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private System.Windows.Forms.Button button1;
    }
}