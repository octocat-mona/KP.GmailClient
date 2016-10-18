using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KP.GmailApi.Models;
using KP.GmailApi.WinFormsSample.Properties;
using Label = KP.GmailApi.Models.Label;
using Message = KP.GmailApi.Models.Message;

namespace KP.GmailApi.WinFormsSample
{
    public partial class MainForm : Form
    {
        private readonly GmailServiceHelper _gmailServiceHelper;

        public MainForm()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            string privateKey = ConfigurationManager.AppSettings["PrivateKey"];
            string tokenUri = ConfigurationManager.AppSettings["TokenUri"];
            string clientEmail = ConfigurationManager.AppSettings["ClientEmail"];
            string emailAddress = ConfigurationManager.AppSettings["EmailAddress"];
            ServiceAccountCredential accountCredential = new ServiceAccountCredential
            {
                PrivateKey = privateKey,
                TokenUri = tokenUri,
                ClientEmail = clientEmail
            };
            _gmailServiceHelper = new GmailServiceHelper(accountCredential, emailAddress, GmailScopes.Modify);
            AppendToLog("Starting");
        }

        private void OnBtnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                _gmailServiceHelper.RequestUserToLogin();
                AppendToLog("User logged in successfully");
            }
            catch (Exception ex)
            {
                AppendToLog(ex);
            }
            finally
            {
                UpdateStatus();

                // Return from the browser to the form
                Activate();
            }
        }

        private async void OnBtnLabelsClick(object sender, EventArgs e)
        {
            try
            {
                AppendToLog("Getting labels");
                IList<Label> labels = await _gmailServiceHelper.GetLabelsAsync();
                var labelNames = labels
                    .Select(label => $"ID: {label.Id}{Environment.NewLine}Name: {label.Name}{Environment.NewLine}")
                    .ToList();

                txtData.Text = string.Join(Environment.NewLine, labelNames);
                AppendToLog("Got ", labels.Count, " labels");
            }
            catch (Exception ex)
            {
                AppendToLog(ex);
            }
        }

        private async void OnBtnInboxMailClick(object sender, EventArgs e)
        {
            try
            {
                AppendToLog("Getting emails");
                IList<Message> messages = await _gmailServiceHelper.GetMailAsync();

                StringBuilder builder = new StringBuilder();
                foreach (var message in messages)
                {
                    string msg = $"From: {message.From}{Environment.NewLine}To: {message.To}{Environment.NewLine}Preview: {message.Snippet}{Environment.NewLine}";
                    builder.AppendLine(msg);
                }

                txtData.Text = builder.ToString();
                AppendToLog("Got ", messages.Count, " mails");
            }
            catch (Exception ex)
            {
                AppendToLog(ex);
            }
        }

        private void AppendToLog(Exception ex)
        {
            txtLog.AppendText(string.Concat(DateTime.Now.ToLongTimeString(), " ERROR: ", ex.Message, Environment.NewLine));
        }

        private void AppendToLog(params object[] message)
        {
            txtLog.AppendText(string.Concat(DateTime.Now.ToLongTimeString(), " ", string.Concat(message), Environment.NewLine));
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            UpdateStatus();
            AppendToLog("Started");
        }

        private void UpdateStatus()
        {
            bool isUserLoggedIn = _gmailServiceHelper.IsUserLoggedIn();

            picBoxStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            picBoxStatus.Image = isUserLoggedIn ? Resources.CheckIcon : Resources.WarningIcon;
            btnInboxMail.Enabled = isUserLoggedIn;
            btnLabels.Enabled = isUserLoggedIn;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string msg = $"Unhandled Exception: {((Exception)e.ExceptionObject).Message}";
            MessageBox.Show(msg, "Error");
        }

        private void OnBtnResetClick(object sender, EventArgs e)
        {
            _gmailServiceHelper.Logout();
            UpdateStatus();

            txtLog.Clear();
            txtData.Clear();
        }
    }
}
