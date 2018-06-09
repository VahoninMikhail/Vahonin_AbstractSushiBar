using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для TakeZakazInWorkWindow.xaml
    /// </summary>
    public partial class TakeZakazInWorkWindow : Window
    {
        public int Id { set { id = value; } }

        private int? id;

        public TakeZakazInWorkWindow()
        {
            InitializeComponent();
            Loaded += TakeZakazInWorkWindow_Load;
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
                List<CookViewModel> list = Task.Run(() => APIClient.GetRequestData<List<CookViewModel>>("api/Cook/GetList")).Result;
                if (list != null)
                {
                    comboBoxCook.DisplayMemberPath = "CookFIO";
                    comboBoxCook.SelectedValuePath = "Id";
                    comboBoxCook.ItemsSource = list;
                    comboBoxCook.SelectedItem = null;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCook.SelectedItem == null)
            {
                MessageBox.Show("Выберите повара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                int rabochiyId = Convert.ToInt32(comboBoxCook.SelectedValue);
                Task task = Task.Run(() => APIClient.PostRequestData("api/Base/TakeZakazInWork", new ZakazBindingModel
                {
                    Id = id.Value,
                    CookId = rabochiyId
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Заказ готовится. Обновите список", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information),
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
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
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
