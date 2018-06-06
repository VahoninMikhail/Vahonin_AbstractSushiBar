using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для IngredientWindow.xaml
    /// </summary>
    public partial class IngredientWindow : Window
    {
        public int Id { set { id = value; } }

        private int? id;

        public IngredientWindow()
        {
            InitializeComponent();
            Loaded += IngredientWindow_Load;
        }

        private void IngredientWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Ingredient/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var ingredient = APIClient.GetElement<IngredientViewModel>(response);
                        textBoxName.Text = ingredient.IngredientName;
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
                    response = APIClient.PostRequest("api/Ingredient/UpdElement", new IngredientBindingModel
                    {
                        Id = id.Value,
                        IngredientName = textBoxName.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Ingredient/AddElement", new IngredientBindingModel
                    {
                        IngredientName = textBoxName.Text
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
