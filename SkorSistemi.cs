using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Arena
{
    public class SkorSistemi
    {
        private const string SKOR_DOSYASI = "skorlar.txt";
        private List<Skor> skorlar;

        public SkorSistemi()
        {
            skorlar = new List<Skor>();
            SkorlariYukle();
        }

        public void SkorEkle(string oyuncuAdi, int yenilenDusmanSayisi, int toplamHasar)
        {
            var yeniSkor = new Skor
            {
                OyuncuAdi = oyuncuAdi,
                YenilenDusmanSayisi = yenilenDusmanSayisi,
                ToplamHasar = toplamHasar,
                Tarih = DateTime.Now
            };

            skorlar.Add(yeniSkor);
            SkorlariKaydet();
        }

        public void EnYuksekSkorlariGoster()
        {
            Console.WriteLine("\n=== EN YÜKSEK SKORLAR ===");
            Console.WriteLine("Sıra | Oyuncu | Yenilen Düşman | Toplam Hasar | Tarih");
            Console.WriteLine("--------------------------------------------------------");

            var siraliSkorlar = skorlar
                .OrderByDescending(s => s.YenilenDusmanSayisi)
                .ThenByDescending(s => s.ToplamHasar)
                .Take(10);

            int sira = 1;
            foreach (var skor in siraliSkorlar)
            {
                Console.WriteLine($"{sira,-4} | {skor.OyuncuAdi,-7} | {skor.YenilenDusmanSayisi,-14} | {skor.ToplamHasar,-12} | {skor.Tarih:dd/MM/yyyy HH:mm}");
                sira++;
            }
        }

        private void SkorlariYukle()
        {
            if (File.Exists(SKOR_DOSYASI))
            {
                string[] satirlar = File.ReadAllLines(SKOR_DOSYASI);
                foreach (string satir in satirlar)
                {
                    string[] parcalar = satir.Split(',');
                    if (parcalar.Length == 4)
                    {
                        skorlar.Add(new Skor
                        {
                            OyuncuAdi = parcalar[0],
                            YenilenDusmanSayisi = int.Parse(parcalar[1]),
                            ToplamHasar = int.Parse(parcalar[2]),
                            Tarih = DateTime.Parse(parcalar[3])
                        });
                    }
                }
            }
        }

        private void SkorlariKaydet()
        {
            var satirlar = skorlar.Select(s => $"{s.OyuncuAdi},{s.YenilenDusmanSayisi},{s.ToplamHasar},{s.Tarih}");
            File.WriteAllLines(SKOR_DOSYASI, satirlar);
        }
    }

    public class Skor
    {
        public string OyuncuAdi { get; set; }
        public int YenilenDusmanSayisi { get; set; }
        public int ToplamHasar { get; set; }
        public DateTime Tarih { get; set; }
    }
}
