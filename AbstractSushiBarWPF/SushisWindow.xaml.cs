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
                List<SushiViewModel> list = Task.Run(() => APIClient.GetRequestData<List<SushiViewModel>>("api/Sushi/GetList")).Result;
                if (list != null)
                {
                    dataGridViewSushis.ItemsSource = list;
                    dataGridViewSushis.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewSushis.Columns[1].Width = DataGridLength.Auto;
                    dataGridViewSushis.Columns[3].Visibility = Visibility.Hidden;
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

                    Task task = Task.Run(() => APIClient.PostRequestData("api/Sushi/DelElement", new VisitorBindingModel { Id = id }));

                    task.ContinueWith((prevTask) => MessageBox.Show("Запись удалена. Обновите список", "Успех", MessageBoxButton.OK, MessageBoxImage.Information),
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
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}