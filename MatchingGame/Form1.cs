using MatchingGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        int satir = 4, sutun = 4;
        int adet, cift, gen, yuk, pay = 10,bulunanCiftAdet;
        Random rnd = new Random();
        int [] tumResimNolar;
        int[] kartlar;

        public Form1()
        {
            InitializeComponent();
            OyunuBaslat();
        }

        void OyunuBaslat()
        {
            #region Oyun Değişkenlerinin Hesaplanması
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
                pb.Image = (Image)Resources.ResourceManager.GetObject("_" + kartlar[i]);
                pb.Width = gen;
                pb.Height = yuk;
                pb.Click += KartaTiklandiginda;
                pnlKartlar.Controls.Add(pb);
            }


        }

        private void KartaTiklandiginda(object sender, EventArgs e)
        {
            PictureBox tiklanan = (PictureBox)sender;
            
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
