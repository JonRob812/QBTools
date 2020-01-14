// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainView.cs" company="CNC Software, Inc.">
//   Copyright (c) 2019 CNC Software, Inc.
// </copyright>
// <summary>
//  If this project is helpful please take a short survey at ->
//  http://ux.mastercam.com/Surveys/APISDKSupport 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace QBtools
{
    using System.Windows.Forms;
    using System;
    using Mastercam.Support;
    using Mastercam.Database;
    using System.Net;
    using System.Text;
    using System.Linq;
    using System.IO;
    




    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
            SetupId.Select();
        }

        private void cmdOK_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void MainView_Load(object sender, System.EventArgs e)
        {

        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            //needed for ssl ewrror message
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //set and read file with Quickbase authentication data
            string ParaFilePath = @"C:\Program Files\Mastercam 2020\Mastercam\chooks\QBtools\MCAMtoQuickbasePRM.txt";            
            string[] QbConnectFile = System.IO.File.ReadAllLines(ParaFilePath);
            string QBauth = QbConnectFile[0];
            string QBtoken = QbConnectFile[1];
            string QBurl = QbConnectFile[2];
            string csvtools = Main.Globals.csvtools.Replace("setupid", $"{SetupId.Text}"); // add in setup id from form to csv 

            

            // make xml POST messages
            string xmlPOSTcsv= $@"<qdbapi>
               <ticket>{QBauth}</ticket>
               <apptoken>{QBtoken}</apptoken>
               <records_csv>
                  <![CDATA[ {csvtools}]]>
               </records_csv>
               <clist>6.7.12.15.9</clist>
            </qdbapi>";

            string xmlPOSTprg= $@"<qdbapi>
               <ticket>{QBauth}</ticket>
               <apptoken>{QBtoken}</apptoken>
               <query>{{9.TV.{SetupId.Text}}}</query>
            </qdbapi>";

            //responsebox.Text = xmlPOSTprg + xmlPOSTcsv; //for teesting xml format

            string requestpurge = Request(QBurl, "API_PurgeRecords", xmlPOSTprg); // purge request
            string requestcsv = Request(QBurl, "API_ImportfromCSV", xmlPOSTcsv); // import csv request
            responsebox.Text = "Request Purge:\n" + xmlPOSTprg + "\nRequest Import CSV:\n" + xmlPOSTcsv + 
                "\nPurge Response:\n" + requestpurge + "\nImport CSV Response:\n" + requestcsv; // show responses    
            

            string Request(string url, string action, string xml) // request method
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(xml);
                request.ContentType = "application/xml";
                request.ContentLength = byteArray.Length;
                request.Headers["QUICKBASE-ACTION"] = $"{action}";// set action
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void SetupId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Add)
            {
                AddToolsButton.PerformClick();
            }
        }


    }
}
