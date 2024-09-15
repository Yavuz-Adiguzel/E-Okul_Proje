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
using System.Data.SqlClient;

namespace E_OKUL_UYGULAMA
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        public SqlConnection baglanti = new SqlConnection("Data Source=ASUS\\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True");
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            dataGridView1.DataSource = ds.OgrenciListesi();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLKULUPLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbKulup.DisplayMember = "KULUPAD";
            CmbKulup.ValueMember = "KULUPID";
            CmbKulup.DataSource = dt;
            baglanti.Close();


        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
        }
        string c = "";
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            

            if (radioButton1.Checked == true)
            {
                c = "KIZ";
            }
            if (radioButton2.Checked == true)
            {
                c = "ERKEK";
            }
            ds.OgrenciEkle(TxtAd.Text, TxtSoyad.Text, c, byte.Parse(CmbKulup.SelectedValue.ToString()));
            MessageBox.Show("Öğrenci Ekleme Yapıldı");
        }

      

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(TxtOgrId.Text));
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {

            ds.OgrenciGuncelle(TxtAd.Text, TxtSoyad.Text,byte.Parse(CmbKulup.SelectedValue.ToString()), c,int.Parse(TxtOgrId.Text));
            MessageBox.Show("Öğrenci Güncellendi");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "KIZ")
            {
                radioButton1.Checked = true;
            }
            if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "ERKEK")
            {
                radioButton2.Checked = true;
            }
            CmbKulup.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtOgrId.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
          

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                c = "KIZ";
            }
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
            if (radioButton2.Checked == true)
            {
                c = "ERKEK";
            }
        }
        private void CmbKulup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Bağlantının açık olup olmadığını kontrol et
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                SqlCommand komut = new SqlCommand("UPDATE TBLKULUPLER SET KULUPAD=@P1 WHERE KULUPID=@P2", baglanti);
                komut.Parameters.AddWithValue("@P1", CmbKulup.Text);
                komut.Parameters.AddWithValue("@P2", TxtOgrId.Text);

                komut.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantı her durumda kapatılır
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciGetir(TxtAra.Text);
        }
    }
}
