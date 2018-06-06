using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для StoragesWindow.xaml
    /// </summary>
    public partial class StoragesWindow : Window
    {
        public StoragesWindow()
        {
            InitializeComponent();
            Loaded += StoragesWindow_Load;
        }

        private void StoragesWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Storage/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<StorageViewModel> list = APIClient.GetElement<List<StorageViewModel>>(response);
                    if (list != null)
                    {
                        dataGridViewStorages.ItemsSource = list;
                        dataGridViewStorages.Columns[0].Visibility = Visibility.Hidden;
                        dataGridViewStorages.Columns[1].Width = DataGridLength.Auto;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new StorageWindow();
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewStorages.SelectedItem != null)
            {
                var form = new StorageWindow();
                form.Id = ((StorageViewModel)dataGridViewStorages.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewStorages.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((StorageViewModel)dataGridViewStorages.SelectedItem).Id;
                    try
                    {
                        var response = APIClient.PostRequest("api/Storage/DelElement", new VisitorBindingModel { Id = id });
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
