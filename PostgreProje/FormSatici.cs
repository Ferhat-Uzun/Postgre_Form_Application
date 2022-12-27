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

namespace PostgreProje
{
    public partial class FormSatici : Form
    {
        public FormSatici()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=dburunler; user Id=postgres; password=12345");


        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("insert into satici values (@p1,@p2)", baglanti);
            command.Parameters.AddWithValue("@p1", int.Parse(TxtSaticiId.Text));
            command.Parameters.AddWithValue("@p2", TxtAd.Text);
            command.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Satıcı Eklendi");

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("Delete From satici where id = @p1", baglanti);
            command.Parameters.AddWithValue("@p1", int.Parse(TxtSaticiId.Text));
            command.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Satıcı Silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("update satici set satici_ad=@p2 where @p1=id", baglanti);

            command.Parameters.AddWithValue("@p1", int.Parse(TxtSaticiId.Text));
            command.Parameters.AddWithValue("@p2", TxtAd.Text);

            command.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Satıcı Başarıyla Güncellendi");
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM satici";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void to_urun_Click(object sender, EventArgs e)
        {
            FrmUrun frmUrun = new FrmUrun();
            this.Hide();
            frmUrun.Show();
        }

        private void to_kategori_Click(object sender, EventArgs e)
        {
            Form1 frmKategori = new Form1();
            this.Hide();
            frmKategori.Show();
        }
    }
}
