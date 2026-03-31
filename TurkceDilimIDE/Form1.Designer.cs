namespace TurkceDilimIDE
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("Yeni Dosya", 12, 12);
            TreeNode treeNode2 = new TreeNode("Dosya Aç", 13, 13);
            TreeNode treeNode3 = new TreeNode("Kaydet", 17, 17);
            TreeNode treeNode4 = new TreeNode("Çıkış", 5, 5);
            TreeNode treeNode5 = new TreeNode("Dosya", 8, 8, new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4 });
            TreeNode treeNode6 = new TreeNode("Kes", 3, 3);
            TreeNode treeNode7 = new TreeNode("Kopyala", 2, 2);
            TreeNode treeNode8 = new TreeNode("Yapıştır", 1, 1);
            TreeNode treeNode9 = new TreeNode("Düzenle", 6, 6, new TreeNode[] { treeNode6, treeNode7, treeNode8 });
            TreeNode treeNode10 = new TreeNode("Kodu Başlat", 16, 16);
            TreeNode treeNode11 = new TreeNode("Debug", 4, 4);
            TreeNode treeNode12 = new TreeNode("Çalıştır ve Debug", 14, 14, new TreeNode[] { treeNode10, treeNode11 });
            TreeNode treeNode13 = new TreeNode("Konsolu Temizle", 0, 0);
            TreeNode treeNode14 = new TreeNode("Yakınlaştır", 19, 19);
            TreeNode treeNode15 = new TreeNode("Uzaklaştır", 20, 20);
            TreeNode treeNode16 = new TreeNode("Gece Modu", 11, 11);
            TreeNode treeNode17 = new TreeNode("Görünüm", 10, 10, new TreeNode[] { treeNode13, treeNode14, treeNode15, treeNode16 });
            TreeNode treeNode18 = new TreeNode("Hakkında", 9, 9);
            TreeNode treeNode19 = new TreeNode("Yardım", 15, 15, new TreeNode[] { treeNode18 });
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtKod = new RichTextBox();
            txtKonsol = new RichTextBox();
            txtSatir = new RichTextBox();
            panel1 = new Panel();
            splitter2 = new Splitter();
            splitter1 = new Splitter();
            treeView1 = new TreeView();
            ımageList1 = new ImageList(components);
            panel2 = new Panel();
            menuStrip1 = new MenuStrip();
            dosyaToolStripMenuItem = new ToolStripMenuItem();
            yeniDosyaToolStripMenuItem = new ToolStripMenuItem();
            dosyaAçToolStripMenuItem = new ToolStripMenuItem();
            kaydetToolStripMenuItem = new ToolStripMenuItem();
            çıkışToolStripMenuItem = new ToolStripMenuItem();
            düzenleToolStripMenuItem = new ToolStripMenuItem();
            kesToolStripMenuItem = new ToolStripMenuItem();
            kopyalaToolStripMenuItem = new ToolStripMenuItem();
            yapıştırToolStripMenuItem = new ToolStripMenuItem();
            tümünüSeçToolStripMenuItem = new ToolStripMenuItem();
            çalıştırToolStripMenuItem = new ToolStripMenuItem();
            koduBaşlatToolStripMenuItem = new ToolStripMenuItem();
            görünümToolStripMenuItem = new ToolStripMenuItem();
            konsoluTemizleToolStripMenuItem = new ToolStripMenuItem();
            yakınlaştırToolStripMenuItem = new ToolStripMenuItem();
            uzaklaştırToolStripMenuItem = new ToolStripMenuItem();
            geceModuToolStripMenuItem = new ToolStripMenuItem();
            hakkındaToolStripMenuItem = new ToolStripMenuItem();
            hakkıındaToolStripMenuItem = new ToolStripMenuItem();
            debugToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButton2 = new ToolStripButton();
            toolStripSeparator9 = new ToolStripSeparator();
            toolStripButton6 = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton3 = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripButton4 = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripButton5 = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            toolStripTextBox1 = new ToolStripTextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtKod
            // 
            txtKod.BackColor = Color.White;
            txtKod.Dock = DockStyle.Fill;
            txtKod.Font = new Font("Segoe UI", 12F);
            txtKod.ForeColor = SystemColors.InactiveBorder;
            txtKod.Location = new Point(281, 54);
            txtKod.Name = "txtKod";
            txtKod.Size = new Size(792, 399);
            txtKod.TabIndex = 0;
            txtKod.Text = "";
            txtKod.SelectionChanged += txtKod_SelectionChanged;
            txtKod.VScroll += txtKod_VScroll;
            txtKod.TextChanged += txtKod_TextChanged;
            txtKod.KeyDown += txtKod_KeyDown;
            txtKod.KeyPress += txtKod_KeyPress;
            txtKod.KeyUp += txtKod_KeyUp;
            // 
            // txtKonsol
            // 
            txtKonsol.BackColor = SystemColors.WindowText;
            txtKonsol.Dock = DockStyle.Fill;
            txtKonsol.ForeColor = Color.Lime;
            txtKonsol.Location = new Point(0, 0);
            txtKonsol.Name = "txtKonsol";
            txtKonsol.Size = new Size(819, 247);
            txtKonsol.TabIndex = 2;
            txtKonsol.Text = "";
            // 
            // txtSatir
            // 
            txtSatir.BackColor = Color.Gray;
            txtSatir.BorderStyle = BorderStyle.None;
            txtSatir.Dock = DockStyle.Left;
            txtSatir.Font = new Font("Segoe UI", 12F);
            txtSatir.Location = new Point(254, 54);
            txtSatir.Name = "txtSatir";
            txtSatir.ReadOnly = true;
            txtSatir.RightToLeft = RightToLeft.Yes;
            txtSatir.ScrollBars = RichTextBoxScrollBars.None;
            txtSatir.Size = new Size(27, 399);
            txtSatir.TabIndex = 5;
            txtSatir.Text = "";
            txtSatir.TextChanged += txtSatir_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DimGray;
            panel1.Controls.Add(splitter2);
            panel1.Controls.Add(splitter1);
            panel1.Controls.Add(treeView1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(254, 732);
            panel1.TabIndex = 6;
            // 
            // splitter2
            // 
            splitter2.Location = new Point(4, 0);
            splitter2.Name = "splitter2";
            splitter2.Size = new Size(4, 732);
            splitter2.TabIndex = 2;
            splitter2.TabStop = false;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(0, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(4, 732);
            splitter1.TabIndex = 1;
            splitter1.TabStop = false;
            // 
            // treeView1
            // 
            treeView1.BackColor = Color.White;
            treeView1.BorderStyle = BorderStyle.None;
            treeView1.Dock = DockStyle.Fill;
            treeView1.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 162);
            treeView1.ForeColor = Color.Black;
            treeView1.ImageIndex = 19;
            treeView1.ImageList = ımageList1;
            treeView1.ItemHeight = 30;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 12;
            treeNode1.Name = "Düğüm1";
            treeNode1.SelectedImageIndex = 12;
            treeNode1.Text = "Yeni Dosya";
            treeNode2.ImageIndex = 13;
            treeNode2.Name = "Düğüm2";
            treeNode2.SelectedImageIndex = 13;
            treeNode2.Text = "Dosya Aç";
            treeNode3.ImageIndex = 17;
            treeNode3.Name = "Düğüm3";
            treeNode3.SelectedImageIndex = 17;
            treeNode3.Text = "Kaydet";
            treeNode4.ImageIndex = 5;
            treeNode4.Name = "Düğüm4";
            treeNode4.SelectedImageIndex = 5;
            treeNode4.Text = "Çıkış";
            treeNode5.ImageIndex = 8;
            treeNode5.Name = "Düğüm0";
            treeNode5.SelectedImageIndex = 8;
            treeNode5.Text = "Dosya";
            treeNode6.ImageIndex = 3;
            treeNode6.Name = "Düğüm6";
            treeNode6.SelectedImageIndex = 3;
            treeNode6.Text = "Kes";
            treeNode7.ImageIndex = 2;
            treeNode7.Name = "Düğüm7";
            treeNode7.SelectedImageIndex = 2;
            treeNode7.Text = "Kopyala";
            treeNode8.ImageIndex = 1;
            treeNode8.Name = "Düğüm8";
            treeNode8.SelectedImageIndex = 1;
            treeNode8.Text = "Yapıştır";
            treeNode9.ImageIndex = 6;
            treeNode9.Name = "Düğüm5";
            treeNode9.SelectedImageIndex = 6;
            treeNode9.Text = "Düzenle";
            treeNode10.ImageIndex = 16;
            treeNode10.Name = "Düğüm11";
            treeNode10.SelectedImageIndex = 16;
            treeNode10.Text = "Kodu Başlat";
            treeNode11.ImageIndex = 4;
            treeNode11.Name = "Düğüm19";
            treeNode11.SelectedImageIndex = 4;
            treeNode11.Text = "Debug";
            treeNode12.ImageIndex = 14;
            treeNode12.Name = "Düğüm10";
            treeNode12.SelectedImageIndex = 14;
            treeNode12.Text = "Çalıştır ve Debug";
            treeNode13.ImageIndex = 0;
            treeNode13.Name = "Düğüm13";
            treeNode13.SelectedImageIndex = 0;
            treeNode13.Text = "Konsolu Temizle";
            treeNode14.ImageIndex = 19;
            treeNode14.Name = "Düğüm14";
            treeNode14.SelectedImageIndex = 19;
            treeNode14.Text = "Yakınlaştır";
            treeNode15.ImageIndex = 20;
            treeNode15.Name = "Düğüm15";
            treeNode15.SelectedImageIndex = 20;
            treeNode15.Text = "Uzaklaştır";
            treeNode16.ImageIndex = 11;
            treeNode16.Name = "Düğüm16";
            treeNode16.SelectedImageIndex = 11;
            treeNode16.Text = "Gece Modu";
            treeNode17.ImageIndex = 10;
            treeNode17.Name = "Düğüm12";
            treeNode17.SelectedImageIndex = 10;
            treeNode17.Text = "Görünüm";
            treeNode18.ImageIndex = 9;
            treeNode18.Name = "Düğüm18";
            treeNode18.SelectedImageIndex = 9;
            treeNode18.Text = "Hakkında";
            treeNode19.ImageIndex = 15;
            treeNode19.Name = "Düğüm17";
            treeNode19.SelectedImageIndex = 15;
            treeNode19.Text = "Yardım";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode5, treeNode9, treeNode12, treeNode17, treeNode19 });
            treeView1.SelectedImageIndex = 8;
            treeView1.ShowLines = false;
            treeView1.Size = new Size(254, 732);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // ımageList1
            // 
            ımageList1.ColorDepth = ColorDepth.Depth32Bit;
            ımageList1.ImageStream = (ImageListStreamer)resources.GetObject("ımageList1.ImageStream");
            ımageList1.TransparentColor = Color.Transparent;
            ımageList1.Images.SetKeyName(0, "icons8-broom-24.png");
            ımageList1.Images.SetKeyName(1, "icons8-clipboard-24.png");
            ımageList1.Images.SetKeyName(2, "icons8-copy-24.png");
            ımageList1.Images.SetKeyName(3, "icons8-cut-24.png");
            ımageList1.Images.SetKeyName(4, "icons8-debug-32.png");
            ımageList1.Images.SetKeyName(5, "icons8-down-arrow-24.png");
            ımageList1.Images.SetKeyName(6, "icons8-edit-32.png");
            ımageList1.Images.SetKeyName(7, "icons8-folder-24.png");
            ımageList1.Images.SetKeyName(8, "icons8-folder-30.png");
            ımageList1.Images.SetKeyName(9, "icons8-info-24.png");
            ımageList1.Images.SetKeyName(10, "icons8-monitor-24.png");
            ımageList1.Images.SetKeyName(11, "icons8-moon-30 (1).png");
            ımageList1.Images.SetKeyName(12, "icons8-new-file-24.png");
            ımageList1.Images.SetKeyName(13, "icons8-opened-folder-26.png");
            ımageList1.Images.SetKeyName(14, "icons8-play-24.png");
            ımageList1.Images.SetKeyName(15, "icons8-question-mark-24.png");
            ımageList1.Images.SetKeyName(16, "icons8-rocket-24.png");
            ımageList1.Images.SetKeyName(17, "icons8-save-30.png");
            ımageList1.Images.SetKeyName(18, "icons8-select-all-24.png");
            ımageList1.Images.SetKeyName(19, "icons8-zoom-in-24.png");
            ımageList1.Images.SetKeyName(20, "icons8-zoom-out-24.png");
            // 
            // panel2
            // 
            panel2.BackColor = Color.FloralWhite;
            panel2.Controls.Add(txtKonsol);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(254, 453);
            panel2.Name = "panel2";
            panel2.Size = new Size(819, 247);
            panel2.TabIndex = 7;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.AppWorkspace;
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dosyaToolStripMenuItem, düzenleToolStripMenuItem, çalıştırToolStripMenuItem, görünümToolStripMenuItem, hakkındaToolStripMenuItem, debugToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1073, 33);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.Visible = false;
            // 
            // dosyaToolStripMenuItem
            // 
            dosyaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { yeniDosyaToolStripMenuItem, dosyaAçToolStripMenuItem, kaydetToolStripMenuItem, çıkışToolStripMenuItem });
            dosyaToolStripMenuItem.ForeColor = Color.Black;
            dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            dosyaToolStripMenuItem.Size = new Size(78, 29);
            dosyaToolStripMenuItem.Text = "Dosya";
            dosyaToolStripMenuItem.Click += dosyaToolStripMenuItem_Click;
            // 
            // yeniDosyaToolStripMenuItem
            // 
            yeniDosyaToolStripMenuItem.Name = "yeniDosyaToolStripMenuItem";
            yeniDosyaToolStripMenuItem.Size = new Size(200, 34);
            yeniDosyaToolStripMenuItem.Text = "Yeni Dosya";
            yeniDosyaToolStripMenuItem.Click += yeniDosyaToolStripMenuItem_Click;
            // 
            // dosyaAçToolStripMenuItem
            // 
            dosyaAçToolStripMenuItem.Name = "dosyaAçToolStripMenuItem";
            dosyaAçToolStripMenuItem.Size = new Size(200, 34);
            dosyaAçToolStripMenuItem.Text = "Dosya Aç";
            dosyaAçToolStripMenuItem.Click += dosyaAçToolStripMenuItem_Click;
            // 
            // kaydetToolStripMenuItem
            // 
            kaydetToolStripMenuItem.Name = "kaydetToolStripMenuItem";
            kaydetToolStripMenuItem.Size = new Size(200, 34);
            kaydetToolStripMenuItem.Text = "Kaydet";
            kaydetToolStripMenuItem.Click += kaydetToolStripMenuItem_Click;
            // 
            // çıkışToolStripMenuItem
            // 
            çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            çıkışToolStripMenuItem.Size = new Size(200, 34);
            çıkışToolStripMenuItem.Text = "Çıkış";
            çıkışToolStripMenuItem.Click += çıkışToolStripMenuItem_Click;
            // 
            // düzenleToolStripMenuItem
            // 
            düzenleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { kesToolStripMenuItem, kopyalaToolStripMenuItem, yapıştırToolStripMenuItem, tümünüSeçToolStripMenuItem });
            düzenleToolStripMenuItem.ForeColor = Color.Black;
            düzenleToolStripMenuItem.Name = "düzenleToolStripMenuItem";
            düzenleToolStripMenuItem.Size = new Size(91, 29);
            düzenleToolStripMenuItem.Text = "Düzenle";
            // 
            // kesToolStripMenuItem
            // 
            kesToolStripMenuItem.Name = "kesToolStripMenuItem";
            kesToolStripMenuItem.Size = new Size(211, 34);
            kesToolStripMenuItem.Text = "Kes";
            kesToolStripMenuItem.Click += kesToolStripMenuItem_Click;
            // 
            // kopyalaToolStripMenuItem
            // 
            kopyalaToolStripMenuItem.Name = "kopyalaToolStripMenuItem";
            kopyalaToolStripMenuItem.Size = new Size(211, 34);
            kopyalaToolStripMenuItem.Text = "Kopyala";
            kopyalaToolStripMenuItem.Click += kopyalaToolStripMenuItem_Click;
            // 
            // yapıştırToolStripMenuItem
            // 
            yapıştırToolStripMenuItem.Name = "yapıştırToolStripMenuItem";
            yapıştırToolStripMenuItem.Size = new Size(211, 34);
            yapıştırToolStripMenuItem.Text = "Yapıştır";
            yapıştırToolStripMenuItem.Click += yapıştırToolStripMenuItem_Click;
            // 
            // tümünüSeçToolStripMenuItem
            // 
            tümünüSeçToolStripMenuItem.Name = "tümünüSeçToolStripMenuItem";
            tümünüSeçToolStripMenuItem.Size = new Size(211, 34);
            tümünüSeçToolStripMenuItem.Text = "Tümünü Seç";
            tümünüSeçToolStripMenuItem.Click += tümünüSeçToolStripMenuItem_Click;
            // 
            // çalıştırToolStripMenuItem
            // 
            çalıştırToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { koduBaşlatToolStripMenuItem });
            çalıştırToolStripMenuItem.ForeColor = Color.Black;
            çalıştırToolStripMenuItem.Name = "çalıştırToolStripMenuItem";
            çalıştırToolStripMenuItem.Size = new Size(80, 29);
            çalıştırToolStripMenuItem.Text = "Çalıştır";
            // 
            // koduBaşlatToolStripMenuItem
            // 
            koduBaşlatToolStripMenuItem.Name = "koduBaşlatToolStripMenuItem";
            koduBaşlatToolStripMenuItem.Size = new Size(207, 34);
            koduBaşlatToolStripMenuItem.Text = "Kodu Başlat";
            koduBaşlatToolStripMenuItem.Click += koduBaşlatToolStripMenuItem_Click;
            // 
            // görünümToolStripMenuItem
            // 
            görünümToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { konsoluTemizleToolStripMenuItem, yakınlaştırToolStripMenuItem, uzaklaştırToolStripMenuItem, geceModuToolStripMenuItem });
            görünümToolStripMenuItem.ForeColor = Color.Black;
            görünümToolStripMenuItem.Name = "görünümToolStripMenuItem";
            görünümToolStripMenuItem.Size = new Size(103, 29);
            görünümToolStripMenuItem.Text = "Görünüm";
            // 
            // konsoluTemizleToolStripMenuItem
            // 
            konsoluTemizleToolStripMenuItem.Name = "konsoluTemizleToolStripMenuItem";
            konsoluTemizleToolStripMenuItem.Size = new Size(240, 34);
            konsoluTemizleToolStripMenuItem.Text = "Konsolu Temizle";
            konsoluTemizleToolStripMenuItem.Click += konsoluTemizleToolStripMenuItem_Click;
            // 
            // yakınlaştırToolStripMenuItem
            // 
            yakınlaştırToolStripMenuItem.Name = "yakınlaştırToolStripMenuItem";
            yakınlaştırToolStripMenuItem.Size = new Size(240, 34);
            yakınlaştırToolStripMenuItem.Text = "Yakınlaştır";
            yakınlaştırToolStripMenuItem.Click += yakınlaştırToolStripMenuItem_Click;
            // 
            // uzaklaştırToolStripMenuItem
            // 
            uzaklaştırToolStripMenuItem.Name = "uzaklaştırToolStripMenuItem";
            uzaklaştırToolStripMenuItem.Size = new Size(240, 34);
            uzaklaştırToolStripMenuItem.Text = "Uzaklaştır";
            uzaklaştırToolStripMenuItem.Click += uzaklaştırToolStripMenuItem_Click;
            // 
            // geceModuToolStripMenuItem
            // 
            geceModuToolStripMenuItem.Name = "geceModuToolStripMenuItem";
            geceModuToolStripMenuItem.Size = new Size(240, 34);
            geceModuToolStripMenuItem.Text = "Gece Modu";
            geceModuToolStripMenuItem.Click += geceModuToolStripMenuItem_Click;
            // 
            // hakkındaToolStripMenuItem
            // 
            hakkındaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { hakkıındaToolStripMenuItem });
            hakkındaToolStripMenuItem.ForeColor = Color.Black;
            hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
            hakkındaToolStripMenuItem.Size = new Size(82, 29);
            hakkındaToolStripMenuItem.Text = "Yardım";
            // 
            // hakkıındaToolStripMenuItem
            // 
            hakkıındaToolStripMenuItem.Name = "hakkıındaToolStripMenuItem";
            hakkıındaToolStripMenuItem.Size = new Size(188, 34);
            hakkıındaToolStripMenuItem.Text = "Hakkında";
            hakkıındaToolStripMenuItem.Click += hakkıındaToolStripMenuItem_Click;
            // 
            // debugToolStripMenuItem
            // 
            debugToolStripMenuItem.ForeColor = Color.Black;
            debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            debugToolStripMenuItem.Size = new Size(82, 29);
            debugToolStripMenuItem.Text = "Debug";
            debugToolStripMenuItem.Click += debugToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.ActiveCaption;
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(254, 700);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(819, 32);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = Color.Black;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(179, 25);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.White;
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(45, 45);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripSeparator1, toolStripButton2, toolStripSeparator9, toolStripButton6, toolStripSeparator2, toolStripButton3, toolStripSeparator3, toolStripButton4, toolStripSeparator4, toolStripButton5, toolStripSeparator5, toolStripTextBox1 });
            toolStrip1.Location = new Point(254, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(819, 54);
            toolStrip1.TabIndex = 10;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(49, 49);
            toolStripButton1.Text = "toolStripButton1";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 54);
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(49, 49);
            toolStripButton2.Text = "toolStripButton2";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(6, 54);
            // 
            // toolStripButton6
            // 
            toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton6.Image = (Image)resources.GetObject("toolStripButton6.Image");
            toolStripButton6.ImageTransparentColor = Color.Magenta;
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new Size(49, 49);
            toolStripButton6.Text = "toolStripButton6";
            toolStripButton6.Click += toolStripButton6_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 54);
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(49, 49);
            toolStripButton3.Text = "toolStripButton3";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 54);
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(49, 49);
            toolStripButton4.Text = "toolStripButton4";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 54);
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(49, 49);
            toolStripButton5.Text = "toolStripButton5";
            toolStripButton5.Click += toolStripButton5_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 54);
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.BackColor = Color.White;
            toolStripTextBox1.Enabled = false;
            toolStripTextBox1.ForeColor = Color.Black;
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new Size(110, 54);
            toolStripTextBox1.Text = "Türkçe Dilim";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1073, 732);
            Controls.Add(txtKod);
            Controls.Add(txtSatir);
            Controls.Add(panel2);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "TurkceDilim";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox txtKod;
        private RichTextBox txtKonsol;
        private RichTextBox txtSatir;
        private Panel panel1;
        private Panel panel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem dosyaToolStripMenuItem;
        private ToolStripMenuItem yeniDosyaToolStripMenuItem;
        private ToolStripMenuItem dosyaAçToolStripMenuItem;
        private ToolStripMenuItem kaydetToolStripMenuItem;
        private ToolStripMenuItem çalıştırToolStripMenuItem;
        private ToolStripMenuItem koduBaşlatToolStripMenuItem;
        private ToolStripMenuItem görünümToolStripMenuItem;
        private ToolStripMenuItem konsoluTemizleToolStripMenuItem;
        private ToolStripMenuItem düzenleToolStripMenuItem;
        private ToolStripMenuItem kesToolStripMenuItem;
        private ToolStripMenuItem kopyalaToolStripMenuItem;
        private ToolStripMenuItem yapıştırToolStripMenuItem;
        private ToolStripMenuItem tümünüSeçToolStripMenuItem;
        private ToolStripMenuItem çıkışToolStripMenuItem;
        private ToolStripMenuItem hakkındaToolStripMenuItem;
        private ToolStripMenuItem hakkıındaToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem yakınlaştırToolStripMenuItem;
        private ToolStripMenuItem uzaklaştırToolStripMenuItem;
        private ToolStripMenuItem debugToolStripMenuItem;
        private ToolStripMenuItem geceModuToolStripMenuItem;
        private TreeView treeView1;
        private ImageList ımageList1;
        private Splitter splitter1;
        private Splitter splitter2;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton2;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton4;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton toolStripButton5;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripButton toolStripButton6;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripTextBox toolStripTextBox1;
    }
}
