# Blog Yönetim Sistemi

**ASP.NET Core MVC** ile geliştirilmiş, çok katmanlı(onion) bir **blog yönetim sistemi** projesidir. Yazarlar, makaleler oluşturabilir ve ziyaretçiler makaleleri okuyabilir. Sistem, **Identity kullanıcı yönetimi** ile yetkilendirme ve kimlik doğrulama sağlar.

## 📌 Proje Özellikleri

- Kullanıcı ve Rol Yönetimi (ASP.NET Core Identity)
- Makale ve Yazar Yönetimi
- Kategori Bazlı Makale Filtreleme
- Görüntü Yükleme Desteği
- Yetkilendirme ve Kimlik Doğrulama
- Veritabanı Olarak **SQL Server** Kullanımı

---

## 🛠 Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|-----------|---------|
| **.NET 7.0** | Ana geliştirme platformu |
| **ASP.NET Core MVC** | Web uygulama çatısı |
| **Entity Framework Core** | ORM katmanı |
| **Identity Framework** | Kullanıcı yönetimi |
| **Bootstrap 5** | Frontend tasarım |
| **Toastr.js** | Bildirim yönetimi |
| **MSSQL** | Veritabanı yönetimi |

---

## 📁 Proje Klasör Yapısı

```plaintext
HS14MVC
│── HS14MVC.Application
│   ├── DTOs
│   ├── Extensions
│   ├── Services
│── HS14MVC.Domain
│   ├── Core
│   ├── Entities
│   ├── Enums
│   ├── Utilities
│── HS14MVC.Infrastructure
│   ├── AppContext
│   ├── Configurations
│   ├── DataAccess
│   ├── Migrations
│   ├── Repositories
│── HS14MVC.UI
│   ├── Controllers
│   ├── Models
│   ├── Views
│   ├── wwwroot
│   ├── Areas
│── README.md

# 📊 Veritabanı Şeması

Bu proje, **kullanıcı yönetimi**, **makale yönetimi**, **yazarlar**, **konular** ve **etiketler** gibi temel bileşenlerden oluşan bir içerik yönetim sistemidir. Aşağıda, **veritabanı şeması ve ilişkileri** detaylandırılmıştır.

---

## 📌 **Tablo Yapıları**

### 🔹 **Kullanıcı ve Yetkilendirme**
| **Tablo** | **Açıklama** |
|-----------|-------------|
| `AspNetUsers` | Kullanıcı bilgilerini saklar (Admin, Yazar, Okuyucu) |
| `AspNetRoles` | Kullanıcı rollerini içerir (Admin, Kullanıcı, Yazar) |
| `AspNetUserRoles` | Kullanıcı ve roller arasındaki **çoktan-çoğa** ilişkiyi yönetir |
| `AspNetUserClaims` | Kullanıcıların özel yetki taleplerini saklar |
| `AspNetUserTokens` | Kullanıcıların giriş tokenlarını saklar |

---

### 📝 **Makale Yönetimi**
| **Tablo** | **Açıklama** |
|-----------|-------------|
| `Articles` | Makale bilgilerini içerir (Başlık, İçerik, Yayın Tarihi, Yazar, Konu) |
| `Authors` | Yazar bilgilerini içerir (Ad, Soyad, Biyografi) |
| `Subjects` | Makalelerin konularını yönetir |
| `ArticleSubjects` | Makale ve konular arasındaki **çoktan-çoğa** ilişkiyi yönetir |
| `ArticleTags` | Makaleler ve etiketler arasındaki **çoktan-çoğa** ilişkiyi yönetir |

---

### 🏷 **Etiketleme Sistemi**
| **Tablo** | **Açıklama** |
|-----------|-------------|
| `Tags` | Makalelerde kullanılan etiketleri içerir (**#ASP.NET, #MVC, #C#**) |
| `ArticleTags` | Makale ve etiketler arasındaki **çoktan-çoğa** ilişkiyi yönetir |

---


## 🔗 **Veritabanı İlişkileri**
- **`AspNetUsers` → `Articles`** → (1-N) **Bir kullanıcı birden fazla makale yazabilir.**
- **`Articles` → `Authors`** → (N-1) **Bir makalenin yalnızca bir yazarı olabilir.**
- **`Articles` → `Subjects`** → (N-N) **Bir makale birden fazla konuya sahip olabilir.**
- **`Articles` → `Tags`** → (N-N) **Bir makaleye birden fazla etiket eklenebilir.**

---



## 📌 **Notlar**
- **Kullanıcılar (`AspNetUsers`) içerik oluşturabilir ve yorum ekleyebilir.**
- **Makale ve konular (`Subjects`) arasında çoktan-çoğa ilişki vardır.**
- **Etiket sistemi (`Tags`) sayesinde makaleler kategorize edilebilir.**

## Gereksinimler

- **.NET 6.0** veya daha yeni bir sürüm
- **SQL Server** veya **SQL Server Express**
- **Visual Studio** veya **Visual Studio Code** (isteğe bağlı)

## Başlangıç

### 1. Projeyi Klonlayın

Öncelikle, projeyi GitHub'dan bilgisayarınıza klonlayın:

```bash
git clone https://github.com/kullaniciadi/MYBlogPage.git
!!! VEYA ZİP OLARAK İNDİR BİLGİSAYARINDA AÇ.

### 2. Bağlantı Dizesini Düzenleyin
appsettings.Development.json  dosyasının içeriğini aşağıdaki gibi düzenleyin.
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "AppConnectionDev": "Server=KENDI_SERVER_ADINIZ;Database=MYBlogPage;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
### 3. Veritabanı Migration'ları Uygulayın

Projeyi ilk kez çalıştırmadan önce, veritabanı yapısını oluşturmak için migration işlemi yapmanız gerekecek. Mevcut Migration klasörünü sil. Bunun için aşağıdaki adımları takip edebilirsiniz:

a. Paket Yöneticisi Konsolunu Açın (Visual Studio)
Tools > NuGet Package Manager > Package Manager Console menüsünden açın.

b. Migration'ları Uygulayın
Migration'ları uygulamak için şu komutları sırasıyla çalıştırın:
Add-Migration InitialCreate
Update-Database

### 4.  Kullanıcı Rolleri
Projede, AspNetRoles tablosuna Author ve AUTHOR gibi roller eklenmiştir. Bu rolleri manuel olarak eklemeniz gerekmektedir. Bunun için SQL Server Management Studio (SSMS) gibi bir araçla aşağıdaki SQL komutlarını çalıştırabilirsiniz:
SQL SERVER MANAGEMNET STUDİO' YU AÇ
AspNetRoles tablosuna  id kısmına guid id yarat ve  name kısmına başharfi Büyük olacak şekilde  Author ekle. NormalizedName kısmına tüm karaterler büyük olacak şekilde AUTHOR yazman gerekicek.
![guid](https://github.com/user-attachments/assets/d269ed89-8e23-4b5d-ad47-843c882fec1d)

## Mail Servisinin Yapılandırılması

Proje içinde kullanılan mail servisi, `MailService` sınıfı üzerinden e-posta gönderimi yapmaktadır. E-posta gönderebilmek için bir **Gmail hesabı** kullanmanız gerekmektedir. Bu hesabı SMTP üzerinden yapılandırarak e-posta gönderebilirsiniz.

### 1. Gmail Hesabı Oluşturun

Mail servisini çalıştırmak için bir Gmail hesabınız olmalıdır. Proje, bu hesap üzerinden e-posta göndermeyi sağlar. Eğer henüz bir Gmail hesabınız yoksa, [Google Hesabı Oluşturma](https://accounts.google.com/signup) sayfasından bir hesap oluşturabilirsiniz.

### 2. Gmail Hesabında Uygulama Şifresi Oluşturun

E-posta gönderimini güvenli bir şekilde yapabilmek için, Gmail hesabınızda uygulama şifresi oluşturmanız gerekmektedir. Gmail hesabınıza giriş yapın ve aşağıdaki adımları izleyin:

1. [Google Hesabınıza Giriş Yapın](https://myaccount.google.com/).
2. "Güvenlik" sekmesine gidin.
3. "Uygulama şifreleri" bölümüne tıklayın.
4. İki faktörlü doğrulama (2FA) etkinse, buradan yeni bir şifre oluşturabilirsiniz.
5. "Uygulama şifresi" oluşturun ve bu şifreyi `MailService` sınıfındaki SMTP doğrulaması için kullanın.

### 3. `MailService` Yapılandırma

Projenin `MailService` sınıfı, aşağıdaki gibi çalışmaktadır. E-posta gönderebilmek için aşağıdaki parametreleri doğru şekilde doldurduğunuzdan emin olun:

```csharp
public class MailService : IMailService
{
    public async Task SendMailAsync(SendMailDTO sendMailDTO)
    {
        try
        {
            var newMail = new MimeMessage();
            newMail.From.Add(MailboxAddress.Parse("marka.musayw@gmail.com"));  // Gönderen e-posta adresi
            newMail.To.Add(MailboxAddress.Parse(sendMailDTO.Email));  // Alıcı e-posta adresi
            newMail.Subject = sendMailDTO.Subject;  // E-posta konusu
            var builder = new BodyBuilder();
            builder.HtmlBody = sendMailDTO.Message;  // E-posta mesaj içeriği
            newMail.Body = builder.ToMessageBody();
            
            var smtp = new SmtpClient();
            
            // SMTP bağlantısı
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            
            // E-posta hesabı doğrulaması
            await smtp.AuthenticateAsync("marka.musayw@gmail.com", "gsprxolgcvndohgb");  // Burada, Gmail hesabınızın uygulama şifresini kullanmalısınız.
            
            // E-postayı gönder
            await smtp.SendAsync(newMail);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"E-posta Gönderiminde bir hata oluştu: {ex.Message}");
        }
    }
}

### ARTIK PROJEYİ ÇALIŞTIRABİLİRSİN...
