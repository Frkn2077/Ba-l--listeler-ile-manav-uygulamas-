using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Furkan_kocyigit
{
    public partial class Form1 : Form
    {
        //Ad-Soyad:Furkan Koçyiğit Bölüm:Bilgisayar Mühendisliği No:233908008
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Ürün Kodu";
            dataGridView1.Columns[0].Width = 170;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[1].Name = "Ürün Adı";
            dataGridView1.Columns[2].Width = 185;
            dataGridView1.Columns[2].Name = "Ürün Kg Fiyatı";
            
        }
       
        public class Dugum 
        {
            public string u_kodu;
            public string u_adı;
            public int u_fiyatı;
            public Dugum sonraki;
            public Dugum önceki;
            public Dugum(string u_kodu,string u_adı,int u_fiyatı ) 
            {
                this.u_adı = u_adı;
                this.u_kodu = u_kodu;
                this.u_fiyatı = u_fiyatı;
                this.önceki = null;
                this.sonraki = null;
            }
        }
        public class Bagliliste 
        {
            Dugum bas;
            Dugum son;
            public Bagliliste() 
            {
                this.bas = null;
                this.son = null;
            }
            public void Ekle(String  a,string b,int c) 
            {
                Dugum dugum = new Dugum(a,b,c);
                if (bas == null) 
                {
                    bas = dugum;
                    son = dugum;
                }
                else 
                {
                    bas.önceki = dugum;
                    dugum.sonraki = bas;
                    bas = dugum;
                    bas.önceki = null;
                   
                }

            }
            public void bul(String  a,TextBox txt,TextBox txt1,TextBox txt2)
            { 
                Dugum temp = bas;
                bool k1 = false;
                string m3 = "MNV-" + a;
                while (temp != null)
                {

                    if (m3 == temp.u_kodu)
                    {
                        txt.Text = a;
                        txt1.Text = temp.u_adı;
                        txt2.Text = temp.u_fiyatı + " ";
                        k1 = true;
                        break;
                    }
                    temp=temp.sonraki;
                }
                if (k1 == false)
                {
                    MessageBox.Show("Girdiğiniz ürün koduna ait bir bilgi bulanamadı.Lütfen tekrar deneyiniz ");
                }
               
            }
            public void U_sil(String b) 
            {
                Dugum d3 = bas;
                bool k1 = false;
                if (bas == null) 
                {
                    MessageBox.Show("Ulaşmaya çalıştığınız liste boş.Lütfen ürün ekleyip tekrar deneyin");
                    return;
                }
                if (b == bas.u_kodu) 
                {
                    k1= true;
                    bas = bas.sonraki;
                    if (bas != null) 
                    {
                        bas.önceki = null;
                    }
                    getir(k1);
                    return;
                    
                }
                else if (b == son.u_kodu)
                {
                    k1= true;
                    son = son.önceki;
                    son.sonraki = null;
                    getir(k1);
                    return;
                }
                while (d3 != null) 
                {
                    if (b == d3.u_kodu) 
                    {
                        k1= true;
                        d3.önceki.sonraki = d3.sonraki;
                        d3.sonraki.önceki = d3.önceki;
                        getir(k1);
                        break;
                    }
                    d3 = d3.sonraki;
                }
                if(k1 == false) 
                {
                    getir(k1);
                }
               

            }
            public void getir(bool k) 
            {
                if (k == true)
                {
                    MessageBox.Show("Girmiş olduğunuz Ürün başarıyla silindi.");
                    return;
                }
                else
                {
                    MessageBox.Show("Girmiş olduğunuz Ürün bilgisine ait ürün bulunamadı.");
                    return;
                }
            }
            public void guncelle(string a,TextBox txt,TextBox txt1) 
            {
               Dugum tmp1 = bas;
                while (tmp1 != null) 
                {
                    if (tmp1.u_kodu==a)
                    {
                       tmp1.u_adı=txt.Text;
                       tmp1.u_fiyatı = Convert.ToInt32(txt1.Text);
                        MessageBox.Show("Girdiğiniz ürünün bilgileri Güncellendi");
                        break;
                    }
                    tmp1 = tmp1.sonraki;
                }
            } 
            public int  hesapla() 
            {
                Dugum d1 = bas;
                int uzunluk = 0;
                while(d1.sonraki != null) 
                {
                    uzunluk++;
                    d1 = d1.sonraki;
                }
                return uzunluk;
            }

            public void Listele(DataGridView dgv ) 
            {
                String s1;
                string s2;
                int s3;
                Dugum tmp4 = bas;
                dgv.Rows.Clear();
                while (tmp4 != null)
                {
                   
                    int i = dgv.Rows.Add();
                    s1 = tmp4.u_kodu;
                    s2=tmp4.u_adı;
                    s3 = tmp4.u_fiyatı;
                    dgv.Rows[i].Cells[0].Value = s1;
                    dgv.Rows[i].Cells[1].Value = s2;
                    dgv.Rows[i].Cells[2].Value = s3;
                    tmp4=tmp4.sonraki;
                 
                }

            }
            public bool kontrol(string x) 
            {
                Dugum temp = bas;
                string b;
                while(temp != null) 
                {
                    b = temp.u_kodu;
                    if (b == x) 
                    {
                       return true;
                      
                    }
                   temp=temp.sonraki;
                }
                return false;
            }
        }
        Bagliliste bl=new Bagliliste();
        private void button1_Click(object sender, EventArgs e)
        {
            
            string m1 = textBox1.Text;
            string m3 = "MNV-"+m1;
            bool k2= bl.kontrol(m3);
            if (k2==true) 
            {
                MessageBox.Show(" girmiş olduğunuz kod listede bulunmaktadır başka bir kod girmeyi deneyin");
                return;
            }
            else 
            {
                string m2 = textBox2.Text;
                int m4 = Convert.ToInt32(textBox3.Text);
                bl.Ekle(m3, m2, m4);
                MessageBox.Show("Girmiş olduğunuz ürün bilgisi sisteme eklendi");

            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string m1= textBox4.Text;
            string m3 = "MNV-" + m1;
            bl.U_sil(m3);
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string a= textBox7.Text;
            string m3 = "MNV-" + a;
            bl.guncelle(m3,textBox8,textBox9);
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bl.Listele(dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string m1 = textBox4.Text;
            bl.bul(m1,textBox4,textBox5,textBox6);
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            string m1 = textBox7.Text;
            bl.bul(m1, textBox7, textBox8, textBox9);
            
        }
    }
}
