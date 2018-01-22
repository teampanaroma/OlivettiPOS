using System.Collections.Generic;

using System.Linq;

namespace OlivettiPOS
{
    public class ErrorCode
    {
        public string errorCode;

        public string errorMessage;

        public string errorExplanation;

        public static List<ErrorCode> errorList;

        private string p1 { get; set; }

        private string p2 { get; set; }

        private string p3 { get; set; }

        private static ErrorCode manualErrorCode = new ErrorCode();

        public static ErrorCode CreateError(string p1, string p2, string p3)
        {
            // TODO: Complete member initialization

            manualErrorCode.errorCode = p1;

            manualErrorCode.errorMessage = p2;

            manualErrorCode.errorExplanation = p3;

            return manualErrorCode;
        }

        public ErrorCode()
        {
            // TODO: Complete member initialization
        }

        public static ErrorCode GetErrorExplanation(string errorCode)//arif ekledi beğenmedim değiştirelecek.
        {
            if (errorList == null)
            {
                errorList = new List<ErrorCode>();

                errorList.Add(new ErrorCode { errorCode = "E0", errorMessage = "", errorExplanation = "Bu İşlemi Gerçekleştirmek için servis modundan çıkmak zorundasınız." });

                errorList.Add(new ErrorCode { errorCode = "E001", errorMessage = "", errorExplanation = "İşlem Gerçekleştirilemedi." });

                errorList.Add(new ErrorCode { errorCode = "E000", errorMessage = "Refuse this function-1", errorExplanation = "Fonksiyon Çalıştırılamadı-1." });

                errorList.Add(new ErrorCode { errorCode = "E001", errorMessage = "Refuse this function-2", errorExplanation = "Fonksiyon Çalıştırılamadı.-2" });

                errorList.Add(new ErrorCode { errorCode = "E002", errorMessage = "Clerk not yet login or login error", errorExplanation = "Kasiyer Girişi Yapılmadı." });

                errorList.Add(new ErrorCode { errorCode = "E003", errorMessage = "There are some wrong char in the Data", errorExplanation = "İşlem Tamamlanamadı, Lütfen parametreleri kontrol ediniz." });

                errorList.Add(new ErrorCode { errorCode = "E004", errorMessage = "Clerk No. invalid", errorExplanation = "Kasiyer numarası Hatalı." });

                errorList.Add(new ErrorCode { errorCode = "E005", errorMessage = "Quantity input error", errorExplanation = "Adet Girişi Hatalı" });

                errorList.Add(new ErrorCode { errorCode = "E006", errorMessage = "Incoming data has syntax error", errorExplanation = "Gelen veride hatalar var. " });

                errorList.Add(new ErrorCode { errorCode = "E007", errorMessage = "Prohibit discount", errorExplanation = "İndirim Engellendi." });

                errorList.Add(new ErrorCode { errorCode = "E008", errorMessage = "Prohibit +%", errorExplanation = "Engelli +%." });

                errorList.Add(new ErrorCode { errorCode = "E009", errorMessage = "Prohibit -%", errorExplanation = "Engelli -%." });

                errorList.Add(new ErrorCode { errorCode = "E010", errorMessage = "Prohibit void", errorExplanation = "Engellenmiş fonskiyon.Çalıştırılamaz." });

                errorList.Add(new ErrorCode { errorCode = "E011", errorMessage = "Press Price key invalid", errorExplanation = "Fiyat Hatalı." });

                errorList.Add(new ErrorCode { errorCode = "E012", errorMessage = "Press FC key invalid", errorExplanation = "FC bilgisi hatalı." });

                errorList.Add(new ErrorCode { errorCode = "E013", errorMessage = "Prohibit to press EC key", errorExplanation = "EC fonksiyonu engellendi." });

                errorList.Add(new ErrorCode { errorCode = "E014", errorMessage = "Prohibit to press Cash key", errorExplanation = "Nakit fonksiyonu engellendi." });

                errorList.Add(new ErrorCode { errorCode = "E013", errorMessage = "Prohibit to press Credit key", errorExplanation = "Kredi fonksiyonu engellendi." });

                errorList.Add(new ErrorCode { errorCode = "E013", errorMessage = "Prohibit to press Check key", errorExplanation = "Kontrol fonksiyonu engellendi." });

                errorList.Add(new ErrorCode { errorCode = "E017", errorMessage = "Please clear account", errorExplanation = "Z Raporu Alınız." });

                errorList.Add(new ErrorCode { errorCode = "E018", errorMessage = "Password Error", errorExplanation = "Şifre Hatalı." });

                errorList.Add(new ErrorCode { errorCode = "E019", errorMessage = "Saat Ayarlanamadı.", errorExplanation = "Saat Ayarlanamadı." });

                errorList.Add(new ErrorCode { errorCode = "E020", errorMessage = "Input Barcode Error", errorExplanation = "Barcode hatalı." });

                errorList.Add(new ErrorCode { errorCode = "E021", errorMessage = "Input date error", errorExplanation = "Tarih Girişi Hatalı." });

                errorList.Add(new ErrorCode { errorCode = "E022", errorMessage = "Daily Memory Open Error", errorExplanation = "Günlük Hafıza Açık Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E023", errorMessage = "Packet No.Error", errorExplanation = "Paket Numarası Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E024", errorMessage = "100 PLU at 1 receipt at most", errorExplanation = "1 Fiş de En fazla 100 PLU Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E025", errorMessage = "No Hardware Reset", errorExplanation = "No hardware reset." });

                errorList.Add(new ErrorCode { errorCode = "E026", errorMessage = "You are in User Mode", errorExplanation = "Kullanıcı Konumundasınız." });

                errorList.Add(new ErrorCode { errorCode = "E027", errorMessage = "You have already printed today!", errorExplanation = "Gün için zaten Yazdırdınız." });

                errorList.Add(new ErrorCode { errorCode = "E028", errorMessage = "ECR can't be setted recovery data when Z count isn't equal 0", errorExplanation = "Z sayısı 0 değilse Kasa recovery datası işleyemez." });

                errorList.Add(new ErrorCode { errorCode = "E029", errorMessage = "Password Error", errorExplanation = "Şifre Hatalı." });

                errorList.Add(new ErrorCode { errorCode = "E030", errorMessage = "DClear Daily report Error", errorExplanation = "Günlük rapor hatası" });

                errorList.Add(new ErrorCode { errorCode = "E031", errorMessage = "Tax index error", errorExplanation = "Vergi Belirleme Numarası Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E032", errorMessage = "DATA GIRIS HATASI!", errorExplanation = "Veri Giriş Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E033", errorMessage = "Total Amount Error", errorExplanation = "Toplam Fiyat Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E034", errorMessage = "FP checksum Error", errorExplanation = "FP CheckSum Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E035", errorMessage = "Invalid Weight", errorExplanation = "Hatalı Ağırlık." });

                errorList.Add(new ErrorCode { errorCode = "E036", errorMessage = "ECR is on Sale", errorExplanation = "Kasa Satış Konumunda." });

                errorList.Add(new ErrorCode { errorCode = "E037", errorMessage = "Cash in drawer overflows", errorExplanation = "Çekmecede Nakit Fazlası." });

                errorList.Add(new ErrorCode { errorCode = "E038", errorMessage = "Olay Kaydı Index Hatası", errorExplanation = "Olay Kaydı Index Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E039", errorMessage = "Cash in drawer is not enough", errorExplanation = "Kasadaki Nakit Yeterli Değil." });

                errorList.Add(new ErrorCode { errorCode = "E040", errorMessage = "R/A Amount overflows", errorExplanation = "R/A Amount overflows" });

                errorList.Add(new ErrorCode { errorCode = "E041", errorMessage = "P/O Amount overflows", errorExplanation = "P/O Amount overflows" });

                errorList.Add(new ErrorCode { errorCode = "E042", errorMessage = "PLU Bulunamadı", errorExplanation = "PLU Bulunamadı!" });

                errorList.Add(new ErrorCode { errorCode = "E043", errorMessage = "Sales prive Invalid", errorExplanation = "Satış fiyatı Hatalı" });

                errorList.Add(new ErrorCode { errorCode = "E044", errorMessage = "Item total error", errorExplanation = "Toplam Ürün Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E045", errorMessage = "PLU report overflows", errorExplanation = "Plu Rapor Taşması." });

                errorList.Add(new ErrorCode { errorCode = "E046", errorMessage = "Fiscal code len error", errorExplanation = "Mali Kod Uzunluk Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E047", errorMessage = "Fiscal code & num error", errorExplanation = "Mali Kod ve Numara Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E048", errorMessage = "Fiscal Number Error", errorExplanation = "Mali Numara Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E049", errorMessage = "Fiscal Code & Number Have Been Set", errorExplanation = "Cihaz Zaten Mali Konumda." });

                errorList.Add(new ErrorCode { errorCode = "E050", errorMessage = "PIN code & number have been set ", errorExplanation = "Pin Code ve Numara Zaten Ayarlı." });

                errorList.Add(new ErrorCode { errorCode = "E051", errorMessage = "Tax number error", errorExplanation = "Vergi Numarası Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E052", errorMessage = "Now in invoice mode", errorExplanation = "Kasa Fatura Modunda." });

                errorList.Add(new ErrorCode { errorCode = "E053", errorMessage = "No Tax", errorExplanation = "Vergi Tanımlanmadı." });

                errorList.Add(new ErrorCode { errorCode = "E054", errorMessage = "Error Department number", errorExplanation = "Department Numarası Hatasıs" });

                errorList.Add(new ErrorCode { errorCode = "E055", errorMessage = "Satış Fişi Hatası", errorExplanation = "Satış Fişi Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E056", errorMessage = "Fiscal code or number error", errorExplanation = "Mali Kod Ya da Numara Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E057", errorMessage = "Fiscalization is Failed Please Set Date/Time", errorExplanation = "Cihaz Malileştirilemedi, Lütfen Tarih Saat Ayarlayın." });

                errorList.Add(new ErrorCode { errorCode = "E058", errorMessage = "write fiscal memory error", errorExplanation = "Z raporunda Mali Hafıza Yazma Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E059", errorMessage = "ECR is not in Fiscal mode", errorExplanation = "Kasa Mali Mod da Değil" });

                errorList.Add(new ErrorCode { errorCode = "E060", errorMessage = "PLU Can not return", errorExplanation = "PLU geri döndürülemedi" });

                errorList.Add(new ErrorCode { errorCode = "E061", errorMessage = "Error Group ID", errorExplanation = "Group Numara Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E062", errorMessage = "Printer No paper", errorExplanation = "Lütfen Yazıcıya kağıt Ekleyin." });

                errorList.Add(new ErrorCode { errorCode = "E063", errorMessage = "Printer 1 No paper", errorExplanation = "Birinci yazıcıya kağıt ekleyin" });

                errorList.Add(new ErrorCode { errorCode = "E064", errorMessage = "Printer 2 No paper", errorExplanation = "İkinci yazıcıya kağıt ekleyin." });

                errorList.Add(new ErrorCode { errorCode = "E065", errorMessage = "Find No relative data", errorExplanation = "İlişkili veri bulunamadı." });

                errorList.Add(new ErrorCode { errorCode = "E066", errorMessage = "Now in fiscal mode", errorExplanation = "Kasa Mali Modda" });

                errorList.Add(new ErrorCode { errorCode = "E067", errorMessage = "Pay Credit error", errorExplanation = "Kredi Kartı ile ödeme hatası" });

                errorList.Add(new ErrorCode { errorCode = "E068", errorMessage = "HARDWARE RESET ERROR", errorExplanation = "Kasa Reset Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E069", errorMessage = "RecycleFlag=1 but no match record in recycle table", errorExplanation = "RecycleFlag=1 but no match record in recycle table." });

                errorList.Add(new ErrorCode { errorCode = "E070", errorMessage = "no set TIN or NIN", errorExplanation = "TC no Ya da Vergi No Ayarlanmadı." });

                errorList.Add(new ErrorCode { errorCode = "E071", errorMessage = "NTP disconnected over 60 days", errorExplanation = "NTP ye Bağlanmama süresi aşıldı." });

                errorList.Add(new ErrorCode { errorCode = "E072", errorMessage = "Electronic journal exchange", errorExplanation = "Ekü Değiştirildi" });

                errorList.Add(new ErrorCode { errorCode = "E073", errorMessage = "No set fis code - num", errorExplanation = "Mali Kod ve Numara Ayarlanmadı." });

                errorList.Add(new ErrorCode { errorCode = "E074", errorMessage = "ECR cant download program", errorExplanation = "Kasa Programı Yükleyemedi." });

                errorList.Add(new ErrorCode { errorCode = "E075", errorMessage = "Member Card input error", errorExplanation = "Üye kart giriş Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E076", errorMessage = "Machine number isnt setted", errorExplanation = "Kasa Numarası set edilmedi." });

                errorList.Add(new ErrorCode { errorCode = "E077", errorMessage = "ECR not in fiscal mode", errorExplanation = "Kasa mali Modda Değil." });

                errorList.Add(new ErrorCode { errorCode = "E078", errorMessage = "Head message are not setted ok", errorExplanation = "Fiş başlığı ayarlanmadı." });

                errorList.Add(new ErrorCode { errorCode = "E079", errorMessage = "No FM or address or data bus short", errorExplanation = "FM Bulunamadı" });

                errorList.Add(new ErrorCode { errorCode = "E080", errorMessage = "ECR ram checksum error", errorExplanation = "Kasa ram checksum error" });

                errorList.Add(new ErrorCode { errorCode = "E081", errorMessage = "Separate pay error", errorExplanation = "Parçalı ödeme hatası." });

                errorList.Add(new ErrorCode { errorCode = "E082", errorMessage = "SERIAL FLASH ERROR", errorExplanation = "USB Disk Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E083", errorMessage = "Please clear PLU report", errorExplanation = "PLU Raporunu Temizleyin" });

                errorList.Add(new ErrorCode { errorCode = "E084", errorMessage = "TIN and NIN are not setted", errorExplanation = "TC No ya da Vergi No ayarlanmadı." });

                errorList.Add(new ErrorCode { errorCode = "E086", errorMessage = "Z report is over 4 times in one day", errorExplanation = "Gün içinde 4 seferden fazla Z raporu alınamaz." });

                errorList.Add(new ErrorCode { errorCode = "E087", errorMessage = "Low battery or short-circuit for RAM", errorExplanation = "Düşük batarya ya da Ram da kısa devre oluştu." });

                errorList.Add(new ErrorCode { errorCode = "E088", errorMessage = "press SAVE error", errorExplanation = "Kaydet Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E089", errorMessage = "press RECALL error", errorExplanation = "Recall Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E090", errorMessage = "Insufficient stock", errorExplanation = "Yetersiz Stok." });

                errorList.Add(new ErrorCode { errorCode = "E091", errorMessage = "Non fiscal or fiscal receipt already open", errorExplanation = "DAHA ÖNCE BİR BELGE AÇILMIŞ" });

                errorList.Add(new ErrorCode { errorCode = "E092", errorMessage = "SD Card verify error", errorExplanation = "SD KART Doğrulama hatası." });

                errorList.Add(new ErrorCode { errorCode = "E093", errorMessage = "SD Card out or bad", errorExplanation = "SD KART Çıkartıldı ya da okunamadı." });

                errorList.Add(new ErrorCode { errorCode = "E094", errorMessage = "SDS ACK timeout", errorExplanation = "SDS ACK timeout." });

                errorList.Add(new ErrorCode { errorCode = "E095", errorMessage = "Lock SD card error", errorExplanation = "Lock SD card error" });

                errorList.Add(new ErrorCode { errorCode = "E096", errorMessage = "Unlock SD card error", errorExplanation = "Unlock SD card error" });

                errorList.Add(new ErrorCode { errorCode = "E097", errorMessage = "No Further Invalid Login Allowed", errorExplanation = "Yanlış giriş Limitiniz Doldu." });

                errorList.Add(new ErrorCode { errorCode = "E098", errorMessage = "price dot isn't setted ok when ECR is entered fiscal mode", errorExplanation = "price dot isn't setted ok when ECR is entered fiscal mode." });

                errorList.Add(new ErrorCode { errorCode = "E099", errorMessage = "fiscal logo isn't setted ok when ECR is entered fiscal mode", errorExplanation = "ECR Mali modda iken Mali logo setlenemedi" });

                errorList.Add(new ErrorCode { errorCode = "E100", errorMessage = "Fiscal mode flag error in FM", errorExplanation = "Kağıt bitmek üzere kontrol ediniz" });

                errorList.Add(new ErrorCode { errorCode = "E101", errorMessage = "ECR isn't in user or fiscal mode", errorExplanation = "Kasa Mali ya da Kullanacı modun da değil." });

                errorList.Add(new ErrorCode { errorCode = "E102", errorMessage = "EJournal is full, end approve first and then change it", errorExplanation = "Ekü doldu. Önce Sonlandırın ardından Değiştirin" });

                errorList.Add(new ErrorCode { errorCode = "E103", errorMessage = "SD with old files", errorExplanation = "Sd kart ta eski dosyalar mevcut." });

                errorList.Add(new ErrorCode { errorCode = "E104", errorMessage = "EJournal not approve yet, please start it", errorExplanation = "Ekü onaylanmadı lütfen başlatın." });

                errorList.Add(new ErrorCode { errorCode = "E105", errorMessage = "EJournal end approve aleady, please change it", errorExplanation = "Ekü zaten sonlandırılmış. Lütfen değiştirin." });

                errorList.Add(new ErrorCode { errorCode = "E106", errorMessage = "DPC is nearly full", errorExplanation = "DPC nerdeyse doldu." });

                errorList.Add(new ErrorCode { errorCode = "E107", errorMessage = "EJournal can't find the object that meet the condition", errorExplanation = "EKÜ Belirtilen şartlarda sonuç bulamadı." });

                errorList.Add(new ErrorCode { errorCode = "E108", errorMessage = "Old EJournal can be read only", errorExplanation = "Eski EKÜ Sadece okunabilir." });

                errorList.Add(new ErrorCode { errorCode = "E109", errorMessage = "EJournal isn't inserted in or invalid card", errorExplanation = "EKÜ Takılmadı ya da Hatalı Ekü" });

                errorList.Add(new ErrorCode { errorCode = "E110", errorMessage = "EJournal is nearly full", errorExplanation = "EKÜ neredeyse dolu" });

                errorList.Add(new ErrorCode { errorCode = "E111", errorMessage = "EJournal is broken, could end approve only", errorExplanation = "EKÜ Hatalı, Lütfen sonlandırın" });

                errorList.Add(new ErrorCode { errorCode = "E112", errorMessage = "SD CARD ERROR", errorExplanation = "SD Kart Hatalı." });

                errorList.Add(new ErrorCode { errorCode = "E113", errorMessage = "ECR date earlier than Jan 1,2008", errorExplanation = "Kasa tarihi   1 Haziran,2008 den önceye ayarlanmış." });

                errorList.Add(new ErrorCode { errorCode = "E114", errorMessage = "Current time earlier than last EJ time, please adjust date and time", errorExplanation = "Kasa Tarihi son EKÜ Tarihinden önceye ayarlanmış.Lütfen güncel hale alınız." });

                errorList.Add(new ErrorCode { errorCode = "E115", errorMessage = "DPC is full", errorExplanation = "DPC dolu." });

                errorList.Add(new ErrorCode { errorCode = "E116", errorMessage = "EJournal approve aleady", errorExplanation = "EKÜ onaylandı." });

                errorList.Add(new ErrorCode { errorCode = "E117", errorMessage = "Other ECR EJournal, please insert correct one", errorExplanation = "Farklı Ekü Algılandı.Lütfen Doğru Eküyü takınız" });

                errorList.Add(new ErrorCode { errorCode = "E118", errorMessage = "Current EJournal not ended yet, please end it first", errorExplanation = "Asıl Ekü Sonlandırılmadı.Lütfen Asıl Eküyü Sonlandırın." });

                errorList.Add(new ErrorCode { errorCode = "E119", errorMessage = "Protocol Error", errorExplanation = "Protokol Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E120", errorMessage = "ONE EJ COPY IS BROKEN!", errorExplanation = "Bir EKÜ Kopyası Bozuk." });

                errorList.Add(new ErrorCode { errorCode = "E121", errorMessage = "EJ Data is Incorrect!", errorExplanation = "EKÜ Datası Hatalı." });

                errorList.Add(new ErrorCode { errorCode = "E122", errorMessage = "EJ communication error", errorExplanation = "EKÜ İletişim Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E123", errorMessage = "Please print Z report before end EJ module", errorExplanation = "EKÜ Sonlandırmadan önce Lütfen Z raporu alınız." });

                errorList.Add(new ErrorCode { errorCode = "E124", errorMessage = "Please clear EJ module No. record", errorExplanation = "Lütfen EKÜ yü temizleyin." });

                errorList.Add(new ErrorCode { errorCode = "E125", errorMessage = "No capacity, end EJ module", errorExplanation = "Kapasite Bulanamadı.EKÜ yü sonlandırın." });

                errorList.Add(new ErrorCode { errorCode = "E126", errorMessage = "Buffer data error, can be erase only", errorExplanation = "Buffer Veri Hatası.Sadece Okunabilir." });

                errorList.Add(new ErrorCode { errorCode = "E127", errorMessage = "EJournal has not uploaded", errorExplanation = "Ekü yüklenmedi" });

                errorList.Add(new ErrorCode { errorCode = "E128", errorMessage = "only daily can print", errorExplanation = "Sadece 1 günlük yazdırılabilir." });

                errorList.Add(new ErrorCode { errorCode = "E131", errorMessage = "Daily Memory Write Error", errorExplanation = "Günlük Hafıza Yazma Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E132", errorMessage = "Daily Memory Read Error", errorExplanation = "Günlük Hafıza Okuma Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E133", errorMessage = "Device error", errorExplanation = "Cihaz Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E134", errorMessage = "SD card error", errorExplanation = "SD Kart Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E135", errorMessage = "NTP connect error", errorExplanation = "NTP Bağlantı Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E136", errorMessage = "Recovery data is setted", errorExplanation = "Veri Geri Yükleme Ayarlandı." });

                errorList.Add(new ErrorCode { errorCode = "E137", errorMessage = "Only can in service mode", errorExplanation = "Bu fonksiyon Sadece servis modunda çalıştırılabilir." });

                errorList.Add(new ErrorCode { errorCode = "E138", errorMessage = "Daily Memory is full", errorExplanation = "Günlük Hafıza Dolu." });

                errorList.Add(new ErrorCode { errorCode = "E139", errorMessage = "SYSTEM TIME behind the date/time of last fiscal operation", errorExplanation = "Sistem Tarihi son mali işlem tarihinden geride." });

                errorList.Add(new ErrorCode { errorCode = "E151", errorMessage = "FISCAL MEMORY BELONGS TO ANOTHER DEVICE", errorExplanation = "Mali Hafıza Başka bir Cihaza ait." });

                errorList.Add(new ErrorCode { errorCode = "E152", errorMessage = "PSAM CARD BELONGS TO ANOTHER DEVICE", errorExplanation = "PSAM Card Başka bir Cihaza Ait." });

                errorList.Add(new ErrorCode { errorCode = "E153", errorMessage = "PSAM CARD OR FISCAL MEMORY NOT BELONGS THIS DEVICE", errorExplanation = "PSAM Card ya da Mali Hafıza Bu Cihaza Ait Değil." });

                errorList.Add(new ErrorCode { errorCode = "E202", errorMessage = "Fiscal memory integrity control error", errorExplanation = "Mali Hafıza Bütünlük Kontrol Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E203", errorMessage = "Fiscal memory communication error ", errorExplanation = "Mali Hafıza İletişim Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E204", errorMessage = "Fiscal Software Integrity Check is Failed", errorExplanation = "Mali yazılım Bütünlük Kontrolü Yapılamadı." });

                errorList.Add(new ErrorCode { errorCode = "E205", errorMessage = "Fiscal memory is full", errorExplanation = "Mali hafıza doldu." });

                errorList.Add(new ErrorCode { errorCode = "E208", errorMessage = "Software Update is Failed", errorExplanation = "Yazılım Güncellemesi yapılamadı." });

                errorList.Add(new ErrorCode { errorCode = "E209", errorMessage = "Ethernet Communication Module does not work", errorExplanation = "Ethernet İletişim Modulu çalıştırılamadı." });

                errorList.Add(new ErrorCode { errorCode = "E210", errorMessage = "GPRS Module cannot be communicated", errorExplanation = "GPRS Modulune Erişilemedi." });

                errorList.Add(new ErrorCode { errorCode = "E211", errorMessage = "GPRS Module cannot communicate with SIM Card", errorExplanation = "GPRS Modulü SIM Card ile iletişim kuramadı." });

                errorList.Add(new ErrorCode { errorCode = "E212", errorMessage = "GPRS APN Error", errorExplanation = "GPRS APN hatası." });

                errorList.Add(new ErrorCode { errorCode = "E213", errorMessage = "GPRS SIM PIN Error", errorExplanation = "GPRS SIM Pin Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E216", errorMessage = "Event Log File Deleted", errorExplanation = "Olay Kaydı Dosyası Silindi" });

                errorList.Add(new ErrorCode { errorCode = "E217", errorMessage = "Event Log File Changed", errorExplanation = "Olay Kaydı Dosyası Değiştirildi." });

                errorList.Add(new ErrorCode { errorCode = "E218", errorMessage = "Fiscal Software Parameters are Changed in an Uncontrolled Manner", errorExplanation = "Mali Yazılım Parametreleri Kontrolsüz değiştirildi." });

                errorList.Add(new ErrorCode { errorCode = "E219", errorMessage = "Sales Database are Deleted", errorExplanation = "Satış Veritabanı Silindi." });

                errorList.Add(new ErrorCode { errorCode = "E220", errorMessage = "FM Disconnected", errorExplanation = "FM Bağlantısı Kesildi." });

                errorList.Add(new ErrorCode { errorCode = "E221", errorMessage = "Not to Get Parameter", errorExplanation = "Parametre Alınamadı." });

                errorList.Add(new ErrorCode { errorCode = "E301", errorMessage = "Fiscal memory is changed", errorExplanation = "Mali Hafıza Değiştirildi." });

                errorList.Add(new ErrorCode { errorCode = "E302", errorMessage = "Fiscal memory reading error", errorExplanation = "Mali Hafıza okuma Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E303", errorMessage = "Fiscal memory writing error", errorExplanation = "Mali Hafıza Yazma Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E304", errorMessage = "Fiscal memory belongs to other register", errorExplanation = "Başka Kasaya Ait Mali Hafıza" });

                errorList.Add(new ErrorCode { errorCode = "E306", errorMessage = "Device Cabinet is Opened in Uncontrolled Manner", errorExplanation = "Cihaz kapağı açık." });

                errorList.Add(new ErrorCode { errorCode = "E307", errorMessage = "Daily Memory is deleted", errorExplanation = "Günlük Hafıza Silindi." });

                errorList.Add(new ErrorCode { errorCode = "E308", errorMessage = "Daily memory integrity control could not be made", errorExplanation = "Günlük Hafıza Kontrolü Yapılamadı." });

                errorList.Add(new ErrorCode { errorCode = "E309", errorMessage = "Fiscal Applications do not Work", errorExplanation = "Mali Uygulamalar çalışmıyor." });

                errorList.Add(new ErrorCode { errorCode = "E310", errorMessage = "Fiscal Software is Deleted", errorExplanation = "Mali Yazılım Silindi." });

                errorList.Add(new ErrorCode { errorCode = "E314", errorMessage = "Device Operating System Self-Check Error", errorExplanation = "Cihaz İşletim Sistemi Self-Check Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E315", errorMessage = "Device Secure Processor Communication Error", errorExplanation = "Cihaz Güvenlik İşlemcisi İletişim Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E316", errorMessage = "GPRS SIM PIN is blocked", errorExplanation = "GPRS PIN SIM kitlendi." });

                errorList.Add(new ErrorCode { errorCode = "E317", errorMessage = "SAM Slot cannot be Read", errorExplanation = "SAM SLot okunamadı." });

                errorList.Add(new ErrorCode { errorCode = "E318", errorMessage = "Fiscal Application Integrity Control is not Provided", errorExplanation = "Mali Yazılım Bütünlük kontrolu sağlanamadı." });

                errorList.Add(new ErrorCode { errorCode = "E319", errorMessage = "Certificate Validation Error", errorExplanation = "Sertifika Doğrulama Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E320", errorMessage = "Need into Smode First", errorExplanation = "Servis Moduna girilmesi gerekli." });

                errorList.Add(new ErrorCode { errorCode = "E321", errorMessage = "Need to Exit Service Mode First", errorExplanation = "Servis Modundan çıkılması gerekli." });

                errorList.Add(new ErrorCode { errorCode = "E400", errorMessage = "Database insert error", errorExplanation = "Veritabanı veri ekleme Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E401", errorMessage = "Database update error", errorExplanation = "Veritabanı veri güncelleme Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E402", errorMessage = "Database delete error", errorExplanation = "Veritabanı veri silme Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E403", errorMessage = "Database read error", errorExplanation = "Veritabanı veri okuma Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E404", errorMessage = "Device loop check error", errorExplanation = "Cihaz loop kontrol hatası" });

                errorList.Add(new ErrorCode { errorCode = "E501", errorMessage = "ERR_KEYFUN", errorExplanation = "ERR_KEYFUN." });

                errorList.Add(new ErrorCode { errorCode = "E601", errorMessage = "S MODE VERIFY CODE ERROR", errorExplanation = "Servis Şifresi Doğrulama Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E602", errorMessage = "S MODE CREATE ID CODE FAILED", errorExplanation = "Servis Modu ID oluşturma Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E603", errorMessage = "NOT PRINT EVENTLOG", errorExplanation = "Olay Kaydı Yazdırılamadı." });

                errorList.Add(new ErrorCode { errorCode = "E604", errorMessage = "SIC Communication failed", errorExplanation = "SIC iletişim Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E605", errorMessage = "SIC KEY LOST", errorExplanation = "SIC anahtarı kaybedildi." });

                errorList.Add(new ErrorCode { errorCode = "E606", errorMessage = "Cap Open Lost", errorExplanation = "Cihaz Kapağı Açık Unutuldu." });

                errorList.Add(new ErrorCode { errorCode = "E607", errorMessage = "SD Card Change", errorExplanation = "SD Kart Değişim Gerekli." });

                errorList.Add(new ErrorCode { errorCode = "E608", errorMessage = "Security IC Cap is open", errorExplanation = "SIC kapağı açık." });

                errorList.Add(new ErrorCode { errorCode = "E609", errorMessage = "Now in Smode", errorExplanation = "Cihaz Servis Modunda." });

                errorList.Add(new ErrorCode { errorCode = "E610", errorMessage = "NTP TIME OUT OF RANGE", errorExplanation = "NTP time out of range." });

                errorList.Add(new ErrorCode { errorCode = "E611", errorMessage = "NTP SYNC FAILED", errorExplanation = "NTP Senkronizasyon Hatası." });

                errorList.Add(new ErrorCode { errorCode = "E612", errorMessage = "Event Log Full", errorExplanation = "Olay Kaydı Doldu." });

                errorList.Add(new ErrorCode { errorCode = "E613", errorMessage = "In Block Mode", errorExplanation = "Bloke Edildi." });

                errorList.Add(new ErrorCode { errorCode = "E614", errorMessage = "NTP CONNECT FAILED OVER 45 DAYS", errorExplanation = "NTP BAĞLANMA SÜRESİ 45 GUN" });

                errorList.Add(new ErrorCode { errorCode = "E615", errorMessage = "Not Connect TSM", errorExplanation = "TSM Bağlantı Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E700", errorMessage = "ERR_EFTPOS_RETRY_AGAIN", errorExplanation = "EFT POS u yeniden başlatın lütfen." });

                errorList.Add(new ErrorCode { errorCode = "E701", errorMessage = "ERR_EFTPOS_COMMUNICATION", errorExplanation = "EFTPOS iletişim hatası." });

                errorList.Add(new ErrorCode { errorCode = "E702", errorMessage = "Connection failed", errorExplanation = "Bağlantı yapılamadı." });

                errorList.Add(new ErrorCode { errorCode = "E703", errorMessage = "End of day must be executed", errorExplanation = "Lütfen Gün Sonu alın." });

                errorList.Add(new ErrorCode { errorCode = "E704", errorMessage = "End Of Day Error", errorExplanation = "Gün sonu Alınamadı." });

                errorList.Add(new ErrorCode { errorCode = "E705", errorMessage = "Terminal keys must be installed", errorExplanation = "Terminal Anahtarları Yüklenmeli." });

                errorList.Add(new ErrorCode { errorCode = "E706", errorMessage = "Terminal must be installed", errorExplanation = "Terminal Yüklenmeli." });

                errorList.Add(new ErrorCode { errorCode = "E707", errorMessage = "Terminal key exchange must be executed", errorExplanation = "Terminal Anahtarları Değiştirilmeli." });

                errorList.Add(new ErrorCode { errorCode = "E708", errorMessage = "Terminal parameter/remote installation not installed", errorExplanation = "Terminal parametre/uzaktan yükleme yapılmadı." });

                errorList.Add(new ErrorCode { errorCode = "E709", errorMessage = "Transaction not available in batch", errorExplanation = "İşlem Bacth de bulunamadı." });

                errorList.Add(new ErrorCode { errorCode = "E710", errorMessage = "Transaction could not save in batch", errorExplanation = "İşlem Bacth e kaydedilemedi." });

                errorList.Add(new ErrorCode { errorCode = "E711", errorMessage = "Transaction exists in batch but copy slip can not be obtained", errorExplanation = "İşlem bulundu ama eknüsha çıkartılamadı." });

                errorList.Add(new ErrorCode { errorCode = "E712", errorMessage = "Interrupted by user", errorExplanation = "Kullanıcı Tarafından iptal edildi." });

                errorList.Add(new ErrorCode { errorCode = "E713", errorMessage = "Acauier ID is not existed", errorExplanation = "Aquier ID geçerli değil." });

                errorList.Add(new ErrorCode { errorCode = "E714", errorMessage = "End of transaction By EftPos", errorExplanation = "İşlem EFTPOS tarafından iptal edildi." });

                errorList.Add(new ErrorCode { errorCode = "E715", errorMessage = "Get response failure", errorExplanation = "Cevap Alınamadı." });

                errorList.Add(new ErrorCode { errorCode = "E716", errorMessage = "Batch is empty", errorExplanation = "Batch Boş." });

                errorList.Add(new ErrorCode { errorCode = "E717", errorMessage = "User Timeout", errorExplanation = "User zaman aşımına uğradı." });

                errorList.Add(new ErrorCode { errorCode = "E718", errorMessage = "Wrong message in Value", errorExplanation = "Mesaj da yanlış değer mevcut." });

                errorList.Add(new ErrorCode { errorCode = "E719", errorMessage = "Wrong Message", errorExplanation = "Yanlış Mesaj." });

                errorList.Add(new ErrorCode { errorCode = "E801", errorMessage = "The record not in extbParam", errorExplanation = "Kayıt extbParam içerisinde bulunamadı" });

                errorList.Add(new ErrorCode { errorCode = "E802", errorMessage = "The device not registered yet", errorExplanation = "The device not registered yet" });

                errorList.Add(new ErrorCode { errorCode = "E803", errorMessage = "Do not exist the terminal type", errorExplanation = "Do not exist the terminal type" });

                errorList.Add(new ErrorCode { errorCode = "E804", errorMessage = "The device not in the prior upgrade devices", errorExplanation = "The device not in the prior upgrade devices" });

                errorList.Add(new ErrorCode { errorCode = "E805", errorMessage = "Download Software Fail", errorExplanation = "Yazılım İndirme Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E806", errorMessage = "Tx JSON FAIL", errorExplanation = "Tx JSON HATASI" });

                errorList.Add(new ErrorCode { errorCode = "E807", errorMessage = "Rx JSON FAIL", errorExplanation = "Rx JSON HATASI" });

                errorList.Add(new ErrorCode { errorCode = "E808", errorMessage = "CONNECT FAIL", errorExplanation = "808 BAGLANTI HATASI" });

                errorList.Add(new ErrorCode { errorCode = "E809", errorMessage = "Check new software fail", errorExplanation = "Yeni Yazılım Kontrol Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E810", errorMessage = "Install APP fail", errorExplanation = "810 Uygulama Yükleme Hatası" });

                errorList.Add(new ErrorCode { errorCode = "E999", errorMessage = "Refuse this function", errorExplanation = "Belge Kapatılamadı" });

                errorList.Add(new ErrorCode { errorCode = "E1453", errorMessage = "Yazıcıda kağıt yok veya kapak açık", errorExplanation = "Yazıcıda kağıt yok veya kapak açık" });
            }

            if (errorCode.Length < 3)
            {
                if (errorCode.Length < 2)

                    errorCode = "E00" + errorCode;
                else

                    errorCode = "E0" + errorCode;
            }
            else
            {
                errorCode = "E" + errorCode;
            }

            return errorList.FirstOrDefault(x => x.errorCode == errorCode);
        }

        //public static void ShowErrorMsg(ErrorCode errMsg)
        //{
        //    MessageBox.Show(errMsg.errorCode + " :" + errMsg.errorExplanation, "İşlem Tamamlanamadı.", MessageBoxButton.OK, MessageBoxImage.Error);

        //}
    }
}