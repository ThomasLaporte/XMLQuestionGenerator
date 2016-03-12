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
        public frmMain()
        {
            InitializeComponent();
        }

        private string path;

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

                if(((Button)sender).Name == "btnCreateXML")
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
            XmlNode elmVideo = niveau;
            elmVideo.AppendChild(newElement);

            // Création de l'élement Label (libellé question)
            newElement = document.CreateElement("LIBELLE");
            // Texte du libelle correspndant a la textbox libelle saisie
            XmlText txtActor = document.CreateTextNode(txtQuestion.Text);
            elmVideo.LastChild.AppendChild(newElement);
            elmVideo.LastChild.LastChild.AppendChild(txtActor);

            int i = 1;
            foreach (TextBox laTextbox in groupBox1.Controls.OfType<TextBox>().Reverse()) // Parcours de la liste des textbox pour définir le nombre de questions
            {
                if (laTextbox.Text != "" && laTextbox.Name != "txtBonneRep")
                {
                    newElement = document.CreateElement("CHOIX");
                    txtActor = document.CreateTextNode(laTextbox.Text);
                    elmVideo.LastChild.AppendChild(newElement);
                    elmVideo.LastChild.LastChild.AppendChild(txtActor);

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
            elmVideo.LastChild.AppendChild(newElement);
            elmVideo.LastChild.LastChild.AppendChild(txtActor);

            document.Save(path);
            MessageBox.Show("Ajout de la question effectué avec succés ! ");
            clearControls();
        }

        private void btnGenerer_Click(object sender, EventArgs e)
        {
            if(txtQuestion.Text != "" && txtRep01.Text != "")
            {
                parcoursXml(path);
            }
            else
            {
                if (txtQuestion.Text == "")
                    MessageBox.Show("Veuillez saisir un libellé à la question");
                if (txtRep01.Text == "")
                    MessageBox.Show("Veuillez saisir au moins une réponse");
            }
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

        private string hachageString(string valRep, string laReponse) // Permet de hacher la valeur de la reponse (true/false) et la reponse (ex : energie electrique)
        {
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(valRep + laReponse + "lpcrspm2015");
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return (BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "").ToUpper());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbGroupe.SelectedIndex = 0;
        }
    }
}
