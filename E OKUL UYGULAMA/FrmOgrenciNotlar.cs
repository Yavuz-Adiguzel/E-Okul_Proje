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

namespace E_OKUL_UYGULAMA
{
    public partial class FrmOgrenciNotlar : Form
    {
        public FrmOgrenciNotlar()
        {
            InitializeComponent();
        }
        public SqlConnection baglanti = new SqlConnection("Data Source=ASUS\\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True");
        public string numara;
        private void FrmOgrenciNotlar_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select DERSAD,SINAV1,SINAV2,SINAV3,PROJE,ORTALAMA,DURUM FROM TBLDERSLER INNER JOIN TBLNOTLAR ON TBLDERSLER.DERSID=TBLNOTLAR.DERSID where OGRID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("SELECT OGRAD,OGRSOYAD FROM TBLOGRENCILER WHERE OGRID=@P2", baglanti);
            komut1.Parameters.AddWithValue("@p2", numara);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                this.Text = dr1[0]+ " " + dr1[1];
            }
        }
    }
}
