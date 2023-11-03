using System;
using System.IO;
using System.Net;
#if NETSTANDARD2_0 || NET6_0_OR_GREATER
using System.Net.Http;
#endif
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace libKimlik
{
    /// <summary>
    /// Bu sınıf T.C. Kimlik Numarası kontrollerini sağlar.
    /// </summary>
    public class TCKimlikNo
    {
        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarası
        /// </summary>
        public long KimlikNo { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 1. Hanesi
        /// </summary>
        public int BirinciHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 2. Hanesi
        /// </summary>
        public int IkinciHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 3. Hanesi
        /// </summary>
        public int UcuncuHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 4. Hanesi
        /// </summary>
        public int DorduncuHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 5. Hanesi
        /// </summary>
        public int BesinciHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 6. Hanesi
        /// </summary>
        public int AltinciHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 7. Hanesi
        /// </summary>
        public int YedinciHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 8. Hanesi
        /// </summary>
        public int SekizinciHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 9. Hanesi
        /// </summary>
        public int DokuzuncuHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 10. Hanesi
        /// </summary>
        public int OnuncuHane { get; private set; }

        /// <summary>
        /// Türkiye Cumhuriyeti Kimlik Numarasının 11. Hanesi
        /// </summary>
        public int OnbirinciHane { get; private set; }

        /// <summary>
        /// Bu sınıf T.C. Kimlik Numarası doğruluk kontrollerini algoritmik veya NVİ üzerinden sağlar.
        /// </summary>
        /// <param name="kimlikNo">11 haneli Türkiye Cumhuriyeti kimlik numarası</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TCKimlikNo(long kimlikNo)
        {
            if (kimlikNo < 10000000000 || kimlikNo > 99999999999)
            {
                throw new ArgumentOutOfRangeException(nameof(kimlikNo), "TC Kimlik Numarası 11 haneden oluşmalıdır. Belirlediğiniz değer(" + kimlikNo.ToString() + ") 11 haneli değil");
            }
            else
            {
                KimlikNo = kimlikNo;
                BirinciHane = int.Parse(kimlikNo.ToString()[0].ToString());
                IkinciHane = int.Parse(kimlikNo.ToString()[1].ToString());
                UcuncuHane = int.Parse(kimlikNo.ToString()[2].ToString());
                DorduncuHane = int.Parse(kimlikNo.ToString()[3].ToString());
                BesinciHane = int.Parse(kimlikNo.ToString()[4].ToString());
                AltinciHane = int.Parse(kimlikNo.ToString()[5].ToString());
                YedinciHane = int.Parse(kimlikNo.ToString()[6].ToString());
                SekizinciHane = int.Parse(kimlikNo.ToString()[7].ToString());
                DokuzuncuHane = int.Parse(kimlikNo.ToString()[8].ToString());
                OnuncuHane = int.Parse(kimlikNo.ToString()[9].ToString());
                OnbirinciHane = int.Parse(kimlikNo.ToString()[10].ToString());
            }
        }

        /// <summary>
        /// T.C. Kimlik numarasının algoritmik doğrulamasını yapar.
        /// </summary>
        /// <returns>Kimlik numarası doğruysa <b>true</b>, değilse <b>false</b> değer geri döndürür.</returns>
        public bool Kontrol()
        {
            if (BirinciHane == 0)
            {
                return false;
            }
            int evenSum = BirinciHane + UcuncuHane + BesinciHane + YedinciHane + DokuzuncuHane;
            int oddSum = IkinciHane + DorduncuHane + AltinciHane + SekizinciHane;
            int tenthDigit = ((evenSum * 7) - oddSum) % 10;
            int eleventhDigit = (evenSum + oddSum + OnuncuHane) % 10;
            if (OnuncuHane == tenthDigit && OnbirinciHane == eleventhDigit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
#if NET48_OR_GREATER
        /// <summary>
        /// T.C. Kimlik numarasının Nüfus ve Vatandaşlık İşleri Genel Müdürlüğü(NVİ) üzerinden doğrulamasını yapar.
        /// </summary>
        /// <param name="ad">Vatandaşın Adı</param>
        /// <param name="soyad">Vatandaşın Soyadı</param>
        /// <param name="dogumYili">Vatandaşın Doğum Yılı</param>
        /// <remarks>
        /// Not: Bu metotun çalışabilmesi için internet bağlantısı gerekmektedir. Bu metot internet bağlantısının kontrolünü sağlamaz.
        /// <br>Lütfen metotu çalıştırmadan önce internet bağlantısının kontrollerini yapınız.</br>
        /// </remarks>
        /// <returns>Vatandaşın bilgileri doğruysa <b>true</b>, değilse <b>false</b> değer geri döndürür.</returns>
        public async Task<bool> NVIKontrolAsync(string ad, string soyad, int dogumYili)
        {
            if (!Kontrol())
            {
                return false;
            }
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">\n  <soap12:Body>\n    <TCKimlikNoDogrula xmlns=\"http://tckimlik.nvi.gov.tr/WS\">\n      <TCKimlikNo>" + KimlikNo.ToString() + "</TCKimlikNo>\n      <Ad>" + ad.ToUpper() + "</Ad>\n      <Soyad>" + soyad.ToUpper() + "</Soyad>\n      <DogumYili>" + dogumYili.ToString() + "</DogumYili>\n    </TCKimlikNoDogrula>\n  </soap12:Body>\n</soap12:Envelope>");
            HttpWebRequest webRequest = CreateWebRequest("https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx", "http://tckimlik.nvi.gov.tr/WS/TCKimlikNoDogrula");
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            bool res;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    string dcc = await rd.ReadToEndAsync();
                    Match result = Regex.Match(dcc, "<TCKimlikNoDogrulaResult>(.*?)</TCKimlikNoDogrulaResult>");
                    if (result.Success)
                    {
                        Console.WriteLine(result.Groups[1].Value);
                        if (result.Groups[1].Value == "true")
                        {
                            res = true;
                        }
                        else
                        {
                            res = false;
                        }
                    }
                    else
                    {
                        res = false;
                    }
                }
            }
            return res;
        }

        private HttpWebRequest CreateWebRequest(string url, string action)
        {

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
                stream.GetHashCode();
            }
        }
#endif



#if NETSTANDARD2_0 || NET6_0_OR_GREATER
        /// <summary>
        /// T.C. Kimlik numarasının Nüfus ve Vatandaşlık İşleri Genel Müdürlüğü(NVİ) üzerinden doğrulamasını yapar.
        /// </summary>
        /// <param name="ad">Vatandaşın Adı</param>
        /// <param name="soyad">Vatandaşın Soyadı</param>
        /// <param name="dogumYili">Vatandaşın Doğum Yılı</param>
        /// <remarks>
        /// Not: Bu metotun çalışabilmesi için internet bağlantısı gerekmektedir. Bu metot internet bağlantısının kontrolünü sağlamaz.
        /// <br>Lütfen metotu çalıştırmadan önce internet bağlantısının kontrollerini yapınız.</br>
        /// </remarks>
        /// <returns>Vatandaşın bilgileri doğruysa <b>true</b>, değilse <b>false</b> değer geri döndürür.</returns>
        public async Task<bool> NVIKontrolAsync(string ad, string soyad, int dogumYili)
        {
            if (!Kontrol())
            {
                return false;
            }

            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
        <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
          <soap12:Body>
            <TCKimlikNoDogrula xmlns=""http://tckimlik.nvi.gov.tr/WS"">
              <TCKimlikNo>{KimlikNo}</TCKimlikNo>
              <Ad>{ad.ToUpper()}</Ad>
              <Soyad>{soyad.ToUpper()}</Soyad>
              <DogumYili>{dogumYili}</DogumYili>
            </TCKimlikNoDogrula>
          </soap12:Body>
        </soap12:Envelope>";
            using(var httpClient = new HttpClient())
	        {
                httpClient.DefaultRequestHeaders.Add("SOAPAction", "http://tckimlik.nvi.gov.tr/WS/TCKimlikNoDogrula");

                var content = new StringContent(soapEnvelope, Encoding.UTF8, "application/soap+xml");
                var response = await httpClient.PostAsync("https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx", content);

                if (response.IsSuccessStatusCode)
                {
                    var soapResponse = await response.Content.ReadAsStringAsync();
                    var result = Regex.Match(soapResponse, "<TCKimlikNoDogrulaResult>(.*?)</TCKimlikNoDogrulaResult>");
                    if (result.Success)
                    {
                        if (result.Groups[1].Value == "true")
                        {
                            return true;
                        }
                    }
                }

                return false;
	        }
        }
#endif

        /// <summary>
        /// Belirtilen nesnenin geçerli nesneye eşit olup olmadığını belirler.
        /// </summary>
        /// <param name="obj">Geçerli nesneyle karşılaştırılacak nesne.</param>
        /// <returns>Belirtilen nesne geçerli nesneye eşitse true; aksi takdirde false döndürür.</returns>
        public override bool Equals(object obj)
        {
            return obj is TCKimlikNo no && KimlikNo == no.KimlikNo;
        }

        /// <summary>
        /// Varsayılan karma işlevi olarak hizmet eder.
        /// </summary>
        /// <returns>Geçerli nesnenin karma kodu.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (17 * 23) + KimlikNo.GetHashCode();
            }
        }
    }
}
