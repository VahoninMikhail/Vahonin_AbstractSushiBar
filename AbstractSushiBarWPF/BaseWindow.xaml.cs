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
    /// Логика взаимодействия для BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IBaseService service;

        public BaseWindow(IBaseService service)
        {
            InitializeComponent();
            this.service = service;
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
    }
}

