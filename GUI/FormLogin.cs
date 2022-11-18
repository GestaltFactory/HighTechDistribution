using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiTechDistribution.GUI {
    public partial class FormLogin : Form {
        public FormLogin() {
            InitializeComponent();
        }

        /// <summary>
        /// entrez nom et password et appuyer sur login pour entrer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogin_Click(object sender, EventArgs e) {
            if(textBoxUser.Text == "Admin" && textBoxPassword.Text == "12345"){
                this.Hide();
                FormGestionApp openAdminForm = new FormGestionApp();
                openAdminForm.ShowDialog();
            }
        }

        /// <summary>
        /// Quitter l'app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonQuitter_Click(object sender, EventArgs e) {
            Validateur.Quit();
        }
    }
}