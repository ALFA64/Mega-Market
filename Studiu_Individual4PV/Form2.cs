using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static Studiu_Individual4PV.Form4;

namespace Studiu_Individual4PV
{
    public partial class Form2 : Form
    {
        private static SqlConnection sqlcon = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=magazin;Integrated Security=True");
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'magazinDataSet.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.magazinDataSet.Table);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.tableBindingSource.EndEdit();
                this.tableTableAdapter.Update(this.magazinDataSet.Table);
                MessageBox.Show("Salvare completă ! ");


            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }


        private int GetMaxId()
        {
            int maxId = 0;
            string query = "SELECT MAX(Id) FROM [Table]";
            SqlCommand cmd = new SqlCommand(query, sqlcon);

            try
            {
                sqlcon.Open();
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    maxId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                sqlcon.Close();
            }
            return maxId;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            // Calculează noul ID
            int newId = GetMaxId() + 1;

            // Adaugă o nouă înregistrare prin BindingSource
            DataRowView newRow = (DataRowView)this.tableBindingSource.AddNew();
            newRow["Id"] = newId;  // Setează noul ID calculat

            // Textbox-urile și alte controale sunt deja legate prin BindingSource,
            // așa că nu trebuie să setezi explicit valorile aici.

            try
            {
                // Salvează schimbările în baza de date
                this.tableBindingSource.EndEdit();
                this.tableTableAdapter.Update(this.magazinDataSet.Table);
                MessageBox.Show("Înregistrare adăugată cu succes!", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugarea înregistrării: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Ștergerea înregistrării curente
                this.tableBindingSource.RemoveCurrent();
                MessageBox.Show("Ștergerea a fost executata cu succes !", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergere: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.tableBindingSource.MoveNext();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.tableBindingSource.MovePrevious();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            textBox1.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();

            form1.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.tableBindingSource.MoveLast();
        }






        public void ShowCheapestProduct()
        {
            string query = "SELECT * FROM dbo.GetCheapestProductDetails()";
            SqlCommand command = new SqlCommand(query, sqlcon);

            try
            {
                sqlcon.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string message = "Cel mai ieftin produs:\n";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        message += $"{reader.GetName(i)}: {reader[i].ToString()}\n";
                    }
                    MessageBox.Show(message, "Informații Produs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la accesarea bazei de date: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public static void ShowProductWithLeastSugar()
        {
            string query = "SELECT TOP 1 Name, Zahar FROM [Table] ORDER BY Zahar ASC";

            // Asigură-te că sqlcon este deschisă
            if (sqlcon.State != ConnectionState.Open)
                sqlcon.Open();

            SqlCommand command = new SqlCommand(query, sqlcon);

            try
                {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string productName = reader["Name"].ToString();
                        decimal sugarPercentage = Convert.ToDecimal(reader["Zahar"]);
                        MessageBox.Show($"Produsul cu cel mai mic procent de zahăr este: {productName} cu {sugarPercentage}% zahăr.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Nu există produse în baza de date.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Eroare la accesarea bazei de date: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Închide conexiunea după finalizarea interogării
                    if (sqlcon.State == ConnectionState.Open)
                        sqlcon.Close();
                }

        }

        public static void ShowLowPrice()
        {
            string query = "SELECT TOP 1 Name, Price FROM [Table] ORDER BY Price ASC";

            // Asigură-te că sqlcon este deschisă
            if (sqlcon.State != ConnectionState.Open)
                sqlcon.Open();

            SqlCommand command = new SqlCommand(query, sqlcon);

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string productName = reader["Name"].ToString();
                        decimal Price = Convert.ToDecimal(reader["Price"]);
                        MessageBox.Show($"Produsul cu cel mai mic preț este: {productName} cu {Price} MDL .", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Nu există produse în baza de date.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Eroare la accesarea bazei de date: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Închide conexiunea după finalizarea interogării
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
            }
        }


        private void btnLoadData_Click()
        {
                try
                {
                    string query = "SELECT * FROM [Table] WHERE Destinatie = 'Copii'";
                    SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare: " + ex.Message);
                }
        }

        public void ApplyFilterData(FilterData filter)
        {
            using (SqlConnection con = sqlcon)
            {
                con.Open();
                // Utilizăm interogarea corectă pentru o funcție tabelară
                string query = $@"SELECT * FROM dbo.GetFilteredProducts(
            @Price, 
            @SugarMin, 
            @SugarMax, 
            @AdditiveMin, 
            @AdditiveMax, 
            @DestinedFor, 
            @FruitType
            @ProductName, 
        )";

                SqlCommand cmd = new SqlCommand(query, con);
                // Nu este o procedură stocată, așa că următoarea linie este incorectă și trebuie eliminată:
                // cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductName", filter.ProductName);
                cmd.Parameters.AddWithValue("@SugarMin", filter.SugarMin);
                cmd.Parameters.AddWithValue("@SugarMax", filter.SugarMax);
                cmd.Parameters.AddWithValue("@AdditiveMin", filter.AdditiveMin);
                cmd.Parameters.AddWithValue("@AdditiveMax", filter.AdditiveMax);
                cmd.Parameters.AddWithValue("@Price", filter.Price);
                cmd.Parameters.AddWithValue("@DestinedFor", filter.DestinedFor);
                cmd.Parameters.AddWithValue("@FruitType", filter.FruitType);

                dataGridView1.DataSource = cmd; // Asumând că există un DataGridView cu acest nume
            }
        }


        private void OpenFrame2()
        {
            Form4 frame2 = new Form4();
            frame2.Owner = this;  // Setează Frame1 ca owner pentru Frame2
            frame2.Show();
        }




        public void ShowAveragePrice()
        {
            // Interogarea SQL care calculează media prețurilor
            string query = "SELECT AVG(Price) AS Media FROM [Table]";

            using (SqlConnection con = sqlcon)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Executarea interogării și citirea rezultatelor
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        decimal averagePrice = Convert.ToDecimal(result);
                        MessageBox.Show($"Media prețurilor este: {averagePrice:F2}", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Nu s-au găsit date pentru a calcula media.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Eroare SQL: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }

            private void button4_Click(object sender, EventArgs e)
        {
            switch(comboBox3.SelectedIndex)
            {
                case 0: { MessageBox.Show("Introdu o optiune de executie", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); break; }
                case 1: { Form3 form3 = new Form3(); form3.Show(); break; }
                case 2: { ShowCheapestProduct(); break; }
                case 3: { ShowProductWithLeastSugar(); break; }
                case 4: { ShowLowPrice(); break; }
                case 5: { btnLoadData_Click(); break; }
                case 6: { OpenFrame2(); break; }
                case 7: { ShowAveragePrice(); break; }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"SELECT * FROM [Table]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, sqlcon);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea datelor: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
