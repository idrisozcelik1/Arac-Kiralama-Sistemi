using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaSistemi
{
    public class Rezervasyon
    {
        // CONSTRUCTOR VE PROPERTİES (YAPICI METOTLARIMIZ VE ÖZELLİKLERİMİZ)
        public string MusteriAdi { get; set; }
        public string Plaka { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public double ToplamUcret { get; set; }

        public Rezervasyon(string musteriAdi, string plaka, DateTime baslangic, DateTime bitis, double ucret)
        {
            MusteriAdi = musteriAdi;
            Plaka = plaka;
            BaslangicTarihi = baslangic;
            BitisTarihi = bitis;
            ToplamUcret = ucret;
        }

        public int GunSayisiniHesapla()
        {
            return (BitisTarihi - BaslangicTarihi).Days;
        }

        public void BilgileriYazdir()
        {
            Console.WriteLine($"Müşteri: {MusteriAdi}");
            Console.WriteLine($"Plaka: {Plaka}");
            Console.WriteLine($"Tarih: {BaslangicTarihi.ToShortDateString()} - {BitisTarihi.ToShortDateString()}");
            Console.WriteLine($"Gün Sayısı: {GunSayisiniHesapla()}");
            Console.WriteLine($"Toplam Ücret: {ToplamUcret} TL");
        }
    }


}

