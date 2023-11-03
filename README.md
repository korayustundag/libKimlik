# Türkçe

# libKimlik Kütüphanesi

`libKimlik` kütüphanesi, Türkiye Cumhuriyeti Kimlik Numarasını (TC Kimlik No) doğrulama ve kontrol işlemleri yapmak için kullanılır. Bu belge, kütüphanenin işlevselliğini ve kullanımını anlatmaktadır.

## TCKimlikNo Sınıfı

`TCKimlikNo` sınıfı, TC Kimlik Numarasını temsil eden bir C# sınıfıdır. Aşağıda sınıfın özellikleri ve yöntemleri açıklanmıştır:

### Özellikler

#### `KimlikNo`

- Türkiye Cumhuriyeti Kimlik Numarasını temsil eden long türünde bir özelliktir.

#### `BirinciHane`, `IkinciHane`, `UcuncuHane`, `DorduncuHane`, `BesinciHane`, `AltinciHane`, `YedinciHane`, `SekizinciHane`, `DokuzuncuHane`, `OnuncuHane`, `OnbirinciHane`

- TC Kimlik Numarasının her bir hanesini temsil eden int özellikleridir.

### Yapıcı Metod

#### `TCKimlikNo(long kimlikNo)`

- Bu yapıcı metod, bir TC Kimlik Numarasını kabul eder ve numarayı analiz ederek sınıfın özelliklerini doldurur. Eğer verilen numara 11 haneli değilse bir exception fırlatılır.

### Metodlar

#### `Kontrol()`

- TC Kimlik Numarasının doğruluğunu kontrol eder ve geçerli bir numara ise `true`, aksi halde `false` döner.

#### `async Task<bool> NVIKontrolAsync(string ad, string soyad, int dogumYili)`

- Bu metod, TC Kimlik Numarasını, ad, soyad ve doğum yılı ile Nüfus ve Vatandaşlık İşleri Genel Müdürlüğü (NVİ) web servisi üzerinden doğrular. Eğer numara geçerliyse `true`, aksi halde `false` döner.

## Örnek Kullanım

Aşağıda `TCKimlikNo` sınıfının nasıl kullanılacağına dair basit bir örnek bulunmaktadır:

```csharp
long kimlikNo = 12345678901; // TC Kimlik Numarası
TCKimlikNo tcKimlik = new TCKimlikNo(kimlikNo);

if (tcKimlik.Kontrol())
{
    Console.WriteLine("TC Kimlik Numarası geçerlidir.");
    string ad = "John";
    string soyad = "Doe";
    int dogumYili = 1990;

    bool dogrulamaSonucu = await tcKimlik.NVIKontrolAsync(ad, soyad, dogumYili);

    if (dogrulamaSonucu)
    {
        Console.WriteLine("Kimlik doğrulama başarılı.");
    }
    else
    {
        Console.WriteLine("Kimlik doğrulama başarısız.");
    }
}
else
{
    Console.WriteLine("TC Kimlik Numarası geçersiz.");
}
```

Bu örnek, `TCKimlikNo` sınıfını kullanarak bir TC Kimlik Numarasının doğruluğunu ve Nüfus ve Vatandaşlık İşleri Genel Müdürlüğü (NVİ) servisi üzerinden doğruluğunu nasıl kontrol edeceğinizi göstermektedir.

# English

# libKimlik Library

The `libKimlik` library is used for validating and checking the Turkish Republic Identity Number (TC Kimlik No) in C#. This document explains the functionality and usage of the library.

## TCKimlikNo Class

The `TCKimlikNo` class represents a Turkish Republic Identity Number as a C# class. Below are the properties and methods of the class:

### Properties

#### `KimlikNo`

- A long property representing the Turkish Republic Identity Number.

#### `BirinciHane`, `IkinciHane`, `UcuncuHane`, `DorduncuHane`, `BesinciHane`, `AltinciHane`, `YedinciHane`, `SekizinciHane`, `DokuzuncuHane`, `OnuncuHane`, `OnbirinciHane`

- Integer properties representing each digit of the TC Kimlik No.

### Constructor Method

#### `TCKimlikNo(long kimlikNo)`

- This constructor takes a TC Kimlik No as input, analyzes the number, and populates the class properties. If the provided number is not 11 digits, an exception is thrown.

### Methods

#### `Kontrol()`

- Checks the validity of the TC Kimlik No, returning `true` if it's valid and `false` otherwise.

#### `async Task<bool> NVIKontrolAsync(string ad, string soyad, int dogumYili)`

- This method validates the TC Kimlik No against the General Directorate of Population and Citizenship Affairs (NVİ) web service using the provided TC Kimlik No, name, surname, and birth year. It returns `true` if the number is valid, and `false` otherwise.

## Example Usage

Here's a simple example of how to use the `TCKimlikNo` class:

```csharp
long kimlikNo = 12345678901; // TC Kimlik No
TCKimlikNo tcKimlik = new TCKimlikNo(kimlikNo);

if (tcKimlik.Kontrol())
{
    Console.WriteLine("TC Kimlik No is valid.");
    string name = "John";
    string surname = "Doe";
    int birthYear = 1990;

    bool validationResult = await tcKimlik.NVIKontrolAsync(name, surname, birthYear);

    if (validationResult)
    {
        Console.WriteLine("Identity validation successful.");
    }
    else
    {
        Console.WriteLine("Identity validation failed.");
    }
}
else
{
    Console.WriteLine("TC Kimlik No is invalid.");
}
```

This example demonstrates how to use the `TCKimlikNo` class to check the validity of a Turkish Republic Identity Number and validate it against the General Directorate of Population and Citizenship Affairs (NVİ) service.
