namespace SGBD
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Connectionlabel = new System.Windows.Forms.Label();
            this.Userlabel = new System.Windows.Forms.Label();
            this.NewConnectionbutton = new System.Windows.Forms.Button();
            this.Disconnectbutton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.DDLbutton = new System.Windows.Forms.Button();
            this.Selectbutton = new System.Windows.Forms.Button();
            this.Createbutton = new System.Windows.Forms.Button();
            this.Alterbutton = new System.Windows.Forms.Button();
            this.Dropbutton = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.Playbutton = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SQLTab = new System.Windows.Forms.TabPage();
            this.DesignTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.Messagelabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.Updatebutton = new System.Windows.Forms.Button();
            this.SQLtextBox = new System.Windows.Forms.TextBox();
            this.DDLTab = new System.Windows.Forms.TabPage();
            this.DDLtextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SQLTab.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.DDLTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.71022F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.28978F));
            this.tableLayoutPanel1.Controls.Add(this.treeView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.00565F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.99435F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1148, 794);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(3, 106);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(266, 685);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDoubleClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.Connectionlabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Userlabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.NewConnectionbutton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Disconnectbutton, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(266, 97);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // Connectionlabel
            // 
            this.Connectionlabel.AutoSize = true;
            this.Connectionlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Connectionlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Connectionlabel.Location = new System.Drawing.Point(3, 0);
            this.Connectionlabel.Name = "Connectionlabel";
            this.Connectionlabel.Size = new System.Drawing.Size(127, 48);
            this.Connectionlabel.TabIndex = 0;
            this.Connectionlabel.Text = "Connection:";
            this.Connectionlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Userlabel
            // 
            this.Userlabel.AutoSize = true;
            this.Userlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Userlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Userlabel.Location = new System.Drawing.Point(136, 0);
            this.Userlabel.Name = "Userlabel";
            this.Userlabel.Size = new System.Drawing.Size(127, 48);
            this.Userlabel.TabIndex = 1;
            this.Userlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NewConnectionbutton
            // 
            this.NewConnectionbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewConnectionbutton.Location = new System.Drawing.Point(3, 51);
            this.NewConnectionbutton.Name = "NewConnectionbutton";
            this.NewConnectionbutton.Size = new System.Drawing.Size(127, 43);
            this.NewConnectionbutton.TabIndex = 2;
            this.NewConnectionbutton.Text = "New Connection";
            this.NewConnectionbutton.UseVisualStyleBackColor = true;
            this.NewConnectionbutton.Click += new System.EventHandler(this.NewConnectionbutton_Click);
            // 
            // Disconnectbutton
            // 
            this.Disconnectbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Disconnectbutton.Location = new System.Drawing.Point(136, 51);
            this.Disconnectbutton.Name = "Disconnectbutton";
            this.Disconnectbutton.Size = new System.Drawing.Size(127, 43);
            this.Disconnectbutton.TabIndex = 3;
            this.Disconnectbutton.Text = "Disconnect";
            this.Disconnectbutton.UseVisualStyleBackColor = true;
            this.Disconnectbutton.Click += new System.EventHandler(this.Disconnectbutton_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.DDLbutton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.Selectbutton, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.Createbutton, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.Alterbutton, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.Dropbutton, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(275, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(870, 97);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // DDLbutton
            // 
            this.DDLbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DDLbutton.Location = new System.Drawing.Point(3, 3);
            this.DDLbutton.Name = "DDLbutton";
            this.DDLbutton.Size = new System.Drawing.Size(168, 42);
            this.DDLbutton.TabIndex = 0;
            this.DDLbutton.Text = "DDL";
            this.DDLbutton.UseVisualStyleBackColor = true;
            this.DDLbutton.Click += new System.EventHandler(this.DDLbutton_Click);
            // 
            // Selectbutton
            // 
            this.Selectbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Selectbutton.Location = new System.Drawing.Point(177, 3);
            this.Selectbutton.Name = "Selectbutton";
            this.Selectbutton.Size = new System.Drawing.Size(168, 42);
            this.Selectbutton.TabIndex = 1;
            this.Selectbutton.Text = "Show";
            this.Selectbutton.UseVisualStyleBackColor = true;
            this.Selectbutton.Click += new System.EventHandler(this.Selectbutton_Click);
            // 
            // Createbutton
            // 
            this.Createbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Createbutton.Location = new System.Drawing.Point(351, 3);
            this.Createbutton.Name = "Createbutton";
            this.Createbutton.Size = new System.Drawing.Size(168, 42);
            this.Createbutton.TabIndex = 2;
            this.Createbutton.Text = "Create";
            this.Createbutton.UseVisualStyleBackColor = true;
            // 
            // Alterbutton
            // 
            this.Alterbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Alterbutton.Location = new System.Drawing.Point(525, 3);
            this.Alterbutton.Name = "Alterbutton";
            this.Alterbutton.Size = new System.Drawing.Size(168, 42);
            this.Alterbutton.TabIndex = 3;
            this.Alterbutton.Text = "Alter";
            this.Alterbutton.UseVisualStyleBackColor = true;
            // 
            // Dropbutton
            // 
            this.Dropbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dropbutton.Location = new System.Drawing.Point(699, 3);
            this.Dropbutton.Name = "Dropbutton";
            this.Dropbutton.Size = new System.Drawing.Size(168, 42);
            this.Dropbutton.TabIndex = 4;
            this.Dropbutton.Text = "Drop";
            this.Dropbutton.UseVisualStyleBackColor = true;
            this.Dropbutton.Click += new System.EventHandler(this.Dropbutton_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.Playbutton, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 51);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(168, 43);
            this.tableLayoutPanel7.TabIndex = 5;
            // 
            // Playbutton
            // 
            this.Playbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Playbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Playbutton.Location = new System.Drawing.Point(3, 18);
            this.Playbutton.Name = "Playbutton";
            this.Playbutton.Size = new System.Drawing.Size(78, 22);
            this.Playbutton.TabIndex = 0;
            this.Playbutton.Text = "Play";
            this.Playbutton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.dataGridView, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(275, 106);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.42105F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(870, 685);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 471);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(864, 174);
            this.dataGridView.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SQLTab);
            this.tabControl1.Controls.Add(this.DesignTab);
            this.tabControl1.Controls.Add(this.DDLTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(864, 462);
            this.tabControl1.TabIndex = 1;
            // 
            // SQLTab
            // 
            this.SQLTab.AllowDrop = true;
            this.SQLTab.Controls.Add(this.SQLtextBox);
            this.SQLTab.Location = new System.Drawing.Point(4, 22);
            this.SQLTab.Name = "SQLTab";
            this.SQLTab.Padding = new System.Windows.Forms.Padding(3);
            this.SQLTab.Size = new System.Drawing.Size(856, 436);
            this.SQLTab.TabIndex = 0;
            this.SQLTab.Text = "SQL";
            this.SQLTab.UseVisualStyleBackColor = true;
            // 
            // DesignTab
            // 
            this.DesignTab.Location = new System.Drawing.Point(4, 22);
            this.DesignTab.Name = "DesignTab";
            this.DesignTab.Padding = new System.Windows.Forms.Padding(3);
            this.DesignTab.Size = new System.Drawing.Size(856, 436);
            this.DesignTab.TabIndex = 1;
            this.DesignTab.Text = "Design";
            this.DesignTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.Messagelabel, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 651);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(864, 31);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // Messagelabel
            // 
            this.Messagelabel.AutoSize = true;
            this.Messagelabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Messagelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Messagelabel.Location = new System.Drawing.Point(3, 0);
            this.Messagelabel.Name = "Messagelabel";
            this.Messagelabel.Size = new System.Drawing.Size(426, 31);
            this.Messagelabel.TabIndex = 0;
            this.Messagelabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.Updatebutton, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(749, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(112, 25);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // Updatebutton
            // 
            this.Updatebutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Updatebutton.Location = new System.Drawing.Point(3, 3);
            this.Updatebutton.Name = "Updatebutton";
            this.Updatebutton.Size = new System.Drawing.Size(106, 19);
            this.Updatebutton.TabIndex = 0;
            this.Updatebutton.Text = "Update";
            this.Updatebutton.UseVisualStyleBackColor = true;
            this.Updatebutton.Click += new System.EventHandler(this.Updatebutton_Click);
            // 
            // SQLtextBox
            // 
            this.SQLtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SQLtextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SQLtextBox.Location = new System.Drawing.Point(3, 3);
            this.SQLtextBox.Multiline = true;
            this.SQLtextBox.Name = "SQLtextBox";
            this.SQLtextBox.ReadOnly = true;
            this.SQLtextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SQLtextBox.Size = new System.Drawing.Size(850, 430);
            this.SQLtextBox.TabIndex = 0;
            // 
            // DDLTab
            // 
            this.DDLTab.Controls.Add(this.DDLtextBox);
            this.DDLTab.Location = new System.Drawing.Point(4, 22);
            this.DDLTab.Name = "DDLTab";
            this.DDLTab.Size = new System.Drawing.Size(856, 436);
            this.DDLTab.TabIndex = 2;
            this.DDLTab.Text = "DDL";
            this.DDLTab.UseVisualStyleBackColor = true;
            // 
            // DDLtextBox
            // 
            this.DDLtextBox.BackColor = System.Drawing.SystemColors.GrayText;
            this.DDLtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DDLtextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DDLtextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.DDLtextBox.Location = new System.Drawing.Point(0, 0);
            this.DDLtextBox.Multiline = true;
            this.DDLtextBox.Name = "DDLtextBox";
            this.DDLtextBox.ReadOnly = true;
            this.DDLtextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DDLtextBox.Size = new System.Drawing.Size(856, 436);
            this.DDLtextBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 794);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "SGBD";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.SQLTab.ResumeLayout(false);
            this.SQLTab.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.DDLTab.ResumeLayout(false);
            this.DDLTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button DDLbutton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SQLTab;
        private System.Windows.Forms.TabPage DesignTab;
        private System.Windows.Forms.Label Connectionlabel;
        private System.Windows.Forms.Button Selectbutton;
        private System.Windows.Forms.Label Userlabel;
        private System.Windows.Forms.Button NewConnectionbutton;
        private System.Windows.Forms.Button Disconnectbutton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label Messagelabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button Updatebutton;
        private System.Windows.Forms.Button Createbutton;
        private System.Windows.Forms.Button Alterbutton;
        private System.Windows.Forms.Button Dropbutton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button Playbutton;
        private System.Windows.Forms.TextBox SQLtextBox;
        private System.Windows.Forms.TabPage DDLTab;
        private System.Windows.Forms.TextBox DDLtextBox;
    }
}

