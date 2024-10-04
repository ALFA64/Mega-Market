using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Studiu_Individual4PV
{
    public partial class Form4 : Form
    {
        public class FilterData
        {
            public string ProductName { get; set; }
            public decimal SugarMin { get; set; }
            public decimal SugarMax { get; set; }
            public decimal AdditiveMin { get; set; }
            public decimal AdditiveMax { get; set; }
            public decimal Price { get; set; }
            public string DestinedFor { get; set; }
            public string FruitType { get; set; }
        }

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilterData filter = new FilterData()
            {
                ProductName = textBox1.Text,
                SugarMin = Convert.ToDecimal(textBox2.Text),
                SugarMax = Convert.ToDecimal(textBox6.Text),
                AdditiveMin = Convert.ToDecimal(textBox3.Text),
                AdditiveMax = Convert.ToDecimal(textBox5.Text),
                Price = Convert.ToDecimal(textBox4.Text),
                DestinedFor = comboBox1.SelectedItem.ToString(),
                FruitType = comboBox2.SelectedItem.ToString()
            };

            // Apelarea metodei ApplyFilterData din Frame1
            var frame1 = this.Owner as Form2;
            if (frame1 != null)
            {
                frame1.ApplyFilterData(filter);
            }
            else
            {
                MessageBox.Show("Nu s-a putut accesa Form2.");
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
