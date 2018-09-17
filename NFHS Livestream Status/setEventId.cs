using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace NFHS_Livestream_Status
{
    public partial class setEventId : Form
    {
        public MainForm mainF;
        public listEventsBySchool school;
        public setEventId(MainForm main = null, listEventsBySchool schooll = null)
        {
            mainF = main;
            school = schooll;
            InitializeComponent();
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {
            if (eventTb.Text == "")
            {
                MessageBox.Show("Invalid event ID!");
            } else
            {
                try
                {
                    WebRequest req = WebRequest.Create("https://cfunity.nfhsnetwork.com/v2/game_or_event/" + eventTb.Text);

                    WebResponse res = req.GetResponse();

                    if (mainF != null)
                    {
                        mainF.Show();
                        mainF.setEventID(eventTb.Text.ToString());
                    }
                    else
                    {
                        MainForm frm = new MainForm(eventTb.Text, "events", null, this);
                        frm.Show();
                    }
                    this.Hide();
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Invalid event ID!");
                }
            }
            
        }

        private void setEventId_Load(object sender, EventArgs e)
        {
        }

        private void setEventId_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (school != null)
            {
                this.Hide();
                school.Show();
            }
            else
            {
                school = new listEventsBySchool(null, this);
                this.Hide();
                school.Show();
            }
        }
    }
}