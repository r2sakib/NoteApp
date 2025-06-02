namespace NoteApp
{
    partial class NotesDashboard
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
            this.PageHeading = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searcchBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.tag = new System.Windows.Forms.ComboBox();
            this.createNoteBtn = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // PageHeading
            // 
            this.PageHeading.AutoSize = true;
            this.PageHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PageHeading.Location = new System.Drawing.Point(666, 31);
            this.PageHeading.Name = "PageHeading";
            this.PageHeading.Size = new System.Drawing.Size(108, 38);
            this.PageHeading.TabIndex = 0;
            this.PageHeading.Text = "Notes";
            this.PageHeading.Click += new System.EventHandler(this.PageHeading_Click);
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.Location = new System.Drawing.Point(36, 38);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(345, 28);
            this.searchBox.TabIndex = 1;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // searcchBtn
            // 
            this.searcchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searcchBtn.Location = new System.Drawing.Point(387, 39);
            this.searcchBtn.Name = "searcchBtn";
            this.searcchBtn.Size = new System.Drawing.Size(95, 28);
            this.searcchBtn.TabIndex = 2;
            this.searcchBtn.Text = "Search";
            this.searcchBtn.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(907, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "Edit Profile";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // logoutBtn
            // 
            this.logoutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutBtn.ForeColor = System.Drawing.Color.OrangeRed;
            this.logoutBtn.Location = new System.Drawing.Point(1202, 31);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(96, 38);
            this.logoutBtn.TabIndex = 4;
            this.logoutBtn.Text = "Logout";
            this.logoutBtn.UseVisualStyleBackColor = true;
            // 
            // tag
            // 
            this.tag.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tag.FormattingEnabled = true;
            this.tag.Location = new System.Drawing.Point(502, 36);
            this.tag.Name = "tag";
            this.tag.Size = new System.Drawing.Size(101, 30);
            this.tag.TabIndex = 6;
            this.tag.Text = "Tags";
            // 
            // createNoteBtn
            // 
            this.createNoteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createNoteBtn.Location = new System.Drawing.Point(1057, 35);
            this.createNoteBtn.Name = "createNoteBtn";
            this.createNoteBtn.Size = new System.Drawing.Size(120, 34);
            this.createNoteBtn.TabIndex = 7;
            this.createNoteBtn.Text = "Create Note";
            this.createNoteBtn.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(36, 94);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(1262, 474);
            this.dataGridView.TabIndex = 8;
            // 
            // NotesPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 706);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.createNoteBtn);
            this.Controls.Add(this.tag);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.searcchBtn);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.PageHeading);
            this.Name = "NotesPage";
            this.Text = "Notes";
            this.Load += new System.EventHandler(this.NotesPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PageHeading;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searcchBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button logoutBtn;
        private System.Windows.Forms.ComboBox tag;
        private System.Windows.Forms.Button createNoteBtn;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}