using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaSistemi
{
    public class Arac
    {
        // CONSTRUCTOR VE PROPERTİES (YAPICI METOTLARIMIZ VE ÖZELLİKLERİMİZ)
        public string Plaka { get; set; }
        public string MarkaModel { get; set; }
        public double GunlukFiyat { get; set; }

        public Arac(string plaka, string markaModel, double gunlukFiyat)
        {
            Plaka = plaka;
            MarkaModel = markaModel;
            GunlukFiyat = gunlukFiyat;
        }

        public void BilgileriYazdir()
        {
            Console.WriteLine($"Plaka: {Plaka} | {MarkaModel} | {GunlukFiyat} TL/gün");
        }

    }
}
