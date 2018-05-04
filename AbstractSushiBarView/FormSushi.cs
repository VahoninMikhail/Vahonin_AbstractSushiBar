using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AbstractSushiBarService.BindingModels;
using System.Threading.Tasks;

namespace AbstractSushiBarView
{
    public partial class FormSushi : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        private List<SushiIngredientViewModel> sushiIngredients;

        public FormSushi()
        {
            InitializeComponent();
        }

        private void FormSushi_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var sushi = Task.Run(() => APIClient.GetRequestData<SushiViewModel>("api/Sushi/Get/" + id.Value)).Result;
                    textBoxName.Text = sushi.SushiName;
                    textBoxPrice.Text = sushi.Price.ToString();
                    sushiIngredients = sushi.SushiIngredients;
                    LoadData();
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
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
            var form = new FormSushiIngredient();
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
                var form = new FormSushiIngredient();
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
                MessageBox.Show("Выберите ингредиенты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
            string name = textBoxName.Text;
            int price = Convert.ToInt32(textBoxPrice.Text);
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Sushi/UpdElement", new SushiBindingModel
                {
                    Id = id.Value,
                    SushiName = name,
                    Price = price,
                    SushiIngredients = sushiIngredientBM
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Sushi/AddElement", new SushiBindingModel
                {
                    SushiName = name,
                    Price = price,
                    SushiIngredients = sushiIngredientBM
                }));
            }

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith((prevTask) =>
            {
                var ex = (Exception)prevTask.Exception;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }, TaskContinuationOptions.OnlyOnFaulted);

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
