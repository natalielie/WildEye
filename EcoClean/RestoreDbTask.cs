using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace EcoClean
{
    public class RestoreDBTask
    {
        #region Properties

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

        private string _backupFileFolder;
        public string BackupFileFolder
        {
            get { return _backupFileFolder; }
            set { _backupFileFolder = value; }
        }


        private string _backupFile;
        public string BackupFile
        {
            get { return _backupFile; }
            set { _backupFile = value; }
        }
        #endregion Properties
        public bool Execute()
        {
            ServerConnection conn = new ServerConnection();
            conn.LoginSecure = false;
            conn.DatabaseName = "Ecoprise_db";
            conn.ServerInstance = this.SqlServerInstanceName;
            conn.Login = this.SQLServerUser;
            conn.Password = this.SQLServerPassword;
            Server svr = new Server(conn);
            Database db = svr.Databases["Ecoprise_db"];
            string dbbackupfile = string.Empty;
            if (this.BackupFile == string.Empty)
                dbbackupfile = this.BackupFileFolder + @"\" + this.SqlServerDBName + ".bak";
            else
                dbbackupfile = this.BackupFile;
            try
            {
                // Restore Database
                Restore restore = new Restore();
                restore.Database = this.SqlServerDBName;
                restore.RestrictedUser = true;
                restore.Action = RestoreActionType.Database;
                restore.ReplaceDatabase = true;
                restore.Devices.AddDevice(dbbackupfile, DeviceType.File);
                svr.KillAllProcesses(this.SqlServerDBName);
                restore.Wait();
                restore.SqlRestore(svr);
                return true;
            }
            catch (Exception ex)
            {
                return false;
               // "Database restore failed", ex.InnerException.Message)
            }

        }
    }
}
