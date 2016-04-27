using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;


namespace CreationXML
{
    public partial class frmMain : Form
    {

        private string path;

        // Elements de la question
        private string laQuestionAvModif = "";
        private string leGroupeAvModif = "";
        private int leNivAvModif = 1;

        private XmlNode newNode;
        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbGroupe.SelectedIndex = 0;
        }

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath;
            openFileDialog1.Filter = "Fichier texte (*.xml)|*.xml|Tous les fichiers (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;

            //--on affiche la boîte de dialogue et on récupère son résultat 
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //-------on récupère le nom du fichier 
                path = openFileDialog1.FileName;

                if (((Button)sender).Name == "btnCreateXML")
                {
                    XmlTextWriter writerXML = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                    writerXML.Formatting = Formatting.Indented;
                    writerXML.WriteStartDocument(false);
                    writerXML.WriteStartElement("QCM");
                    writerXML.WriteEndElement();
                    writerXML.Flush();
                    writerXML.Close();
                }

                string[] leFichier = path.Split('\\');
                lblNomFichier.Visible = true;
                lblNomFichier.Text = "Fichier XML actuel : " + leFichier[leFichier.Count() - 1];

                grpAddQuest.Visible = true;

                this.Controls["btnCreateXML"].Enabled = false;
                this.Controls["btnLoadXML"].Enabled = false;
                btnMajQuest.Visible = true;
            }
        }

        private void parcoursXml(string path)
        {
            XmlDocument document = new XmlDocument();

            document.Load(path);
            //document.CreateElement("qcm");

            XmlNode racine = document.SelectSingleNode("QCM").SelectSingleNode(cbbGroupe.SelectedItem.ToString().ToUpper());
            if (racine == null)
            {
                XmlElement categorie = document.CreateElement(cbbGroupe.SelectedItem.ToString().ToUpper());
                // 2. Lier cet élément à l'élement parent (le niveau correspondant)
                document.SelectSingleNode("QCM").AppendChild(categorie);

                racine = document.SelectSingleNode("QCM").SelectSingleNode(cbbGroupe.SelectedItem.ToString().ToUpper());
            }

            XmlNode niveau = racine.SelectSingleNode("NIVEAU[@niv =" + verifNiveauQuest().ToString() + "]");
            if (niveau == null)
            {
                XmlElement elemNiveau = document.CreateElement("NIVEAU");
                // 2. Lier cet élément à l'élement parent (le niveau correspondant)
                XmlNode elmVideo01 = racine;
                elmVideo01.AppendChild(elemNiveau);

                XmlAttribute newAttr = document.CreateAttribute("niv");
                newAttr.Value = verifNiveauQuest().ToString();
                elemNiveau.Attributes.Append(newAttr);

                niveau = racine.SelectSingleNode("NIVEAU[@niv =" + verifNiveauQuest().ToString() + "]");
            }

            // 1. Créer un nouvel élément question.
            XmlElement newElement = document.CreateElement("QUESTION");
            // 2. Lier cet élément à l'élement parent (le niveau correspondant)
            XmlNode leNiveau = niveau;
            leNiveau.AppendChild(newElement);

            // Création de l'élement Label (libellé question)
            newElement = document.CreateElement("LIBELLE");
            // Texte du libelle correspndant a la textbox libelle saisie
            XmlText txtActor = document.CreateTextNode(txtQuestion.Text);
            leNiveau.LastChild.AppendChild(newElement);
            leNiveau.LastChild.LastChild.AppendChild(txtActor);

            int i = 1;
            foreach (TextBox laTextbox in groupBox1.Controls.OfType<TextBox>().Reverse()) // Parcours de la liste des textbox pour définir le nombre de questions
            {
                if (laTextbox.Text != "" && laTextbox.Name != "txtBonneRep")
                {
                    newElement = document.CreateElement("CHOIX");
                    txtActor = document.CreateTextNode(laTextbox.Text);
                    leNiveau.LastChild.AppendChild(newElement);
                    leNiveau.LastChild.LastChild.AppendChild(txtActor);

                    // Ajout de l'attribut "num" à l'élement "choix"
                    XmlAttribute newAttr = document.CreateAttribute("num");
                    newAttr.Value = i.ToString();
                    newElement.Attributes.Append(newAttr);

                    newAttr = document.CreateAttribute("reponse");

                    // Permet de vérifier si le radiobouton de la textbox est checked (bonne rep)
                    RadioButton btnChecked = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
                    if (btnChecked.Name == "rdbRep" + laTextbox.Name.Substring(laTextbox.Name.Length - 2, 2))
                        newAttr.Value = hachageString("true", laTextbox.Text);
                    else
                        newAttr.Value = hachageString("false", laTextbox.Text);


                    newElement.Attributes.Append(newAttr);
                    i++;
                }
            }
            newElement = document.CreateElement("DESCRIPTION");
            txtActor = document.CreateTextNode(txtBonneRep.Text);
            leNiveau.LastChild.AppendChild(newElement);
            leNiveau.LastChild.LastChild.AppendChild(txtActor);

            document.Save(path);

            clearControls();
        }

        private void btnGenerer_Click(object sender, EventArgs e)
        {
            if (txtQuestion.Text != "" && txtRep01.Text != "")
                parcoursXml(path);
            else
            {
                if (txtQuestion.Text == "")
                    MessageBox.Show("Veuillez saisir un libellé à la question");
                if (txtRep01.Text == "")
                    MessageBox.Show("Veuillez saisir au moins une réponse");
            }
            MessageBox.Show("Ajout de la question effectué avec succés ! ");
        }

        private int verifNiveauQuest()
        {
            if (rdbUp2.Checked)
            {
                return 2;
            }
            if (rdbUp3.Checked)
            {
                return 3;
            }
            return 1;
        }

        private void clearControls()
        {
            txtQuestion.ResetText();
            txtRep01.ResetText();
            txtRep02.ResetText();
            txtRep03.ResetText();
            txtBonneRep.ResetText();
            rdbRep01.Checked = true;
        }

        public string hachageString(string valRep, string laReponse) // Permet de hacher la valeur de la reponse (true/false) et la reponse (ex : energie electrique)
        {
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(valRep + laReponse + "lpcrspm2015");
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return (BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "").ToUpper());
        }



        /// <summary>
        /// Récupère les données de la question selectionnée dans le TreeView
        /// </summary>
        /// <param name="groupe">Groupe de la question(Vent, Solaire ...)</param>
        /// <param name="niveau">Niveau question (1, 2 ou 3)</param>
        /// <param name="question">Libellé de la question</param>
        /// <param name="rep01">Première réponse</param>
        /// <param name="rep02">Deuxième réponse</param>
        /// <param name="rep03">Troisieme Réponse</param>
        /// <param name="idBonneRep">ID de la bonne réponse</param>
        /// <param name="bonneRep">Description de la réponse</param>
        public void recupDonnees(string groupe, int niveau, string question, string rep01, string rep02, string rep03, int idBonneRep, string bonneRep) //public void recupDonnees(string groupe, int niveau, string question, string rep01, string rep02, string rep03, bool vrep01, bool vrep02, bool vrep03, string bonneRep)
        {
            // Récupération de la question avant modification
            laQuestionAvModif = question;
            leGroupeAvModif = groupe;
            leNivAvModif = niveau;

            // Changement du groupe
            int idElemGroupe = 0;
            switch (groupe)
            {
                case "SOLAIRE":
                    idElemGroupe = cbbGroupe.FindString(groupe);
                    break;
                case "VENT":
                    idElemGroupe = cbbGroupe.FindString(groupe);
                    break;
                case "EAU":
                    idElemGroupe = cbbGroupe.FindString(groupe);
                    break;
                case "POLE":
                    idElemGroupe = cbbGroupe.FindString(groupe);
                    break;
                case "HABITATION":
                    idElemGroupe = cbbGroupe.FindString(groupe);
                    break;

            }
            cbbGroupe.SelectedIndex = idElemGroupe;

            // Changement des radios bouton du niveau
            if (niveau == 1)
                rdbUp1.Checked = true;

            if (niveau == 2)
                rdbUp2.Checked = true;

            if (niveau == 3)
                rdbUp3.Checked = true;

            // Changement des textbox
            txtQuestion.Text = question;
            txtRep01.Text = rep01;
            txtRep02.Text = rep02;
            txtRep03.Text = rep03;
            txtBonneRep.Text = bonneRep;

            // Radios boutons gerants la bonne reponse
            if (idBonneRep == 0)
                rdbRep01.Checked = true;
            if (idBonneRep == 1)
                rdbRep02.Checked = true;
            if (idBonneRep == 2)
                rdbRep03.Checked = true;

            btnUpdate.Enabled = true;

        }

        /// <summary>
        /// Affiche la page avec le TreeView du document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMajQuest_Click(object sender, EventArgs e)
        {
            btnGenerer.Enabled = false;

            ArboFichier frm = new ArboFichier(path, this);
            frm.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Souhaitez-vous réellement enregistrer les modifications éffectuées sur la question ?", "Enregistrement modifications", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                foreach (XmlNode elem in document.SelectSingleNode("QCM").SelectSingleNode(leGroupeAvModif))
                {
                    if (elem.Attributes["niv"].Value == leNivAvModif.ToString())
                    {
                        foreach (XmlNode quest in elem.SelectNodes("QUESTION"))
                        {
                            if (quest.SelectSingleNode("LIBELLE").InnerText == laQuestionAvModif)
                            {
                                // Suppression de la question
                                document.SelectSingleNode("QCM").SelectSingleNode(leGroupeAvModif).SelectSingleNode("NIVEAU").RemoveChild(quest);
                                document.Save(path);

                                // Ajout 
                                if (txtQuestion.Text != "" && txtRep01.Text != "")
                                    parcoursXml(path);
                                else
                                {
                                    if (txtQuestion.Text == "")
                                        MessageBox.Show("Veuillez saisir un libellé à la question");
                                    if (txtRep01.Text == "")
                                        MessageBox.Show("Veuillez saisir au moins une réponse");
                                }
                                MessageBox.Show("Mise à jour effectuée avec succés ! ");
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
