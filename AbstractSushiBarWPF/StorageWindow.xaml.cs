using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Net.Http;
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
                    var response = APIClient.GetRequest("api/Storage/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var baza = APIClient.GetElement<StorageViewModel>(response);
                        textBoxName.Text = baza.StorageName;
                        dataGridViewStorage.ItemsSource = baza.StorageIngredients;
                        dataGridViewStorage.Columns[0].Visibility = Visibility.Hidden;
                        dataGridViewStorage.Columns[1].Visibility = Visibility.Hidden;
                        dataGridViewStorage.Columns[2].Visibility = Visibility.Hidden;
                        dataGridViewStorage.Columns[3].Width = DataGridLength.Auto;
                    }
                }
                catch (Exception ex)
                {
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
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Storage/UpdElement", new StorageBindingModel
                    {
                        Id = id.Value,
                        StorageName = textBoxName.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Storage/AddElement", new StorageBindingModel
                    {
                        StorageName = textBoxName.Text
                    });
                }
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