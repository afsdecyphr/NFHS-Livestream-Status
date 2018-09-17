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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NFHS_Livestream_Status
{
    public partial class listEventsBySchool : Form
    {
        public MainForm mainF;
        public setEventId eventWin;
        public listEventsBySchool(MainForm main = null, setEventId ev = null)
        {
            mainF = main;
            eventWin = ev;
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://cfunity.nfhsnetwork.com/v2/game_or_event/" + listBox1.Items[listBox1.SelectedIndex].ToString());

                WebResponse res = req.GetResponse();

                if (mainF != null)
                {
                    mainF.Show();
                    mainF.setEventID(listBox1.Items[listBox1.SelectedIndex].ToString());
                }
                else
                {
                    MainForm frm = new MainForm(listBox1.Items[listBox1.SelectedIndex].ToString(), "events", this);
                    frm.Show();
                }
                this.Hide();
            }
            catch (WebException ex)
            {
            }
        }

        private void searchBtn_Click_1(object sender, EventArgs e)
        {
            if (schoolIDTB.Text == "")
            {
                MessageBox.Show("Invalid school ID!");
            } else
            {
                WebClient WebC = new WebClient();
                var RawJson = WebC.DownloadString("http://search-api.nfhsnetwork.com/search/events/upcoming?school_key=" + schoolIDTB.Text);
                JObject parsedObject = JObject.Parse(RawJson);

                int count = 0;
                foreach (var item in parsedObject["items"])
                {
                    count++;
                    string builder = "";
                    builder += item["key"];
                    listBox1.Items.Add(builder);
                }
                if (count == 0)
                {
                    MessageBox.Show("Invalid school ID!");
                }
            }
        }

        public void reset()
        {
            listBox1.Items.Clear();
            schoolIDTB.Text = "";
        }

        private void listEventsBySchool_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void listEventsBySchool_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (eventWin != null)
            {
                this.Hide();
                eventWin.Show();
            } else
            {
                eventWin = new setEventId(null, this);
                this.Hide();
                eventWin.Show();
            }
        }
    }
}
