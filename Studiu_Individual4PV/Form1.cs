using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Studiu_Individual4PV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=magazin;Integrated Security=True"); 

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username, password;

            username = textBox1.Text; password = textBox2.Text;

            try
            {
                String querry = "SELECT * FROM USERS WHERE User_Name = '" + username + "' AND Password = '" + password + "'";

                MessageBox.Show(textBox1.Text + " - " + textBox2.Text);
                SqlDataAdapter sda = new SqlDataAdapter(querry,conn);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if(dtable.Rows.Count > 0)
                {
                    username = textBox1.Text;
                    password = textBox2.Text;

                    //page that needed to be load next

                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Hide();
                    
                }

                else
                {
                    MessageBox.Show("Invalid login details","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);

                    textBox1.Clear();
                    textBox2.Clear();

                    // to focus username
                    textBox1.Focus();
                }
            }
            catch { MessageBox.Show("ERROR"); }
            finally { conn.Close(); } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();

            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to exit ? ", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }
    }
}
