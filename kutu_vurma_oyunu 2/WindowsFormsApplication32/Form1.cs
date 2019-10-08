using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox[] pb_hedef = new PictureBox[20];
        PictureBox[] pb_mermi = new PictureBox[60];
        Random rnd = new Random();
        int hiz = 1;
        int can = 3;
        int sure = 59;
        int zorluk_seviyesi = 0;
        int yon_degis = 0;
        int zorluk_ayar = 25;
        int kim = 0;
        int puan = 0;
        int hedefe_ulasan_mermi_sirasi = 0;
        int atilan_mermi_sirasi = -1;
        int atilan_mermi_adeti = 60;
        bool doldurma_suresi = true;
        bool durdurma = true;

        private void pbMermiOlustur()
        {
            int aralikx = label3.Location.X + 4;
            int araliky = 230;
            for (int i = 0; i < pb_mermi.Length; i++)
            {
                pb_mermi[i] = new PictureBox();
                pb_mermi[i].Name = "picture_box" + (i + 1).ToString();
                pb_mermi[i].BackColor = Color.Red;
                pb_mermi[i].Width = 5;//5
                pb_mermi[i].Height = 30;//30
                pb_mermi[i].Image = Image.FromFile("res//m1a.png");
                pb_mermi[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pb_mermi[i].Top = araliky;
                pb_mermi[i].Left = aralikx;
                this.Controls.Add(pb_mermi[i]);
                araliky += 6;
                if ((i + 1) % 20 == 0)
                {
                    aralikx += 20;
                    araliky = 230;
                }
            }
        }

        private void mermiKonumlandırma()
        {//tekrar doldurma sirasinda çalışan kod
            int aralikx = label3.Location.X + 4;
            int araliky = 230;
            for (int i = 0; i < pb_mermi.Length; i++)
            {
                pb_mermi[i].Top = araliky;
                pb_mermi[i].Left = aralikx;
                pb_mermi[i].Visible = true;
                pb_mermi[i].Enabled = true;
                araliky += 6;
                if ((i + 1) % 20 == 0)
                {
                    aralikx += 20;
                    araliky = 230;
                }
            }
        }

        private void pbOlustur(int bastanUzaklik, int aralikUzakligi)
        {
            int aralik = bastanUzaklik;
            for (int i = 0; i < pb_hedef.Length; i++)
            {
                pb_hedef[i] = new PictureBox();
                pb_hedef[i].Name = "picture_box" + (i + 1).ToString();
                pb_hedef[i].BackColor = Color.Red;
                pb_hedef[i].Width = 20;
                pb_hedef[i].Height = 20;
                pb_hedef[i].Top = -20;
                pb_hedef[i].Left = aralik;
                this.Controls.Add(pb_hedef[i]);
                aralik += 20 + aralikUzakligi;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pbOlustur(50, 5);
            pbMermiOlustur();
            progressBar1.Maximum = pb_mermi.Length;
            pb_hedef[0].Location = new Point(rnd.Next(0, label1.Location.X - 20), 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region kirmizi_kutu_hareketleri_ve_vurulma_kodlari
            if (pb_hedef[kim].Location.Y >= label2.Location.Y)
            {
                kim++;
                if (kim == 20)
                {
                    kim = 0;
                }
                pb_hedef[kim].Location = new Point(rnd.Next(0, label1.Location.X - 20), 0);
                can = 3;
            }
            else if (pb_hedef[kim].Top == pictureBox1.Top + 20)
            {
                puan -= 10;
                pb_hedef[kim].Top += hiz;
            }
            else
            {
                pb_hedef[kim].Top += hiz;
                pb_hedef[kim].Left += yon_degis;
                for (int i = 0; i <= atilan_mermi_sirasi; i++)
                {
                    if (pb_hedef[kim].Location.X + 20 >= pb_mermi[i].Location.X && pb_hedef[kim].Location.Y <= pb_mermi[i].Location.Y && pb_hedef[kim].Location.Y + pb_hedef[kim].Height >= pb_mermi[i].Location.Y && pb_mermi[i].Location.X + pb_mermi[i].Width >= pb_hedef[kim].Location.X && pb_mermi[i].Enabled)
                    {//hedef vurulunca yapılması gereken kodlar
                        can--;
                        if (can == 0)
                        {
                            pb_hedef[kim].Top = label2.Top;
                            puan += 10 + zorluk_seviyesi;
                            if (puan > zorluk_ayar)
                            {
                                if (zorluk_seviyesi == 6)
                                {
                                    zorluk_ayar += 50 + zorluk_seviyesi;
                                }
                                else
                                {
                                    zorluk_seviyesi++;
                                }

                            }
                        }
                        if (zorluk_seviyesi > 0 && zorluk_seviyesi < 6)
                        {
                            if (zorluk_seviyesi == 5)
                            {
                                hiz = 2;
                            }
                            if (yon_degis > 0)
                            {
                                yon_degis = zorluk_seviyesi * -1;
                            }
                            else
                            {
                                yon_degis = zorluk_seviyesi * -1;
                            }
                        }
                        pb_mermi[i].Enabled = false;
                        pb_mermi[i].Visible = false;
                    }
                    if (pb_mermi[i].Location.Y == 0 && pb_mermi[i].Enabled)
                    {//ıskalayınca yapılması gerekenler
                        puan -= 1;
                    }
                }
            }
            #endregion
            #region kirmizi_kutu_icin_sinir_belirleme
            if (pb_hedef[kim].Location.X <= 0)
            {
                yon_degis *= -1;
            }
            else if (pb_hedef[kim].Location.X + pb_hedef[kim].Width >= label1.Location.X)
            {
                yon_degis *= -1;
            }
            #endregion
            #region mermi_hareketleri
            for (int i = hedefe_ulasan_mermi_sirasi; i <= atilan_mermi_sirasi; i++)
            {
                pb_mermi[i].Top -= 10;
                if (pb_mermi[i].Location.Y <= -10)
                {
                    hedefe_ulasan_mermi_sirasi++;
                }
            }
            #endregion
            #region mavi_kutu_hareketleri
            if (MousePosition.X - 15 <= this.Location.X)
            {
                pictureBox1.Location = new Point(0, pictureBox1.Location.Y);
            }
            else if (MousePosition.X >= this.Location.X + label1.Location.X - 5)
            {
                pictureBox1.Location = new Point(label1.Location.X - 20, pictureBox1.Location.Y);
            }
            else
            {
                pictureBox1.Location = new Point(MousePosition.X - this.Location.X - 14, pictureBox1.Location.Y);
            }
            #endregion
            #region progessBar_ayarlama
            if (doldurma_suresi)
            {
                progressBar1.Value = atilan_mermi_adeti * progressBar1.Step;
            }
            #endregion
            #region mermi_doldurma
            if (!doldurma_suresi)
            {
                if (progressBar1.Value == pb_mermi.Length)
                {//mermi doldurma

                    doldurma_suresi = true;
                    atilan_mermi_sirasi = -1;
                    hedefe_ulasan_mermi_sirasi = 0;
                    label3.ForeColor = Color.LimeGreen;
                    label3.Text = "Hazir!";
                    atilan_mermi_adeti = pb_mermi.Length;
                    mermiKonumlandırma();
                }
                else
                {
                    progressBar1.Value += progressBar1.Step;
                }
            }
            #endregion
            //Cursor.Position = new Point(MousePosition.X, pb_hedef[kim].Location.Y + this.Location.Y + 25);
            //label4.Text = pb_mermi[i].Location.Y.ToString(); mermi doldurma hızı ayarlaması için kullan
            label4.Text = "Puanınız: " + puan.ToString();
            label5.Text = "Zorluk seviyesi: " + zorluk_seviyesi.ToString();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (durdurma)
            {
                if (doldurma_suresi)
                {//doldururken ateş etmeyi engeller
                    if (Keys.Space == e.KeyCode)
                    {//mermi ateşleme
                        if (atilan_mermi_sirasi == pb_mermi.Length - 1)
                        {
                            label3.ForeColor = Color.Red;
                            label3.Text = "Doldur!\n(R tuşuna basınız)";
                        }
                        else
                        {//atilan mermi sayasi 19 olana dek çalışan kod ve merminin ilk konumlandırma kısmı
                            atilan_mermi_adeti--;//progessbar için oluşturulan değişken
                            atilan_mermi_sirasi++;
                            pb_mermi[atilan_mermi_sirasi].Location = new Point(pictureBox1.Location.X + 8, pictureBox1.Location.Y);
                        }
                    }
                }
                if (Keys.R == e.KeyCode)
                {//mermi doldurma
                    if (atilan_mermi_adeti != pb_mermi.Length)//mermi doluyken tekrar doldurmayı önler
                    {
                        doldurma_suresi = false;// mermi dolumu yaparken atşlemeyi engeller
                        progressBar1.Value = 0;
                        atilan_mermi_adeti = pb_mermi.Length;
                        label3.Text = "Dolduruluyor!";
                        label3.ForeColor = Color.Gold;
                    }
                }
            }
            if (Keys.P == e.KeyCode)
            {
                if (timer1.Enabled == true)
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    durdurma = false;
                }
                else
                {
                    timer1.Enabled = true;
                    timer2.Enabled = true;
                    durdurma = true;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label6.Text = "Kalan Süreniz";
            label7.Text = "00:" + sure.ToString();
            if (sure == 0)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                MessageBox.Show("Süreniz bitmiştir. Puannınız : " + puan.ToString(), "Oyun bitti!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            sure--;
        }
    }
}
