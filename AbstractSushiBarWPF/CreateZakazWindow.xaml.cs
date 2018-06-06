using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
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
                var responseC = APIClient.GetRequest("api/Visitor/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<VisitorViewModel> list = APIClient.GetElement<List<VisitorViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxVisitor.DisplayMemberPath = "VisitorFIO";
                        comboBoxVisitor.SelectedValuePath = "Id";
                        comboBoxVisitor.ItemsSource = list;
                        comboBoxSushi.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseP = APIClient.GetRequest("api/Sushi/GetList");
                if (responseP.Result.IsSuccessStatusCode)
                {
                    List<SushiViewModel> list = APIClient.GetElement<List<SushiViewModel>>(responseP);
                    if (list != null)
                    {
                        comboBoxSushi.DisplayMemberPath = "SushiName";
                        comboBoxSushi.SelectedValuePath = "Id";
                        comboBoxSushi.ItemsSource = list;
                        comboBoxSushi.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseP));
                }

            }
            catch (Exception ex)
            {
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
                    var responseP = APIClient.GetRequest("api/Sushi/Get/" + id);
                    if (responseP.Result.IsSuccessStatusCode)
                    {
                        SushiViewModel mebel = APIClient.GetElement<SushiViewModel>(responseP);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSum.Text = (count * (int)mebel.Price).ToString();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(responseP));
                    }
                }
                catch (Exception ex)
                {
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
                MessageBox.Show("Выберите получателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxSushi.SelectedItem == null)
            {
                MessageBox.Show("Выберите мебель", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}