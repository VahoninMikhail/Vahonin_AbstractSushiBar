using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractSushiBarView
{
    public partial class FormCreateZakaz : Form
    {
        public FormCreateZakaz()
        {
            InitializeComponent();
        }

        private void FormCreateZakaz_Load(object sender, EventArgs e)
        {
            try
            {
                var responseV = APIClient.GetRequest("api/Visitor/GetList");
                if (responseV.Result.IsSuccessStatusCode)
                {
                    List<VisitorViewModel> list = APIClient.GetElement<List<VisitorViewModel>>(responseV);
                    if (list != null)
                    {
                        comboBoxVisitor.DisplayMember = "VisitorFIO";
                        comboBoxVisitor.ValueMember = "Id";
                        comboBoxVisitor.DataSource = list;
                        comboBoxVisitor.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseV));
                }
                var responseS = APIClient.GetRequest("api/Sushi/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                {
                    List<SushiViewModel> list = APIClient.GetElement<List<SushiViewModel>>(responseS);
                    if (list != null)
                    {
                        comboBoxSushi.DisplayMember = "SushiName";
                        comboBoxSushi.ValueMember = "Id";
                        comboBoxSushi.DataSource = list;
                        comboBoxSushi.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseS));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxSushi.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxSushi.SelectedValue);
                    var responseS = APIClient.GetRequest("api/Sushi/Get/" + id);
                    if (responseS.Result.IsSuccessStatusCode)
                    {
                        SushiViewModel sushi = APIClient.GetElement<SushiViewModel>(responseS);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSum.Text = (count * (int)sushi.Price).ToString();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(responseS));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxSushi_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxVisitor.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxSushi.SelectedValue == null)
            {
                MessageBox.Show("Выберите суши", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Base/CreateZakaz", new ZakazBindingModel
                {
                    VisitorId = Convert.ToInt32(comboBoxVisitor.SelectedValue),
                    SushiId = Convert.ToInt32(comboBoxSushi.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
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
