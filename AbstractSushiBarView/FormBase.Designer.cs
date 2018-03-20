namespace AbstractSushiBarView
{
    partial class FormBase
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.покупателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ингредиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сушиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.складыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнитьСкладToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonCreateZakaz = new System.Windows.Forms.Button();
            this.buttonTakeZakazInWork = new System.Windows.Forms.Button();
            this.buttonZakazReady = new System.Windows.Forms.Button();
            this.buttonPayZakaz = new System.Windows.Forms.Button();
            this.buttonRef = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.пополнитьСкладToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1049, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.покупателиToolStripMenuItem,
            this.ингредиентыToolStripMenuItem,
            this.сушиToolStripMenuItem,
            this.складыToolStripMenuItem,
            this.повараToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // покупателиToolStripMenuItem
            // 
            this.покупателиToolStripMenuItem.Name = "покупателиToolStripMenuItem";
            this.покупателиToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.покупателиToolStripMenuItem.Text = "Покупатели";
            this.покупателиToolStripMenuItem.Click += new System.EventHandler(this.покупателиToolStripMenuItem_Click);
            // 
            // ингредиентыToolStripMenuItem
            // 
            this.ингредиентыToolStripMenuItem.Name = "ингредиентыToolStripMenuItem";
            this.ингредиентыToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ингредиентыToolStripMenuItem.Text = "Ингредиенты";
            this.ингредиентыToolStripMenuItem.Click += new System.EventHandler(this.ингредиентыToolStripMenuItem_Click);
            // 
            // сушиToolStripMenuItem
            // 
            this.сушиToolStripMenuItem.Name = "сушиToolStripMenuItem";
            this.сушиToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.сушиToolStripMenuItem.Text = "Суши";
            this.сушиToolStripMenuItem.Click += new System.EventHandler(this.сушиToolStripMenuItem_Click);
            // 
            // складыToolStripMenuItem
            // 
            this.складыToolStripMenuItem.Name = "складыToolStripMenuItem";
            this.складыToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.складыToolStripMenuItem.Text = "Склады";
            this.складыToolStripMenuItem.Click += new System.EventHandler(this.складыToolStripMenuItem_Click);
            // 
            // повараToolStripMenuItem
            // 
            this.повараToolStripMenuItem.Name = "повараToolStripMenuItem";
            this.повараToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.повараToolStripMenuItem.Text = "Повара";
            this.повараToolStripMenuItem.Click += new System.EventHandler(this.повараToolStripMenuItem_Click);
            // 
            // пополнитьСкладToolStripMenuItem
            // 
            this.пополнитьСкладToolStripMenuItem.Name = "пополнитьСкладToolStripMenuItem";
            this.пополнитьСкладToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.пополнитьСкладToolStripMenuItem.Text = "Пополнить склад";
            this.пополнитьСкладToolStripMenuItem.Click += new System.EventHandler(this.пополнитьСкладToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView.Location = new System.Drawing.Point(0, 24);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(873, 277);
            this.dataGridView.TabIndex = 2;
            // 
            // buttonCreateZakaz
            // 
            this.buttonCreateZakaz.Location = new System.Drawing.Point(888, 45);
            this.buttonCreateZakaz.Name = "buttonCreateZakaz";
            this.buttonCreateZakaz.Size = new System.Drawing.Size(149, 23);
            this.buttonCreateZakaz.TabIndex = 3;
            this.buttonCreateZakaz.Text = "Создать заказ";
            this.buttonCreateZakaz.UseVisualStyleBackColor = true;
            this.buttonCreateZakaz.Click += new System.EventHandler(this.buttonCreateZakaz_Click);
            // 
            // buttonTakeZakazInWork
            // 
            this.buttonTakeZakazInWork.Location = new System.Drawing.Point(888, 88);
            this.buttonTakeZakazInWork.Name = "buttonTakeZakazInWork";
            this.buttonTakeZakazInWork.Size = new System.Drawing.Size(149, 23);
            this.buttonTakeZakazInWork.TabIndex = 4;
            this.buttonTakeZakazInWork.Text = "Отдать на выполнение";
            this.buttonTakeZakazInWork.UseVisualStyleBackColor = true;
            this.buttonTakeZakazInWork.Click += new System.EventHandler(this.buttonTakeZakazInWork_Click);
            // 
            // buttonZakazReady
            // 
            this.buttonZakazReady.Location = new System.Drawing.Point(888, 129);
            this.buttonZakazReady.Name = "buttonZakazReady";
            this.buttonZakazReady.Size = new System.Drawing.Size(149, 23);
            this.buttonZakazReady.TabIndex = 5;
            this.buttonZakazReady.Text = "Заказ готов";
            this.buttonZakazReady.UseVisualStyleBackColor = true;
            this.buttonZakazReady.Click += new System.EventHandler(this.buttonZakazReady_Click);
            // 
            // buttonPayZakaz
            // 
            this.buttonPayZakaz.Location = new System.Drawing.Point(888, 170);
            this.buttonPayZakaz.Name = "buttonPayZakaz";
            this.buttonPayZakaz.Size = new System.Drawing.Size(149, 23);
            this.buttonPayZakaz.TabIndex = 6;
            this.buttonPayZakaz.Text = "Заказ оплачен";
            this.buttonPayZakaz.UseVisualStyleBackColor = true;
            this.buttonPayZakaz.Click += new System.EventHandler(this.buttonPayZakaz_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(888, 211);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(149, 23);
            this.buttonRef.TabIndex = 7;
            this.buttonRef.Text = "Обновить список";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 301);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonPayZakaz);
            this.Controls.Add(this.buttonZakazReady);
            this.Controls.Add(this.buttonTakeZakazInWork);
            this.Controls.Add(this.buttonCreateZakaz);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormBase";
            this.Text = "Суши-бар";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem покупателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ингредиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сушиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem складыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem повараToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнитьСкладToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCreateZakaz;
        private System.Windows.Forms.Button buttonTakeZakazInWork;
        private System.Windows.Forms.Button buttonZakazReady;
        private System.Windows.Forms.Button buttonPayZakaz;
        private System.Windows.Forms.Button buttonRef;
    }
}