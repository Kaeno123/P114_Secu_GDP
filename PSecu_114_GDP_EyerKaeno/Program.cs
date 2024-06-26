﻿///ETML
///Auteur : Kaeno Eyer
///Date : 19.04.2024
///Description : Programme pincipal, gestion de l'interface
///
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSecu_114_GDP_EyerKaeno
{
    internal class Program
    {
        static List<Password> maxPassword = new List<Password>();
        static FileManager fileManager = new FileManager();
        

        static void Main(string[] args)
        {
            bool verifyMaster = fileManager.VerifiyMasterPassword();
            if (verifyMaster is true)
            {
                maxPassword = fileManager.GetEachFilesOfDir();
            }            
            Menu();
        }

        /// <summary>
        /// Menu principal
        /// </summary>
        public static void Menu()
        {
            string userChoice = "";
            do
            {            
                Console.Clear();

                                                Console.WriteLine(@"
                                 __             ___                                    _ 
                                / _\ ___  ___  / _ \__ _ ___ _____      _____  _ __ __| |
                                \ \ / _ \/ __|/ /_)/ _` / __/ __\ \ /\ / / _ \| '__/ _` |
                                _\ \  __/ (__/ ___/ (_| \__ \__ \\ V  V / (_) | | | (_| |
                                \__/\___|\___\/    \__,_|___/___/ \_/\_/ \___/|_|  \__,_|
                                                         
                                               ");

                Console.WriteLine("Séléctionnez une action :");
                Console.WriteLine("1. Consulter un mot de passe\n2. Ajouter un mot de passe\n3. Modifier un mot de passe\n4. Supprimer un mot de passe\n5. Quitter le programme\n");
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
                        ModifyPassword();
                        break;
                    case "4":
                        DeletePassword();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            } while (userChoice != "5");
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
            for (int i = 0; i < maxPassword.Count(); i++)
            {
                Console.WriteLine($"{i +2}. {maxPassword[i].NameSite}");
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
                    for (int i = 2; i < maxPassword.Count() + 2; i++)
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

            Console.Clear();
            if (maxPassword.Count() == 20)
            {
                Console.WriteLine("Vous avez atteint le seuil maximal de 20 mot de passe de stockage, Désolé :/\nAppuyez sur une touche pour revenir au menu :");
                Console.ReadLine();
                Menu();
            }

            //Commencement du processus
            Console.Write("Entrez le nom du site dont vous souhaitez stockez le mot de passe : ");
            nameSite = Console.ReadLine();
            Console.Write("Entrez l'URL du site : ");
            url = Console.ReadLine();
            Console.Write("Entrez le login : ");
            login = Console.ReadLine();
            Console.Write("Entrez le mot de passe : ");
            password = Console.ReadLine();

            if (maxPassword.Count() < 20)
            {
                maxPassword.Add(new Password(nameSite, url, login, password));
            }
            fileManager.WriteInfoToFile(nameSite, url, login, password);
            Console.WriteLine("\nVotre mot de passe a bien été enregistré.\nAppuyez sur une Enter pour revenir au menu");
            Console.ReadLine();
        }

        /// <summary>
        /// Propose de modifier des propriétés de la classe Password
        /// </summary>
        public static void ModifyPassword()
        {
            string userChoice = "";
            Console.Clear();
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Modifiez un mot de passe :");
            Console.WriteLine("1. Retour au menu principal");
            for (int i = 0; i < maxPassword.Count(); i++)
            {
                Console.WriteLine($"{i + 2}. {maxPassword[i].NameSite}");
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
                    for (int i = 2; i < maxPassword.Count() + 2; i++)
                    {
                        if (userChoice == i.ToString())
                        {
                            maxPassword[i - 2].ModifyPWD();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Supprimer un mot de passe
        /// </summary>
        public static void DeletePassword()
        {
            string userChoice = "";

            //Âffiche le sous-menu
            Console.Clear();
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Supprimez un mot de passe :");
            Console.WriteLine("1. Retour au menu principal");
            for (int i = 0; i < maxPassword.Count(); i++)
            {
                Console.WriteLine($"{i + 2}. {maxPassword[i].NameSite}");
            }
            Console.WriteLine("***********************************************************\n");
            Console.Write("Faites votre choix : ");

            userChoice = Console.ReadLine();

            //Vérifie le choix de l'user
            switch (userChoice)
            {
                case "1":
                    Menu();
                    break;
                default:
                    for (int i = 2; i < maxPassword.Count() + 2; i++)
                    {
                        if (userChoice == i.ToString())
                        {
                            string userchoice2 = "";
                            Console.WriteLine($"Êtes-vous certain de vouloir supprimer {maxPassword[i-2].NameSite} ? o/n");
                            userchoice2 = Console.ReadLine();
                            if (userchoice2 == "o")
                            {
                                fileManager.DeleteFile(maxPassword[i-2].NameSite);
                                maxPassword.RemoveAt(i - 2);
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