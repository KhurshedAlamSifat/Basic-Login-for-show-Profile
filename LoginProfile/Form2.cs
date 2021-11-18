using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace LoginProfile
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

       
        private void uploadphoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select image";
            ofd.Filter = "Image File (.png;.jpg) | *.png; *.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = new Bitmap(ofd.FileName);
            }
        }

        string connectionString = "Data Source=ASUS-SIFAT;Initial Catalog=Demo;Integrated Security=True";

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            var Picture = new ImageConverter().ConvertTo(pictureBox.Image, typeof(Byte[]));
            cmd.Parameters.AddWithValue("@Picture", Picture);
            cmd.CommandText = "insert into Demo(ID, Password, Picture) values('" + textBoxID1.Text + "', '" + textBoxPassword2.Text + "', @Picture)";
            if (cmd.ExecuteNonQuery()>0)
            {
                new Form1().Show();
                this.Hide();
            }
            else { MessageBox.Show("Error"); }
            con.Close();
        }
    }
}
