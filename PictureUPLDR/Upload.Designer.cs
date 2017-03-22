namespace PictureUPLDR
{
    partial class Upload
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
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbPictures = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnremoveAll = new System.Windows.Forms.Button();
            this.tbUploadTo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.Enabled = false;
            this.btnUpload.Location = new System.Drawing.Point(449, 195);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(117, 41);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Upload all";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(464, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(98, 34);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lbPictures
            // 
            this.lbPictures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPictures.FormattingEnabled = true;
            this.lbPictures.ItemHeight = 16;
            this.lbPictures.Location = new System.Drawing.Point(12, 12);
            this.lbPictures.Name = "lbPictures";
            this.lbPictures.Size = new System.Drawing.Size(431, 244);
            this.lbPictures.Sorted = true;
            this.lbPictures.TabIndex = 4;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(464, 66);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(98, 32);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnremoveAll
            // 
            this.btnremoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnremoveAll.Location = new System.Drawing.Point(464, 121);
            this.btnremoveAll.Name = "btnremoveAll";
            this.btnremoveAll.Size = new System.Drawing.Size(98, 34);
            this.btnremoveAll.TabIndex = 8;
            this.btnremoveAll.Text = "Remove all";
            this.btnremoveAll.UseVisualStyleBackColor = true;
            this.btnremoveAll.Click += new System.EventHandler(this.btnremoveAll_Click);
            // 
            // tbUploadTo
            // 
            this.tbUploadTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUploadTo.Location = new System.Drawing.Point(95, 268);
            this.tbUploadTo.Name = "tbUploadTo";
            this.tbUploadTo.Size = new System.Drawing.Size(348, 22);
            this.tbUploadTo.TabIndex = 9;
            this.tbUploadTo.TextChanged += new System.EventHandler(this.tbUploadTo_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Upload to:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(449, 264);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(87, 23);
            this.btnBrowse.TabIndex = 11;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 306);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbUploadTo);
            this.Controls.Add(this.btnremoveAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbPictures);
            this.Name = "Upload";
            this.Text = "Upload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lbPictures;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnremoveAll;
        private System.Windows.Forms.TextBox tbUploadTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
    }
}