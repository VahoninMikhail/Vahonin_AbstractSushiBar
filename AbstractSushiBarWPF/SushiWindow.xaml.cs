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

        private List<SushiIngredientViewModel> ingredientSushis;

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
                    var mebel = Task.Run(() => APIClient.GetRequestData<SushiViewModel>("api/Sushi/Get/" + id.Value)).Result;
                    textBoxName.Text = mebel.SushiName;
                    textBoxPrice.Text = mebel.Price.ToString();
                    ingredientSushis = mebel.SushiIngredients;
                    LoadData();
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                ingredientSushis = new List<SushiIngredientViewModel>();
        }

        private void LoadData()
        {
            try
            {
                if (ingredientSushis != null)
                {
                    dataGridViewIngredient.ItemsSource = null;
                    dataGridViewIngredient.ItemsSource = ingredientSushis;
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
                    ingredientSushis.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewIngredient.SelectedItem != null)
            {
                var form = new SushiIngredientWindow();
                form.Model = ingredientSushis[dataGridViewIngredient.SelectedIndex];
                if (form.ShowDialog() == true)
                {
                    ingredientSushis[dataGridViewIngredient.SelectedIndex] = form.Model;
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
                        ingredientSushis.RemoveAt(dataGridViewIngredient.SelectedIndex);
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
            if (ingredientSushis == null || ingredientSushis.Count == 0)
            {
                MessageBox.Show("Заполните ингредиенты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<SushiIngredientBindingModel> ingredientSushiBM = new List<SushiIngredientBindingModel>();
            for (int i = 0; i < ingredientSushis.Count; ++i)
            {
                ingredientSushiBM.Add(new SushiIngredientBindingModel
                {
                    Id = ingredientSushis[i].Id,
                    SushiId = ingredientSushis[i].SushiId,
                    IngredientId = ingredientSushis[i].IngredientId,
                    Count = ingredientSushis[i].Count
                });
            }
            string name = textBoxName.Text;
            int price = Convert.ToInt32(textBoxPrice.Text);
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Sushi/UpdElement", new SushiBindingModel
                {
                    Id = id.Value,
                    SushiName = name,
                    Price = price,
                    SushiIngredients = ingredientSushiBM
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Sushi/AddElement", new SushiBindingModel
                {
                    SushiName = name,
                    Price = price,
                    SushiIngredients = ingredientSushiBM
                }));
            }

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith((prevTask) =>
            {
                var ex = (Exception)prevTask.Exception;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }, TaskContinuationOptions.OnlyOnFaulted);

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
