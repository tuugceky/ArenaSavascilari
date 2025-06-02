using System;

namespace Arena
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Arena - Savaşçılar";

            // Konsol penceresini büyüt
            Console.WindowHeight = 40;
            Console.WindowWidth = 100;

            // ASCII Art başlığı göster
            AsciiArt.OyunBasligiGoster();

            Console.WriteLine("Konsol Tabanlı RPG Oyunu");
            Console.WriteLine("------------------------");

            Oyun oyun = new Oyun();
            oyun.Baslat();

            Console.WriteLine("\nÇıkmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}