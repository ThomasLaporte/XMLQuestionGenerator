using System;
using System.Windows.Forms;
using System.Xml;

namespace CreationXML
{
    public partial class ArboFichier : Form
    {
        private string chemin;
        private XmlDocument dom;
        private FrmMain main;

        private bool modifValide = false;
        public ArboFichier(string path, FrmMain page)
        {
            InitializeComponent();
            this.chemin = path;
            this.main = page;
        }

        private void ArboFichier_Load(object sender, EventArgs e)
        {
            // SECTION 1. Chargement du fichier XML 
            dom = new XmlDocument();
            dom.Load(chemin);

            LoadTreeView();

        }

        private void LoadTreeView()
        {
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
            var nodeList = inXmlNode.ChildNodes;

            for (int i = 0; i <= nodeList.Count - 1; i++)
            {
                if (inXmlNode.Name == "QUESTION")
                    inTreeNode.Text = inXmlNode.SelectSingleNode("LIBELLE").InnerText;
                else
                {
                    var xNode = inXmlNode.ChildNodes[i];

                    if (inXmlNode.Name == "NIVEAU")
                        inTreeNode.Text = inXmlNode.Name + " " + inXmlNode.Attributes["niv"].Value;

                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));

                    var tNode = inTreeNode.Nodes[i];
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
                        string valeur = main.HachageString("true", lstChoix[i].InnerText).ToUpper(); // appel de la méthode cryptage pour le test
                        if (valHachee.Equals(valeur))
                            idRep = i;
                    }
                    main.RecupDonnees(a.ParentNode.ParentNode.Name, Int32.Parse(a.ParentNode.Attributes["niv"].Value), a.SelectSingleNode("LIBELLE").InnerText, a.SelectNodes("CHOIX").Item(0).InnerText, a.SelectNodes("CHOIX").Item(1).InnerText, a.SelectNodes("CHOIX").Item(2).InnerText, idRep, a.SelectSingleNode("DESCRIPTION").InnerText);

                    modifValide = true;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Méthode pour supprimer un noeud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {

            foreach (XmlNode a in dom.GetElementsByTagName("QUESTION"))
            {
                XmlNode laQuestioNode = a.SelectSingleNode("LIBELLE");
                if (laQuestioNode != null && laQuestioNode.InnerText == treeView1.SelectedNode.Text) // Si l'element selectionné est bien une question
                {
                    var parent = a.ParentNode;
                    var oldParent = parent.ParentNode;

                   parent.RemoveChild(a);

                    if (parent.ChildNodes.Count == 0)
                        parent.ParentNode.RemoveChild(parent);

                    if (oldParent.ChildNodes.Count == 0)
                        oldParent.ParentNode.RemoveChild(oldParent);
                    dom.Save(chemin);

                    LoadTreeView();
                    break;
                }
            }
        }

        private void ArboFichier_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modifValide)
            {
                main.ajoutQuestion = false;
            }
            else
            {
                main.ajoutQuestion = true;
            }
        }
    }
}