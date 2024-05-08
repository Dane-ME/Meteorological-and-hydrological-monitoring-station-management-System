using BsonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    class Time
    {
        public string getCurrentTime() 
        {
            DateTime currentTime = DateTime.Now;
            return currentTime.ToString();
        }
        public string calEndTime()
        {
            DateTime oldTime = DateTime.Parse(getCurrentTime());
            DateTime endTime = oldTime.AddDays(2);
            return endTime.ToString();
        }
    }
    class File : Time
    {

        static File instance;   
        static public File Instance {
            get { 
                if (instance == null)
                {
                    instance = new File();
                }
                return instance;
            }
        }

        private bool Check(string filename, string ParentPath)
        {
            string path = ParentPath + $"/{filename}";
            if (Directory.Exists(path)) { return true; } else return false;
        }
        public void CreateNewDatafolder(string filename, string ParentPath)
        {
            if (!Check(filename, ParentPath))
            {
                string subDirectory = ParentPath + $"/{filename}";
                Directory.CreateDirectory(subDirectory);
            }
            DB.Start(ParentPath + $"/{filename}");
        }

        public void SaveToken(Document content )
        {
            var idFile = content.ObjectId;
            if (DB.Token.Find(idFile) == null) 
            { 
                DB.Token.Insert(content);
            }
            else { }
        }

        public Document FindToken( string id) { return DB.Token.Find(id) ; }
        public void DeleteToken(string id) { DB.Token.Delete(id); }
        public object ?getToken()
        {
            DocumentList listToken = DB.Token.SelectAll();
            if(listToken.Count != 0) 
            { 
                Document Token = listToken.Last();
                return Token; 
            }
            return null; 
        }
    }

}

