﻿using System;
using System.Windows.Forms;
using AbstractSushiBarService.ViewModels;
using AbstractSushiBarService.BindingModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace AbstractSushiBarView
{
    public partial class FormIngredient : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormIngredient()
        {
            InitializeComponent();
        }

        private void FormIngredient_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Ingredient/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var ingredient = APIClient.GetElement<IngredientViewModel>(response);
                        textBoxName.Text = ingredient.IngredientName;
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
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Ingredient/UpdElement", new IngredientBindingModel
                    {
                        Id = id.Value,
                        IngredientName = textBoxName.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Ingredient/AddElement", new IngredientBindingModel
                    {
                        IngredientName = textBoxName.Text
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
