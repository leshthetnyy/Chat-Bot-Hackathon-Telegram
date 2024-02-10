using System;
using System.Drawing;
using System.Windows.Forms;



namespace WF1
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            SetRoundedShape(panel1, 30);
        }
        private void button1_Click(object sender, EventArgs e)
        {
          
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                this.Hide();           
                Form2 adm = new Form2();
                adm.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            
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
    }
}
