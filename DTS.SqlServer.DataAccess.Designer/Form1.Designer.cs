namespace DTS.SqlServer.DataAccess.Designer
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.definition = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dql = new System.Windows.Forms.TextBox();
            this.lookup = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.connect = new System.Windows.Forms.Button();
            this.database = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.server = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.definition);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 424);
            this.panel1.TabIndex = 1;
            // 
            // definition
            // 
            this.definition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.definition.Location = new System.Drawing.Point(0, 0);
            this.definition.Name = "definition";
            this.definition.Size = new System.Drawing.Size(200, 424);
            this.definition.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dql);
            this.panel2.Controls.Add(this.lookup);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(200, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 424);
            this.panel2.TabIndex = 2;
            // 
            // dql
            // 
            this.dql.AcceptsTab = true;
            this.dql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dql.Location = new System.Drawing.Point(0, 0);
            this.dql.Multiline = true;
            this.dql.Name = "dql";
            this.dql.Size = new System.Drawing.Size(200, 340);
            this.dql.TabIndex = 1;
            this.dql.TextChanged += new System.EventHandler(this.dql_TextChanged);
            this.dql.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dql_KeyPress);
            // 
            // lookup
            // 
            this.lookup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lookup.FormattingEnabled = true;
            this.lookup.ItemHeight = 16;
            this.lookup.Location = new System.Drawing.Point(0, 340);
            this.lookup.Name = "lookup";
            this.lookup.Size = new System.Drawing.Size(200, 84);
            this.lookup.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(400, 57);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 424);
            this.panel3.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(200, 424);
            this.textBox3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBox4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(600, 57);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(317, 424);
            this.panel4.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(0, 0);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(317, 424);
            this.textBox4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.connect);
            this.panel5.Controls.Add(this.database);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.server);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(917, 57);
            this.panel5.TabIndex = 0;
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(449, 3);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(75, 23);
            this.connect.TabIndex = 4;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // database
            // 
            this.database.Location = new System.Drawing.Point(324, 3);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(100, 22);
            this.database.TabIndex = 3;
            this.database.Text = "DTS.SqlServer.DataAccess.Tests";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // server
            // 
            this.server.Location = new System.Drawing.Point(116, 3);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(100, 22);
            this.server.TabIndex = 1;
            this.server.Text = ".\\sql2012";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 481);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Name = "Form1";
            this.Text = "DTS Sql Server Data Access Query Designer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox dql;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox database;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox server;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.TreeView definition;
        private System.Windows.Forms.ListBox lookup;

    }
}

