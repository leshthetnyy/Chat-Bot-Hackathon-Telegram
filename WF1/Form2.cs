using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Globalization;

namespace WF1
{
    public partial class Form2 : Form
    {
        string surname;
        string name;
        string middlename;
        int ser_pass;
        int num_pass;
        string dolj;
        string number_phone;
        string email;
        string kod;
        public Form2()
        {
            InitializeComponent();
            SetRoundedShape(panel1, 30);
            SetRoundedShape(panel2, 30);
            SetRoundedShape(panel3, 30);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "гРУЗМАП_СОТРУДНИКИDataSet2.Должности". При необходимости она может быть перемещена или удалена.
            this.должностиTableAdapter2.Fill(this.гРУЗМАП_СОТРУДНИКИDataSet2.Должности);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "гРУЗМАП_СОТРУДНИКИDataSet1.Должности". При необходимости она может быть перемещена или удалена.
            this.должностиTableAdapter1.Fill(this.гРУЗМАП_СОТРУДНИКИDataSet1.Должности);
            SqlConnection str = new SqlConnection(@"Data Source=WINDOWS-ПК;Initial Catalog=""ГРУЗМАП СОТРУДНИКИ"";Integrated Security=True");
            String query = "SELECT ID_Сотрудника,Имя,Фамилия,Отчество,+Номер_телефона,Почта,Серия_паспорта,Номер_паспорта,Должность,Персональный_код FROM [dbo].[Сотрудники] JOIN Должности ON Должности.ID_Должности = Сотрудники.ID_Должности";
            str.Open();
            SqlDataAdapter a = new SqlDataAdapter(query, str);
            DataSet ds = new DataSet();
            a.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            str.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            GetInfoUser(userID);
            textBox2.Visible = true;
            textBox7.Visible = true;
            textBox1.Visible = true;
            textBox4.Visible = true;
            textBox9.Visible = true;
            textBox3.Visible = true;
            textBox10.Visible = true;
            textBox11.Visible = true;
            label2.Visible = true;
            label7.Visible = true;
            label9.Visible = true;
            label6.Visible = true;
            label10.Visible = true;
            label12.Visible = true;
            label3.Visible = true;
            label5.Visible = true;
            label13.Visible = true;
            comboBox2.Visible = true;
            button5.Visible = true;
            button4.Visible = true;
            button2.Enabled = true;
            label4.Visible = false;
            button2.Visible = true;

        }

        public void GetInfoUser(int id)
        {
            SqlConnection str = new SqlConnection(@"Data Source=WINDOWS-ПК;Initial Catalog=""ГРУЗМАП СОТРУДНИКИ"";Integrated Security=True");
            str.Open();
            SqlCommand command = new SqlCommand("SELECT ID_Сотрудника,Имя,Фамилия,Отчество,+Номер_телефона,Почта,Серия_паспорта,Номер_паспорта,Должность,Персональный_код FROM [dbo].[Сотрудники] JOIN Должности ON Должности.ID_Должности = Сотрудники.ID_Должности WHERE ID_Сотрудника = @id", str);
            command.Parameters.AddWithValue("id", id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                name = reader[1].ToString();
                surname = reader[2].ToString();
                middlename = reader[3].ToString();
                number_phone = reader[4].ToString();
                email = reader[5].ToString();
                ser_pass = Convert.ToInt32(reader[6].ToString());
                num_pass = Convert.ToInt32(reader[7].ToString());
                dolj = reader[8].ToString();
                kod = reader[9].ToString();
            }
            reader.Close();
            str.Close();
            textBox2.Text = name;
            textBox1.Text = surname;
            textBox3.Text = middlename;
            textBox11.Text = number_phone;
            textBox7.Text = email;
            textBox4.Text = ser_pass.ToString();
            textBox10.Text = num_pass.ToString();
            comboBox2.Text = dolj;
            textBox9.Text = kod;
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

        private void button2_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            if (textBox2.Text != "" && textBox1.Text != "" && textBox3.Text != "" && textBox11.Text != "" && textBox7.Text != ""
                && textBox4.Text != "" && textBox10.Text != "" && textBox9.Text != "")
            {
                if (IsValidEmail(textBox7.Text) == true)
                    EditUser(userID);
                else
                    MessageBox.Show("Вы указали неверную почту!");
            }
            else
                MessageBox.Show("Заполните все поля!");
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

        public void EditUser(int id)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            SqlConnection str = new SqlConnection(@"Data Source=WINDOWS-ПК;Initial Catalog=""ГРУЗМАП СОТРУДНИКИ"";Integrated Security=True");
            str.Open();
            SqlCommand command = new SqlCommand($"UPDATE [dbo].[Сотрудники] SET [Имя] = '" + ti.ToTitleCase(textBox2.Text) + "', [Фамилия] = '" + ti.ToTitleCase(textBox1.Text) + "', [Отчество] = '" + ti.ToTitleCase(textBox3.Text) + "', [Номер_телефона] = '" + textBox11.Text + "', [Почта] = '" + textBox7.Text.ToLower() + "', [Серия_паспорта] = '" + textBox4.Text + "', [Номер_паспорта] = '" + textBox10.Text + "',  [ID_Должности] = '" + comboBox2.SelectedValue.ToString() + "',  [Персональный_код] = '" + textBox9.Text + "' WHERE ID_Сотрудника = @id", str);
            command.Parameters.AddWithValue("id", Convert.ToString(id));
            command.ExecuteNonQuery();
            MessageBox.Show("Информация успешно изменена!");
            str.Close();
            GetInfoDB();
        }


        public void GetInfoDB()
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "гРУЗМАП_СОТРУДНИКИDataSet2.Должности". При необходимости она может быть перемещена или удалена.
            this.должностиTableAdapter2.Fill(this.гРУЗМАП_СОТРУДНИКИDataSet2.Должности);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "гРУЗМАП_СОТРУДНИКИDataSet1.Должности". При необходимости она может быть перемещена или удалена.
            this.должностиTableAdapter1.Fill(this.гРУЗМАП_СОТРУДНИКИDataSet1.Должности);
            SqlConnection str = new SqlConnection(@"Data Source=WINDOWS-ПК;Initial Catalog=""ГРУЗМАП СОТРУДНИКИ"";Integrated Security=True");
            String query = "SELECT ID_Сотрудника,Имя,Фамилия,Отчество,+Номер_телефона,Почта,Серия_паспорта,Номер_паспорта,Должность,Персональный_код FROM [dbo].[Сотрудники] JOIN Должности ON Должности.ID_Должности = Сотрудники.ID_Должности";
            str.Open();
            SqlDataAdapter a = new SqlDataAdapter(query, str);
            DataSet ds = new DataSet();
            a.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            str.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Visible == true)
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
            else
            {
                MessageBox.Show("Пользователь не выбран!");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            textBox7.Visible = false;
            textBox1.Visible = false;
            textBox4.Visible = false;
            textBox9.Visible = false;
            textBox3.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            label2.Visible = false;
            label7.Visible = false;
            label9.Visible = false;
            label6.Visible = false;
            label10.Visible = false;
            label12.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label13.Visible = false;
            comboBox2.Visible = false;
            button5.Visible = false;
            button4.Visible = false;
            button2.Enabled = false;
            label4.Visible = true;
            button2.Visible = false;
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b')
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form3 newsotr = new Form3();
            newsotr.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            textBox2.Visible = false;
            textBox7.Visible = false;
            textBox1.Visible = false;
            textBox4.Visible = false;
            textBox9.Visible = false;
            textBox3.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            label2.Visible = false;
            label7.Visible = false;
            label9.Visible = false;
            label6.Visible = false;
            label10.Visible = false;
            label12.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label13.Visible = false;
            comboBox2.Visible = false;
            button5.Visible = false;
            button4.Visible = false;
            button2.Enabled = false;
            label4.Visible = true;
            button2.Visible = false;
            panel3.Visible = true;
            button8.Visible = true;
            button9.Visible = true;

            label9.Visible = true;
            label11.Visible = true;
            label8.Visible = true;
            SUserAsync(userID);

        }

        public async Task SUserAsync(int id)
        {
            SqlConnection str = new SqlConnection(@"Data Source=WINDOWS-ПК;Initial Catalog=""ГРУЗМАП СОТРУДНИКИ"";Integrated Security=True");
            str.Open();
            SqlCommand command = new SqlCommand($"SELECT Фамилия +' ' + Имя + ' ' + Отчество FROM Сотрудники WHERE ID_Сотрудника = @id", str);
            command.Parameters.AddWithValue("id", Convert.ToString(id));
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (await reader.ReadAsync())
            {
                object name = reader.GetValue(0);
                string w = name.ToString();
                label8.Text = "Вы уверены что хотите уволить сотрудника:\n                 " + w + " ?";
            }

        }






        public void DeleteUser(int id)
        {
         
            SqlConnection str = new SqlConnection(@"Data Source=WINDOWS-ПК;Initial Catalog=""ГРУЗМАП СОТРУДНИКИ"";Integrated Security=True");
            str.Open();
            SqlCommand command = new SqlCommand($"DELETE [dbo].[Сотрудники] WHERE ID_Сотрудника = @id", str);
            command.Parameters.AddWithValue("id", Convert.ToString(id));
            command.ExecuteNonQuery();
            MessageBox.Show("Сотрудник успешно уволен!");
            panel3.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label11.Visible = false;
            str.Close();
            GetInfoDB();
        }

    private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            textBox7.Visible = false;
            textBox1.Visible = false;
            textBox4.Visible = false;
            textBox9.Visible = false;
            textBox3.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            label2.Visible = false;
            label7.Visible = false;
            label9.Visible = false;
            label6.Visible = false;
            label10.Visible = false;
            label12.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label13.Visible = false;
            comboBox2.Visible = false;
            button5.Visible = false;
            button4.Visible = false;
            button2.Enabled = false;
            label4.Visible = true;
            button2.Visible = false;
            // TODO: данная строка кода позволяет загрузить данные в таблицу "гРУЗМАП_СОТРУДНИКИDataSet2.Должности". При необходимости она может быть перемещена или удалена.
            this.должностиTableAdapter2.Fill(this.гРУЗМАП_СОТРУДНИКИDataSet2.Должности);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "гРУЗМАП_СОТРУДНИКИDataSet1.Должности". При необходимости она может быть перемещена или удалена.
            this.должностиTableAdapter1.Fill(this.гРУЗМАП_СОТРУДНИКИDataSet1.Должности);
            SqlConnection str = new SqlConnection(@"Data Source=WINDOWS-ПК;Initial Catalog=""ГРУЗМАП СОТРУДНИКИ"";Integrated Security=True");
            String query = "SELECT ID_Сотрудника,Имя,Фамилия,Отчество,+Номер_телефона,Почта,Серия_паспорта,Номер_паспорта,Должность,Персональный_код FROM [dbo].[Сотрудники] JOIN Должности ON Должности.ID_Должности = Сотрудники.ID_Должности";
            str.Open();
            SqlDataAdapter a = new SqlDataAdapter(query, str);
            DataSet ds = new DataSet();
            a.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            str.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            DeleteUser(userID);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label11.Visible = false;
        }
    }
}
