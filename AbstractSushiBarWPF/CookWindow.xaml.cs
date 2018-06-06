using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для CookWindow.xaml
    /// </summary>
    public partial class CookWindow : Window
    {
        public int Id { set { id = value; } }

        private int? id;

        public CookWindow()
        {
            InitializeComponent();
            Loaded += CookWindow_Load;
        }

        private void CookWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Cook/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var cook = APIClient.GetElement<CookViewModel>(response);
                        textBoxFullName.Text = cook.CookFIO;
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
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFullName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Cook/UpdElement", new CookBindingModel
                    {
                        Id = id.Value,
                        CookFIO = textBoxFullName.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Cook/AddElement", new CookBindingModel
                    {
                        CookFIO = textBoxFullName.Text
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
