using System;

namespace Arena
{
    public abstract class Karakter
    {
        // Özellikler (Properties)
        public string Isim { get; protected set; }
        public int Can { get; protected set; }
        public int MaksimumCan { get; protected set; }
        public int Guc { get; protected set; }
        public int Mana { get; protected set; }
        public int MaksimumMana { get; protected set; }

        // Statik sayaç
        public static int ToplamSaldiriSayisi { get; private set; }

        // Yapıcı metot
        protected Karakter(string isim, int can, int guc, int mana)
        {
            Isim = isim;
            MaksimumCan = can;
            Can = can;
            Guc = guc;
            MaksimumMana = mana;
            Mana = mana;
        }

        // Sanal saldırı metodu
        public virtual int Saldir()
        {
            ToplamSaldiriSayisi++;
            return Guc;
        }

        // Hasar alma metodu
        public virtual void HasarAl(int hasar)
        {
            Can = Math.Max(0, Can - hasar);
        }

        // Özel saldırı metodu (alt sınıflar tarafından override edilecek)
        public abstract int OzelSaldiri();

        // Durum bilgisi
        public virtual void DurumGoster()
        {
            Console.WriteLine($"\n{Isim} Durumu:");
            Console.WriteLine($"Can: {Can}/{MaksimumCan}");
            Console.WriteLine($"Mana: {Mana}/{MaksimumMana}");
        }
    }
}