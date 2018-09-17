using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace NFHS_Livestream_Status
{
    public partial class searchSchoolByName : Form
    {
        JObject parsedObject;
        listEventsBySchool school = null;
        public searchSchoolByName(listEventsBySchool schoolForm)
        {
            InitializeComponent();
            school = schoolForm;
        }

        private void searchSchoolByName_Load(object sender, EventArgs e)
        {

        }

        private void returnBtn_Click(object sender, EventArgs e)
        {
            if (school == null)
            {
                school = new listEventsBySchool(null, null, this);
            }
            this.Hide();
            school.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (school == null)
            {
                school = new listEventsBySchool(null, null, this);
            }

            school.setSchoolId(parsedObject["items"][listBox1.SelectedIndex]["key"].ToString());
            this.Hide();
            school.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (textBox1.Text == "")
            {

            }
            else
            {
                WebClient WebC = new WebClient();
                var RawJson = WebC.DownloadString("https://search-api.nfhsnetwork.com/search/schools?searchTerm=" + textBox1.Text);
                parsedObject = JObject.Parse(RawJson);

                int count = 0;
                foreach (var item in parsedObject["items"])
                {
                    count++;
                    string builder = "";
                    builder += item["name"];
                    listBox1.Items.Add(builder);
                }
            }
        }

        private void searchSchoolByName_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
