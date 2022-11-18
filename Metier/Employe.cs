using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistribution.Metier {
    public class Employe {
        private uint noEmploye;
        public uint NoEmploye{
            get { return noEmploye; }
            set { noEmploye = value; }
        }
        private string prenom;
        public string Prenom{
            get { return prenom; }
            set { prenom = value; }
        }
        private string nom;
        public string Nom{
            get { return nom; }
            set { nom = value; }
        }
        private string telephone;
        public string Telephone{
            get { return telephone; }
            set { telephone = value; }
        }
        private string courriel;
        public string Courriel{
            get { return courriel; }
            set { courriel = value; }
        }
        private string fonction;
        public string Fonction{
            get { return fonction; }
            set { fonction = value; }
        }

        private string login;
        public string Login{
            get { return login; }
            set { login = value; }
        }

        private string password;
        public string Password{
            get { return password; }
            set { password = value; }
        }
    }
}