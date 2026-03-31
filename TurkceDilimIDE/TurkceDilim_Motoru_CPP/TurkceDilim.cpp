#include <iostream>
#include <string>
#include <vector>
#include <cctype>
#include <unordered_map>
#include <fstream>
#include <sstream>

using namespace std;

// --- 0. DİNAMİK VERİ TİPİ (HEM METİN HEM SAYI TUTABİLEN HAFIZA) ---
enum VeriTuru { SAYI, METIN_TIP };

struct Veri {
    VeriTuru tur;
    double sayi;
    string metin;

    Veri() { tur = SAYI; sayi = 0; metin = ""; }
    Veri(double s) { tur = SAYI; sayi = s; metin = ""; }
    Veri(string m) { tur = METIN_TIP; sayi = 0; metin = m; }
};

// --- 1. TOKEN YAPILARI ---
enum TokenType {
    ANAHTAR_KELIME, KIMLIK, SAYI_DEGERI, METIN, ISARET, OPERATOR, KARSILASTIRMA, BILINMEYEN
};

struct Token {
    TokenType tur;
    string deger;
    int satir;
};

// --- 2. LEXER (Kelime Analizcisi) ---
class Lexer {
private:
    string kaynakKod;
    int pozisyon;
    int satirSayaci;

public:
    Lexer(string kod) {
        kaynakKod = kod;
        pozisyon = 0;
        satirSayaci = 1;
    }

    vector<Token> tokenize() {
        vector<Token> tokenlar;

        while (pozisyon < kaynakKod.length()) {
            char suankiKarakter = kaynakKod[pozisyon];

            // Güvenlik Duvarı
            unsigned char asciiDegeri = (unsigned char)suankiKarakter;
            if (asciiDegeri > 127 && asciiDegeri != 13 && asciiDegeri != 10 && asciiDegeri != 9) {
                string hataMesaji = "DERLEME HATASI! Gecersiz Karakter Tespit Edildi (Satir: " + to_string(satirSayaci) + ")";

                cout << "=====================================" << endl;
                cout << hataMesaji << endl;
                cout << "=====================================" << endl;

                // C# Arayüzü için log dosyasına yaz
                ofstream dosya("cikti.txt", ios::app);
                dosya << hataMesaji << endl;
                dosya.close();

                exit(1);
            }

            // Alt satıra geçişleri say
            if (suankiKarakter == '\n') {
                satirSayaci++;
                pozisyon++;
                continue;
            }

            // Boşlukları atla
            if (isspace(suankiKarakter)) {
                pozisyon++;
                continue;
            }

            // Yorum Satırları (//)
            if (suankiKarakter == '/') {
                if (pozisyon + 1 < kaynakKod.length() && kaynakKod[pozisyon + 1] == '/') {
                    while (pozisyon < kaynakKod.length() && kaynakKod[pozisyon] != '\n') {
                        pozisyon++;
                    }
                    continue;
                }
            }

            // KİMLİKLER VE KOMUTLAR
            if (isalpha(suankiKarakter)) {
                string kelime = "";
                while (pozisyon < kaynakKod.length() && (isalnum(kaynakKod[pozisyon]) || kaynakKod[pozisyon] == '_')) {
                    kelime += kaynakKod[pozisyon];
                    pozisyon++;
                }

                string buyukKelime = "";
                for (char c : kelime) buyukKelime += toupper(c);

                if (buyukKelime == "YAZ" || buyukKelime == "EGER" || buyukKelime == "OKU" || buyukKelime == "DONGU" || buyukKelime == "FONK" || buyukKelime == "VE" || buyukKelime == "VEYA")
                    tokenlar.push_back({ ANAHTAR_KELIME, buyukKelime, satirSayaci });
                else
                    tokenlar.push_back({ KIMLIK, kelime, satirSayaci });

                continue;
            }

            // SAYILAR
            if (isdigit(suankiKarakter)) {
                string sayi = "";
                bool noktaEklendi = false;

                while (pozisyon < kaynakKod.length() && (isdigit(kaynakKod[pozisyon]) || kaynakKod[pozisyon] == '.')) {
                    if (kaynakKod[pozisyon] == '.') {
                        if (noktaEklendi) break;
                        noktaEklendi = true;
                    }
                    sayi += kaynakKod[pozisyon];
                    pozisyon++;
                }
                tokenlar.push_back({ SAYI_DEGERI, sayi, satirSayaci });
                continue;
            }

            // METİNLER (STRING)
            if (suankiKarakter == '"') {
                string metin = "";
                pozisyon++;

                while (pozisyon < kaynakKod.length() && kaynakKod[pozisyon] != '"') {
                    if (kaynakKod[pozisyon] == '\n') satirSayaci++;
                    metin += kaynakKod[pozisyon];
                    pozisyon++;
                }
                pozisyon++;
                tokenlar.push_back({ METIN, metin, satirSayaci });
                continue;
            }

            // KARŞILAŞTIRMA OPERATÖRLERİ
            if (suankiKarakter == '!') {
                if (pozisyon + 1 < kaynakKod.length() && kaynakKod[pozisyon + 1] == '=') {
                    tokenlar.push_back({ KARSILASTIRMA, "!=", satirSayaci });
                    pozisyon += 2;
                    continue;
                }
            }

            if (suankiKarakter == '=') {
                if (pozisyon + 1 < kaynakKod.length() && kaynakKod[pozisyon + 1] == '=') {
                    tokenlar.push_back({ KARSILASTIRMA, "==", satirSayaci });
                    pozisyon += 2;
                    continue;
                }
                else {
                    tokenlar.push_back({ ISARET, "=", satirSayaci });
                    pozisyon++;
                    continue;
                }
            }

            if (suankiKarakter == '>' || suankiKarakter == '<') {
                if (pozisyon + 1 < kaynakKod.length() && kaynakKod[pozisyon + 1] == '=') {
                    string islem = ""; islem += suankiKarakter; islem += "=";
                    tokenlar.push_back({ KARSILASTIRMA, islem, satirSayaci });
                    pozisyon += 2;
                    continue;
                }
                else {
                    string islem = ""; islem += suankiKarakter;
                    tokenlar.push_back({ KARSILASTIRMA, islem, satirSayaci });
                    pozisyon++;
                    continue;
                }
            }

            // İŞARETLER (Diziler ve Parantezler)
            if (suankiKarakter == '{' || suankiKarakter == '}' || suankiKarakter == '[' || suankiKarakter == ']') {
                string isaret = ""; isaret += suankiKarakter;
                tokenlar.push_back({ ISARET, isaret, satirSayaci });
                pozisyon++;
                continue;
            }

            // MATEMATİK OPERATÖRLERİ
            if (suankiKarakter == '+' || suankiKarakter == '-' || suankiKarakter == '*' || suankiKarakter == '/') {
                string islem = ""; islem += suankiKarakter;
                tokenlar.push_back({ OPERATOR, islem, satirSayaci });
                pozisyon++;
                continue;
            }

            // NOKTALI VİRGÜL
            if (suankiKarakter == ';') {
                tokenlar.push_back({ ISARET, ";", satirSayaci });
                pozisyon++;
                continue;
            }

            pozisyon++;
        }
        return tokenlar;
    }
};

// --- 3. PARSER (Ayrıştırıcı ve Çalıştırıcı) ---
class Parser {
private:
    vector<Token> tokenlar;
    int pozisyon;
    unordered_map<string, Veri> hafiza;
    unordered_map<string, unordered_map<int, Veri>> diziHafizasi;
    unordered_map<string, int> fonksiyonlar;
    vector<pair<string, int>> blokYigini;

    bool debugModu; // 🚀 YENİ EKLENDİ: Debug açık mı kapalı mı?

    Veri degerOku() {
        Veri sonuc;
        if (pozisyon >= tokenlar.size()) return sonuc;

        if (tokenlar[pozisyon].tur == SAYI_DEGERI) {
            sonuc = Veri(stod(tokenlar[pozisyon].deger));
            pozisyon++;
        }
        else if (tokenlar[pozisyon].tur == METIN) {
            sonuc = Veri(tokenlar[pozisyon].deger);
            pozisyon++;
        }
        else if (tokenlar[pozisyon].tur == KIMLIK) {
            string ad = tokenlar[pozisyon].deger;
            pozisyon++;

            // Dizi (Array) okuma
            if (pozisyon < tokenlar.size() && tokenlar[pozisyon].deger == "[") {
                pozisyon++;
                int index = 0;

                if (tokenlar[pozisyon].tur == SAYI_DEGERI)
                    index = stoi(tokenlar[pozisyon].deger);
                else if (tokenlar[pozisyon].tur == KIMLIK)
                    index = (int)hafiza[tokenlar[pozisyon].deger].sayi;

                pozisyon += 2;
                sonuc = diziHafizasi[ad][index];
            }
            else {
                sonuc = hafiza[ad];
            }
        }
        return sonuc;
    }

    bool kosulOku() {
        Veri solDeger = degerOku();
        string op = tokenlar[pozisyon].deger;
        pozisyon++;
        Veri sagDeger = degerOku();

        bool sonuc = false;

        if (solDeger.tur == SAYI && sagDeger.tur == SAYI) {
            if (op == ">") sonuc = (solDeger.sayi > sagDeger.sayi);
            else if (op == "<") sonuc = (solDeger.sayi < sagDeger.sayi);
            else if (op == "==") sonuc = (solDeger.sayi == sagDeger.sayi);
            else if (op == "!=") sonuc = (solDeger.sayi != sagDeger.sayi);
            else if (op == ">=") sonuc = (solDeger.sayi >= sagDeger.sayi);
            else if (op == "<=") sonuc = (solDeger.sayi <= sagDeger.sayi);
        }
        else {
            string s1 = (solDeger.tur == METIN_TIP) ? solDeger.metin : to_string((int)solDeger.sayi);
            string s2 = (sagDeger.tur == METIN_TIP) ? sagDeger.metin : to_string((int)sagDeger.sayi);
            if (op == "==") sonuc = (s1 == s2);
            else if (op == "!=") sonuc = (s1 != s2);
        }

        // Zincirleme (VE / VEYA) kontrolleri
        while (pozisyon < tokenlar.size() && tokenlar[pozisyon].tur == ANAHTAR_KELIME &&
            (tokenlar[pozisyon].deger == "VE" || tokenlar[pozisyon].deger == "VEYA")) {

            string mantikOp = tokenlar[pozisyon].deger;
            pozisyon++;

            Veri sol2 = degerOku();
            string op2 = tokenlar[pozisyon].deger;
            pozisyon++;
            Veri sag2 = degerOku();

            bool sonuc2 = false;

            if (sol2.tur == SAYI && sag2.tur == SAYI) {
                if (op2 == ">") sonuc2 = (sol2.sayi > sag2.sayi);
                else if (op2 == "<") sonuc2 = (sol2.sayi < sag2.sayi);
                else if (op2 == "==") sonuc2 = (sol2.sayi == sag2.sayi);
                else if (op2 == "!=") sonuc2 = (sol2.sayi != sag2.sayi);
                else if (op2 == ">=") sonuc2 = (sol2.sayi >= sag2.sayi);
                else if (op2 == "<=") sonuc2 = (sol2.sayi <= sag2.sayi);
            }
            else {
                string s1 = (sol2.tur == METIN_TIP) ? sol2.metin : to_string((int)sol2.sayi);
                string s2 = (sag2.tur == METIN_TIP) ? sag2.metin : to_string((int)sag2.sayi);
                if (op2 == "==") sonuc2 = (s1 == s2);
                else if (op2 == "!=") sonuc2 = (s1 != s2);
            }

            if (mantikOp == "VE") sonuc = (sonuc && sonuc2);
            else if (mantikOp == "VEYA") sonuc = (sonuc || sonuc2);
        }
        return sonuc;
    }

public:
    // 🚀 GÜNCELLENDİ: Kurucu metoda debug parametresi eklendi
    Parser(vector<Token> t, bool debug = false) {
        tokenlar = t;
        pozisyon = 0;
        debugModu = debug;
    }

    void calistir() {
        while (pozisyon < tokenlar.size()) {
            Token suankiToken = tokenlar[pozisyon];

            // 1. DEĞİŞKEN VEYA DİZİ ATAMASI
            if (suankiToken.tur == KIMLIK) {
                string solAd = suankiToken.deger;
                pozisyon++;
                bool diziAtamasi = false;
                int diziIndex = 0;

                if (pozisyon < tokenlar.size() && tokenlar[pozisyon].deger == "[") {
                    diziAtamasi = true;
                    pozisyon++;

                    if (tokenlar[pozisyon].tur == SAYI_DEGERI)
                        diziIndex = stoi(tokenlar[pozisyon].deger);
                    else if (tokenlar[pozisyon].tur == KIMLIK)
                        diziIndex = (int)hafiza[tokenlar[pozisyon].deger].sayi;

                    pozisyon += 2;
                }

                if (pozisyon < tokenlar.size() && tokenlar[pozisyon].deger == "=") {
                    pozisyon++;
                    Veri islemSonucu = degerOku();

                    // Zincirleme Matematik veya Metin Birleştirme (Multi-Concat)
                    while (pozisyon < tokenlar.size() && tokenlar[pozisyon].tur == OPERATOR) {
                        string op = tokenlar[pozisyon].deger;
                        pozisyon++;
                        Veri ikinciDeger = degerOku();

                        if (op == "+") {
                            if (islemSonucu.tur == SAYI && ikinciDeger.tur == SAYI) {
                                islemSonucu.sayi += ikinciDeger.sayi;
                            }
                            else {
                                string s1 = (islemSonucu.tur == METIN_TIP) ? islemSonucu.metin : to_string((int)islemSonucu.sayi);
                                string s2 = (ikinciDeger.tur == METIN_TIP) ? ikinciDeger.metin : to_string((int)ikinciDeger.sayi);
                                islemSonucu.tur = METIN_TIP;
                                islemSonucu.metin = s1 + s2;
                            }
                        }
                        else if (op == "-") islemSonucu.sayi -= ikinciDeger.sayi;
                        else if (op == "*") islemSonucu.sayi *= ikinciDeger.sayi;
                        else if (op == "/") islemSonucu.sayi /= ikinciDeger.sayi;
                    }

                    if (diziAtamasi) diziHafizasi[solAd][diziIndex] = islemSonucu;
                    else hafiza[solAd] = islemSonucu;

                    // 🚀 YENİ EKLENDİ: Atama yapıldığında logla
                    if (debugModu) {
                        ofstream dbgDosya("debug_log.txt", ios::app);
                        dbgDosya << "DEGISKEN|" << solAd;
                        if (diziAtamasi) dbgDosya << "[" << diziIndex << "]";
                        dbgDosya << "|";

                        if (islemSonucu.tur == SAYI) dbgDosya << islemSonucu.sayi;
                        else dbgDosya << islemSonucu.metin;

                        dbgDosya << "|" << suankiToken.satir << endl;
                        dbgDosya.close();
                    }
                }
                else if (!diziAtamasi && fonksiyonlar.count(solAd) > 0) {
                    blokYigini.push_back({ "FONK", pozisyon - 1 });
                    pozisyon = fonksiyonlar[solAd];
                    continue;
                }
                else {
                    string hataMesaji = "SINTAKS HATASI! Hata Yeri: " + to_string(suankiToken.satir) + ". Satir\nHata Sebebi: '" + suankiToken.deger + "' ifadesinden sonra beklenen bir islem bulunamadi.";

                    cout << "=====================================" << endl;
                    cout << "SINTAKS HATASI (Derleme Durduruldu)!" << endl;
                    cout << "Hata Yeri: " << suankiToken.satir << ". Satir" << endl;
                    cout << "Hata Sebebi: '" << suankiToken.deger << "' ifadesinden sonra beklenen bir islem bulunamadi." << endl;
                    cout << "=====================================" << endl;

                    ofstream dosya("cikti.txt", ios::app);
                    dosya << hataMesaji << endl;
                    dosya.close();

                    exit(1);
                }
            }
            // 2. FONKSİYON TANIMLAMA
            else if (suankiToken.tur == ANAHTAR_KELIME && suankiToken.deger == "FONK") {
                pozisyon++;
                string fonkAdi = tokenlar[pozisyon].deger;
                pozisyon++;
                fonksiyonlar[fonkAdi] = pozisyon + 1;

                int parantezSayaci = 1;
                pozisyon++;
                while (pozisyon < tokenlar.size() && parantezSayaci > 0) {
                    if (tokenlar[pozisyon].deger == "{") parantezSayaci++;
                    else if (tokenlar[pozisyon].deger == "}") parantezSayaci--;
                    pozisyon++;
                }
                pozisyon--;
            }
            // 3. EKRANA YAZDIRMA
            else if (suankiToken.tur == ANAHTAR_KELIME && suankiToken.deger == "YAZ") {
                pozisyon++;
                Veri islemSonucu = degerOku();

                while (pozisyon < tokenlar.size() && tokenlar[pozisyon].tur == OPERATOR) {
                    string op = tokenlar[pozisyon].deger;
                    pozisyon++;
                    Veri ikinciDeger = degerOku();

                    if (op == "+") {
                        if (islemSonucu.tur == SAYI && ikinciDeger.tur == SAYI) {
                            islemSonucu.sayi += ikinciDeger.sayi;
                        }
                        else {
                            string s1 = (islemSonucu.tur == METIN_TIP) ? islemSonucu.metin : to_string((int)islemSonucu.sayi);
                            string s2 = (ikinciDeger.tur == METIN_TIP) ? ikinciDeger.metin : to_string((int)ikinciDeger.sayi);
                            islemSonucu.tur = METIN_TIP;
                            islemSonucu.metin = s1 + s2;
                        }
                    }
                    else if (op == "-") islemSonucu.sayi -= ikinciDeger.sayi;
                    else if (op == "*") islemSonucu.sayi *= ikinciDeger.sayi;
                    else if (op == "/") islemSonucu.sayi /= ikinciDeger.sayi;
                }

                ofstream dosya("cikti.txt", ios::app);
                if (islemSonucu.tur == SAYI) {
                    cout << islemSonucu.sayi << endl;
                    dosya << islemSonucu.sayi << endl;
                }
                else {
                    cout << islemSonucu.metin << endl;
                    dosya << islemSonucu.metin << endl;
                }
                dosya.close();
            }

            // 4. KULLANICIDAN OKUMA
            else if (suankiToken.tur == ANAHTAR_KELIME && suankiToken.deger == "OKU") {
                pozisyon++;
                if (pozisyon < tokenlar.size() && tokenlar[pozisyon].tur == KIMLIK) {
                    string degiskenAdi = tokenlar[pozisyon].deger;
                    pozisyon++;

                    bool diziAtamasi = false;
                    int diziIndex = 0;

                    if (pozisyon < tokenlar.size() && tokenlar[pozisyon].deger == "[") {
                        diziAtamasi = true;
                        pozisyon++;

                        if (tokenlar[pozisyon].tur == SAYI_DEGERI)
                            diziIndex = stoi(tokenlar[pozisyon].deger);
                        else if (tokenlar[pozisyon].tur == KIMLIK)
                            diziIndex = (int)hafiza[tokenlar[pozisyon].deger].sayi;

                        pozisyon += 2;
                    }

                    string girilenVeri;
                    cin >> girilenVeri;
                    Veri v;
                    bool hepsiRakam = true;
                    for (char c : girilenVeri) if (!isdigit(c)) hepsiRakam = false;

                    if (hepsiRakam) v = Veri(stod(girilenVeri));
                    else v = Veri(girilenVeri);

                    if (diziAtamasi) diziHafizasi[degiskenAdi][diziIndex] = v;
                    else hafiza[degiskenAdi] = v;

                    // 🚀 YENİ EKLENDİ: Kullanıcıdan okunduğunda da logla
                    if (debugModu) {
                        ofstream dbgDosya("debug_log.txt", ios::app);
                        dbgDosya << "DEGISKEN|" << degiskenAdi;
                        if (diziAtamasi) dbgDosya << "[" << diziIndex << "]";
                        dbgDosya << "|";

                        if (v.tur == SAYI) dbgDosya << v.sayi;
                        else dbgDosya << v.metin;

                        dbgDosya << "|" << suankiToken.satir << endl;
                        dbgDosya.close();
                    }
                }
            }
            // 5. EĞER (IF) YAPISI
            else if (suankiToken.tur == ANAHTAR_KELIME && suankiToken.deger == "EGER") {
                pozisyon++;
                bool sartSaglandi = kosulOku();

                if (sartSaglandi) {
                    blokYigini.push_back({ "EGER", -1 });
                }
                else {
                    int parantezSayaci = 1;
                    pozisyon++;
                    while (pozisyon < tokenlar.size() && parantezSayaci > 0) {
                        if (tokenlar[pozisyon].deger == "{") parantezSayaci++;
                        else if (tokenlar[pozisyon].deger == "}") parantezSayaci--;
                        pozisyon++;
                    }
                    pozisyon--;
                }
            }
            // 6. DÖNGÜ YAPISI
            else if (suankiToken.tur == ANAHTAR_KELIME && suankiToken.deger == "DONGU") {
                int donguBaslangici = pozisyon;
                pozisyon++;
                bool sartSaglandi = kosulOku();

                if (sartSaglandi) {
                    blokYigini.push_back({ "DONGU", donguBaslangici });
                }
                else {
                    int parantezSayaci = 1;
                    pozisyon++;
                    while (pozisyon < tokenlar.size() && parantezSayaci > 0) {
                        if (tokenlar[pozisyon].deger == "{") parantezSayaci++;
                        else if (tokenlar[pozisyon].deger == "}") parantezSayaci--;
                        pozisyon++;
                    }
                    pozisyon--;
                }
            }
            // 7. SÜSLÜ PARANTEZ KAPATMASI
            else if (suankiToken.tur == ISARET && suankiToken.deger == "}") {
                if (!blokYigini.empty()) {
                    pair<string, int> sonBlok = blokYigini.back();
                    if (sonBlok.first == "DONGU") {
                        blokYigini.pop_back();
                        pozisyon = sonBlok.second;
                        continue;
                    }
                    else if (sonBlok.first == "FONK") {
                        blokYigini.pop_back();
                        pozisyon = sonBlok.second;
                    }
                }
            }
            // 8. NOKTALI VİRGÜL
            else if (suankiToken.tur == ISARET && suankiToken.deger == ";") {
                // Sadece atla, sorun yok.
            }
            // 9. BİLİNMEYEN YAPILAR
            else {
                string hataMesaji = "SINTAKS HATASI! Hata Yeri: " + to_string(suankiToken.satir) + ". Satir\nHatali Ifade: '" + suankiToken.deger + "'";

                cout << "=====================================" << endl;
                cout << "SINTAKS HATASI (Derleme Durduruldu)!" << endl;
                cout << "Hata Yeri: " << suankiToken.satir << ". Satir" << endl;
                cout << "Hatali İfade: '" << suankiToken.deger << "'" << endl;
                cout << "=====================================" << endl;

                ofstream dosya("cikti.txt", ios::app);
                dosya << hataMesaji << endl;
                dosya.close();

                exit(1);
            }

            pozisyon++;
        }
    }
};

// --- 4. ANA PROGRAM ---
int main(int argc, char* argv[]) {
    string dosyaYolu = "kod.txt";
    bool debugAktif = false; // 🚀 YENİ EKLENDİ

    if (argc > 1) dosyaYolu = argv[1];

    // 🚀 YENİ EKLENDİ: Debug argümanı kontrolü
    if (argc > 2 && string(argv[2]) == "-debug") {
        debugAktif = true;
        ofstream logTemizle("debug_log.txt", ios::trunc); // Eski logları temizle
        logTemizle.close();
    }

    // Program her çalıştığında eski logları sil ki üst üste binmesin!
    ofstream logTemizle("cikti.txt", ios::trunc);
    logTemizle.close();

    ifstream dosya(dosyaYolu);
    if (!dosya.is_open()) {
        cout << "HATA: Dosya bulunamadi!" << endl;
        return 1;
    }

    stringstream buffer;
    buffer << dosya.rdbuf();
    string kod = buffer.str();
    dosya.close();

    if (kod.empty()) return 1;

    Lexer lexer(kod);
    vector<Token> tokenlar = lexer.tokenize();

    // 🚀 GÜNCELLENDİ: Parser'ı debugAktif değişkeniyle başlat
    Parser parser(tokenlar, debugAktif);
    parser.calistir();

    return 0;
}

