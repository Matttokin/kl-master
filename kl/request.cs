using DataReaderToJsonExample.JsonSerializer;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace kl
{
    static class request
    {
        private static string host = "http://localhost:44339/";

        public static string Host { get => host; set => host = value; }

        static public int getIdUser(string loginUser)
        {
            // Адрес ресурса, к которому выполняется запрос
            string url = host+"api/findUser?loginUser="+ loginUser;

            // Создаём объект WebClient
            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
                //XmlSerializer ser = new XmlSerializer(typeof(int));
                //Console.WriteLine(response);
                return Convert.ToInt32(response);
            }
        }
        static public string addUser(string name, string role)
        {
            string url = host + "api/addUser?name=" + name + "&role=" + role;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(url);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string addAuthor(string name)
        {
            string url = host + "api/addAuthor?name=" + name;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"',' ').Trim();
            }
        }
        static public string addJournal(string name, string publish, int eyarprint, char i)
        {
            string url = host + "api/addJournal?name=" + name + "&publish=" + publish+ "&eyarprint=" + eyarprint + "&i="+ i;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string addGenre(string name)
        {
            string url = host + "api/addGenre?name=" + name;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string addPublishing(string name)
        {
            string url = host + "api/addPublishing?name=" + name;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string addBook(string name, string author, string genre, string publish, int eyarprint, char i)
        {
            string url = host + "api/addBook?name=" + name + "&author="+ author+ "&genre="+ genre + "&publish=" + publish + "&eyarprint=" + eyarprint + "&i=" + i;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string updRulsRoless(int id, string aths, string gnrs, string pbls, string usrs, string blss, string cntls, string scsls, string jrls)
        {
            string url = host + "api/updRulsRoless?id=" + id + "&aths=" + aths + "&gnrs=" + gnrs + "&pbls=" + pbls + "&usrs=" + usrs
                + "&blss=" + blss + "&cntls=" + cntls + "&scsls=" + blss + "&jrls=" + jrls;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string updRulsRoles(int id, int idtables, string write, string read, string delete, string edit)
        {
            string url = host + "api/updRulsRoles?id=" + id + "&idtables=" + idtables + "&write=" + write + "&read=" + read + "&delete=" + delete
                + "&edit=" + edit;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public int[] getAuthorCoalesce(int ids)
        {
            string url = host + "api/getAuthorCoalesce?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length-1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public List<authorInfo> getAuthorList()
        {
            string url = host + "api/getAuthorList";
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<authorInfo>>(response);
            }
        }
        static public string updateAuthor(string nameAuthor, string idAuthor)
        {
            string url = host + "api/updateAuthor?nameAuthor=" + nameAuthor + "&idAuthor=" + idAuthor;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string delAuthor(string idAuthor)
        {
            string url = host + "api/delAuthor?idAuthor=" + idAuthor;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public List<authorInfo> getGenreList()
        {
            string url = host + "api/getGenreList";
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<authorInfo>>(response);
            }
        }
        static public string updateGenre(string nameGenre, string idGenre)
        {
            string url = host + "api/updateGenre?nameGenre=" + nameGenre + "&idGenre=" + idGenre;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public int[] getJournalCoalesce(int ids)
        {
            string url = host + "api/getJournalCoalesce?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public List<journalInfo> getJournalList(int idUser)
        {
            string url = host + "api/getJournalList?idUser=" + idUser;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<journalInfo>>(response);
            }
        }
        static public List<authorInfo> getPublishingList()
        {
            string url = host + "api/getPublishingList";
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<authorInfo>>(response);
            }
        }
        static public string updateJuornal(string nameJournal, string idPublishing, string onVIP, string yearPrint, string idJournal)
        {
            string url = host + "api/updateJuornal?nameJournal=" + nameJournal + "&idPublishing=" + idPublishing
                 + "&onVIP=" + onVIP + "&yearPrint=" + yearPrint + "&idJournal=" + idJournal;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string delJournal(string idJournal)
        {
            string url = host + "api/delJournal?idJournal=" + idJournal;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public int[] getPublishingCoalesce(int ids)
        {
            string url = host + "api/getPublishingCoalesce?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public string updatePublishing(string namePublishing, string idPublishing)
        {
            string url = host + "api/updatePublishing?namePublishing=" + namePublishing + "&idPublishing=" + idPublishing;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string delPublishing(string idPublishing)
        {
            string url = host + "api/delPublishing?idPublishing=" + idPublishing;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public int[] getBookCoalesce(int ids)
        {
            string url = host + "api/getBookCoalesce?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public List<bookInfo> getBookList(int idUser)
        {
            string url = host + "api/getBookList?idUser=" + idUser;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<bookInfo>>(response);
            }
        }
        static public string updateBook(string nameBook, string idAuthor, string idGenre, string idPublishing,  string onVIP, string yearPrint, string idBook)
        {
            string url = host + "api/updateBook?nameBook=" + nameBook + "&idAuthor=" + idAuthor
                + "&idGenre=" + idGenre + "&idPublishing=" + idPublishing + "&onVIP=" + onVIP
                + "&yearPrint=" + yearPrint + "&idBook=" + idBook;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string delBook(string idBook)
        {
            string url = host + "api/delBook?idBook=" + idBook;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public int[] getUser1Coalesce(int ids)
        {
            string url = host + "api/getUser1Coalesce?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public int[] getUser2Coalesce(int ids)
        {
            string url = host + "api/getUser2Coalesce?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public List<userInfo> getUserList()
        {
            string url = host + "api/getUserList";
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                return JsonConvert.DeserializeObject<List<userInfo>>(response);
            }
        }
        static public string updateUser(string loginUser, string idRole, string idUser)
        {
            string url = host + "api/updateUser?loginUser=" + loginUser + "&idRole=" + idRole + "&idUser=" + idUser;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public string delUser(string idUser)
        {
            string url = host + "api/delUser?idUser=" + idUser;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);
                return response.Replace('"', ' ').Trim();
            }
        }
        static public List<authorInfo> getroleUserList()
        {
            string url = host + "api/getroleUserList";
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<authorInfo>>(response);
            }
        }
        static public int[] getRuleCoalesce(int ids)
        {
            string url = host + "api/getRuleCoalesce?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public List<authorInfo> getRoleList(string id)
        {
            string url = host + "api/getRoleList?id="+id;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<authorInfo>>(response);
            }
        }
        static public List<controlActionList> getRoleControlList(string idRole)
        {
            string url = host + "api/getRoleControlList?idRole="+idRole;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<controlActionList>>(response);
            }
        }
        static public List<authorInfo> getTableList()
        {
            string url = host + "api/getTableList";
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<authorInfo>>(response);
            }
        }
        static public int[] getAccessCoalesce(int ids)
        {
            string url = host + "api/getAccessCoalesce?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public List<controlUserList> getAccessControlList(string idRole)
        {
            string url = host + "api/getAccessControlList?idRole=" + idRole;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<List<controlUserList>>(response);
            }
        }
        static public int getIdRole(int ids)
        {
            string url = host + "api/getIdRole?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                return Convert.ToInt32(response);
            }
        }
        static public int[] getDanineCoalesce(int idRole)
        {
            string url = host + "api/getDanineCoalesce?idRole=" + idRole;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public int[] getGenreCoalesce(int ids)
        {
            string url = host + "api/getGenreCoalesce?ids=" + ids;
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                return JsonConvert.DeserializeObject<int[]>(response);
            }
        }
        static public string testConnect()
        {
            string url = host + "api/testConnect";
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
                response = response.Replace("\\", "");
                return response;
            }
        }

    }
    
}
