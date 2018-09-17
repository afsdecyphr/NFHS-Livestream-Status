using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace NFHS_Livestream_Status
{
    public partial class MainForm : Form
    {
        private string eventId = null;
        private string lastStatus = null;
        private listEventsBySchool origForm;
        private setEventId origForm2;

        private System.Timers.Timer RefreshTmr = new System.Timers.Timer();

        private JsonPar _Json;

        public MainForm(string id, string type, listEventsBySchool orig = null, setEventId orig2 = null)
        {
            origForm = orig;
            origForm2 = orig2;
            InitializeComponent();
            eventId = id;

            _Json = new JsonPar(eventId, type);
        }

        public void setEventID(string id)
        {
            _Json.eventId = id;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "Live Stream Information For : " + eventId;

            RefreshTmr.Interval = 1000;
            RefreshTmr.Elapsed += new System.Timers.ElapsedEventHandler(ExecuteRefesh);
            RefreshTmr.Start();
            RefreshTmr.Enabled = true;

            ExecuteRefesh(null, null);

            progBar.Maximum = 20;

            _Json.Load();
            lastStatus = _Json.Status;
        }

        int increment = 20;
        int secOn = 20;
        private void ExecuteRefesh(object sender, System.Timers.ElapsedEventArgs arg)
        {
            this.timeUntlRfrLbl.Text = "Time until next refresh: " + (increment - secOn) + " (s)";

            Invoke(new Action(() => { this.progBar.Value = secOn; }));
            int addedHeight = descriptionLbl.Height - 13;
            Debug.Print(addedHeight.ToString());
            Invoke(new Action(() => { this.Height = 216 + addedHeight; }));
            if (secOn == increment)
            {
                _Json.Load();

                if (_Json.Status != lastStatus)
                {
                    if (notifyCB.Checked)
                    {
                        notifyIcon1.BalloonTipText = "The status for the live stream [" + eventId + "] has changed from " + lastStatus + " to " + _Json.Status;
                        notifyIcon1.ShowBalloonTip(10000);
                        lastStatus = _Json.Status;
                    }
                }

                switch (_Json.Status)
                {
                    case "off_air":
                        statusLbl.ForeColor = Color.Red;
                        break;
                    case "on_air":
                        statusLbl.ForeColor = Color.LimeGreen;
                        break;
                    default:
                        statusLbl.ForeColor = Color.Gold;
                        break;
                }

                switch (_Json.HD)
                {
                    case true:
                        hdLbl.ForeColor = Color.LimeGreen;
                        break;
                    case false:
                        hdLbl.ForeColor = Color.Red;
                        break;
                    default:
                        hdLbl.ForeColor = Color.Gold;
                        break;
                }

                statusLbl.Text = _Json.Status;
                headlineLbl.Text = _Json.Headline;
                subHeadlineLbl.Text = _Json.SubHeadline;
                descriptionLbl.Text = _Json.Description;
                hdLbl.Text = _Json.HD.ToString();
                secOn = 0;
            }
            else
            {
                secOn++;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void refreshTglBtn_Click(object sender, EventArgs e)
        {
            RefreshTmr.Enabled = !RefreshTmr.Enabled;
            if (RefreshTmr.Enabled)
            {
                refreshTglBtn.ForeColor = Color.LimeGreen;
                refreshTglBtn.Image = Properties.Resources.green_button;
            }
            else
            {
                refreshTglBtn.ForeColor = Color.Red;
                refreshTglBtn.Image = Properties.Resources.red_button;
            }
        }

        private void toolStripAboutBtn_Click(object sender, EventArgs e)
        {
            AboutBox frm = new AboutBox();
            frm.Show();
        }
        private void viewLsBtn_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = "ffplay.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = "-i " + _Json.URL;

            Process.Start(startInfo);
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshTmr.Enabled = false;
            RefreshTmr.Stop();

            Application.Exit();
        }

        private void tm5sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tm3mToolStripMenuItem.Checked = false;
            tm1mToolStripMenuItem.Checked = false;
            tm30sToolStripMenuItem.Checked = false;
            tm5sToolStripMenuItem.Checked = true;
            increment = 5;
            secOn = 5;
            progBar.Maximum = 5;
        }

        private void tm30sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tm3mToolStripMenuItem.Checked = false;
            tm1mToolStripMenuItem.Checked = false;
            tm30sToolStripMenuItem.Checked = true;
            tm5sToolStripMenuItem.Checked = false;
            increment = 30;
            secOn = 30;
            progBar.Maximum = 30;
        }

        private void tm1mToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tm3mToolStripMenuItem.Checked = false;
            tm1mToolStripMenuItem.Checked = true;
            tm30sToolStripMenuItem.Checked = false;
            tm5sToolStripMenuItem.Checked = false;
            increment = 60;
            secOn = 60;
            progBar.Maximum = 60;
        }

        private void tm3mToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tm3mToolStripMenuItem.Checked = true;
            tm1mToolStripMenuItem.Checked = false;
            tm30sToolStripMenuItem.Checked = false;
            tm5sToolStripMenuItem.Checked = false;
            increment = 180;
            secOn = 180;
            progBar.Maximum = 180;
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alwaysOnTopToolStripMenuItem.Checked =! alwaysOnTopToolStripMenuItem.Checked;
            this.TopMost = !alwaysOnTopToolStripMenuItem.Checked;
        }

        private void notifyOfStatusChangeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            notifyCB.Checked = notifyOfStatusChangeToolStripMenuItem.Checked;
        }

        private void notifyCB_CheckedChanged(object sender, EventArgs e)
        {
            notifyOfStatusChangeToolStripMenuItem.Checked = notifyCB.Checked;
        }

        private void detailDifferentEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (origForm2 == null)
            {
                origForm2 = new setEventId(this);
            }
            origForm2.Show();
        }

        private void searchForEventBySchoolIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (origForm == null)
            {
                origForm = new listEventsBySchool(this);
            }
            origForm.reset();
            origForm.Show();
        }
    }
}