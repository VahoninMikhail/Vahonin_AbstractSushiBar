using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using AbstractSushiBarService.BindingModels;

namespace AbstractSushiBarView
{
    public partial class FormSushi : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ISushiService service;

        private int? id;

        private List<SushiIngredientViewModel> sushiIngredients;

        public FormSushi(ISushiService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormSushi_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SushiViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.SushiName;
                        textBoxPrice.Text = view.Price.ToString();
                        sushiIngredients = view.SushiIngredients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                sushiIngredients = new List<SushiIngredientViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (sushiIngredients != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = sushiIngredients;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSushiIngredient>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.SushiId = id.Value;
                    }
                    sushiIngredients.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormSushiIngredient>();
                form.Model = sushiIngredients[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    sushiIngredients[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        sushiIngredients.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sushiIngredients == null || sushiIngredients.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<SushiIngredientBindingModel> sushiIngredientBM = new List<SushiIngredientBindingModel>();
                for (int i = 0; i < sushiIngredients.Count; ++i)
                {
                    sushiIngredientBM.Add(new SushiIngredientBindingModel
                    {
                        Id = sushiIngredients[i].Id,
                        SushiId = sushiIngredients[i].SushiId,
                        IngredientId = sushiIngredients[i].IngredientId,
                        Count = sushiIngredients[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new SushiBindingModel
                    {
                        Id = id.Value,
                        SushiName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SushiIngredients = sushiIngredientBM
                    });
                }
                else
                {
                    service.AddElement(new SushiBindingModel
                    {
                        SushiName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SushiIngredients = sushiIngredientBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
