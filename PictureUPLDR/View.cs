using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PictureUPLDR
{
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();
        }

        private void View_Load(object sender, EventArgs e)
        {
            data.SelectionChanged -= btnPreview_Click;
            PopulateData();
            data.SelectionChanged += btnPreview_Click;
        }

        private void PopulateData()
        {
            try
            {
                using (var conn = new MySqlConnection(DBConfig.DB_CONN))
                {
                    string query = "SELECT * FROM PhotoData;";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        data.DataSource = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in communication with DB. Check log for details.");
                File.AppendAllText("log.log", ex.ToString());
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (data.SelectedCells.Count <= 0)
            {
                return;
            }

            var path = (string)data.SelectedCells[1].Value;
            picture.ImageLocation = path;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (data.SelectedCells.Count <= 0)
            {
                return;
            }

            try
            {
                int id = (int)data.SelectedCells[0].Value;
                var path = (string)data.SelectedCells[1].Value;
                using (var conn = new MySqlConnection(DBConfig.DB_CONN))
                {
                    string query = "DELETE FROM PhotoData WHERE ID = " + id + ";";

                    conn.Open();
                    using (var comm = new MySqlCommand(query, conn))
                    {
                        comm.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                File.Delete(path);

                PopulateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in communication with DB. Check log for details.");
                File.AppendAllText("log.log", ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopulateData();
        }
    }
}