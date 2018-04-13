namespace AbstractSushiBarView
{
    partial class FormStoragesLoad
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
			this.buttonSaveToExcel = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.ColumnStorage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnIngredient = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonSaveToExcel
			// 
			this.buttonSaveToExcel.Location = new System.Drawing.Point(12, 12);
			this.buttonSaveToExcel.Name = "buttonSaveToExcel";
			this.buttonSaveToExcel.Size = new System.Drawing.Size(159, 23);
			this.buttonSaveToExcel.TabIndex = 2;
			this.buttonSaveToExcel.Text = "Сохранить в Excel";
			this.buttonSaveToExcel.UseVisualStyleBackColor = true;
			this.buttonSaveToExcel.Click += new System.EventHandler(this.buttonSaveToExcel_Click);
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToOrderColumns = true;
			this.dataGridView.AllowUserToResizeColumns = false;
			this.dataGridView.AllowUserToResizeRows = false;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStorage,
            this.ColumnIngredient,
            this.ColumnCount});
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dataGridView.Location = new System.Drawing.Point(0, 41);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.Size = new System.Drawing.Size(502, 436);
			this.dataGridView.TabIndex = 3;
			// 
			// ColumnStorage
			// 
			this.ColumnStorage.HeaderText = "Склад";
			this.ColumnStorage.Name = "ColumnStorage";
			this.ColumnStorage.ReadOnly = true;
			this.ColumnStorage.Width = 200;
			// 
			// ColumnIngredient
			// 
			this.ColumnIngredient.HeaderText = "Ингредиент";
			this.ColumnIngredient.Name = "ColumnIngredient";
			this.ColumnIngredient.ReadOnly = true;
			this.ColumnIngredient.Width = 200;
			// 
			// ColumnCount
			// 
			this.ColumnCount.HeaderText = "Количество";
			this.ColumnCount.Name = "ColumnCount";
			this.ColumnCount.ReadOnly = true;
			// 
			// FormStoragesLoad
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(502, 477);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.buttonSaveToExcel);
			this.Name = "FormStoragesLoad";
			this.Text = "Загрузка складов";
			this.Load += new System.EventHandler(this.FormStoragesLoad_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveToExcel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStorage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIngredient;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
    }
}