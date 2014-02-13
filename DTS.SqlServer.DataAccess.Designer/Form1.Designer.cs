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
            this.editor = new System.Windows.Forms.TextBox();
            this.lookups = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.sql = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.connect = new System.Windows.Forms.Button();
            this.database = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.server = new System.Windows.Forms.TextBox();
            this.build = new System.Windows.Forms.Button();
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
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 345);
            this.panel1.TabIndex = 1;
            // 
            // definition
            // 
            this.definition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.definition.Location = new System.Drawing.Point(0, 0);
            this.definition.Margin = new System.Windows.Forms.Padding(2);
            this.definition.Name = "definition";
            this.definition.Size = new System.Drawing.Size(150, 345);
            this.definition.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.editor);
            this.panel2.Controls.Add(this.lookups);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(150, 46);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 345);
            this.panel2.TabIndex = 2;
            // 
            // editor
            // 
            this.editor.AcceptsTab = true;
            this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editor.Location = new System.Drawing.Point(0, 0);
            this.editor.Margin = new System.Windows.Forms.Padding(2);
            this.editor.Multiline = true;
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(150, 248);
            this.editor.TabIndex = 1;
            this.editor.TextChanged += new System.EventHandler(this.dql_TextChanged);
            this.editor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dql_KeyPress);
            this.editor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editor_KeyUp);
            // 
            // lookups
            // 
            this.lookups.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lookups.HideSelection = false;
            this.lookups.Location = new System.Drawing.Point(0, 248);
            this.lookups.Name = "lookups";
            this.lookups.Size = new System.Drawing.Size(150, 97);
            this.lookups.TabIndex = 2;
            this.lookups.UseCompatibleStateImageBehavior = false;
            this.lookups.View = System.Windows.Forms.View.List;
            this.lookups.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.sql);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(300, 46);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 345);
            this.panel3.TabIndex = 3;
            // 
            // sql
            // 
            this.sql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sql.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sql.Location = new System.Drawing.Point(0, 0);
            this.sql.Margin = new System.Windows.Forms.Padding(2);
            this.sql.Multiline = true;
            this.sql.Name = "sql";
            this.sql.Size = new System.Drawing.Size(150, 345);
            this.sql.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBox4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(450, 46);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(238, 345);
            this.panel4.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(0, 0);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(238, 345);
            this.textBox4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.build);
            this.panel5.Controls.Add(this.connect);
            this.panel5.Controls.Add(this.database);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.server);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(688, 46);
            this.panel5.TabIndex = 0;
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(337, 2);
            this.connect.Margin = new System.Windows.Forms.Padding(2);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(56, 19);
            this.connect.TabIndex = 4;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // database
            // 
            this.database.Location = new System.Drawing.Point(243, 2);
            this.database.Margin = new System.Windows.Forms.Padding(2);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(76, 20);
            this.database.TabIndex = 3;
            this.database.Text = "DTS.SqlServer.DataAccess.Tests";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // server
            // 
            this.server.Location = new System.Drawing.Point(87, 2);
            this.server.Margin = new System.Windows.Forms.Padding(2);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(76, 20);
            this.server.TabIndex = 1;
            this.server.Text = ".\\sql2012";
            // 
            // build
            // 
            this.build.Location = new System.Drawing.Point(516, 1);
            this.build.Margin = new System.Windows.Forms.Padding(2);
            this.build.Name = "build";
            this.build.Size = new System.Drawing.Size(56, 19);
            this.build.TabIndex = 5;
            this.build.Text = "Build";
            this.build.UseVisualStyleBackColor = true;
            this.build.Click += new System.EventHandler(this.build_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 391);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.TextBox editor;
        private System.Windows.Forms.TextBox sql;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox database;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox server;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.TreeView definition;
        private System.Windows.Forms.ListView lookups;
        private System.Windows.Forms.Button build;

    }
}

