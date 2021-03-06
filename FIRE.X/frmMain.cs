﻿using FIRE.X.UI.Common;
using System;
using System.Windows.Forms;

namespace FIRE.X
{
    public partial class frmMain : Form
    {
        Control Home;

        public frmMain()
        {
            InitializeComponent();
            
            this.Text = $"{this.Text} - {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";

            // we keep the home interface so we can reset to it
            this.Home = tableLayoutPanel.GetControlFromPosition(1, 0);
        }

        public void SetMenu(UserControl menu)
        {
            // clear everything, new menu = new content
            this.tableLayoutPanel.Controls.Clear();

            // add the menu
            if (menu != null)
                this.tableLayoutPanel.Controls.Add(menu, 0, 0);
            else
                this.tableLayoutPanel.Controls.Remove(tableLayoutPanel.GetControlFromPosition(0, 0));
        }

        public void SetContent(Control content)
        {
            // only clear the content
            tableLayoutPanel.Controls.Remove(tableLayoutPanel.GetControlFromPosition(1, 0));

            // add the content
            this.tableLayoutPanel.Controls.Add(content, 1, 0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void p2PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMenu(new MenuUserControl(new MenuUserControlView()
            {
                MenuItems = new MenuItemUserControl[] {
                    new MenuItemUserControl(new EventHandler((o, a) =>
                    {
                        this.SetContent(new P2PImportUserControl());
                    })) {
                    Text = "Import"
                },  new MenuItemUserControl(new EventHandler((o, a) =>
                    {
                        this.SetContent(new ChartUserControl(new ChartUserControlView()));
                    })) {
                    Text = "Charts"
                }}
            }).RegisterEvents());
        }

        /// <summary>
        ///     Event when control has been added to our panel, we want to fill up the control so we use maximum screen width and height
        /// </summary>
        private void tableLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.Dock = DockStyle.Fill;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SetMenu(null);
            SetContent(Home);
        }
    }
}
