using System;
using System.Collections.Generic;
using System.Linq;

namespace AracKiralamaSistemi
{
    public class AracYoneticisi
    {
        private List<Arac> araclar;

        public AracYoneticisi()
        {
            araclar = new List<Arac>();
            OrnekAraclariEkle();
        }

        private void OrnekAraclariEkle()
        {
            araclar.Add(new Arac("34ABC123", "Toyota Corolla", 300));
            araclar.Add(new Arac("35HGF456", "Honda Civic", 550));
            araclar.Add(new Arac("06DEF789", "Renault Clio", 400));
            araclar.Add(new Arac("41GHI012", "Volkswagen Passat", 650));
            araclar.Add(new Arac("16JKL345", "Ford Focus", 480));
        }

        // SEÇİLEN TARİHLER ARALIĞINDAKİ MÜSAİT ARAÇLARI GETİRDİĞİMİZ FONKSİYON.
        public List<string> MusaitAraclariGetir(DateTime baslangic, DateTime bitis, RezervasyonYoneticisi rezervasyonYoneticisi)
        {
            List<string> musaitAraclar = new List<string>();

            foreach (var arac in araclar)
            {
                if (rezervasyonYoneticisi.AracMusaitMi(arac.Plaka, baslangic, bitis))
                {
                    musaitAraclar.Add(arac.Plaka);
                }
            }

            return musaitAraclar;
        }

        // ARACIN GÜNLÜK FİYATINI GETİREN FONKSİYON.
        public double AracGunlukFiyatiniGetir(string plaka)
        {
            Arac arac = araclar.FirstOrDefault(a => a.Plaka == plaka);
            return arac?.GunlukFiyat ?? 0;
        }

        // ARAÇ BİLGİSİNİ GETİRDİĞİMİZ FONKSİYON.
        public Arac AracBilgisiGetir(string plaka)
        {
            return araclar.FirstOrDefault(a => a.Plaka == plaka);
        }

        // SİSTEMDE ARAÇ VAR MI KONTROLÜ YAPTIĞIMIZ FONKSİYON.
        public bool AracVarMi(string plaka)
        {
            return araclar.Any(a => a.Plaka == plaka);
        }

        // SİSTEMDEKİ TÜM ARAÇLARI LİSTELEDİĞİMİZ FONKSİYON.
        public void TumAraclariListele()
        {
            Console.WriteLine("---- TÜM ARAÇLAR ----\n");
            foreach (var arac in araclar)
            {
                arac.BilgileriYazdir();
            }
        }

        // ARAÇ SAYISINI GETİRDİĞİMİZ FONKSİYON.
        public int ToplamAracSayisi()
        {
            return araclar.Count;
        }


        // SİSTEME DİNAMİK OLARAK YENİ ARAÇ EKLEDİĞİMİZ FONKSİYON.
        public void YeniAracEkle(string plaka, string markaModel, double fiyat)
        {
            // AYNI PLAKADA ARAÇ VAR MI KONTROLÜ YAPTIĞIMIZ YER.
            if (AracVarMi(plaka))
            {
                Console.WriteLine($"\n HATA: {plaka} plakalı araç zaten sistemde mevcut!");
                return;
            }

            // YENİ ARACI LİSTEYE EKLEDİĞİMİZ YER.
            araclar.Add(new Arac(plaka, markaModel, fiyat));
            Console.WriteLine($"\n✓ {plaka} plakalı {markaModel} başarıyla sisteme eklendi.");
        }
    }
}