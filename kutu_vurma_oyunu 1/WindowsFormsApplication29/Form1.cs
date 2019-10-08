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
        
        Class1 islemler = new Class1();
        Random salla = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {// başlangıçta mermi konumlandırma
            islemler.mermileri_olustur(this);
            islemler.dusman_mermi_oluturma(panel1);
            islemler.ozel_mermi_olusturma(panel1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {// hedef konumlandırma
            islemler.hedef_konumlandırma(this);
            islemler.mermi_hareketleri(this);
            islemler.kutumuzu_konumlandırma(this, MousePosition.X);
            islemler.ozel_mermi_kullanim_durumu(this);
            //-------------
            int dnm = salla.Next(1, 25);
            islemler.dusman_mermi_hareketleri(this);
            if (dnm == 1 && islemler.dusman_mermi_durum)
            {
                islemler.dusman_atilan_mermi_sirasi++;
                if (islemler.dusman_atilan_mermi_sirasi == 20)
                {
                    islemler.dusman_mermi_durum = false;
                    timer7.Enabled = true;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {// mermi konumlandırma ve mermi dolurma
            islemler.namlu_ucu_ve_sarjor_doldur_bosalt(e.KeyCode, this);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {// R basarak mermi doldur
            islemler.mermi_doldur(this);
        }

        private void timer5_Tick(object sender, EventArgs e)
        {// zaman ayarı
            islemler.zaman(this);
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            islemler.dusman_mermi_doldur(timer7, this);
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            islemler.ozel_mermi_hareketleri(timer8);
        }
    }
}
