using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;

namespace CreationXML
{
    public partial class FrmMain : Form
    {

        private string path;

        // Elements de la question
        private string laQuestionAvModif = "";
        private string leGroupeAvModif = "";
        private int leNivAvModif = 1;
        public bool ajoutQuestion = true;

        private XmlNode newNode;
        public FrmMain()
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

            // On affiche la boîte de dialogue et on récupère son résultat 
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Récupération du nom du fichier
                path = openFileDialog1.FileName;


                if (((Button)sender).Name == "btnCreateXML")
                {
                    XmlTextWriter writerXml = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                    writerXml.Formatting = Formatting.Indented;
                    writerXml.WriteStartDocument(false);
                    writerXml.WriteStartElement("QCM");
                    writerXml.WriteEndElement();
                    writerXml.Flush();
                    writerXml.Close();
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

        private void ParcoursXml(string path)
        {
            XmlDocument document = new XmlDocument();

            document.Load(path);
            //document.CreateElement("qcm");
            try
            {
                XmlNode selectSingleNode = document.SelectSingleNode("QCM");
                if (selectSingleNode != null)
                {
                    XmlNode racine = selectSingleNode.SelectSingleNode(cbbGroupe.SelectedItem.ToString().ToUpper());

                    XmlElement categorie = document.CreateElement(cbbGroupe.SelectedItem.ToString().ToUpper());
                    // 2. Lier cet élément à l'élement parent (le niveau correspondant)
                    selectSingleNode.AppendChild(categorie);

                    racine = selectSingleNode.SelectSingleNode(cbbGroupe.SelectedItem.ToString().ToUpper());


                    XmlNode niveau = racine.SelectSingleNode("NIVEAU[@niv =" + VerifNiveauQuest().ToString() + "]");

                    XmlElement elemNiveau = document.CreateElement("NIVEAU");
                    // 2. Lier cet élément à l'élement parent (le niveau correspondant)
                    XmlNode elmVideo01 = racine;
                    elmVideo01.AppendChild(elemNiveau);

                    XmlAttribute newAttr = document.CreateAttribute("niv");
                    newAttr.Value = VerifNiveauQuest().ToString();
                    elemNiveau.Attributes.Append(newAttr);

                    niveau = racine.SelectSingleNode("NIVEAU[@niv =" + VerifNiveauQuest().ToString() + "]");


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
                    foreach (TextBox laTextbox in groupBox1.Controls.OfType<TextBox>().Reverse())
                    // Parcours de la liste des textbox pour définir le nombre de questions
                    {
                        if (laTextbox.Text != "" && laTextbox.Name != "txtBonneRep")
                        {
                            newElement = document.CreateElement("CHOIX");
                            txtActor = document.CreateTextNode(laTextbox.Text);
                            leNiveau.LastChild.AppendChild(newElement);
                            leNiveau.LastChild.LastChild.AppendChild(txtActor);

                            // Ajout de l'attribut "num" à l'élement "choix"
                            newAttr = document.CreateAttribute("num");
                            newAttr.Value = i.ToString();
                            newElement.Attributes.Append(newAttr);

                            newAttr = document.CreateAttribute("reponse");

                            // Permet de vérifier si le radiobouton de la textbox est checked (bonne rep)
                            RadioButton btnChecked = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
                            if (btnChecked.Name == "rdbRep" + laTextbox.Name.Substring(laTextbox.Name.Length - 2, 2))
                                newAttr.Value = HachageString("true", laTextbox.Text);
                            else
                                newAttr.Value = HachageString("false", laTextbox.Text);


                            newElement.Attributes.Append(newAttr);
                            i++;
                        }
                    }
                    newElement = document.CreateElement("DESCRIPTION");
                    txtActor = document.CreateTextNode(txtBonneRep.Text);
                    leNiveau.LastChild.AppendChild(newElement);
                    leNiveau.LastChild.LastChild.AppendChild(txtActor);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Un problème a été rencontré : \n" + e.Message);
            }
            document.Save(path);

            ClearControls();
        }

        private int VerifNiveauQuest()
        {
            if (rdbUp2.Checked)
                return 2;
            if (rdbUp3.Checked)
                return 3;
            return 1;
        }

        private void ClearControls()
        {
            txtQuestion.ResetText();
            txtRep01.ResetText();
            txtRep02.ResetText();
            txtRep03.ResetText();
            txtBonneRep.ResetText();
            rdbRep01.Checked = true;
        }

        /// <summary>
        /// Permet de hacher la réponse
        /// </summary>
        /// <param name="valRep">true/false</param>
        /// <param name="laReponse">Le libellé de la réponse</param>
        /// <returns></returns>
        public string HachageString(string valRep, string laReponse)
        {
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(valRep + laReponse + "lpcrspm2015");
            SHA1CryptoServiceProvider cryptoTransformSha1 = new SHA1CryptoServiceProvider();
            return (BitConverter.ToString(cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "").ToUpper());
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
        public void RecupDonnees(string groupe, int niveau, string question, string rep01, string rep02, string rep03, int idBonneRep, string bonneRep) //public void recupDonnees(string groupe, int niveau, string question, string rep01, string rep02, string rep03, bool vrep01, bool vrep02, bool vrep03, string bonneRep)
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

            btnSave.Enabled = true;

        }

        /// <summary>
        /// Affiche la page avec le TreeView du document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMajQuest_Click(object sender, EventArgs e)
        {
            ArboFichier frm = new ArboFichier(path, this);
            frm.Show();
            //ajoutQuestion = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Ajout d'une nouvelle question
            if (ajoutQuestion)
            {

                if (txtQuestion.Text != "" && txtRep01.Text != "")
                    ParcoursXml(path);
                else
                {
                    if (txtQuestion.Text == "")
                        MessageBox.Show("Veuillez saisir un libellé à la question");
                    if (txtRep01.Text == "")
                        MessageBox.Show("Veuillez saisir au moins une réponse");
                }
                MessageBox.Show("Ajout de la question effectué avec succés ! ");
            }
            else
            {
                // MAJ Question existante
                DialogResult result = MessageBox.Show("Souhaitez-vous réellement enregistrer les modifications effectuées sur la question ?", "Enregistrement modifications", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(path);
                    XmlNode node = document.SelectSingleNode("QCM");
                    if (node != null)
                    {
                        XmlNode selectSingleNode = node.SelectSingleNode(leGroupeAvModif);
                        if (selectSingleNode != null)
                            foreach (XmlNode elem in selectSingleNode)
                            {
                                if (elem.Attributes != null && elem.Attributes["niv"].Value == leNivAvModif.ToString())
                                {
                                    XmlNodeList xmlNodeList = elem.SelectNodes("QUESTION");
                                    if (xmlNodeList != null)
                                        foreach (XmlNode quest in xmlNodeList)
                                        {
                                            XmlNode singleNode = quest.SelectSingleNode("LIBELLE");
                                            if (singleNode != null && singleNode.InnerText == laQuestionAvModif)
                                            {
                                                if (txtQuestion.Text != "" && txtRep01.Text != "")
                                                {
                                                    // Suppression de la question
                                                    XmlNode xmlNode = selectSingleNode.SelectSingleNode("NIVEAU");
                                                    if (xmlNode != null)
                                                        xmlNode.RemoveChild(quest);
                                                    document.Save(path);

                                                    // Ajout 
                                                    ParcoursXml(path);
                                                    MessageBox.Show("Mise à jour effectuée avec succés ! ");
                                                    ajoutQuestion = true;
                                                }
                                                else
                                                {
                                                    if (txtQuestion.Text == "")
                                                        MessageBox.Show("Veuillez saisir un libellé à la question");
                                                    if (txtRep01.Text == "")
                                                        MessageBox.Show("Veuillez saisir au moins une réponse");
                                                }
                                                break;
                                            }
                                        }
                                }
                            }
                    }
                }
                else
                {
                    if (result == DialogResult.No)
                        ClearControls();
                }
            }
        }
    }
}
