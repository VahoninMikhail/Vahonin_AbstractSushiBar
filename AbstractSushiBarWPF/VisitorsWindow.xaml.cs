using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для VisitorsWindow.xaml
    /// </summary>
    public partial class VisitorsWindow : Window
    {
        public VisitorsWindow()
        {
            InitializeComponent();
            Loaded += VisitorsWindow_Load;
        }

        private void VisitorsWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Visitor/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<VisitorViewModel> list = APIClient.GetElement<List<VisitorViewModel>>(response);
                    if (list != null)
                    {
                        dataGridViewVisitors.ItemsSource = list;
                        dataGridViewVisitors.Columns[0].Visibility = Visibility.Hidden;
                        dataGridViewVisitors.Columns[1].Width = DataGridLength.Auto;
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
            var form = new VisitorWindow();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewVisitors.SelectedItem != null)
            {
                var form = new VisitorWindow();
                form.Id = ((VisitorViewModel)dataGridViewVisitors.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewVisitors.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((VisitorViewModel)dataGridViewVisitors.SelectedItem).Id;
                    try
                    {
                        var response = APIClient.PostRequest("api/Visitor/DelElement", new VisitorBindingModel { Id = id });
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
