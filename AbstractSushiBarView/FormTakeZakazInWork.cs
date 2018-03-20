using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AbstractSushiBarView
{
    public partial class FormTakeZakazInWork : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICookService serviceC;

        private readonly IBaseService serviceB;

        private int? id;

        public FormTakeZakazInWork(ICookService serviceC, IBaseService serviceB)
        {
            InitializeComponent();
            this.serviceC = serviceC;
            this.serviceB = serviceB;
        }

        private void FormTakeZakazInWork_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                List<CookViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxCook.DisplayMember = "CookFIO";
                    comboBoxCook.ValueMember = "Id";
                    comboBoxCook.DataSource = listC;
                    comboBoxCook.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCook.SelectedValue == null)
            {
                MessageBox.Show("Выберите повара", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceB.TakeZakazInWork(new ZakazBindingModel
                {
                    Id = id.Value,
                    CookId = Convert.ToInt32(comboBoxCook.SelectedValue)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
