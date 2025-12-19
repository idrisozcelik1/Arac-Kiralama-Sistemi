using System;
using System.Collections.Generic;
using System.Linq;

namespace AracKiralamaSistemi
{
    public class RezervasyonYoneticisi
    {
        private List<Rezervasyon> rezervasyonlar;
        private AracYoneticisi aracYoneticisi;

        public RezervasyonYoneticisi(AracYoneticisi aracYoneticisi)
        {
            rezervasyonlar = new List<Rezervasyon>();
            this.aracYoneticisi = aracYoneticisi;
        }

        // ARAÇ MÜSAİT Mİ DEĞİL Mİ DİYE KONTROL EDEN FONKSİYON
        public bool AracMusaitMi(string plaka, DateTime bas, DateTime bit)
        {
            foreach (var rezervasyon in rezervasyonlar)
            {
                if (rezervasyon.Plaka == plaka)
                {
                    // TARİH ÇAKIŞMASI KONTROLÜ YAPIYORUZ.
                    if (!(bit <= rezervasyon.BaslangicTarihi || bas >= rezervasyon.BitisTarihi))
                    {
                        return false; // ÇAKIŞMA VAR, MÜSAİT DEĞİL OLARAK GÖSTERİYORUZ.
                    }
                }
            }
            return true; // ÇAKIŞMA YOK, MÜSAİT OLARAK GÖSTERİYORUZ.
        }

        // İNDİRİM MANTIĞI İLE BERABER REZERVASYON ÜCRETİ HESAPLAMASI YAPIYORUZ.
        public double RezervasyonUcretiHesapla(string plaka, DateTime bas, DateTime bit)
        {
            // 1. GİRİLEN TARİHLERDEN GÜN SAYSINI BULUYORUZ.
            int gunSayisi = (bit - bas).Days;

            // HATA KONTROLÜ: 0 VEYA NEGATİF GÜN GİRİLİRSE HATA VERİYORUZ.
            if (gunSayisi <= 0) return 0;

            // 2. İNDİRİMSİZ NORMAL FİYATI HESAPLIYORUZ.
            double gunlukFiyat = aracYoneticisi.AracGunlukFiyatiniGetir(plaka);
            double toplamTutar = gunSayisi * gunlukFiyat;

            // İNDİRİM UYGULAYACAĞIMIZ YER
            double indirimOrani = 0;

            // EĞER REZERVASYON 30 GÜNDEN FAZLA İSE %20 İNDİRİM UYGULUYORUZ.
            if (gunSayisi >= 30)
            {
                indirimOrani = 0.20;
                Console.WriteLine($"\n SÜPER FIRSAT: {gunSayisi} günlük kiralama için %20 indirim uygulandı!");
            }
            // EĞER REZERVASYON 7 GÜNDEN FAZLA 30 GÜNDEN AZSA %10 İNDİRİM UYGULUYORUZ.
            else if (gunSayisi >= 7)
            {
                indirimOrani = 0.10;
                Console.WriteLine($"\n FIRSAT: Haftalık kiralama indirimi (%10) uygulandı!");
            }

            // OLUŞAN İNDİRİM TUTARINI GERÇEK FİYATTAN DÜŞÜYORUZ.
            double indirimTutari = toplamTutar * indirimOrani;
            toplamTutar = toplamTutar - indirimTutari;

            // KAÇ TL İNDİRİM YAPILDIĞININ BİLGİSİNİ KULLANICIYA SUNUYORUZ.
            if (indirimTutari > 0)
            {
                Console.WriteLine($"   (Normal Tutar: {gunSayisi * gunlukFiyat} TL | İndirim: -{indirimTutari} TL)");
            }
            
            return toplamTutar;
        }

        // REZERVASYONU İPTAL ETTİĞİMİZ FONKSİYON
        public void RezervasyonIptal(string plaka)
        {
            int oncekiSayi = rezervasyonlar.Count;
            rezervasyonlar.RemoveAll(r => r.Plaka == plaka);

            if (rezervasyonlar.Count < oncekiSayi)
            {
                Console.WriteLine("\n✓ Rezervasyon(lar) başarıyla iptal edildi!");
            }
            else
            {
                Console.WriteLine("\nBu plakaya ait aktif rezervasyon bulunamadı.");
            }
        }

        // MÜŞTERİNİN REZERVASYONLARINI GETİRDİĞİMİZ FONKSİYON
        public List<Rezervasyon> MusteriRezervasyonlariniGetir(string musteri)
        {
            return rezervasyonlar.Where(r => r.MusteriAdi.ToLower() == musteri.ToLower()).ToList();
        }

        // TÜM REZERVASYONLARI GETİRDİĞİMİZ FONKSİYON
        public List<Rezervasyon> TumRezervasyonlariGetir()
        {
            return rezervasyonlar;
        }

        // REZERVASYON SAYISINI GETİRDİİĞİMİZ FONKSİYON
        public int ToplamRezervasyonSayisi()
        {
            return rezervasyonlar.Count;
        }
    
        // SİLİNEN REZERVASYON EKLEME FONKSİYONU
        public void RezervasyonEkle(string musteri, string plaka, DateTime bas, DateTime bit)
        {
            // ÜCRETİ HESAPLIYORUZ (İNDİRİM MANTIĞI BURADA İŞLİYOR).
            double ucret = RezervasyonUcretiHesapla(plaka, bas, bit);

            // YENİ REZERVASYON OLUŞTURUYORUZ.
            Rezervasyon yeniRezervasyon = new Rezervasyon(musteri, plaka, bas, bit, ucret);
            rezervasyonlar.Add(yeniRezervasyon);

            Console.WriteLine("\nRezervasyon başarıyla oluşturuldu!");
        }

    }
}