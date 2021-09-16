using MatchingGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        int satir = 4, sutun = 4;
        int adet, cift, gen, yuk, pay = 10, bulunanCiftAdet;
        Random rnd = new Random();
        int [] tumResimNolar;
        int[] kartlar;
        List<PictureBox> aciklar;
        Button btnYenidenBaslat;

        public Form1()
        {
            InitializeComponent();
            OyunuBaslat();
        }

        void OyunuBaslat()
        {
            pnlKartlar.Controls.Clear();

            #region Yeniden Başlat Butonunun Eklenip Gizlenmesi
            btnYenidenBaslat = new Button();
            btnYenidenBaslat.Text = "YENİDEN BAŞLAT";
            btnYenidenBaslat.Location = new Point(192, 233);
            btnYenidenBaslat.Size = new Size(180, 35);
            btnYenidenBaslat.Click += BtnYenidenBaslat_Click;
            btnYenidenBaslat.Visible = false;
            pnlKartlar.Controls.Add(btnYenidenBaslat);

            #endregion

            #region Oyun Değişkenlerinin Hesaplanması
            bulunanCiftAdet = 0;
            aciklar = new List<PictureBox>();
            adet = satir * sutun;
            cift = adet / 2;
            gen = (pnlKartlar.Width - (sutun - 1) * pay) / sutun;
            yuk = (pnlKartlar.Height - (satir - 1) * pay) / satir; 
            #endregion

            #region Resimlerin Karışık olarak sıralanması
            tumResimNolar = new int[263];
            for (int i = 0; i < 263; i++)
                tumResimNolar[i] = i;

            Karistir(tumResimNolar);
            #endregion

            #region Kartların Karışık Oluşturulması
            kartlar = new int[adet];
            Array.Copy(tumResimNolar, kartlar, cift);
            Array.Copy(tumResimNolar, 0, kartlar, cift, cift);
            Karistir(kartlar);
            #endregion

            PictureBox pb;

            for (int i = 0; i < kartlar.Length; i++)
            {
                pb = new PictureBox();
                pb.Left = (i % sutun) * (gen + pay);
                pb.Top = (i / sutun) * (gen + pay);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.BackColor = Color.Gray;
                pb.Tag = kartlar[i];
                pb.Width = gen;
                pb.Height = yuk;
                pb.Click += KartaTiklandiginda;
                pnlKartlar.Controls.Add(pb);
            }


        }

        private void BtnYenidenBaslat_Click(object sender, EventArgs e)
        {
            OyunuBaslat();
        }

        private void KartaTiklandiginda(object sender, EventArgs e)
        {
            PictureBox tiklanan = (PictureBox)sender;

            if (aciklar.Contains(tiklanan))
                return;

            if (aciklar.Count == 2)
                AcikKartlariKapat();

            KartiAc(tiklanan);

            if(aciklar.Count == 2 && (int)aciklar[0].Tag == (int)aciklar[1].Tag)
            {
                Refresh();
                Thread.Sleep(400);
                AciklariKapatGizle();
                bulunanCiftAdet++;

                if(bulunanCiftAdet == cift)
                {
                    MessageBox.Show("Oyun Bitti!!");
                    btnYenidenBaslat.Show();
                }
            }
            
        }
        private void AciklariKapatGizle()
        {
            while (aciklar.Count > 0)
            {
                aciklar[0].Hide();
                KartiKapat(aciklar[0]);
            }
        }

        void AcikKartlariKapat()
        {
            while (aciklar.Count > 0)
            {
                KartiKapat(aciklar[0]);
            }
        }

        void KartiAc(PictureBox kart)
        {
            int resimNo = (int)kart.Tag;
            kart.Image = (Image)Resources.ResourceManager.GetObject("_" + resimNo);
            aciklar.Add(kart);
        }

        void KartiKapat(PictureBox kart)
        {
            int resimNo = (int)kart.Tag;
            kart.Image = null;
            aciklar.Remove(kart);
        }

        private void Karistir(int[] dizi)
        {
            int yedek;
            int indeks;
            for (int i = 0; i < dizi.Length; i++)
            {
                indeks = rnd.Next(i, dizi.Length);
                yedek = dizi[i];
                dizi[i] = dizi[indeks];
                dizi[indeks] = yedek;
            }
        }
    }
}
