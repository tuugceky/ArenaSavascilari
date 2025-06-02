using System;
using System.Collections.Generic;

namespace Arena
{
    public class Oyun
    {
        private Oyuncu oyuncu;
        private Dusman mevcutDusman;
        private Random rnd;
        private List<Dusman> dusmanlar;
        private SkorSistemi skorSistemi;
        private int yenilenDusmanSayisi;
        private int toplamHasar;

        public Oyun()
        {
            rnd = new Random();
            dusmanlar = new List<Dusman>
            {
                new Zombi(),
                new Goblin(),
                new Ejderha()
            };
            skorSistemi = new SkorSistemi();
            yenilenDusmanSayisi = 0;
            toplamHasar = 0;
        }

        public void Baslat()
        {
            AsciiArt.OyunBasligiGoster();
            Console.WriteLine("Arena'ya Hoş Geldiniz!");
            Console.Write("Karakterinizin adını girin: ");
            string isim = Console.ReadLine();
            oyuncu = new Oyuncu(isim);

            YeniDusmanOlustur();
            OyunDongusu();
        }

        private void YeniDusmanOlustur()
        {
            mevcutDusman = dusmanlar[rnd.Next(dusmanlar.Count)];
            Console.WriteLine($"\nYeni bir {mevcutDusman.Isim} belirdi!");
            AsciiArt.KarakterGoster(mevcutDusman.GetType().Name);
            DurumGoster();
        }

        private void DurumGoster()
        {
            Console.WriteLine("\n=== DURUM BİLGİSİ ===");
            Console.WriteLine($"Oyuncu: {oyuncu.Isim}");
            Console.WriteLine($"Can: {oyuncu.Can}/{oyuncu.MaksimumCan}");
            Console.WriteLine($"Mana: {oyuncu.Mana}/{oyuncu.MaksimumMana}");
            Console.WriteLine($"\nDüşman: {mevcutDusman.Isim}");
            Console.WriteLine($"Can: {mevcutDusman.Can}/{mevcutDusman.MaksimumCan}");
            Console.WriteLine($"Mana: {mevcutDusman.Mana}/{mevcutDusman.MaksimumMana}");
            Console.WriteLine("=====================");
        }

        private void OyunDongusu()
        {
            while (true)
            {
                Console.WriteLine("\n=== SAVAŞ MENÜSÜ ===");
                Console.WriteLine("1. Normal Saldırı");
                Console.WriteLine("2. Özel Saldırı");
                Console.WriteLine("3. Mana Yenile");
                Console.WriteLine("4. Durum Göster");
                Console.WriteLine("5. Yüksek Skorlar");
                Console.WriteLine("6. Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        SavasTurunuYonet(oyuncu.Saldir());
                        break;
                    case "2":
                        SavasTurunuYonet(oyuncu.OzelSaldiri());
                        break;
                    case "3":
                        oyuncu.ManaYenile();
                        DurumGoster();
                        break;
                    case "4":
                        DurumGoster();
                        break;
                    case "5":
                        skorSistemi.EnYuksekSkorlariGoster();
                        break;
                    case "6":
                        Console.WriteLine("Oyun sonlandırılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }

                if (oyuncu.Can <= 0)
                {
                    Console.WriteLine("\nOyun Bitti! Yenildiniz!");
                    skorSistemi.SkorEkle(oyuncu.Isim, yenilenDusmanSayisi, toplamHasar);
                    skorSistemi.EnYuksekSkorlariGoster();
                    break;
                }
            }
        }

        private void SavasTurunuYonet(int oyuncuHasar)
        {
            // Oyuncunun saldırısı
            mevcutDusman.HasarAl(oyuncuHasar);
            toplamHasar += oyuncuHasar;
            AsciiArt.SavasGoster(oyuncu.Isim, mevcutDusman.Isim, oyuncuHasar);

            // Düşman öldü mü kontrol et
            if (mevcutDusman.Can <= 0)
            {
                Console.WriteLine($"\n{mevcutDusman.Isim} yenildi!");
                yenilenDusmanSayisi++;
                YeniDusmanOlustur();
                return;
            }

            // Düşmanın saldırısı
            int dusmanHasar = rnd.Next(2) == 0 ? mevcutDusman.Saldir() : mevcutDusman.OzelSaldiri();
            oyuncu.HasarAl(dusmanHasar);
            AsciiArt.SavasGoster(mevcutDusman.Isim, oyuncu.Isim, dusmanHasar);

            // Her tur sonunda durum bilgisini göster
            DurumGoster();
        }
    }
}