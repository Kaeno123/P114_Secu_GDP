using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecu_114_GDP_EyerKaeno
{
    internal class Program
    {
        static Password[] maxPassword = new Password[20];
        static int nbPassword = 0;
        static void Main(string[] args)
        {
            do
            {
                Menu();
            } while (true);
        }

        /// <summary>
        /// Menu principal
        /// </summary>
        public static void Menu()
        {
            Console.Clear();
            string userChoice = "";
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Séléctionnez une action :");
            Console.WriteLine("1. Consulter un mot de passe\n2. Ajouter un mot de passe\n3. Supprimer une mot de passe\n4. Quitter le programme");
            Console.WriteLine("***********************************************************\n");
            Console.Write("Faites votre choix : ");

            userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    ShowPassword();
                    break;
                case "2":
                    AddPassword();
                    break;
                case "3":
                    DeletePassword();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        
        /// <summary>
        /// Montrer les mots de passe
        /// </summary>
        public static void ShowPassword()
        {
            string userChoice = "";
            Console.Clear();
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Consultez un mot de passe :");
            Console.WriteLine("1. Retour au menu principal");
            for (int i = 0; i < nbPassword; i++)
            {
                Console.WriteLine($"{i +2}. {maxPassword[i].nameSite}");
            }
            Console.WriteLine("***********************************************************\n");
            Console.Write("Faites votre choix : ");

            userChoice= Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Menu();
                    break;
                default:
                    for (int i = 2; i < nbPassword +2; i++)
                    {
                        if (userChoice == i.ToString())
                        {
                            maxPassword[i-2].ShowPassword();
                        }
                    }
                    break;
            }
        }
        /// <summary>
        /// Ajouter un mot de passe
        /// </summary>
        public static void AddPassword()
        {
            //Déclaration des variables
            
            string nameSite = "";
            string url = "";
            string login = "";
            string password = "";

            if (nbPassword == maxPassword.Length)
            {
                Console.WriteLine("Vous avez atteint le seuil maximal de 20 mot de passe de stockage, Désolé :/\nAppuyez sur une touche pour revenir au menu :");
                Console.ReadLine();
                Menu();
            }

            Console.Write("Entrez le nom du site dont vous souhaitez stockez le mot de passe : ");
            nameSite = Console.ReadLine();
            Console.Write("Entrez l'URL du site : ");
            url = Console.ReadLine();
            Console.Write("Entrez le login : ");
            login = Console.ReadLine();
            Console.Write("Entrez le mot de passe : ");
            password = Console.ReadLine();

            if (nbPassword < maxPassword.Length)
            {
                maxPassword[nbPassword] = new Password(nameSite, url, login, password);
                nbPassword++;
            }
            Console.WriteLine("\nVotre mot de passe a bien été enregistré.\nAppuyez sur une Enter pour revenir au menu");
            Console.ReadLine();
        }

        public static void DeletePassword()
        {
            string userChoice = "";
            Console.Clear();
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Supprimez un mot de passe :");
            Console.WriteLine("1. Retour au menu principal");
            for (int i = 0; i < nbPassword; i++)
            {
                Console.WriteLine($"{i + 2}. {maxPassword[i].nameSite}");
            }
            Console.WriteLine("***********************************************************\n");
            Console.Write("Faites votre choix : ");

            userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Menu();
                    break;
                default:
                    for (int i = 2; i < nbPassword + 2; i++)
                    {
                        if (userChoice == i.ToString())
                        {
                            string userchoice2 = "";
                            Console.WriteLine($"Êtes-vous certain de vouloir supprimer {maxPassword[i-2].nameSite} ? o/n");
                            userchoice2 = Console.ReadLine();
                            if (userchoice2 == "o")
                            {
                                int placementTable = i-2;
                                maxPassword[i - 2].DeletePassword(maxPassword,placementTable);
                            }
                            else
                            {
                                Menu();
                            }
                        }
                    }
                    break;
            }
        }
    }
}
