using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PictureUPLDR
{
    public partial class Upload : Form
    {
        public Upload()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Title = "Pick pictures for upload";
                ofd.SupportMultiDottedExtensions = true;
                ofd.Filter = "JPG Pictures (*.jpg, *.jpeg)|*.jpg;*.jpeg";

                var dr = ofd.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    var files = ofd.FileNames;
                    foreach (var file in files)
                    {
                        if (!lbPictures.Items.Contains(file))
                            lbPictures.Items.Add(file);
                    }
                }
            }

            SetBtnUpload();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbPictures.SelectedIndex < 0)
                return;

            lbPictures.Items.Remove(lbPictures.SelectedItem);

            SetBtnUpload();
        }

        private void SetBtnUpload()
        {
            if (lbPictures.Items.Count > 0 && !string.IsNullOrEmpty(tbUploadTo.Text))
            {
                btnUpload.Enabled = true;
            }
            else
            {
                btnUpload.Enabled = false;
            }
        }

        private void btnremoveAll_Click(object sender, EventArgs e)
        {
            lbPictures.Items.Clear();

            SetBtnUpload();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tbUploadTo.Text))
            {
                MessageBox.Show("Destination directory does not exists!");
                return;
            }

            MessageBox.Show("Please, be patient!");

            var upl = new Uploader(lbPictures.Items.OfType<string>().ToArray(), tbUploadTo.Text);
            int numOfSuccess = upl.Upload();

            MessageBox.Show("Upload finished! " + numOfSuccess + " of " + lbPictures.Items.Count + " uploaded");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                var dr = fbd.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    tbUploadTo.Text = fbd.SelectedPath;
                }
            }
        }

        private void tbUploadTo_TextChanged(object sender, EventArgs e)
        {
            SetBtnUpload();
        }
    }
}