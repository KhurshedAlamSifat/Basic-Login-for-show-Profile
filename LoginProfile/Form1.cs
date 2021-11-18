using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginProfile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string id;
        public static Image dp;
        public static string pass;
        private void buttonCreatAnAccount_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

       

        public Image getPhoto(byte[] photo)
        {
            using(MemoryStream ms = new MemoryStream(photo))
            {
                return Image.FromStream(ms);
            }
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=ASUS-SIFAT;Initial Catalog=Demo;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select *FROM Demo WHERE ID COLLATE Latin1_general_CS_AS = @ID  and Password COLLATE Latin1_general_CS_AS =@Password ", connection);

            connection.Open();
            cmd.Parameters.Add(new SqlParameter("ID ", textBoxID.Text));
            cmd.Parameters.Add(new SqlParameter("Password", textBoxPassword.Text));

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            if (dt.Rows.Count > 0)
                {
                    id = dt.Rows[0]["ID"].ToString();
                    dp = getPhoto((byte[])dt.Rows[0]["Picture"]);
                    pass = dt.Rows[0]["Password"].ToString();
                    MessageBox.Show("LogIn successful !");
                    new Form3().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Log in failed,doesn't match email or Password", "Enter valide information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Close();
            
        }
    }
}
