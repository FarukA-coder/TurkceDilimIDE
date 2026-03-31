# Türkçe Dilim IDE ve Yorumlayıcı Motoru

Türkçe Dilim, programlamaya yeni başlayan Türk öğrencilerin İngilizce bariyerine ve karmaşık noktalama işaretlerine takılmadan doğrudan algoritma mantığını kavrayabilmeleri için tasarlanmış, dinamik tipli ve Türkçe sözdizimine sahip modern bir programlama dilidir. 

Bu proje, C++ ile sıfırdan yazılmış bir derleyici/yorumlayıcı motoru (Backend) ve .NET mimarisi üzerinde geliştirilmiş gelişmiş bir Entegre Geliştirme Ortamı (IDE / Frontend) içermektedir.

## Geliştirme Ortamı (IDE) Özellikleri

Geliştirilen IDE, kullanıcı deneyimini profesyonel bir seviyeye taşımak için modern kod editörlerinden ilham alınarak tasarlanmıştır:

* **Dinamik Gece/Gündüz Modu:** Tema geçişlerinde metin bellekte korunur ve sözdizimi renkleri anında yeniden hesaplanarak ekrana yansıtılır.
* **Gelişmiş Sözdizimi Renklendirme (Syntax Highlighting):** Kullanıcı dile ait anahtar kelimeleri yazdığı an arka planda metin taranır ve algoritmik okunabilirlik için vurgulanır.
* **Bağımsız Özel Geri Alma (Custom Undo) Motoru:** Windows'un varsayılan sisteminden bağımsız çalışan, `Stack<string>` mimarisiyle kodlanmış, renk hatalarını engelleyen özel hafıza yapısı.
* **Arama ve Bulma Motoru:** Dinamik olarak metin içinde kelime indeksleme ve odak kaydırma sistemi (Ctrl + F).
* **Debug Entegrasyonu:** Arka plandaki C++ motorunun `-debug` parametresi ile çalıştırılarak RAM'deki değişkenlerin anlık izlenmesini (Variable Watch) sağlayan altyapı.
* **Ergonomik Çalışma Alanı:** Hiyerarşik dosya ağacı (TreeView) ve dinamik boyutlandırılabilir konsol çıktısı (Splitter/Docking).

## Dil Mimarisi ve Sözdizimi (Syntax)

Türkçe Dilim, C++ ile kodlanmış özgün bir ayrıştırıcı (Lexer/Parser) kullanır. Harici bir kütüphane (Lex/Yacc vb.) barındırmaz.

* **Dinamik Tip (Dynamic Typing):** Değişkenlerin tipleri (Sayı, Metin) önceden belirtilmez, atanan değere göre motor tarafından otomatik tahsis edilir.
* **Parantezsiz Okunabilirlik:** Normal parantez kullanımı sözdiziminden kaldırılarak tamamen düz metin mantığında bir okuma hedeflenmiştir. Kod blokları sadece `{ }` ile sınırlandırılır.

### Anahtar Kelimeler
* **Koşul ve Döngüler:** `EGER`, `DONGU`
* **Girdi / Çıktı:** `YAZ`, `OKU`
* **Mantıksal Operatörler:** `VE`, `VEYA`
* **Alt Programlar:** `FONK`

## Örnek Kod Kullanımları

**Merhaba Dünya:**
```text
YAZ "Merhaba Dunya!";
