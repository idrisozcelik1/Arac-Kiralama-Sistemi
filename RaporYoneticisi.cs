using System;
using System.Collections.Generic;
using System.Linq;

namespace AracKiralamaSistemi
{
    public class RaporYoneticisi
    {
        private RezervasyonYoneticisi rezervasyonYoneticisi;
        private AracYoneticisi aracYoneticisi;

        public RaporYoneticisi(RezervasyonYoneticisi rezervasyonYoneticisi, AracYoneticisi aracYoneticisi)
        {
            this.rezervasyonYoneticisi = rezervasyonYoneticisi;
            this.aracYoneticisi = aracYoneticisi;
        }

        // TOPLAM GELİRİ HESAPLAYAN FONKSİYON
        public double ToplamGelir()
        {
            double toplam = 0;
            foreach (var rezervasyon in rezervasyonYoneticisi.TumRezervasyonlariGetir())
            {
                toplam += rezervasyon.ToplamUcret;
            }
            return toplam;
        }

        // EN ÇOK KİRALANAN ARACI BULAN FONKSİYON
        public string EnCokKiralananArac()
        {
            var rezervasyonlar = rezervasyonYoneticisi.TumRezervasyonlariGetir();

            if (rezervasyonlar.Count == 0)
                return "Henüz oluşturulan bir rezervasyon yok";

            var gruplar = rezervasyonlar.GroupBy(r => r.Plaka)
                                       .Select(g => new { Plaka = g.Key, Sayi = g.Count() })
                                       .OrderByDescending(x => x.Sayi)
                                       .FirstOrDefault();

            return gruplar?.Plaka ?? "Bulunamadı";
        }

        // TOPLAM GELİR RAPORUNU GÖSTEREN FONKSİYON
        public void ToplamGelirRaporuGoster()
        {
            Console.Clear();
            Console.WriteLine("---- TOPLAM GELİR RAPORU ----\n");

            double gelir = ToplamGelir();
            Console.WriteLine($"Toplam Gelir: {gelir:N2} TL");
            Console.WriteLine($"Toplam Rezervasyon Sayısı: {rezervasyonYoneticisi.ToplamRezervasyonSayisi()}");

            if (rezervasyonYoneticisi.ToplamRezervasyonSayisi() > 0)
            {
                double ortalamaGelir = gelir / rezervasyonYoneticisi.ToplamRezervasyonSayisi();
                Console.WriteLine($"Rezervasyon Başına Ortalama Gelir: {ortalamaGelir:N2} TL");
            }
        }

        // EN ÇOK KİRALANAN ARAÇ RAPORUNU GÖSTEREN FONKSİYON
        public void EnCokKiralananAracRaporu()
        {
            Console.Clear();
            Console.WriteLine("---- EN ÇOK KİRALANAN ARAÇ ----\n");

            string plaka = EnCokKiralananArac();
            Arac arac = aracYoneticisi.AracBilgisiGetir(plaka);

            if (arac != null)
            {
                int kiralamaSayisi = rezervasyonYoneticisi.TumRezervasyonlariGetir()
                                                         .Count(r => r.Plaka == plaka);
                Console.WriteLine($"En çok kiralanan araç: {arac.MarkaModel}");
                Console.WriteLine($"Plaka: {arac.Plaka}");
                Console.WriteLine($"Kiralama Sayısı: {kiralamaSayisi}");
            }
            else
            {
                Console.WriteLine(plaka);
            }
        }

        // MÜŞTERİ REZERVASYONLARI RAPORU FONKSİYONU
        public void MusteriRezervasyonlariRaporu(string musteri)
        {
            Console.Clear();
            Console.WriteLine("---- MÜŞTERİ REZERVASYONLARI ----\n");

            List<Rezervasyon> rezervasyonlar = rezervasyonYoneticisi.MusteriRezervasyonlariniGetir(musteri);

            if (rezervasyonlar.Count == 0)
            {
                Console.WriteLine($"\n{musteri} adlı müşteriye ait rezervasyon bulunamadı.");
            }
            else
            {
                Console.WriteLine($"{musteri} adlı müşterinin rezervasyonları:\n");
                foreach (var rez in rezervasyonlar)
                {
                    Console.WriteLine("----------------------------------");
                    rez.BilgileriYazdir();
                }
                Console.WriteLine("-------------------------------------");
                Console.WriteLine($"\nToplam {rezervasyonlar.Count} rezervasyon");
            }
        }
    }
}