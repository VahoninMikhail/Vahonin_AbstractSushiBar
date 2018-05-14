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
    /// Логика взаимодействия для TakeZakazInWorkWindow.xaml
    /// </summary>
    public partial class TakeZakazInWorkWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int ID { set { id = value; } }

        private readonly ICookService serviceCook;

        private readonly IBaseService serviceBase;

        private int? id;

        public TakeZakazInWorkWindow(ICookService serviceI, IBaseService serviceM)
        {
            InitializeComponent();
            Loaded += TakeZakazInWorkWindow_Load;
            this.serviceCook = serviceI;
            this.serviceBase = serviceM;
        }

        private void TakeZakazInWorkWindow_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
                List<CookViewModel> listCook = serviceCook.GetList();
                if (listCook != null)
                {
                    comboBoxCook.DisplayMemberPath = "CookFIO";
                    comboBoxCook.SelectedValuePath = "Id";
                    comboBoxCook.ItemsSource = listCook;
                    comboBoxCook.SelectedItem = null;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCook.SelectedItem == null)
            {
                MessageBox.Show("Выберите повара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceBase.TakeZakazInWork(new ZakazBindingModel
                {
                    Id = id.Value,
                    CookId = ((CookViewModel)comboBoxCook.SelectedItem).Id,
                });
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

