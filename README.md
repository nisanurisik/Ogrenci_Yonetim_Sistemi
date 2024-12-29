# Öğrenci Yönetim Sistemi

Bu uygulama, bir eğitim kurumunun öğrenci bilgi sistemini yönetmek için tasarlanmış bir Windows Forms uygulamasıdır. Orta ölçekli bir eğitim kurumunun ihtiyaçlarını karşılamak üzere geliştirilmiştir.

## Projenin Amacı
Bu sistem, eğitim kurumundaki öğrenci, öğretmen ve memur işlemlerini etkin bir şekilde yönetmek için tasarlanmıştır. Üç ana kullanıcı tipi desteklenmektedir:
- **Öğrenciler**
- **Öğretmenler**
- **Memurlar (İdari personel)**

## Kullanılan Teknolojiler
- **Platform ve Framework**:
  - .NET Framework
  - Windows Forms (WinForms)
  - C# programlama dili
- **Veritabanı ve ORM**:
  - Entity Framework (Code-First Yaklaşımı)
  - SQL Server
- **Mimari ve Tasarım**:
  - Katmanlı Mimari (Entity katmanı ayrı tutulmuştur)
  - Form-based UI Tasarımı

## Temel Özellikler

### Kullanıcı Yönetimi
- Öğrenci, öğretmen ve memur giriş ekranları
- Kullanıcı doğrulama ve rol bazlı erişim kontrolü

### Öğrenci İşlemleri
- Ders seçme
- Not görüntüleme
- Transkript görüntüleme
- Danışman onayı sistemi

### Öğretmen İşlemleri
- Ders yönetimi
- Not girişi
- Danışmanlık işlemleri

### Memur İşlemleri
- Öğrenci kaydı oluşturma
- Öğretmen kaydı oluşturma
- Bölüm yönetimi

## Veritabanı Yapısı
Projede kullanılan temel tablolar:
- **Ogrenci**: Öğrenci bilgileri (ad, soyad, öğrenci numarası, bölüm, sınıf ve yarıyıl bilgisi)
- **Ogretmen**: Öğretmen bilgileri (ad, soyad, bölüm, verdiği dersler)
- **Ders**: Ders bilgileri
- **OgrenciDers**: Öğrenci-ders ilişkisi
- **OgrenciNot**: Not kayıtları

## Güçlü Yönler
- Katmanlı mimari kullanımı
- Entity Framework ile güçlü veritabanı yönetimi
- Rol bazlı erişim kontrolü
- Kapsamlı öğrenci bilgi sistemi özellikleri

