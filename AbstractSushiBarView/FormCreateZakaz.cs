using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                List<VisitorViewModel> listV = Task.Run(() => APIClient.GetRequestData<List<VisitorViewModel>>("api/Visitor/GetList")).Result;
                if (listV != null)
                {
                    comboBoxVisitor.DisplayMember = "VisitorFIO";
                    comboBoxVisitor.ValueMember = "Id";
                    comboBoxVisitor.DataSource = listV;
                    comboBoxVisitor.SelectedItem = null;
                }

                List<SushiViewModel> listS = Task.Run(() => APIClient.GetRequestData<List<SushiViewModel>>("api/Sushi/GetList")).Result;
                if (listS != null)
                {
                    comboBoxSushi.DisplayMember = "SushiName";
                    comboBoxSushi.ValueMember = "Id";
                    comboBoxSushi.DataSource = listS;
                    comboBoxSushi.SelectedItem = null;
                }
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

        private void CalcSum()
        {
            if (comboBoxSushi.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxSushi.SelectedValue);
                    SushiViewModel sushi = Task.Run(() => APIClient.GetRequestData<SushiViewModel>("api/Sushi/Get/" + id)).Result;
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * (int)sushi.Price).ToString();
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
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int visitorId = Convert.ToInt32(comboBoxVisitor.SelectedValue);
            int sushiId = Convert.ToInt32(comboBoxSushi.SelectedValue);
            int count = Convert.ToInt32(textBoxCount.Text);
            int sum = Convert.ToInt32(textBoxSum.Text);
            Task task = Task.Run(() => APIClient.PostRequestData("api/Base/CreateZakaz", new ZakazBindingModel
            {
                VisitorId = visitorId,
                SushiId = sushiId,
                Count = count,
                Sum = sum
            }));

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
