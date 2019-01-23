using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BCC.Interface_View.StandardInterface
{
    class StandardForm : Form
    {
        public TabControl WorkSpace { get; private set; }

        public StandardForm()
        {
            InitializeComponent();

            // WorkTabsControl
            Controls.Add(new Func<Control>(() =>
            {
                WorkSpace = new TabControl
                {
                    Cursor = Cursors.Default,
                    Dock = DockStyle.Fill,
                    Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238))),
                    Location = new Point(0, 29),
                    SelectedIndex = 0,
                    Size = new Size(1264, 652),
                    TabIndex = 2
                };

                return WorkSpace;
            })());

            // ISOMenuBar
            Controls.Add(new Func<Control>(() => 
            {
                var ISOMenuStrip = new MenuStrip
                {
                    Font = new Font("Segoe UI", 12F),
                    Location = new Point(0, 0),
                    Size = new Size(1264, 29),
                    TabIndex = 1
                };
                var ISOMenuItems = new List<ToolStripMenuItem>();

                // File
                new Action(() =>
                {
                    var FileItem = new ToolStripMenuItem
                    {
                        Size = new Size(46, 25)
                    };
                    var FileItemItems = new List<ToolStripItem>();
                    Vocabulary.AddNameCall(() => FileItem.Text = Vocabulary.ISOBar.File());
                    ISOMenuItems.Add(FileItem);

                    // New
                    new Action(() =>
                    {
                        var NewItem = new ToolStripMenuItem
                        {
                            Size = new Size(118, 26)
                        };
                        Vocabulary.AddNameCall(() => NewItem.Text = Vocabulary.ISOBar.New());
                        FileItemItems.Add(NewItem);
                    })();

                    // Open
                    new Action(() =>
                    {
                        var OpenItem = new ToolStripMenuItem
                        {
                            Size = new Size(118, 26)
                        };
                        Vocabulary.AddNameCall(() => OpenItem.Text = Vocabulary.ISOBar.Open());
                        FileItemItems.Add(OpenItem);
                    })();

                    // Save
                    new Action(() =>
                    {
                        var SaveItem = new ToolStripMenuItem
                        {
                            Size = new Size(118, 26)
                        };
                        Vocabulary.AddNameCall(() => SaveItem.Text = Vocabulary.ISOBar.Save());
                        FileItemItems.Add(SaveItem);
                    })();

                    FileItemItems.Add(new ToolStripSeparator());

                    // Exit
                    new Action(() =>
                    {
                        var ExitItem = new ToolStripMenuItem
                        {
                            Size = new Size(118, 26)
                        };
                        Vocabulary.AddNameCall(() => ExitItem.Text = Vocabulary.ISOBar.Exit());
                        ExitItem.Click += (sender, e) => this.Close();
                        FileItemItems.Add(ExitItem);
                    })();

                    FileItem.DropDownItems.AddRange(FileItemItems.ToArray());
                })();

                // Edit
                new Action(() =>
                {
                    var EditItem = new ToolStripMenuItem
                    {
                        Size = new Size(46, 25)
                    };
                    var EditItemItems = new List<ToolStripItem>();
                    Vocabulary.AddNameCall(() => EditItem.Text = Vocabulary.ISOBar.Edit());
                    ISOMenuItems.Add(EditItem);

                    // Language
                    new Action(() =>
                    {
                        var LanguageItem = new ToolStripMenuItem
                        {
                            Size = new Size(118, 26)
                        };
                        var LanguageItemItems = new List<ToolStripItem>();
                        Vocabulary.AddNameCall(() => LanguageItem.Text = Vocabulary.ISOBar.Language());
                        EditItemItems.Add(LanguageItem);

                        // Polish
                        new Action(() =>
                        {
                            var PolishItem = new ToolStripMenuItem
                            {
                                Size = new Size(118, 26)
                            };
                            Vocabulary.AddNameCall(() => PolishItem.Text = Vocabulary.ISOBar.Polish());
                            LanguageItemItems.Add(PolishItem);
                            PolishItem.Click += (sender, e) => Vocabulary.SetLanguage(Vocabulary.Language.POLISH);
                        })();

                        // Engilsh
                        new Action(() =>
                        {
                            var EnglishItem = new ToolStripMenuItem
                            {
                                Size = new Size(118, 26)
                            };
                            Vocabulary.AddNameCall(() => EnglishItem.Text = Vocabulary.ISOBar.English());
                            LanguageItemItems.Add(EnglishItem);
                            EnglishItem.Click += (sender, e) => Vocabulary.SetLanguage(Vocabulary.Language.ENGLISH);
                        })();

                        LanguageItem.DropDownItems.AddRange(LanguageItemItems.ToArray());
                    })();

                    EditItem.DropDownItems.AddRange(EditItemItems.ToArray());
                })();

                // View
                new Action(() =>
                {
                    var ViewItem = new ToolStripMenuItem
                    {
                        Size = new Size(46, 25)
                    };
                    var ViewItemItems = new List<ToolStripItem>();
                    Vocabulary.AddNameCall(() => ViewItem.Text = Vocabulary.ISOBar.View());
                    ISOMenuItems.Add(ViewItem);



                    ViewItem.DropDownItems.AddRange(ViewItemItems.ToArray());
                })();

                // Help
                new Action(() =>
                {
                    var HelpItem = new ToolStripMenuItem
                    {
                        Size = new Size(46, 25)
                    };
                    var HelpItemItems = new List<ToolStripItem>();
                    Vocabulary.AddNameCall(() => HelpItem.Text = Vocabulary.ISOBar.Help());
                    ISOMenuItems.Add(HelpItem);



                    HelpItem.DropDownItems.AddRange(HelpItemItems.ToArray());
                })();

                ISOMenuStrip.Items.AddRange(ISOMenuItems.ToArray());

                return ISOMenuStrip;
            })());
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StandardForm
            // 
            BackColor = SystemColors.Control;
            ClientSize = new Size(1264, 681);
            MaximizeBox = false;
            MaximumSize = new Size(1920, 1080);
            Name = "StandardForm";
            ResumeLayout(false);

        }
    }
}