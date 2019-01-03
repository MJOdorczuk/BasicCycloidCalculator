using BCC.Core.Geometry;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BCC.Interface_View.StandardInterface
{
    class StandardForm : Form
    {

        private TabControl workTabsControl;
        public TabControl WorkSpace => workTabsControl;

        public StandardForm()
        {
            InitializeComponent();

            // WorkTabsControl
            Controls.Add(new Func<Control>(() =>
            {
                workTabsControl = new TabControl
                {
                    Cursor = Cursors.Default,
                    Dock = DockStyle.Fill,
                    Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238))),
                    Location = new Point(0, 29),
                    SelectedIndex = 0,
                    Size = new Size(1264, 652),
                    TabIndex = 2
                };

                // DummyPage
                workTabsControl.TabPages.Add(new Func<TabPage>(() =>
                {
                    var DummyPage = new TabPage
                    {
                        Location = new Point(4, 25),
                        Size = new Size(1256, 623),
                        TabIndex = 0,
                        Text = "Dummy Page",
                        UseVisualStyleBackColor = true
                    };
                    var LanguageButton = new Button
                    {
                        Location = new Point(285, 185),
                        Size = new Size(75, 23),
                        TabIndex = 0,
                        AutoSize = true,
                        Text = "Language button",
                        UseVisualStyleBackColor = true
                    };
                    LanguageButton.Click += (sender, e) =>
                    {
                        switch (Vocabulary.GetLanguage())
                        {
                            case Vocabulary.Language.POLISH:
                                Vocabulary.SetLanguage(Vocabulary.Language.ENGLISH);
                                break;
                            case Vocabulary.Language.ENGLISH:
                                Vocabulary.SetLanguage(Vocabulary.Language.POLISH);
                                break;
                        }
                    };
                    DummyPage.Controls.Add(LanguageButton);
                    return DummyPage;
                })());

                return workTabsControl;
            })());

            // ISOMenuBar
            Controls.Add(new Func<Control>(() => 
            {
                var ISOMenuStrip = new MenuStrip
                {
                    Font = new System.Drawing.Font("Segoe UI", 12F),
                    Location = new System.Drawing.Point(0, 0),
                    Size = new System.Drawing.Size(1264, 29),
                    TabIndex = 1
                };
                var ISOMenuItems = new List<ToolStripMenuItem>();

                // File
                new Action(() =>
                {
                    var FileItem = new ToolStripMenuItem
                    {
                        Size = new System.Drawing.Size(46, 25)
                    };
                    var FileItemItems = new List<ToolStripItem>();
                    Vocabulary.AddNameCall(() => FileItem.Text = Vocabulary.ISOBar.File());
                    ISOMenuItems.Add(FileItem);

                    // New
                    new Action(() =>
                    {
                        var NewItem = new ToolStripMenuItem
                        {
                            Size = new System.Drawing.Size(118, 26)
                        };
                        Vocabulary.AddNameCall(() => NewItem.Text = Vocabulary.ISOBar.New());
                        FileItemItems.Add(NewItem);
                    })();

                    // Open
                    new Action(() =>
                    {
                        var OpenItem = new ToolStripMenuItem
                        {
                            Size = new System.Drawing.Size(118, 26)
                        };
                        Vocabulary.AddNameCall(() => OpenItem.Text = Vocabulary.ISOBar.Open());
                        FileItemItems.Add(OpenItem);
                    })();

                    // Save
                    new Action(() =>
                    {
                        var SaveItem = new ToolStripMenuItem
                        {
                            Size = new System.Drawing.Size(118, 26)
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
                            Size = new System.Drawing.Size(118, 26)
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
                        Size = new System.Drawing.Size(46, 25)
                    };
                    var EditItemItems = new List<ToolStripItem>();
                    Vocabulary.AddNameCall(() => EditItem.Text = Vocabulary.ISOBar.Edit());
                    ISOMenuItems.Add(EditItem);



                    EditItem.DropDownItems.AddRange(EditItemItems.ToArray());
                })();

                // View
                new Action(() =>
                {
                    var ViewItem = new ToolStripMenuItem
                    {
                        Size = new System.Drawing.Size(46, 25)
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
                        Size = new System.Drawing.Size(46, 25)
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
            this.ClientSize = new Size(1264, 681);
            this.MaximizeBox = false;
            this.MaximumSize = new Size(1920, 1080);
            this.Name = "StandardForm";
            this.ResumeLayout(false);

        }
    }
}