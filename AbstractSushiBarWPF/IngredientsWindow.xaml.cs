using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для IngredientsWindow.xaml
    /// </summary>
    public partial class IngredientsWindow : Window
    {
        public IngredientsWindow()
        {
            InitializeComponent();
            Loaded += IngredientsWindow_Load;
        }

        private void IngredientsWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Ingredient/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<IngredientViewModel> list = APIClient.GetElement<List<IngredientViewModel>>(response);
                    if (list != null)
                    {
                        dataGridViewIngredients.ItemsSource = list;
                        dataGridViewIngredients.Columns[0].Visibility = Visibility.Hidden;
                        dataGridViewIngredients.Columns[1].Width = DataGridLength.Auto;
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
            var form = new IngredientWindow();
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewIngredients.SelectedItem != null)
            {
                var form = new IngredientWindow();
                form.Id = ((IngredientViewModel)dataGridViewIngredients.SelectedItem).Id;
                if (form.ShowDialog() == true)
                    LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewIngredients.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((IngredientViewModel)dataGridViewIngredients.SelectedItem).Id;
                    try
                    {
                        var response = APIClient.PostRequest("api/Ingredient/DelElement", new VisitorBindingModel { Id = id });
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