using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kayit_ekrani
{
    public partial class Form1: Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //BU KOD İLE BBAĞLANTILI OLARAK EKLEME YAPILABİLİR
            string str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SARIGÖL\\Desktop\\c# tekrar\\Kayit_ekrani\\Kayit_ekrani\\KullaniciDB.mdf\";Integrated Security=True";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Person(UserName,UserSurname,UserMail,UserPasword) Values(@p1,@p2,@p3,@p4)", con);
            string psw = Sha256Converter.ComputeSha256Hash(textBox4.Text);
            cmd.Parameters.AddWithValue("@p1",textBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", textBox3.Text);
            cmd.Parameters.AddWithValue("@p4", psw);
            SqlDataReader dr = cmd.ExecuteReader();
            MessageBox.Show("kayıt başarılı");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";        
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SARIGÖL\\Desktop\\c# tekrar\\Kayit_ekrani\\Kayit_ekrani\\KullaniciDB.mdf\";Integrated Security=True";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Person where UserMail=@p1 and UserPasword=@p2",con);
            string psw = Sha256Converter.ComputeSha256Hash(textBox8.Text);
            cmd.Parameters.AddWithValue("@p1",textBox7.Text);
            cmd.Parameters.AddWithValue("@p2",psw);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count>0)
            {
                MessageBox.Show("Başarılı giriş");
            }
            else
            {
                MessageBox.Show("Hatalı giriş");
            }
        }
    }
}
