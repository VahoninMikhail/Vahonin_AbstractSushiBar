using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для SushiWindow.xaml
    /// </summary>
    public partial class SushiWindow : Window
    {
        public int Id { set { id = value; } }

        private int? id;

        private List<SushiIngredientViewModel> sushiIngredients;

        public SushiWindow()
        {
            InitializeComponent();
            Loaded += SushiWindow_Load;
        }

        private void SushiWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Sushi/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var mebel = APIClient.GetElement<SushiViewModel>(response);
                        textBoxName.Text = mebel.SushiName;
                        textBoxPrice.Text = mebel.Price.ToString();
                        sushiIngredients = mebel.SushiIngredients;
                        LoadData();
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
            else
                sushiIngredients = new List<SushiIngredientViewModel>();
        }

        private void LoadData()
        {
            try
            {
                if (sushiIngredients != null)
                {
                    dataGridViewIngredient.ItemsSource = null;
                    dataGridViewIngredient.ItemsSource = sushiIngredients;
                    dataGridViewIngredient.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewIngredient.Columns[1].Visibility = Visibility.Hidden;
                    dataGridViewIngredient.Columns[2].Visibility = Visibility.Hidden;
                    dataGridViewIngredient.Columns[3].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new SushiIngredientWindow();
            if (form.ShowDialog() == true)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                        form.Model.SushiId = id.Value;
                    sushiIngredients.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewIngredient.SelectedItem != null)
            {
                var form = new SushiIngredientWindow();
                form.Model = sushiIngredients[dataGridViewIngredient.SelectedIndex];
                if (form.ShowDialog() == true)
                {
                    sushiIngredients[dataGridViewIngredient.SelectedIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewIngredient.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        sushiIngredients.RemoveAt(dataGridViewIngredient.SelectedIndex);
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (sushiIngredients == null || sushiIngredients.Count == 0)
            {
                MessageBox.Show("Заполните заготовки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<SushiIngredientBindingModel> sushiIngredientBM = new List<SushiIngredientBindingModel>();
                for (int i = 0; i < sushiIngredients.Count; ++i)
                {
                    sushiIngredientBM.Add(new SushiIngredientBindingModel
                    {
                        Id = sushiIngredients[i].Id,
                        SushiId = sushiIngredients[i].SushiId,
                        IngredientId = sushiIngredients[i].IngredientId,
                        Count = sushiIngredients[i].Count
                    });
                }
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Sushi/UpdElement", new SushiBindingModel
                    {
                        Id = id.Value,
                        SushiName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SushiIngredients = sushiIngredientBM
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Sushi/AddElement", new SushiBindingModel
                    {
                        SushiName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SushiIngredients = sushiIngredientBM
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
