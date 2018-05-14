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
    /// Логика взаимодействия для SushisWindow.xaml
    /// </summary>
    public partial class SushisWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISushiService service;

        public SushisWindow(ISushiService service)
        {
            InitializeComponent();
            Loaded += SushisWindow_Load;
            this.service = service;
        }

        private void SushisWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<SushiViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewSushis.ItemsSource = list;
                    dataGridViewSushis.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewSushis.Columns[1].Width = DataGridLength.Auto;
                    dataGridViewSushis.Columns[3].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<SushiWindow>();
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewSushis.SelectedItem != null)
            {
                var form = Container.Resolve<SushiWindow>();
                form.ID = ((SushiViewModel)dataGridViewSushis.SelectedItem).Id;
                if (form.ShowDialog() == true)
                    LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewSushis.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    int id = ((SushiViewModel)dataGridViewSushis.SelectedItem).Id;
                    try
                    {
                        service.DelElement(id);
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
    }
}

