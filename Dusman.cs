using System;

namespace Arena
{
    public abstract class Dusman : Karakter
    {
        protected Dusman(string isim, int can, int guc, int mana) : base(isim, can, guc, mana)
        {
        }

        public override int OzelSaldiri()
        {
            if (Mana >= 20)
            {
                Mana -= 20;
                return Guc * 2;
            }
            return Saldir();
        }

        public override void DurumGoster()
        {
            base.DurumGoster();
            Console.WriteLine($"Düşman Tipi: {this.GetType().Name}");
        }
    }

    public class Zombi : Dusman
    {
        public Zombi() : base("Zombi", can: 50, guc: 10, mana: 30)
        {
        }

        public override int Saldir()
        {
            // Zombiler bazen daha güçlü saldırabilir
            if (new Random().Next(100) < 20)
            {
                return base.Saldir() * 2;
            }
            return base.Saldir();
        }
    }

    public class Goblin : Dusman
    {
        public Goblin() : base("Goblin", can: 40, guc: 12, mana: 40)
        {
        }

        public override int OzelSaldiri()
        {
            if (Mana >= 15)
            {
                Mana -= 15;
                return Guc * 3;
            }
            return Saldir();
        }
    }

    public class Ejderha : Dusman
    {
        public Ejderha() : base("Ejderha", can: 150, guc: 25, mana: 100)
        {
        }

        public override int OzelSaldiri()
        {
            if (Mana >= 50)
            {
                Mana -= 50;
                return Guc * 4;
            }
            return Saldir();
        }
    }
}

