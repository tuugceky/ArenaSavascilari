using System;

namespace Arena
{
    public class Oyuncu : Karakter
    {
        private const int MANA_YENILEME_MIKTARI = 20;
        private const int OZEL_SALDIRI_MANA_MALIYETI = 30;
        private const int OZEL_SALDIRI_CARPANI = 2;

        public Oyuncu(string isim) : base(isim, can: 100, guc: 15, mana: 50)
        {
        }

        public override int OzelSaldiri()
        {
            if (Mana >= OZEL_SALDIRI_MANA_MALIYETI)
            {
                Mana -= OZEL_SALDIRI_MANA_MALIYETI;
                return Guc * OZEL_SALDIRI_CARPANI;
            }
            else
            {
                Console.WriteLine("Yeterli mana yok!");
                return 0;
            }
        }

        public void ManaYenile()
        {
            Mana = Math.Min(MaksimumMana, Mana + MANA_YENILEME_MIKTARI);
            Console.WriteLine($"Mana yenilendi! Yeni mana: {Mana}");
        }

        public override void DurumGoster()
        {
            base.DurumGoster();
            Console.WriteLine($"Güç: {Guc}");
            Console.WriteLine($"Toplam Saldırı Sayısı: {ToplamSaldiriSayisi}");
        }
    }
}