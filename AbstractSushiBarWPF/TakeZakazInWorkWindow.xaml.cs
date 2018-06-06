using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
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
                    MessageBox.Show("Не указана заявка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
                var response = APIClient.GetRequest("api/Cook/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<CookViewModel> list = APIClient.GetElement<List<CookViewModel>>(response);
                    if (list != null)
                    {
                        comboBoxCook.DisplayMemberPath = "CookFIO";
                        comboBoxCook.SelectedValuePath = "Id";
                        comboBoxCook.ItemsSource = list;
                        comboBoxCook.SelectedItem = null;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCook.SelectedItem == null)
            {
                MessageBox.Show("Выберите рабочего", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Base/TakeZakazInWork", new ZakazBindingModel
                {
                    Id = id.Value,
                    CookId = ((CookViewModel)comboBoxCook.SelectedItem).Id,
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
