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
    /// Логика взаимодействия для FormCreateZakaz.xaml
    /// </summary>
    public partial class CreateZakazWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IVisitorService serviceVisitor;

        private readonly ISushiService serviceSushi;

        private readonly IBaseService serviceBase;


        public CreateZakazWindow(IVisitorService serviceV, ISushiService serviceS, IBaseService serviceB)
        {
            InitializeComponent();
            Loaded += CreateZakazWindow_Load;
            comboBoxSushi.SelectionChanged += comboBoxSushi_SelectedIndexChanged;
            comboBoxSushi.SelectionChanged += new SelectionChangedEventHandler(comboBoxSushi_SelectedIndexChanged);
            this.serviceVisitor = serviceV;
            this.serviceSushi = serviceS;
            this.serviceBase = serviceB;
        }

        private void CreateZakazWindow_Load(object sender, EventArgs e)
        {
            try
            {
                List<VisitorViewModel> listVisitor = serviceVisitor.GetList();
                if (listVisitor != null)
                {
                    comboBoxVisitor.DisplayMemberPath = "VisitorFIO";
                    comboBoxVisitor.SelectedValuePath = "Id";
                    comboBoxVisitor.ItemsSource = listVisitor;
                    comboBoxSushi.SelectedItem = null;
                }
                List<SushiViewModel> listSushi = serviceSushi.GetList();
                if (listSushi != null)
                {
                    comboBoxSushi.DisplayMemberPath = "SushiName";
                    comboBoxSushi.SelectedValuePath = "Id";
                    comboBoxSushi.ItemsSource = listSushi;
                    comboBoxSushi.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxSushi.SelectedItem != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = ((SushiViewModel)comboBoxSushi.SelectedItem).Id;
                    SushiViewModel suhsi = serviceSushi.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * suhsi.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxSushi_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxVisitor.SelectedItem == null)
            {
                MessageBox.Show("Выберите посетителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxSushi.SelectedItem == null)
            {
                MessageBox.Show("Выберите суши", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceBase.CreateZakaz(new ZakazBindingModel
                {
                    VisitorId = ((VisitorViewModel)comboBoxVisitor.SelectedItem).Id,
                    SushiId = ((SushiViewModel)comboBoxSushi.SelectedItem).Id,
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
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
