using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Unity;
using Unity.Attributes;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для SushiWindow.xaml
    /// </summary>
    public partial class SushiWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int ID { set { id = value; } }

        private readonly ISushiService service;

        private int? id;

        private List<SushiIngredientViewModel> sushiIngredients;

        public SushiWindow(ISushiService service)
        {
            InitializeComponent();
            Loaded += SushiWindow_Load;
            this.service = service;
        }

        private void SushiWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SushiViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.SushiName;
                        textBoxPrice.Text = view.Price.ToString();
                        sushiIngredients = view.SushiIngredients;
                        LoadData();
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
            var form = Container.Resolve<SushiIngredientWindow>();
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
                var form = Container.Resolve<SushiIngredientWindow>();
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
                MessageBox.Show("Заполните ингредиенты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (id.HasValue)
                {
                    service.UpdElement(new SushiBindingModel
                    {
                        Id = id.Value,
                        SushiName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SushiIngredients = sushiIngredientBM
                    });
                }
                else
                {
                    service.AddElement(new SushiBindingModel
                    {
                        SushiName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SushiIngredients = sushiIngredientBM
                    });
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
