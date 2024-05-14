using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PSecu_114_GDP_EyerKaeno
{
    internal class FileManager
    {
        private string _filePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\password\"; //Chemin de fichier temporaire
        private string _masterFilePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\";
        private const int _CESARSHIFT = 6;
        private string _masterPassowrd;
        private string _key = "KaEnoEyeR";
        public string Key { get { return _key; } }
        
        public int CESARSHIFT { get { return _CESARSHIFT; } }
          
        /// <summary>
        /// Créer un fichier, insère les informations du mot de passe et le chiffre
        /// </summary>
        /// <param name="nameSite"></param>
        /// <param name="url"></param>
        /// <param name="login"></param>
        /// <param name="mdp"></param>
        public void WriteInfoToFile(string nameSite, string url, string login, string mdp)
        {
            _filePath += $"{nameSite}.txt";

            File.WriteAllText(_filePath, string.Empty); //Anticipe le cas où l'user voudrait modifier une information depuis la console.

            //Crypte les informations sensibles
            /*login = CesarCrypting(login, _CESARSHIFT);
            mdp = CesarCrypting(mdp, _CESARSHIFT);*/
            login = VigenereCrypting(login, _key);
            mdp = VigenereDecrypting(mdp, _key);

            //Écrit les informations dans le fichier texte
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(url);
                writer.WriteLine(login);
                writer.WriteLine(mdp);
            }
            _filePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\password\";
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

        /// <summary>
        /// Chiffre un texte avec le chiffrement de Vigenère
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
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
                    cryptedtext += (char)(((letter + splitKey[countLetterKey] - alphabetBeginning) % 26) + alphabetBeginning);
                    countLetterKey++;
                }
                else
                {
                    cryptedtext += letter;
                }
            }
            return cryptedtext;
        }

        /// <summary>
        /// Déchiffre une ligne d'un fichier texte avec le chiffrement César
        /// </summary>
        /// <param name="text"></param>
        /// <param name="difference"></param>
        /// <returns></returns>
        internal string DecryptingFile(string nameSite,/*int difference*/ string key, int line)
        {
            _filePath += $"{nameSite}.txt";
            string lineFile = File.ReadLines(_filePath).ElementAt(line);
            _filePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\password\";

            return VigenereDecrypting(lineFile, key) /*CesarDecrypting(lineFile, difference)*/;
        }

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
                    decryptedtext += (char)((((caractere - alphabetBeginning) - splitKey[countLetterKey] + 26) % 26) + alphabetBeginning);
                    countLetterKey++;
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
        /// <param name="nameSite"></param>
        public void DeleteFile(string nameSite)
        {
            _filePath += $"{nameSite}.txt";

            //Supprime le fichier
            File.Delete(_filePath);

            _filePath = @"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\password\";
        }

        /// <summary>
        /// Lors du lancement du programme, la méthode s'exécute pour vérifier si le répertoire contient des MDP
        /// </summary>
        /// <returns></returns>
        public List<Password> GetEachFilesOfDir()
        {
           string[] files = Directory.GetFiles(_filePath);
           List<Password> passwordMemo= new List<Password>();

            if (files.Length > 0)
            {
                foreach (string MDP in files)
                {
                    string nameSite = MDP.Replace(@"D:\Module 114 - Sécurité\Projet\PSecu_114_GDP_EyerKaeno\password\", "").Replace(".txt", "");
                    string[] lines = File.ReadAllLines(MDP);
                    string[] infoFile = new string[3];
                    int count = 0;
                    foreach (string line in lines)
                    {
                        infoFile[count] = line;
                        count++;
                    }
                    Password passTemp = new Password(nameSite, infoFile[0], infoFile[1], infoFile[2]) ;
                    passwordMemo.Add(passTemp);
                }
            }
            return passwordMemo;
        }

        /// <summary>
        /// chiffre le master password avec le password lui-même en clé d'enchiffrement
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string MasterPasswordCrypting(string password)
        {
            string passwordCrypted = "";
            foreach (char letter in password)
            {
                char shift = char.IsUpper(letter) ? 'A' : 'a';
                passwordCrypted += (char)(((letter + shift) % 26) + shift);
            }
            return passwordCrypted;
        }

        /// <summary>
        /// Vérifie si l'user possède un master password, si oui, ça demande le mdp. Si non, demande de création.
        /// </summary>
        public bool VerifiyMasterPassword()
        {
            _masterFilePath += "config.txt";
            
            bool fileFound = File.Exists(_masterFilePath);

            if (fileFound is true)//Si le fichier existe, demande d'entrer le MDP
            {
                int numberTry = 0;
                string DecryptedPassword = CesarDecrypting(File.ReadAllText(_masterFilePath), _CESARSHIFT);
                do
                {
                    Console.Write("Entrez votre mot de passe : ");
                    string mdpTry = Console.ReadLine();

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
            else if (fileFound is false)//Si le fichier n'existe pas, demande un MDP pour le créer
            {
                Console.Write("Veuillez créer votre mot de passe maître : ");
                string masterPassword = Console.ReadLine();
                _masterPassowrd = masterPassword;
                string masterPass = MasterPasswordCrypting(masterPassword);

                using (StreamWriter writer = new StreamWriter(_masterFilePath))
                {
                    writer.Write(masterPass);
                }
            }
            return fileFound;
        }
    }
}