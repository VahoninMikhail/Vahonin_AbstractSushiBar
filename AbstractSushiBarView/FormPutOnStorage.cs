using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AbstractSushiBarService.ViewModels;
using AbstractSushiBarService.BindingModels;

namespace AbstractSushiBarView
{
    public partial class FormPutOnStorage : Form
    {
        public FormPutOnStorage()
        {
            InitializeComponent();
        }

        private void FormPutOnStorage_Load(object sender, EventArgs e)
        {
            try
            {
                var responseI = APIClient.GetRequest("api/Ingredient/GetList");
                if (responseI.Result.IsSuccessStatusCode)
                {
                    List<IngredientViewModel> list = APIClient.GetElement<List<IngredientViewModel>>(responseI);
                    if (list != null)
                    {
                        comboBoxIngredient.DisplayMember = "IngredientName";
                        comboBoxIngredient.ValueMember = "Id";
                        comboBoxIngredient.DataSource = list;
                        comboBoxIngredient.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseI));
                }
                var responseSt = APIClient.GetRequest("api/Storage/GetList");
                if (responseSt.Result.IsSuccessStatusCode)
                {
                    List<StorageViewModel> list = APIClient.GetElement<List<StorageViewModel>>(responseSt);
                    if (list != null)
                    {
                        comboBoxStorage.DisplayMember = "StorageName";
                        comboBoxStorage.ValueMember = "Id";
                        comboBoxStorage.DataSource = list;
                        comboBoxStorage.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseI));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Base/PutIngredientOnStorage", new StorageIngredientBindingModel
                {
                    IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                    StorageId = Convert.ToInt32(comboBoxStorage.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
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
