using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EcoClean
{
    public class BackupDbTask
    {
        private string _sqlserverUser;
        public string SQLServerUser
        {
            get { return _sqlserverUser; }
            set { _sqlserverUser = value; }
        }
        private string _sqlserverPassword;
        public string SQLServerPassword
        {
            get { return _sqlserverPassword; }
            set { _sqlserverPassword = value; }
        }
        private string _sqlserverInstanceName;
        public string SqlServerInstanceName
        {
            get { return _sqlserverInstanceName; }
            set { _sqlserverInstanceName = value; }
        }
        private string _sqlserverDBName;
        public string SqlServerDBName
        {
            get { return _sqlserverDBName; }
            set { _sqlserverDBName = value; }
        }

        private string _databaseBackupFolder;

        public string DatabaseBackupFolder
        {
            get { return _databaseBackupFolder; }
            set { _databaseBackupFolder = value; }
        }

        private string _backupFile;

        public string BackupFile
        {
            get { return _backupFile; }
            set { _backupFile = value; }
        }
        //#endregion Properties

        public void Execute()
        {

            // Backup Db
            //try
            //{
            //    ServerConnection conn = new ServerConnection();
            //    conn.LoginSecure = false;
            //    conn.DatabaseName = this.SqlServerDBName;
            //    conn.ServerInstance = this.SqlServerInstanceName;
            //    conn.Login = this.SQLServerUser;
            //    conn.Password = this.SQLServerPassword;
            //    Server svr = new Server(conn);
            //    Database BuildDB = svr.Databases[this.SqlServerDBName];

            //    string dbbackupfile = this.DatabaseBackupFolder + @"\" + this.SqlServerDBName + ".bak"; ;


            //    Backup backup = new Backup();
            //    backup.Database = this.SqlServerDBName;
            //    backup.MediaName = "FileSystem";
            //    BackupDeviceItem bkpDeviceItem = new BackupDeviceItem();
            //    bkpDeviceItem.DeviceType = DeviceType.File;
            //    bkpDeviceItem.Name = dbbackupfile;
            //    backup.Devices.Add(bkpDeviceItem);
            //    backup.Initialize = true;
            //    backup.SqlBackup(svr);

            //    this.BackupFile = dbbackupfile;
            //}
            //catch (Exception ex)
            //{


            //}

            SqlConnection sqlconn = new SqlConnection("Server=tcp:ecoprisedbserver.database.windows.net,1433;Initial Catalog=Ecoprise_db;Persist Security Info=False;User ID=natmalysch;Password=Bigban_123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand sqlcmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            // Backup destibation
            string backupDestination = "C:\\SQLBackUpFolder";
            // check if backup folder exist, otherwise create it.
            if (!System.IO.Directory.Exists(backupDestination))
            {
                System.IO.Directory.CreateDirectory("C:\\SQLBackUpFolder");
            }
            try
            {
                sqlconn.Open();
                sqlcmd = new SqlCommand("backup database TutorialsPanel to disk='" + backupDestination + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".bak'", sqlconn);
                sqlcmd.ExecuteNonQuery();
                //Close connection
                sqlconn.Close();
                Console.Write("Backup database successfully");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }


    }
}
