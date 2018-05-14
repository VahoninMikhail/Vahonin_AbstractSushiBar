using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для PutOnStorageWindow.xaml
    /// </summary>
    public partial class PutOnStorageWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStorageService serviceStorage;

        private readonly IIngredientService serviceIngredient;

        private readonly IBaseService serviceBase;

        public PutOnStorageWindow(IStorageService serviceS, IIngredientService serviceC, IBaseService serviceM)
        {
            InitializeComponent();
            Loaded += PutOnStorageWindow_Load;
            this.serviceStorage = serviceS;
            this.serviceIngredient = serviceC;
            this.serviceBase = serviceM;
        }

        private void PutOnStorageWindow_Load(object sender, EventArgs e)
        {
            try
            {
                List<IngredientViewModel> listIngredient = serviceIngredient.GetList();
                if (listIngredient != null)
                {
                    comboBoxIngredient.DisplayMemberPath = "IngredientName";
                    comboBoxIngredient.SelectedValuePath = "Id";
                    comboBoxIngredient.ItemsSource = listIngredient;
                    comboBoxIngredient.SelectedItem = null;
                }
                List<StorageViewModel> listStorage = serviceStorage.GetList();
                if (listStorage != null)
                {
                    comboBoxStorage.DisplayMemberPath = "StorageName";
                    comboBoxStorage.SelectedValuePath = "Id";
                    comboBoxStorage.ItemsSource = listStorage;
                    comboBoxStorage.SelectedItem = null;
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
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxStorage.SelectedItem == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceBase.PutIngredientOnStorage(new StorageIngredientBindingModel
                {
                    IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                    StorageId = Convert.ToInt32(comboBoxStorage.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
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
