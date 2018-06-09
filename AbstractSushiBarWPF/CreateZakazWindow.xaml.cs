using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для CreateZakazWindow.xaml
    /// </summary>
    public partial class CreateZakazWindow : Window
    {
        public CreateZakazWindow()
        {
            InitializeComponent();
            Loaded += FormCreateZakaz_Load;
            comboBoxSushi.SelectionChanged += comboBoxSushi_SelectedIndexChanged;
            comboBoxSushi.SelectionChanged += new SelectionChangedEventHandler(comboBoxSushi_SelectedIndexChanged);
        }

        private void FormCreateZakaz_Load(object sender, EventArgs e)
        {
            try
            {
                List<VisitorViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<VisitorViewModel>>("api/Visitor/GetList")).Result;
                if (listC != null)
                {
                    comboBoxVisitor.DisplayMemberPath = "VisitorFIO";
                    comboBoxVisitor.SelectedValuePath = "Id";
                    comboBoxVisitor.ItemsSource = listC;
                    comboBoxSushi.SelectedItem = null;
                }

                List<SushiViewModel> listP = Task.Run(() => APIClient.GetRequestData<List<SushiViewModel>>("api/Sushi/GetList")).Result;
                if (listP != null)
                {
                    comboBoxSushi.DisplayMemberPath = "SushiName";
                    comboBoxSushi.SelectedValuePath = "Id";
                    comboBoxSushi.ItemsSource = listP;
                    comboBoxSushi.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxSushi.SelectedItem != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = ((SushiViewModel)comboBoxSushi.SelectedItem).Id;
                    SushiViewModel product = Task.Run(() => APIClient.GetRequestData<SushiViewModel>("api/Sushi/Get/" + id)).Result;
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * (int)product.Price).ToString();
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxVisitor.SelectedItem == null)
            {
                MessageBox.Show("Выберите посетителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxSushi.SelectedItem == null)
            {
                MessageBox.Show("Выберите суши", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int poluchatId = Convert.ToInt32(comboBoxVisitor.SelectedValue);
            int mebelId = Convert.ToInt32(comboBoxSushi.SelectedValue);
            int count = Convert.ToInt32(textBoxCount.Text);
            int sum = Convert.ToInt32(textBoxSum.Text);
            Task task = Task.Run(() => APIClient.PostRequestData("api/Base/CreateZakaz", new ZakazBindingModel
            {
                VisitorId = poluchatId,
                SushiId = mebelId,
                Count = count,
                Sum = sum
            }));

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith((prevTask) =>
            {
                var ex = (Exception)prevTask.Exception;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }, TaskContinuationOptions.OnlyOnFaulted);

            Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}