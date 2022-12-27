using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PostgreProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=dburunler; user Id=postgres; password=12345");

        private void BtnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM kategoriler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command= new NpgsqlCommand("insert into kategoriler (kategori_ad) values (@p1,@p2)",baglanti);
            command.Parameters.AddWithValue("@p1", int.Parse(TxtKategoriId.Text));
            command.Parameters.AddWithValue("@p2", TxtAd.Text);
            command.ExecuteNonQuery(); 
            baglanti.Close();
            MessageBox.Show("Kategori Eklendi");

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("Delete From kategoriler where kategori_id = @p1", baglanti);
            command.Parameters.AddWithValue("@p1", int.Parse(TxtKategoriId.Text));
            command.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori Silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("update kategoriler set kategori_ad=@p2 where @p1=kategori_id", baglanti);

            command.Parameters.AddWithValue("@p1", int.Parse(TxtKategoriId.Text));
            command.Parameters.AddWithValue("@p2", TxtAd.Text);

            command.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori Başarıyla Güncellendi");
        }

        private void to_urun_Click(object sender, EventArgs e)
        {
            FrmUrun frmUrun = new FrmUrun();   
            this.Hide();
            frmUrun.Show();
        }

        private void to_satici_Click(object sender, EventArgs e)
        {
            FormSatici frm = new FormSatici();   
            this.Hide();
            frm.Show();
        }
    }
}
