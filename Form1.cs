using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.BC;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string konfigurasi = "server=localhost;user=root;database=db_apotek_gudang;password=";
        public Form1()
        {
            InitializeComponent();
        }
        public void bersihkan()
        {
            textBox1.Focus();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = 0;
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
        
        public void TambahData()
        {
            try
            {
                MySqlConnection koneksi = new MySqlConnection(konfigurasi);
                koneksi.Open();
                string query = "INSERT INTO tb_suplemen (id_suplemen,kode_barang , nama_barang, kategori, stok, satuan,harga_satuan,batas_minimun) VALUES (@id_suplemen, @kode_barang, @nama_barang, @kategori, @stok, @satuan, @harga_satuan, @batas_minimun)";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                cmd.Parameters.AddWithValue("@kode_barang", textBox1.Text);
                cmd.Parameters.AddWithValue("@nama_barang", textBox2.Text);
                cmd.Parameters.AddWithValue("@kategori", comboBox1.Text);
                cmd.Parameters.AddWithValue("@stok", textBox3.Text);
                cmd.Parameters.AddWithValue("@satuan", textBox4.Text);
                cmd.Parameters.AddWithValue("@harga_satuan", textBox5.Text);
                cmd.Parameters.AddWithValue("@batas_minimum", textBox6.Text);
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
                {
                    MessageBox.Show("Data tidak lengkap.", "Peringatan", MessageBoxButtons.OK);
                }
                else
                {
                   dataGridView1.Rows.Add
                        (textBox1.Text,textBox2.Text, comboBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text).ToString();
                    cmd.ExecuteNonQuery();
                    bersihkan();
                }
   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void UbahData()
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                koneksi.Open();
                string query = "UPDATE tb_suplemen SET kode_barang=@kode_barang, nama_barang=@nama_barang, kategori=@kategori, stok=@stok, satuan=@satuan, harga_satuan=@harga_satuan, batas_minimum=@batas_minimum WHERE kode_barang=@kode_barang";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                cmd.Parameters.AddWithValue("@kode_barang", textBox1.Text);
                cmd.Parameters.AddWithValue("@nama_barang", textBox2.Text);
                cmd.Parameters.AddWithValue("@kategori", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@stok", textBox3.Text);
                cmd.Parameters.AddWithValue("@satuan", textBox4.Text);
                cmd.Parameters.AddWithValue("@harga_satuan", textBox5.Text);
                cmd.Parameters.AddWithValue("@batas_minimum", textBox6.Text);
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
                    cmd.ExecuteNonQuery();
                {
                    MessageBox.Show("Data tidak lengkap.", "Peringatan", MessageBoxButtons.OK);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                koneksi.Open();
                string query = "SELECT * FROM tb_suplemen";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                MySqlDataReader hasil = cmd.ExecuteReader();
                while (hasil.Read())
                {
                    dataGridView1.Rows.Add(hasil.GetString("kode_barang"), hasil.GetString("nama_barang"), hasil.GetString("kategori"), hasil.GetInt32("stok").ToString(), hasil.GetString("satuan"), hasil.GetInt32("harga_satuan").ToString(), hasil.GetInt32("batas_minimum").ToString());
                    string cekstok = hasil.GetInt32("stok").ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bersihkan();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TambahData();
            


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row= dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                comboBox1.SelectedItem = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                textBox4.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
                textBox6.Text = row.Cells[6].Value.ToString();
            }
        }
        private void HapusData()
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                koneksi.Open();
                string query = "DELETE FROM tb_suplemen WHERE kode_barang=@kode_barang";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                cmd.Parameters.AddWithValue("@kode_barang", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus.", "Informasi", MessageBoxButtons.OK);
                bersihkan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }
    }
}  
