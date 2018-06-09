using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для StorageWindow.xaml
    /// </summary>
    public partial class StorageWindow : Window
    {
        public int Id { set { id = value; } }

        private int? id;

        public StorageWindow()
        {
            InitializeComponent();
            Loaded += StorageWindow_Load;

        }

        private void StorageWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var baza = Task.Run(() => APIClient.GetRequestData<StorageViewModel>("api/Storage/Get/" + id.Value)).Result;
                    textBoxName.Text = baza.StorageName;
                    dataGridViewStorage.ItemsSource = baza.StorageIngredients;
                    dataGridViewStorage.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewStorage.Columns[1].Visibility = Visibility.Hidden;
                    dataGridViewStorage.Columns[2].Visibility = Visibility.Hidden;
                    dataGridViewStorage.Columns[3].Width = DataGridLength.Auto;

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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string name = textBoxName.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Storage/UpdElement", new StorageBindingModel
                {
                    Id = id.Value,
                    StorageName = name
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Storage/AddElement", new StorageBindingModel
                {
                    StorageName = name
                }));
            }

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
