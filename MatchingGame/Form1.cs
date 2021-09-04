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
        int adet, cift, gen, yuk, pay = 10;
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
            tumResimNolar = new int[263];
            for (int i = 0; i < 263; i++)
            {
                tumResimNolar[i] = i;
                Karistir(tumResimNolar);
            }
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
