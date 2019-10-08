using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    class Class1
    {
        int saniye;
        int puan;
        int dusman_puan;
        int ozel_mermi_durum;
        int mermi_konum_x;
        int mermi_konum_y;
        int hedef_yon;
        public int dusman_mermi_tekrar_yukle;
        public int atilan_mermi_sayisi;
        public bool dusman_mermi_durum;
        public int mermi_sayisi;
        public bool doldur_bosalt_kontrol;
        public bool ozel_mermi_kontrol;
        int mermi_hiz;
        public int hedef_konum_y;
        public int hedef_konum_x;
        public int dusman_atilan_mermi_sirasi;
        public PictureBox[] mermiler;
        PictureBox[] dusman_mermileri;
        PictureBox[] sacma_mermileri;
        Random rnd = new Random();

        public Class1() 
        {
            saniye = 1;
            puan = 0;
            dusman_puan = 0;
            ozel_mermi_durum = 0;
            hedef_yon = 2;
            mermi_konum_x = 700;
            mermi_konum_y = 200;
            mermi_sayisi = 20;
            mermi_hiz = -10;
            dusman_mermi_tekrar_yukle = 0;
            mermiler = new PictureBox[mermi_sayisi];
            dusman_mermileri = new PictureBox[20];
            atilan_mermi_sayisi = 0;
            doldur_bosalt_kontrol = true;
            dusman_mermi_durum = true;
            ozel_mermi_kontrol = false;
            hedef_konum_y = 40;
            hedef_konum_x = 849;
            dusman_atilan_mermi_sirasi = 0;
            sacma_mermileri = new PictureBox[5];
        }

        public void namlu_ucu_ve_sarjor_doldur_bosalt(Keys basilan_tus, Form1 frm)
        {
            if (Keys.Space == basilan_tus && atilan_mermi_sayisi != mermi_sayisi)
            {
                mermi_konumlandırma(frm);
            }
            else if (atilan_mermi_sayisi == mermi_sayisi)
            {
                frm.label1.ForeColor = Color.Red;
                frm.label1.Text = "Mermi Durumu :\n(R Bas Ve Doldur)";
            }
            if (doldur_bosalt_kontrol)
            {
                if (Keys.R == basilan_tus && atilan_mermi_sayisi == mermi_sayisi)
                {
                    frm.timer4.Enabled = true;
                    doldur_bosalt_kontrol = false;
                }
            }
            if (Keys.G == basilan_tus )
            {
                ozel_mermi_konumlandirma(frm.pictureBox1.Location.X + (frm.pictureBox1.Width / 2), frm.pictureBox1.Location.Y);
                frm.label7.Text = "Ozel Mermi Durumu :";
                frm.label7.ForeColor = Color.Red;
                frm.timer8.Enabled = true;
                ozel_mermi_durum = 0;
            }
        }

        public void kutumuzu_konumlandırma(Form1 frm, int fare_konum_x)
        {
            if (frm.pictureBox1.Location.X + frm.pictureBox1.Height >= 600)
            {
                frm.pictureBox1.Location = new Point(580, frm.pictureBox1.Location.Y);
            }
            else if (frm.pictureBox1.Location.X <= 0)
            {
                frm.pictureBox1.Location = new Point(0, frm.pictureBox1.Location.Y);
            }
            if (fare_konum_x - 12 + frm.pictureBox1.Height < 600 && fare_konum_x - 12 > 0)
            {
                frm.pictureBox1.Location = new Point(fare_konum_x - 12, frm.pictureBox1.Location.Y);
            }
        }
        
        public void hedef_konumlandırma(Form1 frm)
        {   
            frm.pictureBox3.Visible = true;
            frm.label3.Text = "PUAN DURUMUNUZ : " + puan.ToString();
            frm.label6.Text = "DÜŞMAN PUAN DURUMU : " + dusman_puan.ToString();
            //hedef özellikleri
            int hedef_x = frm.pictureBox3.Location.X;
            int hedef_y = frm.pictureBox3.Location.Y;
            int hedef_boy = frm.pictureBox3.Height;
            int hedef_en = frm.pictureBox3.Width;
            //kutumuz özellikleri
            int kutumuz_x = frm.pictureBox1.Location.X;
            int kutumuz_y = frm.pictureBox1.Location.Y;
            int kutumuz_boy = frm.pictureBox1.Height;
            int kutumuz_en = frm.pictureBox3.Width;

            int hiz;
            frm.pictureBox3.BackColor = Color.DarkRed;
            frm.pictureBox1.BackColor = Color.DarkBlue;
            
            hiz = rnd.Next(2, 4);
            if (rnd.Next(1, 200) == 1)
            {
                hedef_yon = hiz * -1;
            }
            frm.pictureBox3.Location = new Point(hedef_x + hedef_yon, frm.pictureBox3.Location.Y);
            if (600 <= hedef_x + frm.pictureBox3.Width - 1)
            {
                hedef_yon = -hiz;
            }
            else if (hedef_x <= 1)
            {
                hedef_yon = +hiz;
            }
            for (int i = 0; i < atilan_mermi_sayisi; i++)
            {//dusmanın vurulup vurlulmadığını kontrol eder
                if ((hedef_y + hedef_boy == mermiler[i].Location.Y) && (mermiler[i].Location.X >= hedef_x && mermiler[i].Location.X <= hedef_x + hedef_en))
                {
                    puan += 10;
                    ozel_mermi_durum += 10;
                    mermiler[i].Dispose();
                    hedef_yon *= -1;
                    frm.pictureBox3.BackColor = Color.Red;
                }
            }
            for (int i = 0; i < dusman_atilan_mermi_sirasi; i++)
            {//kutumuzun vurulup vurlulmadığını kontrol eder
                if ((kutumuz_y + kutumuz_boy == dusman_mermileri[i].Location.Y) && (dusman_mermileri[i].Location.X >= kutumuz_x && dusman_mermileri[i].Location.X <= kutumuz_x + kutumuz_en))
                {
                    dusman_puan += 10;
                    dusman_mermileri[i].Dispose();
                    frm.pictureBox1.BackColor = Color.DodgerBlue;
                }
            }
        }

        public void mermi_konumlandırma(Form1 frm)
        {
            mermiler[atilan_mermi_sayisi].Location = new Point(frm.pictureBox1.Location.X + 8, frm.pictureBox1.Location.Y - 10);
            atilan_mermi_sayisi++;
            frm.progressBar1.Value--;
        }
       
        public void mermileri_olustur(Form1 frm) 
        {
            for (int i = 0; i < mermiler.Length; i++)
            {
                mermiler[i] = new PictureBox();
                mermiler[i].Name = "m";
                mermiler[i].Height = 10;
                mermiler[i].Width = 5;
                mermiler[i].BackColor = Color.Red;
                if (i != 0 && i % 10 == 0)
                {
                    mermi_konum_y = 200;
                    mermi_konum_x += 6;
                }
                mermiler[i].Location = new Point(mermi_konum_x, mermi_konum_y);
                frm.panel1.Controls.Add(mermiler[i]);
                mermi_konum_y += 11;
            }
            frm.progressBar1.Maximum = mermi_sayisi;
        }

        public void mermi_hareketleri(Form1 frm) 
        {
            for (int i = 0; i < atilan_mermi_sayisi; i++)
            {
                mermiler[i].Location = new Point(mermiler[i].Location.X, mermiler[i].Location.Y + mermi_hiz);
                if (mermiler[i].Location.Y <= 0)
                {
                    mermiler[i].Dispose();
                }
            }  
        }

        public void mermi_doldur(Form1 frm) 
        {
            frm.progressBar1.Value++;
            if (frm.progressBar1.Value == atilan_mermi_sayisi)
            {
                mermi_konum_x = 700;
                mermi_konum_y = 200;
                mermileri_olustur(frm);
                atilan_mermi_sayisi = 0;
                frm.label1.Text = "Mermi Durumu";
                frm.label1.ForeColor = Color.DarkGreen;
                doldur_bosalt_kontrol = true;
                frm.timer4.Enabled = false;
            }
            
        }

        public void zaman(Form1 frm)
        {
            frm.label4.Text = "SÜRE : " + saniye.ToString() + " sn";
            saniye++;
        }
        
        public void dusman_mermi_oluturma(Panel eklenecek_yer) 
        {
            for (int i = 0; i < dusman_mermileri.Length; i++)
            {
                dusman_mermileri[i] = new PictureBox();
                dusman_mermileri[i].Width = 5;
                dusman_mermileri[i].Height = 10;
                dusman_mermileri[i].BackColor = Color.DodgerBlue;
                if (i % 10 == 0 && i != 0)
                {
                    hedef_konum_x += 6;
                    hedef_konum_y = 40;
                }
                dusman_mermileri[i].Location = new Point(hedef_konum_x, hedef_konum_y);
                eklenecek_yer.Controls.Add(dusman_mermileri[i]);
                hedef_konum_y += 11;
            }
        }

        public void dusman_saldirisi_mermi_konumlandirma(int dusman_yer_x, int dusman_yer_y, int genislik) 
        {
            if (dusman_atilan_mermi_sirasi < 20)
            {
                dusman_mermileri[dusman_atilan_mermi_sirasi].Location = new Point(dusman_yer_x + (genislik/2), dusman_yer_y); 
            }    
        }

        public void dusman_mermi_hareketleri(Form1 frm) 
        {
            dusman_saldirisi_mermi_konumlandirma(frm.pictureBox3.Location.X, frm.pictureBox3.Location.Y, frm.pictureBox3.Width);
            for (int i = 0; i < dusman_atilan_mermi_sirasi; i++)
            {
                dusman_mermileri[i].Location = new Point(dusman_mermileri[i].Location.X, dusman_mermileri[i].Location.Y + 5);
                if (dusman_mermileri[i].Location.Y >= 400 )
                {
                    dusman_mermileri[i].Dispose(); 
                }
            }
        }

        public void dusman_mermi_doldur(Timer zaman, Form1 frm) 
        {
            if (dusman_mermi_tekrar_yukle < 5)
            {
                dusman_mermi_tekrar_yukle++;
            }
            else
            {
                dusman_mermi_durum = true;
                zaman.Enabled = false;
                dusman_mermi_tekrar_yukle = 0;
                dusman_atilan_mermi_sirasi = 0;
                hedef_konum_y = 40;
                hedef_konum_x = 849;
                dusman_mermi_oluturma(frm.panel1);
            }
        }

        public void ozel_mermi_olusturma(Panel eklenecek_yer) 
        {
            int konum_y = 10;
            for (int i = 0; i < 5; i++)
            {
                sacma_mermileri[i] = new PictureBox();
                sacma_mermileri[i].Width = 5;
                sacma_mermileri[i].Height = 10;
                sacma_mermileri[i].BackColor = Color.DodgerBlue;
                sacma_mermileri[i].Location = new Point(30, konum_y);
                sacma_mermileri[i].Visible = false;
                eklenecek_yer.Controls.Add(sacma_mermileri[i]);
                konum_y += 11;
            }
        }

        public void ozel_mermi_konumlandirma(int x, int y) 
        {
            int konum_x = -10;
            int konum_y = 10;
            sacma_mermileri[0].Location = new Point(x, y);
            for (int i = 1; i < 5; i++)
            {
                sacma_mermileri[i].Location = new Point(x + konum_x, y + konum_y);
                konum_x *= -1;
                if (i % 2 == 0)
                {
                    konum_y += 10;
                    konum_x = (-1) * konum_y;
                }
            }
        }

        public void ozel_mermi_hareketleri(Timer zaman) 
        {
            int dagilim = 2;
            sacma_mermileri[0].Visible = true;
            sacma_mermileri[0].Location = new Point(sacma_mermileri[0].Location.X, sacma_mermileri[0].Location.Y - 10);
            for (int i = 1; i < 5; i++)
            {
                sacma_mermileri[i].Visible = true;
                if (i % 2 == 0)
                {
                    sacma_mermileri[i].Location = new Point(sacma_mermileri[i].Location.X + dagilim , sacma_mermileri[i].Location.Y - 10);
                    dagilim += 2;
                }
                else
                {
                    sacma_mermileri[i].Location = new Point(sacma_mermileri[i].Location.X - dagilim , sacma_mermileri[i].Location.Y - 10);
                }
                if (sacma_mermileri[0].Location.Y <= 100)
                {
                    sacma_mermileri[i].Dispose();
                    sacma_mermileri[0].Dispose();
                }
            }
            if (sacma_mermileri[4].Location.Y <= 0)
            {
                zaman.Enabled = false;
            }
        }

        public void ozel_mermi_kullanim_durumu(Form1 frm) 
        {
            //frm.progressBar2.Value = ozel_mermi_durum;
            if (ozel_mermi_durum == 10)
            {
                ozel_mermi_kontrol = true;
                frm.timer8.Enabled = false;
                //ozel_mermi_olusturma(frm.panel1);
                frm.label7.Text = "Ozel Mermi Durumu :\n(G Bas Ve Kullan)";
                frm.label7.ForeColor = Color.Gold;
            }
        }
    }
}
