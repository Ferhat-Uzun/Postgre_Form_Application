using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreProje
{
    public partial class FrmUrun : Form
    {
        public FrmUrun()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=dburunler; user Id=postgres; password=12345");

      

        private void FrmUrun_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT * FROM kategoriler", baglanti);    
            DataTable dt = new DataTable();

            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("SELECT * FROM satici", baglanti);
            DataTable dt1 = new DataTable();

            da.Fill(dt);
            da1.Fill(dt1);

            comboBox1.DisplayMember = "kategori_ad";
            comboBox1.ValueMember = "kategori_id";
            comboBox1.DataSource = dt;

            comboBox2.DisplayMember = "satici_ad";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = dt1;

            baglanti.Close();

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("insert into urunler (urun_id,urun_ad,stok,alis_fiyat,satis_fiyat,kategori,satici_id)" +
                "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
            command.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
            command.Parameters.AddWithValue("@p2", TxtAd.Text);
            command.Parameters.AddWithValue("@p3", int.Parse(numericUpDown1.Value.ToString()));
            command.Parameters.AddWithValue("@p4", double.Parse(TxtAlisFiyat.Text));
            command.Parameters.AddWithValue("@p5", double.Parse(TxtSatisFiyat.Text));
            command.Parameters.AddWithValue("@p6", int.Parse(comboBox1.SelectedValue.ToString()));
            command.Parameters.AddWithValue("@p7", int.Parse(comboBox2.SelectedValue.ToString()));
            command.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Kaydı Başarılı");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("Delete From urunler where urun_id = @p1", baglanti);
            command.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
            command.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("update urunler set urun_ad=@p2,stok=@p3,alis_fiyat=@p4,satis_fiyat=@p5,kategori=@p6, satici_id=@p7 " +
                "where urun_id = @p1", baglanti);
            
            command.Parameters.AddWithValue("@p1", int.Parse(TxtId.Text));
            command.Parameters.AddWithValue("@p2", TxtAd.Text);
            command.Parameters.AddWithValue("@p3", int.Parse(numericUpDown1.Value.ToString()));
            command.Parameters.AddWithValue("@p4", double.Parse(TxtAlisFiyat.Text));
            command.Parameters.AddWithValue("@p5", double.Parse(TxtSatisFiyat.Text));
            command.Parameters.AddWithValue("@p6", int.Parse(comboBox1.SelectedValue.ToString()));
            command.Parameters.AddWithValue("@p7", int.Parse(comboBox2.SelectedValue.ToString()));

            command.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Başarıyla Güncellendi");
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("Select * from urunlistesi", baglanti);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void to_kategori_Click(object sender, EventArgs e)
        {
            Form1 kategori= new Form1();
            this.Hide();
            kategori.Show();
        }

        private void to_satici_Click(object sender, EventArgs e)
        {
            FormSatici satici = new FormSatici();
            this.Hide();
            satici.Show();
        }
    }
}
