using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для SushiIngredientWinodw.xaml
    /// </summary>
    public partial class SushiIngredientWindow : Window
    {
        public SushiIngredientViewModel Model { set { model = value; } get { return model; } }

        private SushiIngredientViewModel model;

        public SushiIngredientWindow()
        {
            InitializeComponent();
            Loaded += FormIngredientSushi_Load;
        }

        private void FormIngredientSushi_Load(object sender, EventArgs e)
        {
            try
            {
                var response = APIClient.GetRequest("api/Ingredient/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    comboBoxIngredient.DisplayMemberPath = "IngredientName";
                    comboBoxIngredient.SelectedValuePath = "Id";
                    comboBoxIngredient.ItemsSource = APIClient.GetElement<List<IngredientViewModel>>(response);
                    comboBoxIngredient.SelectedItem = null;
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

            if (model != null)
            {
                comboBoxIngredient.IsEnabled = false;
                comboBoxIngredient.SelectedValue = model.IngredientId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxIngredient.SelectedItem == null)
            {
                MessageBox.Show("Выберите заготовку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new SushiIngredientViewModel
                    {
                        IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                        IngredientName = comboBoxIngredient.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
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


