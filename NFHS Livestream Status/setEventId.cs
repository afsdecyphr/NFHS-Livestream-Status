using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace NFHS_Livestream_Status
{
    public partial class setEventId : Form
    {
        public setEventId()
        {
            InitializeComponent();
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                WebRequest req = WebRequest.Create("http://cfunity.nfhsnetwork.com/v1/events/" + eventTb.Text);

                WebResponse res = req.GetResponse();

                MainForm frm = new MainForm(eventTb.Text, "events", this);
                frm.Show();
            }
            catch (WebException ex)
            {
                try
                {
                    this.Hide();
                    WebRequest req = WebRequest.Create("http://cfunity.nfhsnetwork.com/v1/games/" + eventTb.Text);

                    WebResponse res = req.GetResponse();

                    MainForm frm = new MainForm(eventTb.Text, "games", this);
                    frm.Show();
                }
                catch
                {
                    this.Show();
                    invalidEvntLbl.Text = "Invalid event ID : " + eventTb.Text;
                    invalidEvntLbl.Visible = true;
                    invalidEvntLbl.ForeColor = Color.Red;
                    Height = 100;
                }
            }
        }

        private void setEventId_Load(object sender, EventArgs e)
        {
        }
    }
}