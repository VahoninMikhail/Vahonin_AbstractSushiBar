using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для CooksWindow.xaml
    /// </summary>
    public partial class CooksWindow : Window
    {
        public CooksWindow()
        {
            InitializeComponent();
            Loaded += CooksWindow_Load;
        }

        private void CooksWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Cook/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<CookViewModel> list = APIClient.GetElement<List<CookViewModel>>(response);
                    if (list != null)
                    {
                        dataGridViewCooks.ItemsSource = list;
                        dataGridViewCooks.Columns[0].Visibility = Visibility.Hidden;
                        dataGridViewCooks.Columns[1].Width = DataGridLength.Auto;
                    }
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new CookWindow();
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewCooks.SelectedItem != null)
            {
                var form = new CookWindow();
                form.Id = ((CookViewModel)dataGridViewCooks.SelectedItem).Id;
                if (form.ShowDialog() == true)
                    LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewCooks.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((CookViewModel)dataGridViewCooks.SelectedItem).Id;
                    try
                    {
                        var response = APIClient.PostRequest("api/Cook/DelElement", new VisitorBindingModel { Id = id });
                        if (!response.Result.IsSuccessStatusCode)
                        {
                            throw new Exception(APIClient.GetError(response));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}

