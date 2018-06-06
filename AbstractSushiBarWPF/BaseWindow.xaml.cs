using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Base/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<ZakazViewModel> list = APIClient.GetElement<List<ZakazViewModel>>(response);
                    if (list != null)
                    {
                        dataGridViewBase.ItemsSource = list;
                        dataGridViewBase.Columns[0].Visibility = Visibility.Hidden;
                        dataGridViewBase.Columns[1].Visibility = Visibility.Hidden;
                        dataGridViewBase.Columns[3].Visibility = Visibility.Hidden;
                        dataGridViewBase.Columns[5].Visibility = Visibility.Hidden;
                        dataGridViewBase.Columns[1].Width = DataGridLength.Auto;
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

        private void посетителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new VisitorsWindow();
            form.ShowDialog();
        }

        private void ингредиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new IngredientsWindow();
            form.ShowDialog();
        }

        private void сушиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SushisWindow();
            form.ShowDialog();
        }

        private void складыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new StoragesWindow();
            form.ShowDialog();
        }

        private void повараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new CooksWindow();
            form.ShowDialog();
        }

        private void пополнитьСкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new PutOnStorageWindow();
            form.ShowDialog();
        }

        private void buttonCreateZakaz_Click(object sender, EventArgs e)
        {
            var form = new CreateZakazWindow();
            form.ShowDialog();
            LoadData();
        }

        private void buttonTakeZakazInWork_Click(object sender, EventArgs e)
        {
            if (dataGridViewBase.SelectedItem != null)
            {
                var form = new TakeZakazInWorkWindow();
                form.Id = ((ZakazViewModel)dataGridViewBase.SelectedItem).Id;
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonZakazReady_Click(object sender, EventArgs e)
        {
            if (dataGridViewBase.SelectedItem != null)
            {
                int id = ((ZakazViewModel)dataGridViewBase.SelectedItem).Id;
                try
                {
                    var response = APIClient.PostRequest("api/Base/FinishZakaz", new ZakazBindingModel
                    {
                        Id = id
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        LoadData();
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
        }

        private void buttonPayZakaz_Click(object sender, EventArgs e)
        {
            if (dataGridViewBase.SelectedItem != null)
            {
                int id = ((ZakazViewModel)dataGridViewBase.SelectedItem).Id;
                try
                {
                    var response = APIClient.PostRequest("api/Base/PayZakaz", new ZakazBindingModel
                    {
                        Id = id
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        LoadData();
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
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void прайсСушиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };

            if (sfd.ShowDialog() == true)
            {

                try
                {

                    var response = APIClient.PostRequest("api/Report/SaveSushiPrice", new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void загруженностьСкладовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    var response = APIClient.PostRequest("api/Report/SaveStoragesLoad", new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }

        private void заказыПосетителейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new VisitorZakazsWindow();
            form.ShowDialog();
        }
    }
}
