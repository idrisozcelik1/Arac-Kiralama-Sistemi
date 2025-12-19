# 🚗 Akıllı Araç Kiralama Rezervasyon Sistemi

Bu proje, **C# Console Application** kullanılarak geliştirilmiş, nesne tabanlı mimariye (OOP) uygun bir araç kiralama ve rezervasyon yönetim sistemidir.

Proje, araç müsaitlik durumlarını kontrol etme, dinamik fiyatlandırma algoritmaları ve raporlama gibi temel iş akışlarını simüle eder.

---

## 🚀 Projenin Özellikleri

### 1. Temel Fonksiyonlar
* **Araç Müsaitlik Kontrolü:** Seçilen tarih aralığında aracın dolu olup olmadığını algılayan çakışma kontrol algoritması.
* **Rezervasyon Yönetimi:** Yeni rezervasyon oluşturma ve var olan rezervasyonu iptal etme.
* **Raporlama Sistemi:**
  * Toplam ciro hesaplama.
  * En çok kiralanan aracı (Best Seller) tespit etme.
  * Müşteri bazlı rezervasyon geçmişi dökümü.

### 2. Gelişmiş (Bonus) Özellikler ✨
Proje standart isterlerin ötesine geçerek şu özellikleri de barındırır:
* **💸 Akıllı İndirim Algoritması:** * 7 günden uzun kiralamalarda **%10**, 
  * 30 günden uzun kiralamalarda **%20** otomatik indirim uygular.
* **🚙 Dinamik Araç Yönetimi:** Program çalışırken konsol üzerinden sisteme yeni araç eklenebilir.
* **🏷️ Araç Sınıflandırması:** Araçlar SUV, Sedan, Hatchback gibi segmentlere ayrılmıştır.
* **🛡️ Hata Yakalama (Exception Handling):** Tarih ve sayısal girişlerde kullanıcının programı çökertmesi engellenmiştir.

---

## 🏗️ Proje Mimarisi

Proje **"Manager Design Pattern"** (Yönetici Tasarım Deseni) mantığıyla kurgulanmıştır:

* **📂 Varlıklar (Entities):**
  * `Arac`: Plaka, model, fiyat ve sınıf bilgilerini tutar.
  * `Rezervasyon`: Müşteri, tarih ve ücret bilgilerini tutar.
  
* **📂 Yöneticiler (Managers):**
  * `AracYoneticisi`: Araç ekleme, listeleme ve bilgi getirme işlemlerini yönetir.
  * `RezervasyonYoneticisi`: Kiralama mantığını, fiyat hesaplamayı ve tarih çakışmalarını yönetir.
  * `RaporYoneticisi`: İstatistiksel verileri hesaplar ve sunar.

---

## 💻 Kurulum ve Çalıştırma

1- Projeyi bilgisayarınıza indirin.
2- Gerekli class'ları çalıştırın.
3- Otomasyonu kullanmaya hazırsınız.
