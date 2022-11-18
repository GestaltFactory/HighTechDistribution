using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HiTechDistribution.Metier;
using HiTechDistribution.IO;

namespace HiTechDistribution.GUI {
    public partial class FormGestionApp : Form {
        public FormGestionApp() {
            InitializeComponent();
        }

        /// <summary>
        /// Affiche les employes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAfficher_Click(object sender, EventArgs e){
            listViewEmploye.Items.Clear();
            List<Employe> listEmp = EmployeIO.GetListEmploye();
            foreach(Employe emp in listEmp){
                ListViewItem item = new ListViewItem(emp.NoEmploye.ToString());
                item.SubItems.Add(emp.Prenom);
                item.SubItems.Add(emp.Nom);
                item.SubItems.Add(emp.Telephone);
                item.SubItems.Add(emp.Courriel);
                item.SubItems.Add(emp.Fonction);
                item.SubItems.Add(emp.Login);
                item.SubItems.Add(emp.Password);
                listViewEmploye.Items.Add(item);
            }
        }

        /// <summary>
        /// crée le compte et copie les donnees dans le fichier txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGenCompte_Click(object sender, EventArgs e) {         
            int taille = 4;
            Employe emp = new Employe();
            if (Validateur.EstValidEmploye(textBoxNoEmploye, taille, textBoxPrenom, textBoxPrenom.Text, textBoxNom, textBoxNom.Text, maskedTextBoxTelephone.Text.Length, textBoxCourriel)) {   
                emp.NoEmploye = Convert.ToUInt32(textBoxNoEmploye.Text);
                emp.Prenom = textBoxPrenom.Text.Substring(0,1).ToUpper() + textBoxPrenom.Text.Trim().Remove(0,1).ToLower();
                emp.Nom = textBoxNom.Text.Substring(0, 1).ToUpper() + textBoxNom.Text.Trim().Remove(0,1).ToLower();
                emp.Telephone = maskedTextBoxTelephone.Text;
                emp.Courriel = textBoxCourriel.Text;

                if (radioButtonFirst.Checked == true) {
                    emp.Fonction = radioButtonFirst.Text;
                    emp.Login = "N/A";
                    emp.Password = "N/A";

                    //Sauvegarder info
                    string msg = "";
                    EmployeIO.SauvegarderEmploye(emp, out msg);
                    labelInfoCreation.Text = msg;
                }
                else if (radioButtonSecond.Checked == true) {
                    emp.Fonction = radioButtonSecond.Text;
                }
                else if (radioButtonThird.Checked == true) {
                    emp.Fonction = radioButtonThird.Text;
                }
                else {
                    emp.Fonction = radioButtonFourth.Text;                  
                }

                if (radioButtonSecond.Checked || radioButtonThird.Checked || radioButtonFourth.Checked) {
                     if (Validateur.EstValidUser(textBoxCreateLogin, textBoxCreatePassword)) {
                        emp.Login = textBoxCreateLogin.Text;
                        emp.Password = textBoxCreatePassword.Text;

                        //Sauvegarder info
                        string msg = "";
                        EmployeIO.SauvegarderEmploye(emp, out msg);
                        labelInfoCreation.Text = msg;
                    }
                }

            }
        }

        /// <summary>
        /// Event quand le formulaire load pour la premiere fois
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGestionApp_Load(object sender, EventArgs e) {
            labelMgs.Visible = false;
            textBoxInfo.Visible = false;
            radioButtonFirst.Checked = true;
            comboBoxMode.SelectedIndex = 1;
            comboBoxRechercheFonction.Visible = false;          
            buttonModifier.Enabled = false;
            buttonSupprimer.Enabled = false;
            textBoxNoEmployeResult.Enabled = false;
            textBoxPrenomResult.Enabled = false;
            textBoxNomResult.Enabled = false;
            maskedTextBoxTelephoneResult.Enabled = false;
            textBoxCourrielResult.Enabled = false;
            comboBoxFonctionResult.Enabled = false;
            textBoxLoginResult.Enabled = false;
            textBoxPasswordResult.Enabled = false;
        }

        /// <summary>
        /// fonction de l'employe. sélection avec un radio button. Le premier choix n'est pas un utilisateur, donc login est password sont deactivé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonFirst_CheckedChanged(object sender, EventArgs e) {
            if (radioButtonFirst.Checked == true) {
                textBoxCreatePassword.Enabled = false;
                textBoxCreateLogin.Enabled = false;
            }
            else if (radioButtonSecond.Checked == true) {
                textBoxCreatePassword.Enabled = true;
                textBoxCreateLogin.Enabled = true;
            }
            else if (radioButtonThird.Checked == true) {
                textBoxCreatePassword.Enabled = true;
                textBoxCreateLogin.Enabled = true;
            }
            else {
                textBoxCreatePassword.Enabled = true;
                textBoxCreateLogin.Enabled = true;
            }
        }

        /// <summary>
        /// vide les textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e) {
            textBoxNoEmploye.Clear();
            textBoxPrenom.Clear();
            textBoxNom.Clear();
            maskedTextBoxTelephone.Clear();
            textBoxCourriel.Clear();
            radioButtonFirst.Checked = true;
            textBoxCreateLogin.Clear();
            textBoxCreatePassword.Clear();
        }

        private void buttonQuitter_Click(object sender, EventArgs e) {
            Validateur.Quit();
        }

        /// <summary>
        /// sélectionne la categorie de recherche. Pour afficher dans le but de modifier, utiliser no d'employé. Les autres affichent dans le listview et ne permet pas de modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxCategorie_SelectedIndexChanged(object sender, EventArgs e) {
            switch(comboBoxCategorie.SelectedIndex){
                case 0:
                    textBoxInfo.Clear();
                    buttonModifier.Enabled = true;
                    buttonSupprimer.Enabled = true;
                    textBoxPrenomResult.Enabled = true;
                    textBoxNomResult.Enabled = true;
                    maskedTextBoxTelephoneResult.Enabled = true;
                    textBoxCourrielResult.Enabled = true;
                    comboBoxFonctionResult.Enabled = true;
                    textBoxLoginResult.Enabled = true;
                    textBoxPasswordResult.Enabled = true;

                    labelMgs.Visible = true;
                    labelMgs.Text = "Entrez le numero d'employe : ";
                    textBoxInfo.Visible = true;
                    comboBoxRechercheFonction.Visible = false;
                    textBoxInfo.Focus();
                    break;
                case 1:
                    textBoxInfo.Clear();

                    buttonModifier.Enabled = false;
                    buttonSupprimer.Enabled = false;
                    textBoxNoEmployeResult.Enabled = false;
                    textBoxPrenomResult.Enabled = false;
                    textBoxNomResult.Enabled = false;
                    maskedTextBoxTelephoneResult.Enabled = false;
                    textBoxCourrielResult.Enabled = false;
                    comboBoxFonctionResult.Enabled = false;
                    textBoxLoginResult.Enabled = false;
                    textBoxPasswordResult.Enabled = false;

                    labelMgs.Visible = true;
                    labelMgs.Text = "Entrez le prenom de l'employe : ";
                    textBoxInfo.Visible = true;
                    comboBoxRechercheFonction.Visible = false;
                    textBoxInfo.Focus();
                    break;
                case 2:
                    textBoxInfo.Clear();

                    buttonModifier.Enabled = false;
                    buttonSupprimer.Enabled = false;
                    textBoxNoEmployeResult.Enabled = false;
                    textBoxPrenomResult.Enabled = false;
                    textBoxNomResult.Enabled = false;
                    maskedTextBoxTelephoneResult.Enabled = false;
                    textBoxCourrielResult.Enabled = false;
                    comboBoxFonctionResult.Enabled = false;
                    textBoxLoginResult.Enabled = false;
                    textBoxPasswordResult.Enabled = false;

                    labelMgs.Visible = true;
                    labelMgs.Text = "Entrez le nom de l'employe : ";
                    textBoxInfo.Visible = true;
                    comboBoxRechercheFonction.Visible = false;
                    textBoxInfo.Focus();
                    break;
                case 3:
                    textBoxInfo.Clear();

                    buttonModifier.Enabled = false;
                    buttonSupprimer.Enabled = false;
                    textBoxNoEmployeResult.Enabled = false;
                    textBoxPrenomResult.Enabled = false;
                    textBoxNomResult.Enabled = false;
                    maskedTextBoxTelephoneResult.Enabled = false;
                    textBoxCourrielResult.Enabled = false;
                    comboBoxFonctionResult.Enabled = false;
                    textBoxLoginResult.Enabled = false;
                    textBoxPasswordResult.Enabled = false;

                    labelMgs.Visible = true;
                    labelMgs.Text = "Entrez la fonction de l'employe : ";
                    comboBoxRechercheFonction.Visible = true;
                    textBoxInfo.Focus();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// afficher les resultats de la recherche dans le listview ou le menu de droite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAfficherRecherche_Click(object sender, EventArgs e) {          
            Employe emp = new Employe();
            uint rechercheID = 0;
            string rechercheNomEtPrenom = "";
            string rechercheFonction = "";
            List<Employe> listEmp;
            switch (comboBoxCategorie.SelectedIndex){
                case 0:
                    rechercheID = Convert.ToUInt32(textBoxInfo.Text);
                    emp = EmployeIO.RechercherEmpParNo(rechercheID);
                    if(emp.Nom != null){
                        textBoxNoEmployeResult.Text = rechercheID.ToString();
                        textBoxPrenomResult.Text = emp.Prenom;
                        textBoxNomResult.Text = emp.Nom;
                        maskedTextBoxTelephoneResult.Text = emp.Telephone;
                        textBoxCourrielResult.Text = emp.Courriel;
                        if(emp.Login == " N/A"){
                            comboBoxFonctionResult.SelectedIndex = 0;
                            textBoxLoginResult.Enabled = false;
                            textBoxPasswordResult.Enabled = false;
                        }
                        else{
                            textBoxLoginResult.Enabled = true;
                            textBoxPasswordResult.Enabled = true;
                        }
                        textBoxLoginResult.Text = emp.Login;
                        textBoxPasswordResult.Text = emp.Password;
                    }
                    else{
                        MessageBox.Show("Le numéro n'existe pas dans la liste.");
                    }
                    break;
                case 1:
                    rechercheNomEtPrenom = textBoxInfo.Text.Trim();
                    listEmp = EmployeIO.RechercherParPrenom(rechercheNomEtPrenom);
                    if (comboBoxMode.SelectedIndex == 1) {
                        if (listEmp != null) {
                            listViewRecherche.Items.Clear();
                            foreach (Employe unEmp in listEmp) {
                                ListViewItem item = new ListViewItem(unEmp.NoEmploye.ToString());
                                item.SubItems.Add(unEmp.Prenom);
                                item.SubItems.Add(unEmp.Nom);
                                item.SubItems.Add(unEmp.Telephone);
                                item.SubItems.Add(unEmp.Courriel);
                                item.SubItems.Add(unEmp.Fonction);
                                item.SubItems.Add(unEmp.Login);
                                item.SubItems.Add(unEmp.Password);
                                listViewRecherche.Items.Add(item);
                            }
                        }
                    }
                    else{                                               
                        if (listEmp != null) {
                            listViewRecherche.Items.Clear();
                            foreach (Employe unEmp in listEmp) {
                                if (unEmp.Login != " ") {
                                    ListViewItem item = new ListViewItem(unEmp.NoEmploye.ToString());
                                    item.SubItems.Add(unEmp.Prenom);
                                    item.SubItems.Add(unEmp.Nom);
                                    item.SubItems.Add(unEmp.Telephone);
                                    item.SubItems.Add(unEmp.Courriel);
                                    item.SubItems.Add(unEmp.Fonction);
                                    item.SubItems.Add(unEmp.Login);
                                    item.SubItems.Add(unEmp.Password);
                                    listViewRecherche.Items.Add(item);
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    rechercheNomEtPrenom = textBoxInfo.Text.Trim();
                    listEmp = EmployeIO.RechercherParNom(rechercheNomEtPrenom);
                    if (comboBoxMode.SelectedIndex == 1) {
                        if (listEmp != null) {
                            listViewRecherche.Items.Clear();
                            foreach (Employe unEmp in listEmp) {
                                ListViewItem item = new ListViewItem(unEmp.NoEmploye.ToString());
                                item.SubItems.Add(unEmp.Prenom);
                                item.SubItems.Add(unEmp.Nom);
                                item.SubItems.Add(unEmp.Telephone);
                                item.SubItems.Add(unEmp.Courriel);
                                item.SubItems.Add(unEmp.Fonction);
                                item.SubItems.Add(unEmp.Login);
                                item.SubItems.Add(unEmp.Password);
                                listViewRecherche.Items.Add(item);
                            }
                        }
                    }
                    else {
                        if (listEmp != null) {
                            listViewRecherche.Items.Clear();
                            foreach (Employe unEmp in listEmp) {
                                if (unEmp.Login != " ") {
                                    ListViewItem item = new ListViewItem(unEmp.NoEmploye.ToString());
                                    item.SubItems.Add(unEmp.Prenom);
                                    item.SubItems.Add(unEmp.Nom);
                                    item.SubItems.Add(unEmp.Telephone);
                                    item.SubItems.Add(unEmp.Courriel);
                                    item.SubItems.Add(unEmp.Fonction);
                                    item.SubItems.Add(unEmp.Login);
                                    item.SubItems.Add(unEmp.Password);
                                    listViewRecherche.Items.Add(item);
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    rechercheFonction = " " + comboBoxRechercheFonction.SelectedItem.ToString();
                    listEmp = EmployeIO.RechercherFonction(rechercheFonction);
                    switch(comboBoxRechercheFonction.SelectedIndex){
                        case 0:
                            if (comboBoxMode.SelectedIndex == 0){
                                listViewRecherche.Items.Clear();
                                break;
                            }
                            else{
                                if (listEmp != null) {
                                    listViewRecherche.Items.Clear();
                                    foreach (Employe unEmp in listEmp) {
                                        ListViewItem item = new ListViewItem(unEmp.NoEmploye.ToString());
                                        item.SubItems.Add(unEmp.Prenom);
                                        item.SubItems.Add(unEmp.Nom);
                                        item.SubItems.Add(unEmp.Telephone);
                                        item.SubItems.Add(unEmp.Courriel);
                                        item.SubItems.Add(unEmp.Fonction);
                                        item.SubItems.Add(unEmp.Login);
                                        item.SubItems.Add(unEmp.Password);
                                        listViewRecherche.Items.Add(item);
                                    }
                                }
                            }
                            break;
                        case 1:
                            if (listEmp != null) {
                                listViewRecherche.Items.Clear();
                                foreach (Employe unEmp in listEmp) {
                                    ListViewItem item = new ListViewItem(unEmp.NoEmploye.ToString());
                                    item.SubItems.Add(unEmp.Prenom);
                                    item.SubItems.Add(unEmp.Nom);
                                    item.SubItems.Add(unEmp.Telephone);
                                    item.SubItems.Add(unEmp.Courriel);
                                    item.SubItems.Add(unEmp.Fonction);
                                    item.SubItems.Add(unEmp.Login);
                                    item.SubItems.Add(unEmp.Password);
                                    listViewRecherche.Items.Add(item);                                   
                                }
                            }
                            break;
                        case 2:
                            if (listEmp != null) {
                                listViewRecherche.Items.Clear();
                                foreach (Employe unEmp in listEmp) {
                                    ListViewItem item = new ListViewItem(unEmp.NoEmploye.ToString());
                                    item.SubItems.Add(unEmp.Prenom);
                                    item.SubItems.Add(unEmp.Nom);
                                    item.SubItems.Add(unEmp.Telephone);
                                    item.SubItems.Add(unEmp.Courriel);
                                    item.SubItems.Add(unEmp.Fonction);
                                    item.SubItems.Add(unEmp.Login);
                                    item.SubItems.Add(unEmp.Password);
                                    listViewRecherche.Items.Add(item);
                                }
                            }
                            break;
                        case 3:
                            if (listEmp != null) {
                                listViewRecherche.Items.Clear();
                                foreach (Employe unEmp in listEmp) {
                                    ListViewItem item = new ListViewItem(unEmp.NoEmploye.ToString());
                                    item.SubItems.Add(unEmp.Prenom);
                                    item.SubItems.Add(unEmp.Nom);
                                    item.SubItems.Add(unEmp.Telephone);
                                    item.SubItems.Add(unEmp.Courriel);
                                    item.SubItems.Add(unEmp.Fonction);
                                    item.SubItems.Add(unEmp.Login);
                                    item.SubItems.Add(unEmp.Password);
                                    listViewRecherche.Items.Add(item);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}