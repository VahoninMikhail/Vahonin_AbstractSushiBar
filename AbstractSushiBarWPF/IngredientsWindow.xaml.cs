﻿using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                List<IngredientViewModel> list = Task.Run(() => APIClient.GetRequestData<List<IngredientViewModel>>("api/Ingredient/GetList")).Result;
                if (list != null)
                {
                    dataGridViewIngredients.ItemsSource = list;
                    dataGridViewIngredients.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewIngredients.Columns[1].Width = DataGridLength.Auto;
                }
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
                    Task task = Task.Run(() => APIClient.PostRequestData("api/Ingredient/DelElement", new VisitorBindingModel { Id = id }));

                    task.ContinueWith((prevTask) => MessageBox.Show("Запись удалена. Обновите список", "Успех", MessageBoxButton.OK, MessageBoxImage.Information),
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
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
