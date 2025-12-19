using System;

namespace AracKiralamaSistemi
{
    class Program
    {
        // YÖNETİCİ SINIFLARINI STATİK OLARAK TUTUYORUZ.
        static AracYoneticisi aracYoneticisi;
        static RezervasyonYoneticisi rezervasyonYoneticisi;
        static RaporYoneticisi raporYoneticisi;

        static void Main(string[] args)
        {
            // SİSTEMİN BAŞLANGICINDA YÖNETİCİ SINIFLARINI OLUŞTURUYORUZ.
            aracYoneticisi = new AracYoneticisi();
            rezervasyonYoneticisi = new RezervasyonYoneticisi(aracYoneticisi);
            raporYoneticisi = new RaporYoneticisi(rezervasyonYoneticisi, aracYoneticisi);

            MenuGoster();
        }

        static void MenuGoster()
        {
            // YAPMAK İSTEDİĞİMİZ İŞLEMLERİ MENÜ HALİNDE SUNUYORUZ.
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----ARAÇ REZERVASYON YÖNETİCİSİ----");
                Console.WriteLine();
                Console.WriteLine("1. Müsait Araçları Göster");
                Console.WriteLine("2. Rezervasyon Yap");
                Console.WriteLine("3. Rezervasyon İptal Et");
                Console.WriteLine("4. Müşteri Rezervasyonlarını Göster");
                Console.WriteLine("5. Toplam Geliri Göster");
                Console.WriteLine("6. En Çok Kiralanan Aracı Göster");
                Console.WriteLine("7. Tüm Araçları Listele");
                Console.WriteLine("8. Sisteme Yeni Araç Ekle"); 
                Console.WriteLine("0. Çıkış");
                Console.WriteLine();
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        MusaitAraclariGoster();
                        break;
                    case "2":
                        RezervasyonYap();
                        break;
                    case "3":
                        RezervasyonIptalEt();
                        break;
                    case "4":
                        MusteriRezervasyonlariGoster();
                        break;
                    case "5":
                        raporYoneticisi.ToplamGelirRaporuGoster();
                        break;
                    case "6":
                        raporYoneticisi.EnCokKiralananAracRaporu();
                        break;
                    case "7":
                        aracYoneticisi.TumAraclariListele();
                        break;
                    case "8":                
                        YeniAracEklemeEkrani();
                        break;
                    case "0":
                        Console.WriteLine("\nSistemden çıkılıyor... İyi günler!");
                        return;
                    default:
                        Console.WriteLine("\n Geçersiz seçim! Lütfen 0-7 arasında bir değer girin.");
                        break;
                }

                Console.WriteLine("\nDevam etmek için herhangi bir tuşa basın...");
                Console.ReadKey();
            }
        }

        static void MusaitAraclariGoster()
        {
            Console.Clear();
            Console.WriteLine("---- MÜSAİT ARAÇLARI GÖSTER ----\n");

            Console.Write("Başlangıç tarihi (gg.aa.yyyy): ");
            DateTime baslangic = TarihAl();

            Console.Write("Bitiş tarihi (gg.aa.yyyy): ");
            DateTime bitis = TarihAl();

            if (bitis <= baslangic)
            {
                Console.WriteLine("\n HATA: Bitiş tarihi başlangıç tarihinden sonra olmalıdır!");
                return;
            }

            var musaitAraclar = aracYoneticisi.MusaitAraclariGetir(baslangic, bitis, rezervasyonYoneticisi);

            if (musaitAraclar.Count == 0)
            {
                Console.WriteLine("\n Bu tarihler arasında müsait araç bulunmamaktadır.");
            }
            else
            {
                Console.WriteLine($"\n✓ {musaitAraclar.Count} adet müsait araç bulundu:\n");
                Console.WriteLine("-------------------------------------");
                foreach (var plaka in musaitAraclar)
                {
                    Arac arac = aracYoneticisi.AracBilgisiGetir(plaka);
                    arac.BilgileriYazdir();
                }
                Console.WriteLine("-----------------------------------");
            }
        }

        // REZERVASYON YAPTIĞIMIZ FONKSİYON.
        static void RezervasyonYap()
        {
            Console.Clear();
            Console.WriteLine("---- YENİ REZERVASYON ----\n");

            Console.Write("Müşteri adı: ");
            string musteri = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(musteri))
            {
                Console.WriteLine("\n HATA: Müşteri adı boş olamaz!");
                return;
            }

            Console.Write("Plaka: ");
            string plaka = Console.ReadLine().ToUpper();

            // Araç kontrolü
            if (!aracYoneticisi.AracVarMi(plaka))
            {
                Console.WriteLine("\n HATA: Bu plakaya ait araç bulunamadı!");
                return;
            }

            Console.Write("Başlangıç tarihi (gg.aa.yyyy): ");
            DateTime baslangic = TarihAl();

            Console.Write("Bitiş tarihi (gg.aa.yyyy): ");
            DateTime bitis = TarihAl();

            if (bitis <= baslangic)
            {
                Console.WriteLine("\n HATA: Bitiş tarihi başlangıç tarihinden sonra olmalıdır!");
                return;
            }

            // SEÇİLEN TARİHLERDE ARAÇ MÜSAİT Mİ KONTROLÜ YAPIYORUZ.
            if (!rezervasyonYoneticisi.AracMusaitMi(plaka, baslangic, bitis))
            {
                Console.WriteLine("\n HATA: Bu araç seçilen tarihlerde müsait değil!");
                return;
            }

            double ucret = rezervasyonYoneticisi.RezervasyonUcretiHesapla(plaka, baslangic, bitis);
            Arac arac = aracYoneticisi.AracBilgisiGetir(plaka);

            Console.WriteLine("\n---- REZERVASYON ÖZETİ ----");
            Console.WriteLine($"Araç: {arac.MarkaModel}");
            Console.WriteLine($"Plaka: {plaka}");
            Console.WriteLine($"Müşteri: {musteri}");
            Console.WriteLine($"Tarih: {baslangic.ToShortDateString()} - {bitis.ToShortDateString()}");
            Console.WriteLine($"Gün Sayısı: {(bitis - baslangic).Days}");
            Console.WriteLine($"Günlük Fiyat: {arac.GunlukFiyat} TL");
            Console.WriteLine($"TOPLAM ÜCRET: {ucret} TL");
            Console.WriteLine("------------------------");

            Console.Write("\nRezervasyon onaylıyor musunuz? (E/H): ");
            string onay = Console.ReadLine().ToUpper();

            if (onay == "E")
            {
                rezervasyonYoneticisi.RezervasyonEkle(musteri, plaka, baslangic, bitis);
            }
            else
            {
                Console.WriteLine("\n Rezervasyon iptal edildi.");
            }
        }

        static void RezervasyonIptalEt()
        {
            Console.Clear();
            Console.WriteLine("---- REZERVASYON İPTAL ----n");

            Console.Write("İptal edilecek aracın plakası: ");
            string plaka = Console.ReadLine().ToUpper();

            rezervasyonYoneticisi.RezervasyonIptal(plaka);
        }

        static void MusteriRezervasyonlariGoster()
        {
            Console.Clear();
            Console.Write("Müşteri adı: ");
            string musteri = Console.ReadLine();

            raporYoneticisi.MusteriRezervasyonlariRaporu(musteri);
        }

        // TARİH GİRİŞİ ALDIĞIMIZ FONKSİYON.
        static DateTime TarihAl()
        {
            while (true)
            {
                try
                {
                    string girdi = Console.ReadLine();
                    return DateTime.Parse(girdi);
                }
                catch
                {
                    Console.Write(" Geçersiz tarih formatı! Tekrar deneyin (gg.aa.yyyy): ");
                }
            }
        }
    

        static void YeniAracEklemeEkrani()
        {
            Console.Clear();
            Console.WriteLine("---- SİSTEME YENİ ARAÇ EKLEME ----\n");

            Console.Write("Araç Plakası (Örn: 34ABC99): ");
            string plaka = Console.ReadLine().ToUpper().Trim();

            if (string.IsNullOrWhiteSpace(plaka))
            {
                Console.WriteLine(" Plaka boş olamaz!");
                return;
            }

            // ARACIN ZATEN SİSTEMDE OLUP OLMADIĞINI KONTROL EDİYORUZ.
            if (aracYoneticisi.AracVarMi(plaka))
            {
                Console.WriteLine("\n Bu plaka zaten sistemde kayıtlı!");
                Console.WriteLine("Devam etmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            Console.Write("Marka ve Model (Örn: BMW 320i): ");
            string marka = Console.ReadLine();

            Console.Write("Günlük Kiralama Fiyatı (TL): ");
            double fiyat;

            // FİYAT GİRİŞİNİ DOĞRULUYORUZ.
            if (!double.TryParse(Console.ReadLine(), out fiyat) || fiyat <= 0)
            {
                Console.WriteLine(" Geçersiz fiyat! Pozitif bir sayı girmelisiniz.");
                Console.WriteLine("Devam etmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            // YÖNETİCİ SINIFI ÜZERİNDEN MENÜYE DÖNÜYORUZ.
            aracYoneticisi.YeniAracEkle(plaka, marka, fiyat);

            Console.WriteLine("\nMenüye dönmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}