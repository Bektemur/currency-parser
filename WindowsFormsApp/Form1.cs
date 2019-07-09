using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using HtmlAgilityPack;
namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        public new List<Money> AddCourse()
        {
            var request = new HttpRequest();
            string content = request.Get("http://www.cbr.ru/currency_base/daily/").ToString();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(content);
            var node = doc.DocumentNode.SelectSingleNode("//table");
            var query = from table in doc.DocumentNode.SelectNodes("//tr").Cast<HtmlNode>()
                        select new { CellText = table.InnerText };
            string data = "";
            foreach (var cell in query)
            {
                data += cell.CellText.Replace("     ", String.Empty).Replace("\r", String.Empty).Trim(' ') + "\n";     
            }
            List<Money> money = new List<Money>();
            String[] words = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 5; i < words.Length; i += 5)
            {
                money.Add(new Money(Convert.ToInt32(words[i].ToString()), words[i + 1], words[i + 2], words[i + 3], (float.Parse(words[i + 4]))));  
            }
            return money;      
        }    

        private void Form1_Load(object sender, EventArgs e)
        {
            var list = AddCourse();
            dataGridView1.ColumnCount = 5;
            dataGridView1.RowCount = list.Count;
            var data = AddCourse();
            for (int i = 0; i < data.Count; i++)
            {
                comboBox2.Items.Add(data[i].code_word);
            }
            comboBox2.SelectedIndex = 1;
            dataGridView1.Columns[0].HeaderText = "Цифр. код";
            dataGridView1.Columns[1].HeaderText = "Букв. код";
            dataGridView1.Columns[2].HeaderText = "Единиц";
            dataGridView1.Columns[3].HeaderText = "Валюта";
            dataGridView1.Columns[4].HeaderText = "Курс";

            List<Money> money = new List<Money>();
            for (int i = 0; i < list.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = list[i].code;
                dataGridView1.Rows[i].Cells[1].Value = list[i].code_word;
                dataGridView1.Rows[i].Cells[2].Value = list[i].unit;
                dataGridView1.Rows[i].Cells[3].Value = list[i].currency;
                dataGridView1.Rows[i].Cells[4].Value = list[i].value;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = AddCourse();
            string code_word = comboBox2.SelectedItem.ToString();
            decimal value1 = decimal.Parse(textBox1.Text);
            decimal value2 = 0;
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() == code_word)
                {   
                    value2 =  (value1/ decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()))* (int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                    break;
                }
            }
            textBox2.Text = Math.Round(value2,2).ToString();
            
        }
    }
}

