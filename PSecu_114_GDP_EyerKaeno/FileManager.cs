///ETML
///Auteur : Kaeno Eyer
///Date : 19.04.2024
///Description : Classe contenant la gestion de fichier et le chiffrement
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace PSecu_114_GDP_EyerKaeno
{
    internal class FileManager
    {
        private string _filePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\passwords\"; //Chemin de fichier temporaire
        private string _masterFilePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\";
        private const int _CESARSHIFT = 6;
        private string _masterPassowrd;
        private string _key = "KaEnoEyeR";
        public string Key { get { return _key; } }
        
        public int CESARSHIFT { get { return _CESARSHIFT; } }
          
        /// <summary>
        /// Créer un fichier, insère les informations du mot de passe et le chiffre
        /// </summary>
        /// <param name="nameSite">Nom du site</param>
        /// <param name="url">URL</param>
        /// <param name="login">Login</param>
        /// <param name="mdp">Mot de passe</param>
        public void WriteInfoToFile(string nameSite, string url, string login, string mdp)
        {
            _filePath += $"{nameSite}.txt";

            File.WriteAllText(_filePath, string.Empty); //Anticipe le cas où l'user voudrait modifier une information depuis la console.

            //Crypte les informations sensibles
            login = VigenereCrypting(login, _key);
            mdp = VigenereCrypting(mdp, _key);

            //Écrit les informations dans le fichier texte
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(url);
                writer.WriteLine(login);
                writer.WriteLine(mdp);
            }
            _filePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\passwords\";
        }

        /// <summary>
        /// Chiffre un texte avec le chiffrement de Vigenère
        /// </summary>
        /// <param name="text">texte à chiffrer</param>
        /// <param name="key">Clé de chiffrement</param>
        /// <returns>Retourne le texte chiffré</returns>
        internal string VigenereCrypting(string text, string key)
        {
            string cryptedtext = "";
            char[] splitKey = key.ToCharArray();
            int countLetterKey = 0;
            foreach (char letter in text)
            {
                if (char.IsLetter(letter))
                {
                    char alphabetBeginning = char.IsUpper(letter) ? 'A' : 'a';
                    int keyOffset = char.IsUpper(splitKey[countLetterKey]) ? splitKey[countLetterKey] - 'A' : splitKey[countLetterKey] - 'a';
                    cryptedtext += (char)(((letter - alphabetBeginning + keyOffset) % 26) + alphabetBeginning);
                    countLetterKey++;
                    if (countLetterKey == _key.Length)
                    {
                        countLetterKey = 0;
                    }
                }
                else
                {
                    cryptedtext += letter;
                }
            }
            return cryptedtext;
        }

        /// <summary>
        /// Cherche un fichier et appelle une méthode déchiffrant un texte
        /// </summary>
        /// <param name="nameSite">Nom du site (Nom du fichier texte)</param>
        /// <param name="key">Clé de déchiffrement</param>
        /// <param name="line">Ligne où se trouve le texte</param>
        /// <returns>Retourne le texte</returns>
        internal string DecryptingFile(string nameSite, string key, int line)
        {
            _filePath += $"{nameSite}.txt";
            string lineFile = File.ReadLines(_filePath).ElementAt(line);
            _filePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\passwords\";

            return VigenereDecrypting(lineFile, key);
        }

        /// <summary>
        /// Déchiffre un texte chiffré en Vigenère
        /// </summary>
        /// <param name="linefile">ligne du fichier correspond au texte voulu</param>
        /// <param name="key">Clé de déchiffrement</param>
        /// <returns>Retourne le texte déchiffré</returns>
        internal string VigenereDecrypting(string linefile, string key)
        {
            string decryptedtext = "";
            char[] splitKey = key.ToCharArray();
            int countLetterKey = 0;

            foreach (char caractere in linefile)
            {
                if (char.IsLetter(caractere))
                {
                    char alphabetBeginning = char.IsUpper(caractere) ? 'A' : 'a';
                    int keyOffset = char.IsUpper(splitKey[countLetterKey]) ? splitKey[countLetterKey] - 'A' : splitKey[countLetterKey] - 'a';
                    decryptedtext += (char)(((caractere - alphabetBeginning - keyOffset + 26) % 26) + alphabetBeginning);
                    countLetterKey++;
                    if (countLetterKey == _key.Length)
                    {
                        countLetterKey = 0;
                    }
                }
                else
                {
                    decryptedtext += caractere;
                }
            }
            return decryptedtext;
        }

        /// <summary>
        /// Supprime le fichier
        /// </summary>
        /// <param name="nameSite">Nom du site (correspond au nom du fichier texte)</param>
        public void DeleteFile(string nameSite)
        {
            _filePath += $"{nameSite}.txt";

            //Supprime le fichier
            File.Delete(_filePath);

            _filePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\passwords\";
        }

        /// <summary>
        /// Lors du lancement du programme, la méthode s'exécute pour vérifier si le répertoire contient des MDP
        /// </summary>
        /// <returns>Retourne la liste de mot de passe</returns>
        public List<Password> GetEachFilesOfDir()
        {
           string[] files = Directory.GetFiles(_filePath);
           List<Password> passwordMemo= new List<Password>();

            if (files.Length > 0)
            {
                foreach (string MDP in files)
                {
                    string nameSite = MDP.Replace(@"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\passwords\", "").Replace(".txt", "");
                    string[] lines = File.ReadAllLines(MDP);
                    string[] infoFile = new string[3];
                    int count = 0;
                    foreach (string line in lines)
                    {
                        infoFile[count] = line;
                        count++;
                    }
                    infoFile[1] = VigenereDecrypting(infoFile[1], Key);
                    infoFile[2] = VigenereDecrypting(infoFile[2], Key);
                    Password passTemp = new Password(nameSite, infoFile[0], infoFile[1], infoFile[2]) ;
                    passwordMemo.Add(passTemp);
                }
            }
            return passwordMemo;
        }

        /// <summary>
        /// Chiffre le master Passwords avec le Passwords lui-même en clé d'enchiffrement
        /// </summary>
        /// <param name="password">Mot de passe maître</param>
        /// <returns>Retourne le texte chiffré</returns>
        public string MasterPasswordCrypting(string password)
        {
            StringBuilder encryptedPassword = new StringBuilder();
            foreach (char letter in password)
            {
                char shift = char.IsUpper(letter) ? 'A' : 'a';
                encryptedPassword.Append((char)((((letter - shift) * 2 ) % 26) + shift));
            }
            return encryptedPassword.ToString();
        }

        /// <summary>
        /// Vérifie si l'user possède un master Passwords, si oui, ça demande le mdp. Si non, demande de création.
        /// </summary>
        public bool VerifiyMasterPassword()
        {
            _masterFilePath += "config.txt";
            
            bool fileFound = File.Exists(_masterFilePath);

            if (fileFound is true)//Si le fichier existe, demande d'entrer le MDP
            {
                int numberTry = 0;
                string DecryptedPassword = File.ReadAllText(_masterFilePath);
                do
                {
                    Console.Write("Entrez votre mot de passe : ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    string mdpTry = MasterPasswordCrypting(Console.ReadLine());
                    Console.ResetColor();
                    if (mdpTry == DecryptedPassword)//Si le mdp entré est correct, l'user accède au menu.
                    {
                        _masterPassowrd = DecryptedPassword;
                        numberTry = 3;
                        Console.WriteLine("yes");
                    }
                    else if(mdpTry != DecryptedPassword) //3 essais, si échoué, fermetture de la console.
                    {
                        numberTry++;

                        if (numberTry == 3)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine($"Il vous reste {3 - numberTry} essai(s).\n");
                        }
                    }
                } while (numberTry != 3);
            }            
            else if (fileFound is false)//Si le fichier n'existe pas, demande un MDP et le créer
            {
                bool validPassword = false;
                do
                {
                    Console.Write("Veuillez créer votre mot de passe maître : ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    string masterPassword = Console.ReadLine();
                    Console.ResetColor();
                    Console.Clear();
                    Console.Write("Veuillez le confirmer : ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    string masterPassowrdConfirm = Console.ReadLine();
                    Console.ResetColor();
                    if (masterPassword == masterPassowrdConfirm)
                    {
                        validPassword = true;
                        _masterPassowrd = masterPassword;
                        string masterPass = MasterPasswordCrypting(masterPassword);

                        using (StreamWriter writer = new StreamWriter(_masterFilePath))
                        {
                            writer.Write(masterPass);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Vous n'avez pas entré le même mot de passe !! Veuillez recommencer\n");
                        Console.ResetColor();
                    }
                } while (validPassword is false);
            }
            return fileFound;
        }



        /********************************************************************************************************************/
        ///Méthodes pas utilisées

        /// <summary>
        /// Déchiffre le chiffrement de César
        /// </summary>
        /// <param name="linefile"></param>
        /// <param name="difference"></param>
        /// <returns></returns>
        public string CesarDecrypting(string linefile, int difference)
        {
            string decryptedtext = "";

            foreach (char caractere in linefile)
            {
                if (char.IsLetter(caractere))
                {
                    char alphabetBeginning = char.IsUpper(caractere) ? 'A' : 'a';
                    decryptedtext += (char)((((caractere - alphabetBeginning) - difference + 26) % 26) + alphabetBeginning);
                }
                else
                {
                    decryptedtext += caractere;
                }
            }
            return decryptedtext;
        }


        /// <summary>
        /// Chiffre un texte avec le chiffrement César
        /// </summary>
        /// <param name="text"></param>
        /// <param name="difference"></param>
        /// <returns></returns>
        internal string CesarCrypting(string text, int difference)
        {
            string cryptedtext = "";

            foreach (char letter in text)
            {
                if (char.IsLetter(letter))
                {
                    char alphabetBeginning = char.IsUpper(letter) ? 'A' : 'a';
                    cryptedtext += (char)((((letter + difference) - alphabetBeginning) % 26) + alphabetBeginning);
                }
                else
                {
                    cryptedtext += letter;
                }
            }
            return cryptedtext;
        }
    }
}