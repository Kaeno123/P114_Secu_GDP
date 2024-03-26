using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PSecu_114_GDP_EyerKaeno
{
    internal class Password
    {
        //Déclaration de variable
        private string _password;
        private string _username;
        private string _url;
        private string _nameSite;

        public Password(string Sitename, string URL, string login, string password)
        {
            _username = login;
            _url = URL;
            _password = password;
            _nameSite = Sitename;
        }

        public string password { get { return _password; } }
        public string username { get { return _username; } }
        public string url { get { return _url; } }
        public string nameSite { get { return _nameSite; } }
        
        /// <summary>
        /// Propose de modifier chaque propriété de la classe Password
        /// </summary>
        public void ModifyPWD()
        {
            string userchoice = "";

            Console.WriteLine($"Voici le nom du site actuelle : {_nameSite}. Souhaitez-vous le changer ? o/n");
            userchoice = Console.ReadLine();
            if (userchoice == "o")
            {
                Console.Write("\nÉcrivez le nouveau nom du site : ");
                userchoice = Console.ReadLine();
                _nameSite = userchoice;
            }

            Console.WriteLine($"Voici l'URL actuelle du site : {_url}. Souhaitez-vous le changer ? o/n");
            userchoice = Console.ReadLine();
            if (userchoice == "o")
            {
                Console.Write("\nÉcrivez le nouvel url du site : ");
                userchoice = Console.ReadLine();
                _url = userchoice;
            }

            Console.WriteLine($"Voici votre login actuel : {_username}. Souhaitez-vous le changer ? o/n");
            userchoice = Console.ReadLine();
            if (userchoice == "o")
            {
                Console.Write("\nÉcrivez votre nouveau nom d'utilisateur : ");
                userchoice = Console.ReadLine();
                _username = userchoice;
            }

            Console.WriteLine($"Voici votre mot de passe actuel : {_password}. Souhaitez-vous le changer ? o/n");
            userchoice = Console.ReadLine();
            if (userchoice == "o")
            {
                Console.Write("\nÉcrivez votre nouveau mot de passe : ");
                userchoice = Console.ReadLine();
                _password = userchoice;
            }

            Console.Clear();
            Console.WriteLine("Les modifications ont bien été enregistrées. Appuyez sur Enter pour revenir au menu.");
            Console.ReadLine();
        }

        /// <summary>
        /// Afficher les données liées au mot de passe
        /// </summary>
        public void ShowPassword()
        {
            Console.WriteLine($"\nURL : {url}");
            Console.WriteLine($"Login : {_username}");
            Console.WriteLine($"Mot de passe : {password}");
            Console.WriteLine("Appuyez sur Enter pour masquer le mot de passe et revenir au menu");

            Console.ReadLine();
        }
    }
}