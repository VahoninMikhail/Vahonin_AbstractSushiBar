using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для VisitorWindow.xaml
    /// </summary>
    public partial class VisitorWindow : Window
    {
        public int Id { set { id = value; } }

        private int? id;

        public VisitorWindow()
        {
            InitializeComponent();
            Loaded += VisitorWindow_Load;
        }

        private void VisitorWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Visitor/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var visitor = APIClient.GetElement<VisitorViewModel>(response);
                        textBoxFullName.Text = visitor.VisitorFIO;
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
                    response = APIClient.PostRequest("api/Visitor/UpdElement", new VisitorBindingModel
                    {
                        Id = id.Value,
                        VisitorFIO = textBoxFullName.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Visitor/AddElement", new VisitorBindingModel
                    {
                        VisitorFIO = textBoxFullName.Text
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


