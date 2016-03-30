using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace CreationXML
{
    public partial class ArboFichier : Form
    {
        private string chemin;
        private XmlDocument dom;
        private frmMain main;

        public ArboFichier(string Path, frmMain page)
        {
            InitializeComponent();
            this.chemin = Path;
            this.main = page;
        }

        private void ArboFichier_Load(object sender, EventArgs e)
        {
            // SECTION 1. Chargement du fichier XML
            dom = new XmlDocument();
            dom.Load(chemin);

            // SECTION 2. Initialisation du TreeView
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(new TreeNode(dom.DocumentElement.Name));
            TreeNode tNode = new TreeNode();
            tNode = treeView1.Nodes[0];

            // SECTION 3. Remplissage du treeView
            AddNode(dom.DocumentElement, tNode);
            treeView1.ExpandAll();

        }

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;

            nodeList = inXmlNode.ChildNodes;

            for (int i = 0; i <= nodeList.Count - 1; i++)
            {
                if (inXmlNode.Name == "QUESTION")
                    inTreeNode.Text = inXmlNode.SelectSingleNode("LIBELLE").InnerText;
                else
                {
                    xNode = inXmlNode.ChildNodes[i];

                    if (inXmlNode.Name == "NIVEAU")
                        inTreeNode.Text = inXmlNode.Name + " " + inXmlNode.Attributes["niv"].Value;

                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));

                    tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            XmlNodeList liste = dom.GetElementsByTagName("QUESTION");
            foreach (XmlNode a in liste)
            {
                if (a.SelectSingleNode("LIBELLE").InnerText == e.Node.Text) // Si l'element selectionné est bien une question
                {
                    // Verif de la bonne reponse
                    XmlNodeList lstChoix = a.SelectNodes("CHOIX"); // liste des choix de reponse de la question
                    int idRep = 0;
                    for (int i = 0; i < lstChoix.Count - 1; i++) // Parcours de la liste des choix
                    {
                        string valHachee = lstChoix[i].Attributes["reponse"].Value;
                        string valeur = main.hachageString("true", lstChoix[i].InnerText).ToUpper(); // appel de la méthode cryptage pour le test
                        if (valHachee.Equals(valeur))
                            idRep = i;
                    }
                    main.recupDonnees(a.ParentNode.ParentNode.Name, Int32.Parse(a.ParentNode.Attributes["niv"].Value), a.SelectSingleNode("LIBELLE").InnerText, a.SelectNodes("CHOIX").Item(0).InnerText, a.SelectNodes("CHOIX").Item(1).InnerText, a.SelectNodes("CHOIX").Item(2).InnerText, idRep, a.SelectSingleNode("DESCRIPTION").InnerText);
                    this.Close();
                }
            }
        }
    }
}