using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kl
{
    public class authorInfo
    {
        public int id;
        public string name;
    }
    public class journalInfo
    {
        public int idJournal;
        public string nameJournal;
        public string namePublishing;
        public string onVip;
        public int yearPrint;
    }
    public class bookInfo
    {
        public int idBook;
        public string nameBook;
        public string nameAuthor;
        public string nameGenre;
        public string namePublishing;
        public string onVip;
        public int yearPrint;
    }
    public class userInfo
    {
        public int idUser;
        public string loginUser;
        public string nameRole;
    }
    public class controlActionList
    {
        public int idTable;
        public string W;
        public string R;
        public string D;
        public string E;
    }
    public class controlUserList
    {
        public int idRole;
        public string auth;
        public string gen;
        public string pub;
        public string user;
        public string book;
        public string contAct;
        public string contUser;
        public string jou;
    }
}
