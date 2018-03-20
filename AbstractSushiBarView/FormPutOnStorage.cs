using AbstractSushiBarService.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using AbstractSushiBarService.ViewModels;
using AbstractSushiBarService.BindingModels;

namespace AbstractSushiBarView
{
    public partial class FormPutOnStorage : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStorageService serviceSt;

        private readonly IIngredientService serviceI;

        private readonly IBaseService serviceB;

        public FormPutOnStorage(IStorageService serviceSt, IIngredientService serviceI, IBaseService serviceB)
        {
            InitializeComponent();
            this.serviceSt = serviceSt;
            this.serviceI = serviceI;
            this.serviceB = serviceB;
        }

        private void FormPutOnStorage_Load(object sender, EventArgs e)
        {
            try
            {
                List<IngredientViewModel> listI = serviceI.GetList();
                if (listI != null)
                {
                    comboBoxIngredient.DisplayMember = "IngredientName";
                    comboBoxIngredient.ValueMember = "Id";
                    comboBoxIngredient.DataSource = listI;
                    comboBoxIngredient.SelectedItem = null;
                }
                List<StorageViewModel> listS = serviceSt.GetList();
                if (listS != null)
                {
                    comboBoxStorage.DisplayMember = "StorageName";
                    comboBoxStorage.ValueMember = "Id";
                    comboBoxStorage.DataSource = listS;
                    comboBoxStorage.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceB.PutIngredientOnStorage(new StorageIngredientBindingModel
                {
                    IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                    StorageId = Convert.ToInt32(comboBoxStorage.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
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
