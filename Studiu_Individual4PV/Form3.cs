using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Studiu_Individual4PV
{
    public partial class Form3 : Form
    {

        private string conectionString = @"Data Source=(localdb)\Local;Initial Catalog=magazin;Integrated Security=True";

        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteProduct(int  productID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM [Table] WHERE Id = @productID";

                    using (SqlCommand command = new  SqlCommand(query, connection)) {
                        command.Parameters.AddWithValue("@productID", productID);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Produsul a fost șters cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Nu s-a putut șterge produsul.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }

                }
            }catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergerea produsului: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(int.TryParse(textBox1.Text, out int value))
            {
                if(value > 0)
                {
                    DeleteProduct(value);
                }
                else
                {
                    MessageBox.Show("Vă rugăm să introduceți un cod de produs valid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să introduceți un cod de produs valid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
