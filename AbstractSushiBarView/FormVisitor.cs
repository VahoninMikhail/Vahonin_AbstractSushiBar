﻿using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbstractSushiBarView
{
    public partial class FormVisitor : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormVisitor()
        {
            InitializeComponent();
        }

        private void FormVisitor_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Visitor/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var client = APIClient.GetElement<VisitorViewModel>(response);
                        textBoxFIO.Text = client.VisitorFIO;
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Visitor/UpdElement", new VisitorBindingModel
                    {
                        Id = id.Value,
                        VisitorFIO = textBoxFIO.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Visitor/AddElement", new VisitorBindingModel
                    {
                        VisitorFIO = textBoxFIO.Text
                    });
                }
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
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
