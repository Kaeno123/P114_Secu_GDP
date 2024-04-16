using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PSecu_114_GDP_EyerKaeno
{
    internal class FileManager
    {
        private string _FILEPATH = @"D:\Module 114\Projet\PSecu_114_GDP_EyerKaeno\password\"; //Chemin de fichier temporaire
        private const int _CESARSHIFT = 6;
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
            _FILEPATH += $"{nameSite}.txt";

            File.WriteAllText(_FILEPATH, string.Empty); //Anticipe le cas où l'user voudrait modifier une information depuis la console.

            //Crypte les informations sensibles
            login = CesarCrypting(login, _CESARSHIFT);
            mdp = CesarCrypting(mdp, _CESARSHIFT);

            //Écrit les informations dans le fichier texte
            using (StreamWriter writer = new StreamWriter(_FILEPATH))
            {
                writer.WriteLine(url);
                writer.WriteLine(login);
                writer.WriteLine(mdp);
            }
            _FILEPATH = @"D:\Module 114\Projet\PSecu_114_GDP_EyerKaeno\password\";
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
                    char  alphabetBeginning = char.IsUpper(letter) ? 'A' : 'a';
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
        /// Déchiffre une ligne d'un fichier texte avec le chiffrement César
        /// </summary>
        /// <param name="text"></param>
        /// <param name="difference"></param>
        /// <returns></returns>
        internal string CesarDecryptingFile(string nameSite, int difference, int line)
        {
            _FILEPATH += $"{nameSite}.txt";
            string lineFile = File.ReadLines(_FILEPATH).ElementAt(line);
            _FILEPATH = @"D:\Module 114\Projet\PSecu_114_GDP_EyerKaeno\password\";

            return CesarDecrypting(lineFile, 26 - difference);
        }

        public string CesarDecrypting(string linefile, int difference)
        {
            string decryptedtext = "";

            foreach (char caractere in linefile)
            {
                if (char.IsLetter(caractere))
                {
                    char beginningalphabet = char.IsUpper(caractere) ? 'A' : 'a';
                    decryptedtext += (char)((((caractere - difference) - beginningalphabet + 26) % 26) + beginningalphabet);
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
            _FILEPATH += $"{nameSite}.txt";

            //Supprime le fichier
            File.Delete(_FILEPATH);

            _FILEPATH = @"D:\Module 114\Projet\PSecu_114_GDP_EyerKaeno\password\";
        }
    }
}