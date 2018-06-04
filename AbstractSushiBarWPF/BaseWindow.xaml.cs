using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Unity;
using Unity.Attributes;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IBaseService service;

        private readonly IReportService reportService;

        public BaseWindow(IBaseService service, IReportService reportService)
        {
            InitializeComponent();
            this.service = service;
            this.reportService = reportService;
        }

        private void LoadData()
        {
            try
            {
                List<ZakazViewModel> list = service.GetList();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void посетителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<VisitorsWindow>();
            form.ShowDialog();
        }

        private void ингредиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<IngredientsWindow>();
            form.ShowDialog();
        }

        private void сушиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<SushisWindow>();
            form.ShowDialog();
        }

        private void складыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<StoragesWindow>();
            form.ShowDialog();
        }

        private void повараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<CooksWindow>();
            form.ShowDialog();
        }

        private void пополнитьСкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<PutOnStorageWindow>();
            form.ShowDialog();
        }

        private void buttonCreateZakaz_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<CreateZakazWindow>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonTakeZakazInWork_Click(object sender, EventArgs e)
        {
            if (dataGridViewBase.SelectedItem != null)
            {
                var form = Container.Resolve<TakeZakazInWorkWindow>();
                form.ID = ((ZakazViewModel)dataGridViewBase.SelectedItem).Id;
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
                    service.FinishZakaz(id);
                    LoadData();
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
                    service.PayZakaz(id);
                    LoadData();
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
                    reportService.SaveSushiPrice(new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    reportService.SaveStoragesLoad(new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void заказыПосетителейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<VisitorZakazsWindow>();
            form.ShowDialog();
        }
    }
}

