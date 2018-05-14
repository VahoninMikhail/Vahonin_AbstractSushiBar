using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace AbstractSushiBarWPF
{
    /// <summary>
    /// Логика взаимодействия для VisitorWindow.xaml
    /// </summary>
    public partial class VisitorWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int ID { set { id = value; } }

        private readonly IVisitorService service;

        private int? id;

        public VisitorWindow(IVisitorService service)
        {
            InitializeComponent();
            Loaded += VisitorWindow_Load;
            this.service = service;
        }

        private void VisitorWindow_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    VisitorViewModel view = service.GetElement(id.Value);
                    if (view != null)
                        textBoxFullName.Text = view.VisitorFIO;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFullName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new VisitorBindingModel
                    {
                        Id = id.Value,
                        VisitorFIO = textBoxFullName.Text
                    });
                }
                else
                {
                    service.AddElement(new VisitorBindingModel
                    {
                        VisitorFIO = textBoxFullName.Text
                    });
                }
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

