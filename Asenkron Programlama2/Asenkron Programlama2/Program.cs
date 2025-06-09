using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asenkron_Programlama2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Asal ve kare sayıların hesaplanması başladı...\n");

            // Görevleri aynı anda başlat
            Task<List<int>> asalTask = AsalSayilariHesaplaAsync(2, 50);
            Task<List<int>> kareTask = KareSayilariHesaplaAsync(1, 10);

            // Eşzamanlı görevleri bekle
            var asalSayilar = await asalTask;
            var kareSayilar = await kareTask;

            Console.WriteLine("Asal Sayılar: " + string.Join(", ", asalSayilar));
            Console.WriteLine("Kare Sayılar: " + string.Join(", ", kareSayilar));
        }

        static async Task<List<int>> AsalSayilariHesaplaAsync(int basla, int bitir)
        {
            return await Task.Run(() =>
            {
                List<int> asalListesi = new List<int>();
                for (int sayi = basla; sayi <= bitir; sayi++)
                {
                    if (IsAsal(sayi))
                        asalListesi.Add(sayi);
                }
                return asalListesi;
            });
        }

        static async Task<List<int>> KareSayilariHesaplaAsync(int basla, int bitir)
        {
            return await Task.Run(() =>
            {
                return Enumerable.Range(basla, bitir).Select(x => x * x).ToList();
            });
        }

        static bool IsAsal(int sayi)
        {
            if (sayi < 2) return false;
            for (int i = 2; i <= Math.Sqrt(sayi); i++)
            {
                if (sayi % i == 0) return false;
            }
            return true;
        }
    }
}
