using ReaLTaiizor.Colors;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ReaLTaiizor.Controls.HopeTabPage;

namespace TurkceDilimIDE
{
    public partial class Form1 : Form
    {
        Stack<string> geriAlHafizasi = new Stack<string>();
        bool motorCalisiyor = false;
        bool karanlikTema = false;
        Panel pnlBul;
        TextBox txtAranan;
        int sonBulunanIndex = 0;
        ListBox lstOto; // Öneri listemiz
        string[] komutlar = { "YAZ", "EGER", "DONGU", "FONK", "OKU", "VE", "VEYA" };
        Panel pnlAktifSatir;
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);
        public Form1()
        {
            InitializeComponent();


        }

        private async void btnCalistir_Click(object sender, EventArgs e)
        {

            try
            {
                // 1. Yazýlan kodu "gecici_kod.faruk" adýyla arka planda kaydet
                string dosyaYolu = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "gecici_kod.faruk");
                System.IO.File.WriteAllText(dosyaYolu, txtKod.Text);

                txtKonsol.Text = "Terminal baslatiliyor...\n";

                // 2. Týpký VS Code gibi, CMD'yi harici olarak çaðýr ve motorumuza dosyayý ver
                System.Diagnostics.Process islem = new System.Diagnostics.Process();
                islem.StartInfo.FileName = "cmd.exe";

                // Burada motorumuz "TurkceDilim.exe" yi çaðýrýp, yazdýðýmýz kodu içine atýyoruz
                islem.StartInfo.Arguments = "/c TurkceDilim.exe " + dosyaYolu + " & pause";

                islem.StartInfo.UseShellExecute = true;
                islem.StartInfo.CreateNoWindow = false;

                islem.Start();

                // Arka planda motorun iþini bitirmesini bekle (Arayüz donmaz!)
                await Task.Run(() => islem.WaitForExit());

                txtKonsol.Text += "\nIslem tamamlandi. Terminal kapandi.";
            }
            catch (Exception ex)
            {
                txtKonsol.Text = "DERLEYICI HATASI!\nMotor (TurkceDilim.exe) bulunamadi veya calistirilamadi.\n" + ex.Message;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SaveFileDialog kaydetPenceresi = new SaveFileDialog();
            kaydetPenceresi.Title = "Kodlarý Kaydet";
            // Ýþte kendi uzantýmýz!
            kaydetPenceresi.Filter = "Faruk Kod Dosyasý (*.faruk)|*.faruk|Tüm Dosyalar (*.*)|*.*";

            if (kaydetPenceresi.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(kaydetPenceresi.FileName, txtKod.Text);
                MessageBox.Show("Kodlar baþariyla kaydedildi!", "Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAc_Click(object sender, EventArgs e)
        {
            OpenFileDialog acPenceresi = new OpenFileDialog();
            acPenceresi.Title = "Kod Dosyasý Seç";
            acPenceresi.Filter = "Faruk Kod Dosyasý (*.faruk)|*.faruk|Metin Dosyalarý (*.txt)|*.txt";

            if (acPenceresi.ShowDialog() == DialogResult.OK)
            {
                txtKod.Text = System.IO.File.ReadAllText(acPenceresi.FileName);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtKonsol.Clear();
        }

        private void txtKod_TextChanged(object sender, EventArgs e)
        {
            if (!motorCalisiyor)
            {
                // Hafýza boþsa veya son kaydedilen þey þu anki yazýdan farklýysa arþive ekle
                if (geriAlHafizasi.Count == 0 || geriAlHafizasi.Peek() != txtKod.Text)
                {
                    geriAlHafizasi.Push(txtKod.Text); // Yazýnýn son halini hafýzaya fýrlat
                }
            }
            int imlecYeri = txtKod.SelectionStart;

            int satirSayisi = txtKod.Lines.Length;
            if (txtSatir.Lines.Length != satirSayisi) // Eðer yeni satýra geçildiyse numaralarý güncelle
            {
                string numaralar = "";
                for (int i = 1; i <= satirSayisi; i++)
                {
                    numaralar += i.ToString() + "\n";
                }
                txtSatir.Text = numaralar;
            }

            // 1. Yazýlan her þeyi standart siyah renge ve normal fonta çevir
            txtKod.SelectAll();
            txtKod.SelectionColor = karanlikTema ? Color.White : Color.Black;
            txtKod.SelectionFont = new Font(txtKod.Font, FontStyle.Regular);

            // 2. RENK SÖZLÜÐÜ (Hangi kelime hangi renk olacak buradan ayarlayabilirsin)
            Dictionary<string, Color> renkler = new Dictionary<string, Color>()
    {
        { "VE", Color.Crimson },     // Kýrmýzý
        { "VEYA", Color.Crimson },    // Kýrmýzý
        { "YAZ", Color.DarkOrange },     // YAZ komutu Turuncu
        { "EGER", Color.DodgerBlue },    // EGER komutu Mavi
        { "DONGU", Color.Purple },       // DONGU komutu Mor
        { "FONK", Color.DeepPink },      // FONK komutu Pembe
        { "OKU", Color.Teal }            // OKU komutu Turkuaz
    };

            // 3. Sözlükteki kelimeleri kodun içinde ara ve boya
            foreach (var eleman in renkler)
            {
                string kelime = eleman.Key;
                Color renk = eleman.Value;

                int aramaBaslangici = 0;
                while (aramaBaslangici < txtKod.TextLength)
                {
                    int kelimeninYeri = txtKod.Find(kelime, aramaBaslangici, RichTextBoxFinds.WholeWord);
                    if (kelimeninYeri != -1) // Kelime bulunduysa
                    {
                        txtKod.SelectionStart = kelimeninYeri;
                        txtKod.SelectionLength = kelime.Length;
                        txtKod.SelectionColor = renk; // Rengi sözlükten çekip uygula
                        txtKod.SelectionFont = new Font(txtKod.Font, FontStyle.Bold); // Kalýn yap
                        aramaBaslangici = kelimeninYeri + kelime.Length;
                    }
                    else
                    {
                        break; // Bu kelime bitince diðer renge geç
                    }
                }
            }

            // 4. Ýmleci yazmaya devam edebilmek için eski yerine geri koy
            txtKod.SelectionStart = imlecYeri;
            txtKod.SelectionLength = 0;
            txtKod.SelectionColor = Color.Black;
            txtKod.SelectionFont = new Font(txtKod.Font, FontStyle.Regular);
            string[] parantezler = { "[", "]" };
            foreach (string p in parantezler)
            {
                int aramaBaslangici = 0;
                while (aramaBaslangici < txtKod.TextLength)
                {
                    // RichTextBoxFinds.None kullanýyoruz ki kelimenin içinde bile olsa bulsun
                    int yer = txtKod.Find(p, aramaBaslangici, RichTextBoxFinds.None);
                    if (yer != -1)
                    {
                        txtKod.SelectionStart = yer;
                        txtKod.SelectionLength = 1;
                        txtKod.SelectionColor = Color.Magenta; // Rengi Eflatun (Magenta) yaptýk, çok dikkat çeker
                        txtKod.SelectionFont = new Font(txtKod.Font, FontStyle.Bold); // Kalýn yap
                        aramaBaslangici = yer + 1;
                    }
                    else { break; }
                }
            }
            txtKod_VScroll(sender, e);
        }

        private void txtKod_VScroll(object sender, EventArgs e)
        {


            int nPos = SendMessage(txtKod.Handle, 0x00CE, 0, 0);
            nPos -= SendMessage(txtSatir.Handle, 0x00CE, 0, 0);
            SendMessage(txtSatir.Handle, 0x00B6, 0, nPos);

            //  BUNU EKLE: Ekran kaydýrýldýkça çubuðun yeri de güncellensin
            txtKod_SelectionChanged(sender, e);
        }

        private void txtKod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '"')
            {
                e.Handled = true; // Windows'un kendi basmasýný engelle (biz basacaðýz)
                int imlec = txtKod.SelectionStart;
                txtKod.SelectedText = "\"\""; // Ýki tane týrnak bas
                txtKod.SelectionStart = imlec + 1; // Ýmleci ikisinin arasýna al!
            }
            // Süslü parantez için (Araya enter koyup kod bloðu açar)
            else if (e.KeyChar == '{')
            {
                e.Handled = true;
                int imlec = txtKod.SelectionStart;
                txtKod.SelectedText = "{\n    \n}"; // Süslüleri koy, alt satýra geçip 4 boþluk (TAB) býrak
                txtKod.SelectionStart = imlec + 6; // Ýmleci tam o boþluðun ortasýna oturt!
            }
            // Köþeli parantez (Diziler için)
            else if (e.KeyChar == '[')
            {
                e.Handled = true;
                int imlec = txtKod.SelectionStart;
                txtKod.SelectedText = "[]";
                txtKod.SelectionStart = imlec + 1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSatir.WordWrap = false;

            konsoluTemizleToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.L;

            // Kodu Çalýþtýr için: F5
            koduBaþlatToolStripMenuItem.ShortcutKeys = Keys.F5;

            // Kaydetmek için: Ctrl + S
            kaydetToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;

            // Yeni Dosya için: Ctrl + N
            yeniDosyaToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;


            // Tümünü Seç için: Ctrl + A
            tümünüSeçToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;

            debugToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.B;

            // --- INTELLISENSE KUTUSUNU OLUÞTUR ---
            lstOto = new ListBox();
            lstOto.Visible = false;
            lstOto.Width = 120;
            lstOto.Height = 85;
            lstOto.Font = new Font("Consolas", 10, FontStyle.Bold);
            lstOto.BackColor = Color.FromArgb(40, 40, 40); // Gece moduna uygun koyu arka plan
            lstOto.ForeColor = Color.LimeGreen;            // Matrix yeþili yazýlar
            lstOto.BorderStyle = BorderStyle.FixedSingle;
            lstOto.DoubleClick += LstOto_DoubleClick;    // Çift týklanýrsa da seçsin
            this.Controls.Add(lstOto);                     // Kýrmýzý halýyla forma ekle
            lstOto.BringToFront();                         // En önde dursun

            // --- SAÐ TIK MENÜSÜ (HAZIR ÞABLONLAR) KURULUMU ---
            ContextMenuStrip sagTikMenu = new ContextMenuStrip();

            // 1. Döngü Þablonu
            ToolStripMenuItem donguSablonu = new ToolStripMenuItem("Döngü (DONGU) Þablonu Ekle");
            donguSablonu.Click += (s, ev) =>
            {
                txtKod.SelectedText = "DONGU sayac < 10 {\n    \n}\n";
            };

            // 2. Eðer Þablonu
            ToolStripMenuItem egerSablonu = new ToolStripMenuItem("Eðer (EGER) Þablonu Ekle");
            egerSablonu.Click += (s, ev) =>
            {
                txtKod.SelectedText = "EGER kosul == 1 {\n    \n}\n";
            };

            // 3. Fonksiyon Þablonu
            ToolStripMenuItem fonkSablonu = new ToolStripMenuItem("Fonksiyon (FONK) Þablonu Ekle");
            fonkSablonu.Click += (s, ev) =>
            {
                txtKod.SelectedText = "FONK YeniFonksiyon {\n    \n}\n";
            };

            // 4. Ekrana Yazdýrma Þablonu
            ToolStripMenuItem yazSablonu = new ToolStripMenuItem("Yazdýrma (YAZ) Þablonu Ekle");
            yazSablonu.Click += (s, ev) =>
            {
                txtKod.SelectedText = "YAZ \"Buraya metin girin\" ;\n";
            };

            // Elemanlarý menüye ekliyoruz
            sagTikMenu.Items.Add(donguSablonu);
            sagTikMenu.Items.Add(egerSablonu);
            sagTikMenu.Items.Add(fonkSablonu);
            sagTikMenu.Items.Add(new ToolStripSeparator()); // Araya þýk bir çizgi çektik
            sagTikMenu.Items.Add(yazSablonu);

            // Gece moduna uygun renkler verelim ki sýrýýtmasýn
            sagTikMenu.BackColor = Color.FromArgb(45, 45, 48);
            sagTikMenu.ForeColor = Color.White;

            // Bu menüyü bizim kod yazdýðýmýz alana (txtKod) baðlýyoruz
            txtKod.ContextMenuStrip = sagTikMenu;

            pnlAktifSatir = new Panel();
            pnlAktifSatir.Width = 4; // Çubuðun kalýnlýðý
            pnlAktifSatir.BackColor = Color.DodgerBlue; // VS Code Mavisi (Gece modunda efsane durur)
            txtSatir.Controls.Add(pnlAktifSatir); // Çubuðu satýr numaralarýnýn kutusuna ekliyoruz
            pnlAktifSatir.BringToFront();

            menuStrip1.Renderer = new KendiTemaMotorum();

            // --- YAN MENÜ AYIRICI (SPLITTER) VE ÝÐNE SÝSTEMÝ ---
            Splitter ayirici = new Splitter();
            ayirici.Dock = DockStyle.Left;
            ayirici.Width = 3; // Çizginin kalýnlýðý
            ayirici.BackColor = Color.FromArgb(60, 60, 60); // Gece moduna uygun koyu þýk bir çizgi
            ayirici.Cursor = Cursors.VSplit; // Fare gelince sað-sol ok iþareti çýksýn
            this.Controls.Add(ayirici);

            // DÝZÝLÝÞ SIRASI (Hayati Önem Taþýr! Çizginin tam araya girmesini saðlar)
            ayirici.SendToBack(); // Önce ayýrýcýyý en sola at
            panel1.SendToBack();  // Sonra paneli onun da soluna (en baþa) at

            // BONUS (ÝÐNE MANTIÐI): Çizgiye çift týklayýnca menü Açýlsýn/Kapansýn
            ayirici.DoubleClick += (s, ev) =>
            {
                if (panel1.Width > 10)
                    panel1.Width = 0; // Ýðne çýktý: Menüyü tamamen gizle
                else
                    panel1.Width = 220; // Ýðne takýldý: Menüyü geri aç

                ayirici.SendToBack(); // Önce ayýrýcýyý en sola at
                panel2.SendToBack();  // Sonra paneli onun da soluna (en baþa) at


            };

            // --- ALT KONSOL (PANEL 2) ÝÇÝN AYIRICI ---
            Splitter ayiriciAlt = new Splitter();
            ayiriciAlt.Dock = DockStyle.Bottom; // Aþaðýya yapýþsýn
            ayiriciAlt.Height = 3; // Kalýnlýk
            ayiriciAlt.BackColor = Color.FromArgb(60, 60, 60);
            ayiriciAlt.Cursor = Cursors.HSplit; // Fare gelince Aþaðý-Yukarý ok çýksýn
            this.Controls.Add(ayiriciAlt);

            // DÝZÝLÝÞ SIRASI (Çizginin konsolun hemen üstüne oturmasý için)
            ayiriciAlt.SendToBack();
            panel2.SendToBack(); // NOT: Eðer konsolunun bulunduðu panelin adý farklýysa burayý ona göre deðiþtir

            pnlBul = new Panel();
            pnlBul.Size = new Size(250, 40);
            pnlBul.BackColor = Color.FromArgb(45, 45, 48); // Gece moduna uygun koyu panel
            pnlBul.BorderStyle = BorderStyle.FixedSingle;
            pnlBul.Visible = false; // Ýlk baþta gizli duracak, Ctrl+F yapýnca açýlacak

            txtAranan = new TextBox();
            txtAranan.Location = new Point(10, 10);
            txtAranan.Width = 150;
            txtAranan.BackColor = Color.FromArgb(30, 30, 30);
            txtAranan.ForeColor = Color.White;
            txtAranan.BorderStyle = BorderStyle.FixedSingle;
            pnlBul.Controls.Add(txtAranan);

            Button btnBul = new Button();
            btnBul.Text = "Bul";
            btnBul.Location = new Point(170, 8);
            btnBul.Size = new Size(60, 24);
            btnBul.FlatStyle = FlatStyle.Flat;
            btnBul.ForeColor = Color.White;
            btnBul.Cursor = Cursors.Hand;
            pnlBul.Controls.Add(btnBul);

            this.Controls.Add(pnlBul); // Paneli direkt ana pencereye (Form'a) ekle
            pnlBul.BringToFront();     // Ne olursa olsun en önde (yazýlarýn üstünde) tut

            // Konumunu, kod ekranýnýn sað üst köþesine denk gelecek þekilde hesapla
            pnlBul.Location = new Point(txtKod.Left + txtKod.Width - pnlBul.Width - 30, txtKod.Top + 10);
            pnlBul.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // "Bul" Butonuna Týklanýnca Çalýþacak Arama Motoru
            btnBul.Click += (s, ev) =>
            {
                string aranan = txtAranan.Text;
                if (string.IsNullOrEmpty(aranan)) return;

                // Yazýyý kodlar içinde ara
                int index = txtKod.Text.IndexOf(aranan, sonBulunanIndex, StringComparison.OrdinalIgnoreCase);
                if (index != -1) // Bulunduysa
                {
                    txtKod.Select(index, aranan.Length); // Kelimeyi maviyle seç (vurgula)
                    txtKod.Focus();
                    txtKod.ScrollToCaret(); // Ekraný o kelimenin olduðu yere kaydýr
                    sonBulunanIndex = index + aranan.Length; // Bir sonrakine geçmek için sýrayý ilerlet
                }
                else // Bulunamadýysa veya sona ulaþýldýysa
                {
                    sonBulunanIndex = 0; // Baþa dönüp tekrar ara
                }
            };
        }

        private async void koduBaþlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string dosyaYolu = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "gecici_kod.faruk");
                System.IO.File.WriteAllText(dosyaYolu, txtKod.Text);

                txtKonsol.Text = "Terminal baslatiliyor...\n";

                System.Diagnostics.Process islem = new System.Diagnostics.Process();
                islem.StartInfo.FileName = "cmd.exe";
                islem.StartInfo.Arguments = "/c TurkceDilim.exe " + dosyaYolu + " & pause";
                islem.StartInfo.UseShellExecute = true;
                islem.StartInfo.CreateNoWindow = false;

                islem.Start();
                await Task.Run(() => islem.WaitForExit());

                string ciktiYolu = "cikti.txt";
                if (System.IO.File.Exists(ciktiYolu))
                {
                    txtKonsol.Text = System.IO.File.ReadAllText(ciktiYolu);
                }
            }
            catch (Exception ex)
            {
                txtKonsol.Text = "DERLEYICI HATASI!\n" + ex.Message;
            }

        }

        private void yeniDosyaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Mevcut kodu silip yepyeni, bos bir sayfa acmak istiyor musun? (Kaydetmediysen her sey silinir!)", "Yeni Dosya Uyarisi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                txtKod.Clear(); // Kod ekranýný temizle
                txtKonsol.Text = "Yeni ve temiz bir dosya acildi. Kodlamaya baslayabilirsiniz!";
            }
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog kaydetPenceresi = new SaveFileDialog();
            kaydetPenceresi.Title = "Kodlarý Kaydet";
            // Ýþte kendi uzantýmýz!
            kaydetPenceresi.Filter = "Faruk Kod Dosyasý (*.faruk)|*.faruk|Tüm Dosyalar (*.*)|*.*";

            if (kaydetPenceresi.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(kaydetPenceresi.FileName, txtKod.Text);
                MessageBox.Show("Kodlar baþariyla kaydedildi!", "Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dosyaAçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog acPenceresi = new OpenFileDialog();
            acPenceresi.Title = "Kod Dosyasý Seç";
            acPenceresi.Filter = "Faruk Kod Dosyasý (*.faruk)|*.faruk|Metin Dosyalarý (*.txt)|*.txt";

            if (acPenceresi.ShowDialog() == DialogResult.OK)
            {
                txtKod.Text = System.IO.File.ReadAllText(acPenceresi.FileName);
            }
        }

        private void geriAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtKod.Undo();
        }

        private void ileriAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtKod.Redo();
        }

        private void kesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtKod.Cut();
        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtKod.Copy();
        }

        private void yapýþtýrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtKod.Paste();
        }

        private void tümünüSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtKod.SelectAll();
        }

        private void çýkýþToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hakkýýndaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Türkçe Dilim IDE v1.0\nGeliþtirici: Faruk\n\nBu IDE, tamamen Türkçe komutlarla çalýþan, özel olarak derlenmiþ bir C++ motoru kullanmaktadýr. E-Arena ve Teknoloji vizyonuyla geliþtirilmiþtir.", "Sistem Hakkýnda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtKod_SelectionChanged(object sender, EventArgs e)
        {
            int satir = txtKod.GetLineFromCharIndex(txtKod.SelectionStart) + 1;
            int sutun = txtKod.SelectionStart - txtKod.GetFirstCharIndexOfCurrentLine() + 1;

            // Status bar üzerindeki yazýya aktarýr
            toolStripStatusLabel1.Text = $"Satýr: {satir}, Sütun: {sutun}   |   UTF-8   |   Türkçe Dilim Motoru Aktif";

            //  AKTÝF SATIR ÇUBUÐUNU HAREKET ETTÝR
            if (pnlAktifSatir != null && txtKod.TextLength > 0)
            {
                // Þu anki satýrýn ekrandaki (Y) koordinatýný bul
                int ilkKarakterIndex = txtKod.GetFirstCharIndexOfCurrentLine();
                Point koordinat = txtKod.GetPositionFromCharIndex(ilkKarakterIndex);

                // Çubuðu o hizaya çekip boyunu yazýnýn boyuna (font yüksekliðine) göre ayarla
                pnlAktifSatir.Location = new Point(0, koordinat.Y);
                pnlAktifSatir.Height = txtKod.Font.Height + 2;
            }
        }

        private void yakýnlaþtýrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float yeniBoyut = txtKod.Font.Size + 2.0f; // Fontu 2 birim büyüt
            txtKod.Font = new Font(txtKod.Font.FontFamily, yeniBoyut, txtKod.Font.Style);
        }

        private void uzaklaþtýrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float yeniBoyut = txtKod.Font.Size - 2.0f; // Fontu 2 birim küçült
            if (yeniBoyut > 6) // Yazý çok küçülüp kaybolmasýn diye sýnýr koyuyoruz
            {
                txtKod.Font = new Font(txtKod.Font.FontFamily, yeniBoyut, txtKod.Font.Style);
            }
        }

        private void konsoluTemizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtKonsol.Clear();
        }

        private async void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Yazýlan kodu "gecici_kod.faruk" adýyla kaydet
                string dosyaYolu = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "gecici_kod.faruk");
                System.IO.File.WriteAllText(dosyaYolu, txtKod.Text);

                txtKonsol.Text = "Hata Ayiklama (Debug) baslatiliyor...\nMotor izleme modunda calisiyor...\n";

                // 2. Motoru bu sefer "-debug" parametresiyle çaðýrýyoruz!
                System.Diagnostics.Process islem = new System.Diagnostics.Process();
                islem.StartInfo.FileName = "cmd.exe";
                islem.StartInfo.Arguments = "/c TurkceDilim.exe " + dosyaYolu + " -debug & pause";
                islem.StartInfo.UseShellExecute = true;
                islem.StartInfo.CreateNoWindow = false;

                islem.Start();

                // Motorun iþini bitirmesini bekle
                await Task.Run(() => islem.WaitForExit());

                // 3. Standart konsol çýktýsýný ekrana yazdýr
                string ciktiYolu = "cikti.txt";
                if (System.IO.File.Exists(ciktiYolu))
                {
                    txtKonsol.Text += "\n--- KONSOL ÇIKTISI ---\n" + System.IO.File.ReadAllText(ciktiYolu);
                }

                // 4. EFSANE KISIM: Debug Loglarýný Oku ve Arayüzde Göster!
                string debugYolu = "debug_log.txt";
                if (System.IO.File.Exists(debugYolu))
                {
                    string[] loglar = System.IO.File.ReadAllLines(debugYolu);

                    // Kodla yepyeni, þýk bir pencere (Form) oluþturuyoruz
                    Form debugEkrani = new Form();
                    debugEkrani.Text = "E-Arena | Deðiþken Ýzleme (Watch)";
                    debugEkrani.Size = new Size(450, 350);
                    debugEkrani.StartPosition = FormStartPosition.CenterParent;
                    debugEkrani.ShowIcon = false;

                    // Ýçine bir tablo (DataGridView) ekliyoruz
                    DataGridView dgv = new DataGridView();
                    dgv.Dock = DockStyle.Fill;
                    dgv.AllowUserToAddRows = false;
                    dgv.ReadOnly = true; // Kullanýcý tabloyu deðiþtiremesin
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.BackgroundColor = Color.White;
                    dgv.RowHeadersVisible = false;

                    // Tablo Sütunlarý
                    dgv.Columns.Add("Degisken", "Deðiþken Adý");
                    dgv.Columns.Add("Deger", "Hafýzadaki Son Deðer");
                    dgv.Columns.Add("Satir", "Son Ýþlem Yeri");

                    // Okunan loglarý satýr satýr parçala
                    foreach (string log in loglar)
                    {
                        string[] parcalar = log.Split('|');

                        // Güvenlik kontrolü: Formatýmýz "DEGISKEN|isim|deger|satir" olmalý
                        if (parcalar.Length == 4 && parcalar[0] == "DEGISKEN")
                        {
                            string ad = parcalar[1];
                            string deger = parcalar[2];
                            string satir = "Satýr " + parcalar[3];

                            bool bulundu = false;

                            // Eðer deðiþken tabloda zaten varsa, sadece deðerini ve satýrýný güncelle
                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == ad)
                                {
                                    row.Cells[1].Value = deger;
                                    row.Cells[2].Value = satir;
                                    bulundu = true;
                                    break;
                                }
                            }

                            // Eðer deðiþken tabloya ilk kez geliyorsa yeni satýr olarak ekle
                            if (!bulundu)
                            {
                                dgv.Rows.Add(ad, deger, satir);
                            }
                        }
                    }

                    // Tabloyu pencereye ekle ve pencereyi göster
                    debugEkrani.Controls.Add(dgv);
                    debugEkrani.ShowDialog(); // Ekranda pop-up olarak çýkar!
                }
                else
                {
                    txtKonsol.Text += "\n[SÝSTEM]: Uyarý! debug_log.txt bulunamadý. Kodda deðiþken tanýmlanmamýþ olabilir.";
                }
            }
            catch (Exception ex)
            {
                txtKonsol.Text = "DERLEYICI HATASI!\n" + ex.Message;
            }
        }

        private void geceModuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            karanlikTema = !karanlikTema;

            if (karanlikTema)
            {
                //  GECE MODU
                this.BackColor = Color.FromArgb(30, 30, 30);
                panel1.BackColor = Color.FromArgb(30, 30, 30);

                treeView1.BackColor = Color.FromArgb(40, 40, 40);
                treeView1.ForeColor = Color.White;
                treeView1.LineColor = Color.White;

                txtSatir.BackColor = Color.FromArgb(50, 50, 50);
                txtSatir.ForeColor = Color.DarkGray;

                txtKonsol.BackColor = Color.Black;
                txtKonsol.ForeColor = Color.LimeGreen;

                statusStrip1.BackColor = Color.FromArgb(0, 122, 204);
                statusStrip1.ForeColor = Color.White;
                foreach (ToolStripItem eleman in statusStrip1.Items) eleman.ForeColor = Color.White;

                //  DÝKKAT: Sadece arka planý deðiþtiriyoruz, ForeColor YOK!
                txtKod.BackColor = Color.FromArgb(40, 40, 40);
            }
            else
            {
                //  GÜNDÜZ MODU
                this.BackColor = SystemColors.Control;
                panel1.BackColor = SystemColors.Control;

                treeView1.BackColor = Color.White;
                treeView1.ForeColor = Color.Black;
                treeView1.LineColor = Color.Black;

                txtSatir.BackColor = SystemColors.Control;
                txtSatir.ForeColor = Color.Black;

                txtKonsol.BackColor = Color.Black;
                txtKonsol.ForeColor = Color.White;

                statusStrip1.BackColor = SystemColors.Control;
                foreach (ToolStripItem eleman in statusStrip1.Items) eleman.ForeColor = Color.Black;

                //  DÝKKAT: Sadece arka planý deðiþtiriyoruz, ForeColor YOK!
                txtKod.BackColor = Color.White;
            }

            // Renkler bozulmadan tazelensin diye bizim canavar metodu çaðýrýyoruz
            TemaRenkleriniTazele();
        }
        private string SonKelimeyiBul()
        {
            int imlec = txtKod.SelectionStart;
            if (imlec == 0) return "";

            int baslangic = imlec - 1;
            while (baslangic >= 0 && char.IsLetterOrDigit(txtKod.Text[baslangic]))
            {
                baslangic--;
            }
            baslangic++;

            if (baslangic < imlec)
                return txtKod.Text.Substring(baslangic, imlec - baslangic).ToUpper();
            return "";
        }

        // 2. Seçilen kelimeyi koda yerleþtiren metot
        private void OtomatikTamamla()
        {
            if (lstOto.Visible && lstOto.SelectedItem != null)
            {
                string secilen = lstOto.SelectedItem.ToString();
                string suanki = SonKelimeyiBul();

                int imlec = txtKod.SelectionStart;
                txtKod.Select(imlec - suanki.Length, suanki.Length);
                txtKod.SelectedText = secilen + " ";
                lstOto.Visible = false;
            }
        }

        // 3. ÝÞTE HATA VEREN VE EKSÝK OLAN KISIM BURASI (Mouse ile çift týklanýrsa)
        private void LstOto_DoubleClick(object sender, EventArgs e)
        {
            OtomatikTamamla();
        }

        private void txtKod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Eðer hafýzada geri dönecek bir geçmiþ varsa
                if (geriAlHafizasi.Count > 1)
                {
                    motorCalisiyor = true; // Kalkanlarý aç ki geri alýrken tekrar hafýzaya kaydetmesin

                    geriAlHafizasi.Pop(); // Þu anki hatalý yazýyý çöpe at
                    string eskiYazi = geriAlHafizasi.Peek(); // Bir önceki temiz yazýyý al

                    int imlec = txtKod.SelectionStart; // Ýmlecin yerini aklýmýzda tutalým

                    txtKod.Text = eskiYazi; // ÞAK! Kodu eski haline döndürdük

                    TemaRenkleriniTazele(); // Eski haline dönünce renkler solmasýn diye tekrar boyatýyoruz

                    // Ýmleci düzgün yere koy
                    if (imlec <= txtKod.TextLength) txtKod.SelectionStart = imlec;
                    else txtKod.SelectionStart = txtKod.TextLength;

                    motorCalisiyor = false;
                }
                else if (geriAlHafizasi.Count == 1)
                {
                    // Hafýzada sadece ilk boþ ekran kaldýysa her þeyi temizle
                    motorCalisiyor = true;
                    txtKod.Clear();
                    geriAlHafizasi.Clear();
                    motorCalisiyor = false;
                }
                return; // Ctrl+Z iþini yaptý, baþka koda inme
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                pnlBul.Visible = !pnlBul.Visible; // Paneli aç/kapat

                if (pnlBul.Visible)
                {
                    pnlBul.BringToFront(); // Garanti olsun, en öne çek
                    txtAranan.Focus();     // Ýmleci direkt arama kutusuna koy
                    sonBulunanIndex = 0;   // Taramayý sýfýrla
                }
                else
                {
                    txtKod.Focus(); // Panel kapanýnca imleç geri kod ekranýna dönsün
                }
                return; // Ýþlem bitti, aþaðýdaki kodlara inme
            }

            //  2. ÖNCELÝK: INTELLISENSE (KOD TAMAMLAMA) KONTROLLERÝ
            if (lstOto != null && lstOto.Visible)
            {
                if (e.KeyCode == Keys.Down)
                {
                    e.Handled = true;
                    if (lstOto.SelectedIndex < lstOto.Items.Count - 1) lstOto.SelectedIndex++;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    e.Handled = true;
                    if (lstOto.SelectedIndex > 0) lstOto.SelectedIndex--;
                }
                else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    OtomatikTamamla();
                }
                else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Space)
                {
                    lstOto.Visible = false;
                }
            }


        }

        private void txtKod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape || e.KeyCode == Keys.Tab)
                return;

            string aranan = SonKelimeyiBul();
            lstOto.Items.Clear();

            // ÝÞTE DEÐÝÞEN TEK SATIR BURASI! 
            // Artýk kelimenin en az 2 harfi yazýldýðýnda menü açýlacak. (Ýstersen 3 de yapabilirsin)
            if (aranan.Length >= 2)
            {
                foreach (string komut in komutlar)
                {
                    if (komut.StartsWith(aranan) && komut != aranan)
                    {
                        lstOto.Items.Add(komut);
                    }
                }
            }

            if (lstOto.Items.Count > 0)
            {
                Point koordinat = txtKod.GetPositionFromCharIndex(txtKod.SelectionStart);
                lstOto.Location = new Point(koordinat.X + txtKod.Left, koordinat.Y + txtKod.Top + 20);
                lstOto.SelectedIndex = 0;
                lstOto.Visible = true;
                lstOto.BringToFront();
            }
            else
            {
                lstOto.Visible = false;
            }
        }

        private void txtSatir_TextChanged(object sender, EventArgs e)
        {

        }
        // Menünün o gýcýk mavi rengini ezen kendi tema sýnýfýmýz
        private class KendiTemaMotorum : ToolStripProfessionalRenderer
        {
            public KendiTemaMotorum() : base(new KendiRenkTablom()) { }
        }

        private class KendiRenkTablom : ProfessionalColorTable
        {
            // Üzerine gelinceki arka plan rengi (Koyu Gri)
            public override Color MenuItemSelected { get { return Color.FromArgb(60, 60, 60); } }
            // Týklanýnca açýlan menünün arka planý (Daha Koyu Gri)
            public override Color MenuItemBorder { get { return Color.Transparent; } }
            public override Color MenuItemPressedGradientBegin { get { return Color.FromArgb(45, 45, 48); } }
            public override Color MenuItemPressedGradientEnd { get { return Color.FromArgb(45, 45, 48); } }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            koduBaþlatToolStripMenuItem.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kaydetToolStripMenuItem.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            geceModuToolStripMenuItem.PerformClick();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dosyaAçToolStripMenuItem.PerformClick();
        }

        private void dosyaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            // Týklanan elemanýn ismini alýyoruz
            string tiklanan = e.Node.Text;

            // --- DOSYA ÝÞLEMLERÝ ---
            if (tiklanan == "Yeni Dosya") yeniDosyaToolStripMenuItem.PerformClick();
            else if (tiklanan == "Dosya Aç") dosyaAçToolStripMenuItem.PerformClick();
            else if (tiklanan == "Kaydet") kaydetToolStripMenuItem.PerformClick();
            else if (tiklanan == "Çýkýþ") çýkýþToolStripMenuItem.PerformClick();

            // --- DÜZENLE ÝÞLEMLERÝ ---
            else if (tiklanan == "Kes") kesToolStripMenuItem.PerformClick();
            else if (tiklanan == "Kopyala") kopyalaToolStripMenuItem.PerformClick();
            else if (tiklanan == "Yapýþtýr") yapýþtýrToolStripMenuItem.PerformClick();
            else if (tiklanan == "Tümünü Seç")
            {
                txtKod.SelectAll(); //  1. Adým: Bütün metni arka planda seçer
                txtKod.Focus();     //  2. Adým: Mavi seçim efektini görebilmen için odaðý tekrar kod ekranýna fýrlatýr!
            }

            // --- ÇALIÞTIR VE DEBUG ---
            else if (tiklanan == "Kodu Baþlat") koduBaþlatToolStripMenuItem.PerformClick();
            // Kendi debug menünün text'ine göre burayý ayarla (Örn: "Hata Ayýkla" veya "Debug")
            else if (tiklanan == "Debug") debugToolStripMenuItem.PerformClick();

            // --- GÖRÜNÜM ÝÞLEMLERÝ ---
            else if (tiklanan == "Konsolu Temizle") konsoluTemizleToolStripMenuItem.PerformClick();
            else if (tiklanan == "Yakýnlaþtýr") yakýnlaþtýrToolStripMenuItem.PerformClick();
            else if (tiklanan == "Uzaklaþtýr") uzaklaþtýrToolStripMenuItem.PerformClick();
            else if (tiklanan == "Gece Modu") geceModuToolStripMenuItem.PerformClick();

            // --- YARDIM ---
            else if (tiklanan == "Hakkýnda") hakkýýndaToolStripMenuItem.PerformClick();

            // Ýþlem bittikten sonra seçimi iptal edelim ki kullanýcý ayný butona bir daha týklayabilsin
            treeView1.SelectedNode = null;
            toolStrip1.BackColor = Color.FromArgb(40, 40, 40);
        }

        private void TemaRenkleriniTazele()
        {
            if (txtKod.TextLength == 0) return;

            //  KALKAN AÇIK: Windows'a diyoruz ki "Þimdi yapacaklarýmý senin geri alma hafýzana kaydetme!"
            motorCalisiyor = true;

            int imlec = txtKod.SelectionStart;

            txtKod.SelectAll();
            txtKod.SelectionColor = karanlikTema ? Color.White : Color.Black;

            string[] komutlar = { "YAZ", "EGER", "DONGU", "FONK", "OKU", "VE", "VEYA" };

            foreach (string komut in komutlar)
            {
                int aramaBaslangici = 0;
                while (aramaBaslangici < txtKod.TextLength)
                {
                    int index = txtKod.Find(komut, aramaBaslangici, RichTextBoxFinds.WholeWord);
                    if (index != -1)
                    {
                        txtKod.Select(index, komut.Length);
                        txtKod.SelectionColor = Color.DarkOrange;
                        aramaBaslangici = index + komut.Length;
                    }
                    else break;
                }
            }

            txtKod.Select(imlec, 0);
            txtKod.SelectionColor = karanlikTema ? Color.White : Color.Black;

            // KALKAN KAPALI: Boyama bitti, normal hayata dönebiliriz.
            motorCalisiyor = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            yeniDosyaToolStripMenuItem.PerformClick();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            kaydetToolStripMenuItem.PerformClick();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            debugToolStripMenuItem.PerformClick();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            koduBaþlatToolStripMenuItem.PerformClick();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            çýkýþToolStripMenuItem.PerformClick();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            dosyaAçToolStripMenuItem.PerformClick();
        }
    }
}
