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
    public partial class FormCreateZakaz : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IVisitorService serviceV;

        private readonly ISushiService serviceS;

        private readonly IBaseService serviceB;

        public FormCreateZakaz(IVisitorService serviceV, ISushiService serviceS, IBaseService serviceB)
        {
            InitializeComponent();
            this.serviceV = serviceV;
            this.serviceS = serviceS;
            this.serviceB = serviceB;
        }

        private void FormCreateZakaz_Load(object sender, EventArgs e)
        {
            try
            {
                List<VisitorViewModel> listV = serviceV.GetList();
                if (listV != null)
                {
                    comboBoxVisitor.DisplayMember = "VisitorFIO";
                    comboBoxVisitor.ValueMember = "Id";
                    comboBoxVisitor.DataSource = listV;
                    comboBoxVisitor.SelectedItem = null;
                }
                List<SushiViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    comboBoxSushi.DisplayMember = "SushiName";
                    comboBoxSushi.ValueMember = "Id";
                    comboBoxSushi.DataSource = listS;
                    comboBoxSushi.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxSushi.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxSushi.SelectedValue);
                    SushiViewModel product = serviceS.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxVisitor.SelectedValue == null)
            {
                MessageBox.Show("Выберите покупателя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxSushi.SelectedValue == null)
            {
                MessageBox.Show("Выберите суши", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceB.CreateZakaz(new ZakazBindingModel
                {
                    VisitorId = Convert.ToInt32(comboBoxVisitor.SelectedValue),
                    SushiId = Convert.ToInt32(comboBoxSushi.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
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
