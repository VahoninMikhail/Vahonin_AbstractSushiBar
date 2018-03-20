namespace AbstractSushiBarView
{
    partial class FormCreateZakaz
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
            this.labelVisitor = new System.Windows.Forms.Label();
            this.comboBoxVisitor = new System.Windows.Forms.ComboBox();
            this.labelSushi = new System.Windows.Forms.Label();
            this.comboBoxSushi = new System.Windows.Forms.ComboBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.labelSum = new System.Windows.Forms.Label();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelVisitor
            // 
            this.labelVisitor.AutoSize = true;
            this.labelVisitor.Location = new System.Drawing.Point(12, 9);
            this.labelVisitor.Name = "labelVisitor";
            this.labelVisitor.Size = new System.Drawing.Size(46, 13);
            this.labelVisitor.TabIndex = 1;
            this.labelVisitor.Text = "Клиент:";
            // 
            // comboBoxVisitor
            // 
            this.comboBoxVisitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVisitor.FormattingEnabled = true;
            this.comboBoxVisitor.Location = new System.Drawing.Point(90, 9);
            this.comboBoxVisitor.Name = "comboBoxVisitor";
            this.comboBoxVisitor.Size = new System.Drawing.Size(217, 21);
            this.comboBoxVisitor.TabIndex = 2;
            // 
            // labelSushi
            // 
            this.labelSushi.AutoSize = true;
            this.labelSushi.Location = new System.Drawing.Point(12, 36);
            this.labelSushi.Name = "labelSushi";
            this.labelSushi.Size = new System.Drawing.Size(54, 13);
            this.labelSushi.TabIndex = 3;
            this.labelSushi.Text = "Изделие:";
            // 
            // comboBoxSushi
            // 
            this.comboBoxSushi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSushi.FormattingEnabled = true;
            this.comboBoxSushi.Location = new System.Drawing.Point(90, 33);
            this.comboBoxSushi.Name = "comboBoxSushi";
            this.comboBoxSushi.Size = new System.Drawing.Size(217, 21);
            this.comboBoxSushi.TabIndex = 4;
            this.comboBoxSushi.SelectedIndexChanged += new System.EventHandler(this.comboBoxSushi_SelectedIndexChanged);
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(12, 59);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 5;
            this.labelCount.Text = "Количество:";
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(90, 56);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(217, 20);
            this.textBoxCount.TabIndex = 6;
            this.textBoxCount.TextChanged += new System.EventHandler(this.textBoxCount_TextChanged);
            // 
            // labelSum
            // 
            this.labelSum.AutoSize = true;
            this.labelSum.Location = new System.Drawing.Point(12, 81);
            this.labelSum.Name = "labelSum";
            this.labelSum.Size = new System.Drawing.Size(44, 13);
            this.labelSum.TabIndex = 7;
            this.labelSum.Text = "Сумма:";
            // 
            // textBoxSum
            // 
            this.textBoxSum.Location = new System.Drawing.Point(90, 78);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.ReadOnly = true;
            this.textBoxSum.Size = new System.Drawing.Size(217, 20);
            this.textBoxSum.TabIndex = 8;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(232, 104);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(138, 104);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormCreateZakaz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 142);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxSum);
            this.Controls.Add(this.labelSum);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.comboBoxSushi);
            this.Controls.Add(this.labelSushi);
            this.Controls.Add(this.comboBoxVisitor);
            this.Controls.Add(this.labelVisitor);
            this.Name = "FormCreateZakaz";
            this.Text = "Создать заказ";
            this.Load += new System.EventHandler(this.FormCreateZakaz_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelVisitor;
        private System.Windows.Forms.ComboBox comboBoxVisitor;
        private System.Windows.Forms.Label labelSushi;
        private System.Windows.Forms.ComboBox comboBoxSushi;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label labelSum;
        private System.Windows.Forms.TextBox textBoxSum;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}