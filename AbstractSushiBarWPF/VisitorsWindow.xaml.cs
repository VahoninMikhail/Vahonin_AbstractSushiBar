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
    /// Логика взаимодействия для VisitorsWindow.xaml
    /// </summary>
    public partial class VisitorsWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IVisitorService service;

        public VisitorsWindow(IVisitorService service)
        {
            InitializeComponent();
            Loaded += VisitorsWindow_Load;
            this.service = service;
        }

        private void VisitorsWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<VisitorViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewVisitors.ItemsSource = list;
                    dataGridViewVisitors.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewVisitors.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<VisitorWindow>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewVisitors.SelectedItem != null)
            {
                var form = Container.Resolve<VisitorWindow>();
                form.ID = ((VisitorViewModel)dataGridViewVisitors.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewVisitors.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((VisitorViewModel)dataGridViewVisitors.SelectedItem).Id;
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

