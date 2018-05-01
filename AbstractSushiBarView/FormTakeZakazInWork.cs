using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractSushiBarView
{
    public partial class FormTakeZakazInWork : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormTakeZakazInWork()
        {
            InitializeComponent();
        }

        private void FormTakeZakazInWork_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                var response = APIClient.GetRequest("api/Cook/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<CookViewModel> list = APIClient.GetElement<List<CookViewModel>>(response);
                    if (list != null)
                    {
                        comboBoxCook.DisplayMember = "CookFIO";
                        comboBoxCook.ValueMember = "Id";
                        comboBoxCook.DataSource = list;
                        comboBoxCook.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCook.SelectedValue == null)
            {
                MessageBox.Show("Выберите повара", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Base/TakeZakazInWork", new ZakazBindingModel
                {
                    Id = id.Value,
                    CookId = Convert.ToInt32(comboBoxCook.SelectedValue)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
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
