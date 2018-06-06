using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для SushisWindow.xaml
    /// </summary>
    public partial class SushisWindow : Window
    {
        public SushisWindow()
        {
            InitializeComponent();
            Loaded += SushisWindow_Load;
        }

        private void SushisWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Sushi/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<SushiViewModel> list = APIClient.GetElement<List<SushiViewModel>>(response);
                    if (list != null)
                    {
                        dataGridViewSushis.ItemsSource = list;
                        dataGridViewSushis.Columns[0].Visibility = Visibility.Hidden;
                        dataGridViewSushis.Columns[1].Width = DataGridLength.Auto;
                        dataGridViewSushis.Columns[3].Visibility = Visibility.Hidden;
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
            var form = new SushiWindow();
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewSushis.SelectedItem != null)
            {
                var form = new SushiWindow();
                form.Id = ((SushiViewModel)dataGridViewSushis.SelectedItem).Id;
                if (form.ShowDialog() == true)
                    LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewSushis.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    int id = ((SushiViewModel)dataGridViewSushis.SelectedItem).Id;
                    try
                    {
                        var response = APIClient.PostRequest("api/Sushi/DelElement", new VisitorBindingModel { Id = id });
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