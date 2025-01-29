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

## 🖼 **Veritabanı ER Diyagramı**
📌 **ER Diagram**  
![Veritabanı Şeması](./screenshots/database-schema.png)

---

## 📌 **Notlar**
- **Kullanıcılar (`AspNetUsers`) içerik oluşturabilir ve yorum ekleyebilir.**
- **Makale ve konular (`Subjects`) arasında çoktan-çoğa ilişki vardır.**
- **Yorumlar (`Comments`) beğenilebilir (`CommentLikes`).**
- **Etiket sistemi (`Tags`) sayesinde makaleler kategorize edilebilir.**

---
