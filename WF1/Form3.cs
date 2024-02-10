using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace WF1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            SetRoundedShape(panel1, 30);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "гРУЗМАП_СОТРУДНИКИDataSet3.Должности". При необходимости она может быть перемещена или удалена.
            this.должностиTableAdapter.Fill(this.гРУЗМАП_СОТРУДНИКИDataSet3.Должности);         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox1.Text != "" && textBox3.Text != "" && textBox11.Text != "" && textBox7.Text != ""
                 && textBox4.Text != "" && textBox10.Text != "" && textBox9.Text != "")
            {
                if (IsValidEmail(textBox7.Text) == true)
                {
                    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                    SqlConnection str = new SqlConnection(@"Data Source=WINDOWS-ПК;Initial Catalog=""ГРУЗМАП СОТРУДНИКИ"";Integrated Security=True");
                    str.Open();
                    SqlCommand command = new SqlCommand($"INSERT INTO [dbo].[Сотрудники] (Имя, Фамилия, Отчество, Номер_телефона, Почта, Серия_паспорта, Номер_паспорта,ID_Должности,Персональный_код) values ('" + ti.ToTitleCase(textBox2.Text) + "' , '" + ti.ToTitleCase(textBox1.Text) + "' , '" + ti.ToTitleCase(textBox3.Text) + "','" + textBox11.Text + "' , '" + textBox7.Text.ToLower() + "' , '" + textBox4.Text + "', '" + textBox10.Text + "', '" + comboBox2.SelectedValue.ToString() + "' , '" + textBox9.Text + "')", str);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Сотрудник успешно добавлен!");
                    str.Close();
                    this.Hide();            
                }
                else
                {
                    MessageBox.Show("Вы указали неверную почту!");
                }
            }     
            else
                MessageBox.Show("Пожалуйста заполните все поля!");

        }
      
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        static void SetRoundedShape(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random res = new Random();
            String str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int size = 6;
            String randomstring = "";
            for (int i = 0; i < size; i++)
            {
                int x = res.Next(str.Length);
                randomstring = randomstring + str[x];
            }
            textBox9.Text = randomstring;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (sender.Equals(textBox1))
                        textBox1.Focus();
                }
                return;
            }
            e.Handled = true;
        }
    }
}
