using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using HiTechDistribution.Metier;

namespace HiTechDistribution.IO {
   public static class EmployeIO {
        const string dir = @"";
        const string filePath = dir + "Employe.txt";

        /// <summary>
        /// Méthode qui sauvegarde les infos employe dans le fichier Employe.txt
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="msg"></param>
        public static void SauvegarderEmploye(Employe emp, out string msg){
            StreamWriter sWriter = new StreamWriter(new FileStream(filePath, FileMode.Append, FileAccess.Write));
            sWriter.WriteLine(emp.NoEmploye.ToString() + ", " + emp.Prenom + ", " + emp.Nom + ", " + emp.Telephone + ", " + emp.Courriel + ", " + emp.Fonction + ", " + emp.Login + ", " + emp.Password);
            msg = "L'employe a été sauvegardé.";
            sWriter.Close();
        }

        /// <summary>
        /// Méthode qui returne la Liste d'employes sauvegardes dans le fichier
        /// </summary>
        /// <returns></returns>
        public static List<Employe> GetListEmploye(){
            List<Employe> list = new List<Employe>();
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            while (line != null) {
                Employe emp = new Employe();
                string[] champs = line.Split(',');
                emp.NoEmploye = Convert.ToUInt32(champs[0]);
                emp.Prenom = champs[1];
                emp.Nom = champs[2];
                emp.Telephone = champs[3];
                emp.Courriel = champs[4];
                emp.Fonction = champs[5];
                emp.Login = champs[6];
                emp.Password = champs[7];
                list.Add(emp);
                line = sReader.ReadLine();
            }
            sReader.Close();
            return list;
        }

       /// <summary>
       /// recherche par no. d'employe
       /// </summary>
       /// <param name="noEmp"></param>
       /// <returns></returns>
       public static Employe RechercherEmpParNo(uint noEmp){
           Employe emp = new Employe();
           if(File.Exists(filePath)){
               StreamReader sReader = new StreamReader(filePath);
               string line = sReader.ReadLine();
               while (line != null){
                   string[] champs = line.Split(',');
                   if (Convert.ToUInt32(champs[0]) == noEmp){ 
                       emp.Prenom = champs[1];
                       emp.Nom = champs[2];
                       emp.Telephone = champs[3];
                       emp.Courriel = champs[4];
                       emp.Fonction = champs[5];
                       emp.Login = champs[6];
                       emp.Password = champs[7];
                       sReader.Close();
                       break;
                   }
                   line = sReader.ReadLine();
               }
               sReader.Close();
           }
           return emp;
       }

       /// <summary>
       /// recherche par le prenom de l'employe
       /// </summary>
       /// <param name="prenom"></param>
       /// <returns></returns>
       public static List<Employe> RechercherParPrenom(string prenom) {
           List<Employe> listEmp = new List<Employe>();
           bool employeTrouve = false;
           if (File.Exists(filePath)) {
               StreamReader sReader = new StreamReader(filePath);
               string line = sReader.ReadLine();
               while (line != null) {
                   string[] champs = line.Split(',');
                   if (champs[1].ToUpper().Trim() == prenom.ToUpper().Trim()) {
                       Employe unEmp = new Employe();
                       unEmp.NoEmploye = Convert.ToUInt32(champs[0]);
                       unEmp.Prenom = champs[1];
                       unEmp.Nom = champs[2];
                       unEmp.Telephone = champs[3];
                       unEmp.Courriel = champs[4];
                       unEmp.Fonction = champs[5];
                       unEmp.Login = champs[6];
                       unEmp.Password = champs[7];
                       listEmp.Add(unEmp);
                       employeTrouve = true;
                   }
                   line = sReader.ReadLine();
               }
               sReader.Close();
               if (!employeTrouve) {
                   MessageBox.Show("Le prénom n'existe pas dans la liste.");
               }
           }
           return listEmp;
       }

       /// <summary>
       /// recherche par le nom de l'employe
       /// </summary>
       /// <param name="nom"></param>
       /// <returns></returns>
       public static List<Employe> RechercherParNom(string nom) {
           List<Employe> listEmp = new List<Employe>();
           bool employeTrouve = false;
           if (File.Exists(filePath)) {
               StreamReader sReader = new StreamReader(filePath);
               string line = sReader.ReadLine();
               while (line != null) {
                   string[] champs = line.Split(',');
                   if (champs[2].ToUpper().Trim() == nom.ToUpper().Trim()) {
                       Employe unEmp = new Employe();
                       unEmp.NoEmploye = Convert.ToUInt32(champs[0]);
                       unEmp.Prenom = champs[1];
                       unEmp.Nom = champs[2];
                       unEmp.Telephone = champs[3];
                       unEmp.Courriel = champs[4];
                       unEmp.Fonction = champs[5];
                       unEmp.Login = champs[6];
                       unEmp.Password = champs[7];
                       listEmp.Add(unEmp);
                       employeTrouve = true;
                   }
                   line = sReader.ReadLine();
               }
               sReader.Close();
               if (!employeTrouve) {
                   MessageBox.Show("Le nom n'existe pas dans la liste.");
               }
           }
           return listEmp;
       }

       /// <summary>
       /// recherche par la fonction de l'employe
       /// </summary>
       /// <param name="fonction"></param>
       /// <returns></returns>
       public static List<Employe> RechercherFonction(string fonction) {
           List<Employe> listEmp = new List<Employe>();
           bool employeTrouve = false;
           if (File.Exists(filePath)) {
               StreamReader sReader = new StreamReader(filePath);
               string line = sReader.ReadLine();
               while (line != null) {
                   string[] champs = line.Split(',');
                   if (champs[5] == fonction) {
                       Employe unEmp = new Employe();
                       unEmp.NoEmploye = Convert.ToUInt32(champs[0]);
                       unEmp.Prenom = champs[1];
                       unEmp.Nom = champs[2];
                       unEmp.Telephone = champs[3];
                       unEmp.Courriel = champs[4];
                       unEmp.Fonction = champs[5];
                       unEmp.Login = champs[6];
                       unEmp.Password = champs[7];
                       listEmp.Add(unEmp);
                       employeTrouve = true;
                   }
                   line = sReader.ReadLine();
               }
               sReader.Close();
               if (!employeTrouve) {
                   MessageBox.Show(fonction);
                   MessageBox.Show("Le nom n'existe pas dans la liste.");
               }
           }
           return listEmp;
       }
    }
}