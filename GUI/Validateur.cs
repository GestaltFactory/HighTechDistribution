using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HiTechDistribution.GUI {
    public static class Validateur {
        
        /// <summary>
        /// textbox vide
        /// </summary>
        const string err = "Entrée invalide";
        public static bool EstPresent(TextBox textBox){
            if(textBox.Text == ""){
                MessageBox.Show(textBox.Tag.ToString() + " est un champ obligatoire.", err, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// est un char
        /// </summary>
        /// <param name="str"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool EstValidString(string str, string info){
            bool estValid = true;
            for (int i = 0; i < str.Length; i++) {
                if(!Char.IsLetter(str,i)){
                    MessageBox.Show("Le " + info + " N'est pas valide.");
                    return false;
                }
            }
            return estValid;
        }

        /// <summary>
        /// valide taille et chiffre
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="taille"></param>
        /// <returns></returns>
        public static bool NumExpressionValid(string temp, int taille){
            if(!Regex.IsMatch(temp, @"^\d{" + taille + "}$")){
                MessageBox.Show("Le numéro de l'employé doit contenir " + taille + "chiffre.", err, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //valide courriel
        public static bool CourrielExpressionValid(string temp) {
            if (!Regex.IsMatch(temp, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase)){
                MessageBox.Show("Le format de votre email est invalide.", err, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        
        /// <summary>
        /// valid login
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static bool LoginExpressionValid(string temp) {
            if (!Regex.IsMatch(temp, @"^[a-zA-Z][a-zA-Z0-9]{3,9}$")) {
                MessageBox.Show("Votre Login est invalide. Il doit commencer par une lettre, il peut contenir des lettres et des chiffres et doit avoir entre 4 et 9 caractère.", err, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// valide password
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static bool PasswordExpressionValid(string temp) {
            if (!Regex.IsMatch(temp, @"^(?=^.{8,15}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$")) {
                MessageBox.Show("Votre password est invalide. Il doit contenir au moins 1 chiffre, une minuscule, une majuscule et avoir entre 8 et 15 caractères.", err, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// valide la taille du numero de tel
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TelNumberTaille(int value){
            if(value != 14){
                MessageBox.Show("Le numéro de téléphone doit contenir 10 chiffres.", err, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
 
       
/*

        static public bool IsDuplicate(string copie) {
            bool duplicate = false;
            foreach (Employe emp in listEmp) {
                if (textBoxNoEmploye.Text == emp.NoEmploye.ToString()) {
                    duplicate = true;
                    break;
                }
            }
            return duplicate;
        }

*/


        /// <summary>
        /// valide touts les champs sauf login et password dans la partie création du profil
        /// </summary>
        /// <param name="textBoxEmp"></param>
        /// <param name="taille"></param>
        /// <param name="textBoxPrenom"></param>
        /// <param name="prenom"></param>
        /// <param name="textBoxNom"></param>
        /// <param name="nom"></param>
        /// <param name="value"></param>
        /// <param name="textBoxCourriel"></param>
        /// <returns></returns>
        public static bool EstValidEmploye(TextBox textBoxEmp, int taille, TextBox textBoxPrenom, string prenom, TextBox textBoxNom, string nom, int value, TextBox textBoxCourriel) {
            bool valid = EstPresent(textBoxEmp) && NumExpressionValid(textBoxEmp.Text, taille) && EstPresent(textBoxPrenom) && EstValidString(prenom, "prenom") && EstPresent(textBoxNom)
                         && EstValidString(nom, "nom") && TelNumberTaille(value) && EstPresent(textBoxCourriel) && CourrielExpressionValid(textBoxCourriel.Text);

            return valid;
        }

        /// <summary>
        /// valide le login et le password
        /// </summary>
        /// <param name="textBoxLogin"></param>
        /// <param name="textBoxPassword"></param>
        /// <returns></returns>
        public static bool EstValidUser(TextBox textBoxLogin, TextBox textBoxPassword){
            bool valid = EstPresent(textBoxLogin) && LoginExpressionValid(textBoxLogin.Text) && EstPresent(textBoxPassword) && PasswordExpressionValid(textBoxPassword.Text);    

            return valid;
        }

        /// <summary>
        /// valide avant de quitter
        /// </summary>
        /// <returns></returns>
        public static bool Quit(){
            DialogResult rep = MessageBox.Show("Desirez-vous quitter l'application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rep == DialogResult.Yes){
                Application.Exit();
                return true;
            }
            return false;
        }
    }
}