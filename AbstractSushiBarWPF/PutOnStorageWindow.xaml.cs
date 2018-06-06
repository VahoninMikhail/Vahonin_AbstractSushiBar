using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для PutOnStorageWindow.xaml
    /// </summary>
    public partial class PutOnStorageWindow : Window
    {
        public PutOnStorageWindow()
        {
            InitializeComponent();
            Loaded += PutOnStorageWindow_Load;
        }

        private void PutOnStorageWindow_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIClient.GetRequest("api/Ingredient/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<IngredientViewModel> list = APIClient.GetElement<List<IngredientViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxIngredient.DisplayMemberPath = "IngredientName";
                        comboBoxIngredient.SelectedValuePath = "Id";
                        comboBoxIngredient.ItemsSource = list;
                        comboBoxIngredient.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseS = APIClient.GetRequest("api/Storage/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                {
                    List<StorageViewModel> list = APIClient.GetElement<List<StorageViewModel>>(responseS);
                    if (list != null)
                    {
                        comboBoxStorage.DisplayMemberPath = "StorageName";
                        comboBoxStorage.SelectedValuePath = "Id";
                        comboBoxStorage.ItemsSource = list;
                        comboBoxStorage.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (comboBoxStorage.SelectedItem == null)
            {
                MessageBox.Show("Выберите базу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Base/PutIngredientOnStorage", new StorageIngredientBindingModel
                {
                    IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                    StorageId = Convert.ToInt32(comboBoxStorage.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
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