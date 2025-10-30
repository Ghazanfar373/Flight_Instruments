using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RJCodeAdvance.RJControls;
using RJCodeAdvance;
using KLCToolbox.KLCControls;

namespace TestCases
{
    public partial class ConnectionOptions:Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        public ConnectionOptions()
        {
            InitializeComponent();
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionOptions));

            // Main containers
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.pnl_ConnectionPanel = new System.Windows.Forms.Panel();
            this.pnl_ActiveConnections = new System.Windows.Forms.Panel();
            this.pnl_ButtonFooter = new System.Windows.Forms.FlowLayoutPanel();

            // Connection input controls
            this.lbl_Port = new System.Windows.Forms.Label();
            this.cmb_SerialPort = new System.Windows.Forms.ComboBox();
            this.lbl_BaudRate = new System.Windows.Forms.Label();
            this.cmb_BaudRate = new System.Windows.Forms.ComboBox();
            this.lbl_SystemID = new System.Windows.Forms.Label();
            this.cmb_SystemID = new System.Windows.Forms.ComboBox();

            // Connection list header
            this.lbl_ActiveConnectionsTitle = new System.Windows.Forms.Label();
            this.lbl_ConnectionCount = new System.Windows.Forms.Label();

            // Connection list
            this.lvw_ActiveConnections = new System.Windows.Forms.ListView();
            this.col_Port = new System.Windows.Forms.ColumnHeader();
            this.col_BaudRate = new System.Windows.Forms.ColumnHeader();
            this.col_SystemID = new System.Windows.Forms.ColumnHeader();
            this.col_Status = new System.Windows.Forms.ColumnHeader();
            this.col_Action = new System.Windows.Forms.ColumnHeader();

            // Buttons
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();

            // Status indicator
            this.lbl_StatusIndicator = new System.Windows.Forms.Label();
            this.lbl_StatusText = new System.Windows.Forms.Label();

            this.pnl_Main.SuspendLayout();
            this.pnl_ConnectionPanel.SuspendLayout();
            this.pnl_ActiveConnections.SuspendLayout();
            this.pnl_ButtonFooter.SuspendLayout();
            this.SuspendLayout();

            // ============ MAIN PANEL ============
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Padding = new System.Windows.Forms.Padding(15);
            this.pnl_Main.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnl_Main.Controls.Add(this.pnl_ActiveConnections);
            this.pnl_Main.Controls.Add(this.pnl_ConnectionPanel);

            // ============ CONNECTION INPUT PANEL ============
            this.pnl_ConnectionPanel.BackColor = System.Drawing.Color.White;
            this.pnl_ConnectionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_ConnectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ConnectionPanel.Height = 220;
            this.pnl_ConnectionPanel.Padding = new System.Windows.Forms.Padding(15);

            // Status bar at top of connection panel
            this.lbl_StatusIndicator.AutoSize = false;
            this.lbl_StatusIndicator.Width = 12;
            this.lbl_StatusIndicator.Height = 12;
            this.lbl_StatusIndicator.BackColor = System.Drawing.Color.Gray;
            this.lbl_StatusIndicator.Location = new System.Drawing.Point(15, 15);
            this.lbl_StatusIndicator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            this.lbl_StatusText.AutoSize = true;
            this.lbl_StatusText.Text = "Ready to connect";
            this.lbl_StatusText.Location = new System.Drawing.Point(32, 15);
            this.lbl_StatusText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lbl_StatusText.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);

            // Port selection
            this.lbl_Port.AutoSize = true;
            this.lbl_Port.Text = "Serial Port:";
            this.lbl_Port.Location = new System.Drawing.Point(15, 50);
            this.lbl_Port.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lbl_Port.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);

            this.cmb_SerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SerialPort.Location = new System.Drawing.Point(15, 70);
            this.cmb_SerialPort.Size = new System.Drawing.Size(200, 24);
            this.cmb_SerialPort.Font = new System.Drawing.Font("Segoe UI", 9F);

            // Baud rate selection
            this.lbl_BaudRate.AutoSize = true;
            this.lbl_BaudRate.Text = "Baud Rate:";
            this.lbl_BaudRate.Location = new System.Drawing.Point(230, 50);
            this.lbl_BaudRate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lbl_BaudRate.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);

            this.cmb_BaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_BaudRate.Location = new System.Drawing.Point(230, 70);
            this.cmb_BaudRate.Size = new System.Drawing.Size(120, 24);
            this.cmb_BaudRate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmb_BaudRate.Items.AddRange(new object[] {
                "9600",
                "19200",
                "38400",
                "57600",
                "115200",
                "230400",
                "460800",
                "921600"
            });

            // System ID selection
            this.lbl_SystemID.AutoSize = true;
            this.lbl_SystemID.Text = "System ID:";
            this.lbl_SystemID.Location = new System.Drawing.Point(365, 50);
            this.lbl_SystemID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lbl_SystemID.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);

            this.cmb_SystemID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SystemID.Location = new System.Drawing.Point(365, 70);
            this.cmb_SystemID.Size = new System.Drawing.Size(100, 24);
            this.cmb_SystemID.Font = new System.Drawing.Font("Segoe UI", 9F);

            // Refresh button
            this.btn_Refresh.Location = new System.Drawing.Point(480, 70);
            this.btn_Refresh.Size = new System.Drawing.Size(70, 24);
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btn_Refresh.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand;

            // Connect and Disconnect buttons
            this.btn_Connect.Location = new System.Drawing.Point(15, 120);
            this.btn_Connect.Size = new System.Drawing.Size(110, 36);
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_Connect.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btn_Connect.ForeColor = System.Drawing.Color.White;
            this.btn_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Connect.FlatAppearance.BorderSize = 0;
            this.btn_Connect.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btn_Disconnect.Location = new System.Drawing.Point(135, 120);
            this.btn_Disconnect.Size = new System.Drawing.Size(110, 36);
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_Disconnect.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.btn_Disconnect.ForeColor = System.Drawing.Color.White;
            this.btn_Disconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Disconnect.FlatAppearance.BorderSize = 0;
            this.btn_Disconnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Disconnect.Enabled = false;

            // Add controls to connection panel
            this.pnl_ConnectionPanel.Controls.Add(this.lbl_StatusText);
            this.pnl_ConnectionPanel.Controls.Add(this.lbl_StatusIndicator);
            this.pnl_ConnectionPanel.Controls.Add(this.lbl_Port);
            this.pnl_ConnectionPanel.Controls.Add(this.cmb_SerialPort);
            this.pnl_ConnectionPanel.Controls.Add(this.lbl_BaudRate);
            this.pnl_ConnectionPanel.Controls.Add(this.cmb_BaudRate);
            this.pnl_ConnectionPanel.Controls.Add(this.lbl_SystemID);
            this.pnl_ConnectionPanel.Controls.Add(this.cmb_SystemID);
            this.pnl_ConnectionPanel.Controls.Add(this.btn_Refresh);
            this.pnl_ConnectionPanel.Controls.Add(this.btn_Connect);
            this.pnl_ConnectionPanel.Controls.Add(this.btn_Disconnect);

            // ============ ACTIVE CONNECTIONS PANEL ============
            this.pnl_ActiveConnections.BackColor = System.Drawing.Color.White;
            this.pnl_ActiveConnections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_ActiveConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_ActiveConnections.Padding = new System.Windows.Forms.Padding(15);
            this.pnl_ActiveConnections.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);

            // Active Connections header
            this.lbl_ActiveConnectionsTitle.AutoSize = true;
            this.lbl_ActiveConnectionsTitle.Text = "Active Connections";
            this.lbl_ActiveConnectionsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbl_ActiveConnectionsTitle.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lbl_ActiveConnectionsTitle.Location = new System.Drawing.Point(15, 15);

            this.lbl_ConnectionCount.AutoSize = true;
            this.lbl_ConnectionCount.Text = "(0)";
            this.lbl_ConnectionCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbl_ConnectionCount.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lbl_ConnectionCount.Location = new System.Drawing.Point(150, 18);

            // List view for active connections
            this.lvw_ActiveConnections.Location = new System.Drawing.Point(15, 45);
            this.lvw_ActiveConnections.Size = new System.Drawing.Size(560, 220);
            this.lvw_ActiveConnections.View = System.Windows.Forms.View.Details;
            this.lvw_ActiveConnections.FullRowSelect = true;
            this.lvw_ActiveConnections.GridLines = true;
            this.lvw_ActiveConnections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvw_ActiveConnections.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvw_ActiveConnections.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);

            // Column headers
            this.col_Port.Text = "Port";
            this.col_Port.Width = 100;

            this.col_BaudRate.Text = "Baud Rate";
            this.col_BaudRate.Width = 90;

            this.col_SystemID.Text = "System ID";
            this.col_SystemID.Width = 80;

            this.col_Status.Text = "Status";
            this.col_Status.Width = 100;

            this.col_Action.Text = "Action";
            this.col_Action.Width = 90;

            this.lvw_ActiveConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.col_Port,
                this.col_BaudRate,
                this.col_SystemID,
                this.col_Status,
                this.col_Action
            });

            // Add controls to active connections panel
            this.pnl_ActiveConnections.Controls.Add(this.lvw_ActiveConnections);
            this.pnl_ActiveConnections.Controls.Add(this.lbl_ConnectionCount);
            this.pnl_ActiveConnections.Controls.Add(this.lbl_ActiveConnectionsTitle);

            // ============ BUTTON FOOTER ============
            this.pnl_ButtonFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_ButtonFooter.AutoSize = true;
            this.pnl_ButtonFooter.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnl_ButtonFooter.Padding = new System.Windows.Forms.Padding(0, 15, 15, 0);
            this.pnl_ButtonFooter.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            this.btn_Close.Size = new System.Drawing.Size(90, 36);
            this.btn_Close.Text = "Close";
            this.btn_Close.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_Close.BackColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.btn_Close.ForeColor = System.Drawing.Color.White;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnl_ButtonFooter.Controls.Add(this.btn_Close);

            // Add footer to main
            this.pnl_Main.Controls.Add(this.pnl_ButtonFooter);

            // ============ MAIN FORM ============
            resources.ApplyResources(this, "$this");
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.pnl_Main);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionOptions";
            this.Text = "MAVLink Connection Manager";
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionOptions_FormClosing);
            //this.Load += new System.EventHandler(this.ConnectionOptions_Load);

            this.pnl_Main.ResumeLayout(false);
            this.pnl_ConnectionPanel.ResumeLayout(false);
            this.pnl_ActiveConnections.ResumeLayout(false);
            this.pnl_ButtonFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // Panels
        private System.Windows.Forms.Panel pnl_Main;
        private System.Windows.Forms.Panel pnl_ConnectionPanel;
        private System.Windows.Forms.Panel pnl_ActiveConnections;
        private System.Windows.Forms.FlowLayoutPanel pnl_ButtonFooter;

        // Connection input controls
        private System.Windows.Forms.Label lbl_Port;
        private System.Windows.Forms.ComboBox cmb_SerialPort;
        private System.Windows.Forms.Label lbl_BaudRate;
        private System.Windows.Forms.ComboBox cmb_BaudRate;
        private System.Windows.Forms.Label lbl_SystemID;
        private System.Windows.Forms.ComboBox cmb_SystemID;

        // Connection list header
        private System.Windows.Forms.Label lbl_ActiveConnectionsTitle;
        private System.Windows.Forms.Label lbl_ConnectionCount;

        // Connection list
        private System.Windows.Forms.ListView lvw_ActiveConnections;
        private System.Windows.Forms.ColumnHeader col_Port;
        private System.Windows.Forms.ColumnHeader col_BaudRate;
        private System.Windows.Forms.ColumnHeader col_SystemID;
        private System.Windows.Forms.ColumnHeader col_Status;
        private System.Windows.Forms.ColumnHeader col_Action;

        // Buttons
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Refresh;

        // Status indicator
        private System.Windows.Forms.Label lbl_StatusIndicator;
        private System.Windows.Forms.Label lbl_StatusText;
    }
}