using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace TesteBancoMySQL.SGBD
{
    class SGBDFileAcess
    {
        private bool initialized = false;
        public string HostName {
            get;
            set;
        }
        public string DatabaseName
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }

        public SGBDFileAcess()
        {
            HostName = "";
            DatabaseName = "";
            UserName = "";
            Password = "";
            initialized = true;
        }

        public SGBDFileAcess(string hostName, string databaseName, string userName, string password)
        {
            HostName = hostName;
            DatabaseName = databaseName;
            UserName = userName;
            Password = password;
            initialized = true;         
        }

        public void saveChanges()
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "conf.sgbdaccess"))){
                sw.WriteLine(HostName);
                sw.WriteLine(DatabaseName);
                sw.WriteLine(UserName);
                sw.WriteLine(Password);
            }
        }
        public bool Configured()
        {
            return (HostName == "" || DatabaseName == "" || UserName == "") == false;
        }
        public static SGBDFileAcess loadChanges()
        {
            if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "conf.sgbdaccess")) == false)
                return new SGBDFileAcess();
            using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "conf.sgbdaccess")))
            {
                string hostName =      sr.ReadLine();
                string databaseName =  sr.ReadLine();
                string userName =      sr.ReadLine();
                string password =      sr.ReadLine();
                return new SGBDFileAcess(hostName, databaseName, userName, password);
            }
        }
        public static void DeleteChanges()
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "conf.sgbdaccess")))
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "conf.sgbdaccess"));
        }

        public override string ToString()
        {
            return $"server={HostName};database={DatabaseName};uid={UserName};pwd={Password};";
        }
    }    
}
