using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecu_114_GDP_EyerKaeno
{
    internal class Password
    {
        private string _password;
        private string _username;
        private string _url;
        private string _nameSite;
        public Password(string Sitename, string URL, string login, string password)
        {
            _username = password;
            _url = URL;
            _password = login;
            _nameSite = Sitename;
        }

        public string password { get { return _password; } }

        public string username { get { return _username; } }
        public string url { get { return _url; } }
        public string nameSite { get { return _nameSite; } }
        
        public void ShowPassword()
        {
            Console.WriteLine($"\nURL : {url}");
            Console.WriteLine($"Login : {_username}");
            Console.WriteLine($"Mot de passe : {password}");
            Console.WriteLine("Appuyez sur Enter pour masquer le mot de passe et revenir au menu");

            Console.ReadLine();
        }

        public void DeletePassword(Password[] passwordsTable, int positionPassword)
        {
            passwordsTable[positionPassword].;

        }

 /*       // Suppose you have removed an element at index 2
        int removedIndex = 2;

// Shift elements to the left starting from the position after the removed element
for (int i = removedIndex; i<nbPassword - 1; i++)
{
    maxPassword[i] = maxPassword[i + 1]; // Move the next element to the current position
}

    // Set the last element to null (or empty string) to clear the duplicate element at the end
    maxPassword[nbPassword - 1] = null; // or set to empty strings depending on your logic

// Decrement the count of passwords stored
nbPassword--;
*/
    }
}
