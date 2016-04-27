namespace CreationXML
{
    partial class frmMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRep03 = new System.Windows.Forms.TextBox();
            this.txtRep02 = new System.Windows.Forms.TextBox();
            this.txtRep01 = new System.Windows.Forms.TextBox();
            this.chkRep04 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbRep03 = new System.Windows.Forms.RadioButton();
            this.rdbRep02 = new System.Windows.Forms.RadioButton();
            this.rdbRep01 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBonneRep = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbGroupe = new System.Windows.Forms.ComboBox();
            this.btnGenerer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbUp3 = new System.Windows.Forms.RadioButton();
            this.rdbUp2 = new System.Windows.Forms.RadioButton();
            this.rdbUp1 = new System.Windows.Forms.RadioButton();
            this.grpAddQuest = new System.Windows.Forms.GroupBox();
            this.lblNomFichier = new System.Windows.Forms.Label();
            this.btnCreateXML = new System.Windows.Forms.Button();
            this.btnMajQuest = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpAddQuest.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.CheckFileExists = false;
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.Location = new System.Drawing.Point(198, 13);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(147, 23);
            this.btnLoadXML.TabIndex = 14;
            this.btnLoadXML.Text = "Selection du fichier";
            this.btnLoadXML.UseVisualStyleBackColor = true;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Réponse 1 : ";
            // 
            // txtRep03
            // 
            this.txtRep03.Location = new System.Drawing.Point(110, 141);
            this.txtRep03.Name = "txtRep03";
            this.txtRep03.Size = new System.Drawing.Size(205, 20);
            this.txtRep03.TabIndex = 6;
            // 
            // txtRep02
            // 
            this.txtRep02.Location = new System.Drawing.Point(110, 96);
            this.txtRep02.Name = "txtRep02";
            this.txtRep02.Size = new System.Drawing.Size(205, 20);
            this.txtRep02.TabIndex = 5;
            // 
            // txtRep01
            // 
            this.txtRep01.Location = new System.Drawing.Point(110, 54);
            this.txtRep01.Name = "txtRep01";
            this.txtRep01.Size = new System.Drawing.Size(205, 20);
            this.txtRep01.TabIndex = 4;
            // 
            // chkRep04
            // 
            this.chkRep04.AutoSize = true;
            this.chkRep04.Location = new System.Drawing.Point(36, 171);
            this.chkRep04.Name = "chkRep04";
            this.chkRep04.Size = new System.Drawing.Size(15, 14);
            this.chkRep04.TabIndex = 16;
            this.chkRep04.UseVisualStyleBackColor = true;
            this.chkRep04.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Réponse 3:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Réponse 2 :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Selectionnez le fichier des questions :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbRep03);
            this.groupBox2.Controls.Add(this.rdbRep02);
            this.groupBox2.Controls.Add(this.rdbRep01);
            this.groupBox2.Controls.Add(this.chkRep04);
            this.groupBox2.Location = new System.Drawing.Point(321, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(94, 157);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selectionnez la bonne réponse ";
            // 
            // rdbRep03
            // 
            this.rdbRep03.AutoSize = true;
            this.rdbRep03.Location = new System.Drawing.Point(41, 125);
            this.rdbRep03.Name = "rdbRep03";
            this.rdbRep03.Size = new System.Drawing.Size(14, 13);
            this.rdbRep03.TabIndex = 19;
            this.rdbRep03.UseVisualStyleBackColor = true;
            // 
            // rdbRep02
            // 
            this.rdbRep02.AutoSize = true;
            this.rdbRep02.Location = new System.Drawing.Point(41, 80);
            this.rdbRep02.Name = "rdbRep02";
            this.rdbRep02.Size = new System.Drawing.Size(14, 13);
            this.rdbRep02.TabIndex = 18;
            this.rdbRep02.UseVisualStyleBackColor = true;
            // 
            // rdbRep01
            // 
            this.rdbRep01.AutoSize = true;
            this.rdbRep01.Checked = true;
            this.rdbRep01.Location = new System.Drawing.Point(41, 38);
            this.rdbRep01.Name = "rdbRep01";
            this.rdbRep01.Size = new System.Drawing.Size(14, 13);
            this.rdbRep01.TabIndex = 17;
            this.rdbRep01.TabStop = true;
            this.rdbRep01.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.txtBonneRep);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnGenerer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRep03);
            this.groupBox1.Controls.Add(this.txtRep02);
            this.groupBox1.Controls.Add(this.txtRep01);
            this.groupBox1.Location = new System.Drawing.Point(19, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 304);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Réponses";
            // 
            // txtBonneRep
            // 
            this.txtBonneRep.Location = new System.Drawing.Point(37, 190);
            this.txtBonneRep.Multiline = true;
            this.txtBonneRep.Name = "txtBonneRep";
            this.txtBonneRep.Size = new System.Drawing.Size(378, 59);
            this.txtBonneRep.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Description réponse :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Groupe question(solaire, vent, eau ...)";
            // 
            // cbbGroupe
            // 
            this.cbbGroupe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGroupe.FormattingEnabled = true;
            this.cbbGroupe.Items.AddRange(new object[] {
            "Solaire",
            "Vent",
            "Eau",
            "Pole",
            "Habitation"});
            this.cbbGroupe.Location = new System.Drawing.Point(229, 19);
            this.cbbGroupe.Name = "cbbGroupe";
            this.cbbGroupe.Size = new System.Drawing.Size(183, 21);
            this.cbbGroupe.TabIndex = 11;
            // 
            // btnGenerer
            // 
            this.btnGenerer.Location = new System.Drawing.Point(50, 275);
            this.btnGenerer.Name = "btnGenerer";
            this.btnGenerer.Size = new System.Drawing.Size(128, 23);
            this.btnGenerer.TabIndex = 11;
            this.btnGenerer.Text = "Ajouter la question";
            this.btnGenerer.UseVisualStyleBackColor = true;
            this.btnGenerer.Click += new System.EventHandler(this.btnGenerer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Question : ";
            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new System.Drawing.Point(100, 129);
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(317, 20);
            this.txtQuestion.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbUp3);
            this.groupBox3.Controls.Add(this.rdbUp2);
            this.groupBox3.Controls.Add(this.rdbUp1);
            this.groupBox3.Location = new System.Drawing.Point(19, 49);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 56);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pour quel niveau d\'amélioration? ";
            // 
            // rdbUp3
            // 
            this.rdbUp3.AutoSize = true;
            this.rdbUp3.Location = new System.Drawing.Point(307, 19);
            this.rdbUp3.Name = "rdbUp3";
            this.rdbUp3.Size = new System.Drawing.Size(86, 17);
            this.rdbUp3.TabIndex = 2;
            this.rdbUp3.Text = "Niveau 3 à 4";
            this.rdbUp3.UseVisualStyleBackColor = true;
            // 
            // rdbUp2
            // 
            this.rdbUp2.AutoSize = true;
            this.rdbUp2.Location = new System.Drawing.Point(151, 19);
            this.rdbUp2.Name = "rdbUp2";
            this.rdbUp2.Size = new System.Drawing.Size(86, 17);
            this.rdbUp2.TabIndex = 1;
            this.rdbUp2.Text = "Niveau 2 à 3";
            this.rdbUp2.UseVisualStyleBackColor = true;
            // 
            // rdbUp1
            // 
            this.rdbUp1.AutoSize = true;
            this.rdbUp1.Checked = true;
            this.rdbUp1.Location = new System.Drawing.Point(14, 19);
            this.rdbUp1.Name = "rdbUp1";
            this.rdbUp1.Size = new System.Drawing.Size(86, 17);
            this.rdbUp1.TabIndex = 0;
            this.rdbUp1.TabStop = true;
            this.rdbUp1.Text = "Niveau 1 à 2";
            this.rdbUp1.UseVisualStyleBackColor = true;
            // 
            // grpAddQuest
            // 
            this.grpAddQuest.Controls.Add(this.label2);
            this.grpAddQuest.Controls.Add(this.groupBox3);
            this.grpAddQuest.Controls.Add(this.txtQuestion);
            this.grpAddQuest.Controls.Add(this.label1);
            this.grpAddQuest.Controls.Add(this.groupBox1);
            this.grpAddQuest.Controls.Add(this.cbbGroupe);
            this.grpAddQuest.Location = new System.Drawing.Point(15, 69);
            this.grpAddQuest.Name = "grpAddQuest";
            this.grpAddQuest.Size = new System.Drawing.Size(484, 480);
            this.grpAddQuest.TabIndex = 17;
            this.grpAddQuest.TabStop = false;
            this.grpAddQuest.Text = "Ajout Question";
            this.grpAddQuest.Visible = false;
            // 
            // lblNomFichier
            // 
            this.lblNomFichier.AutoSize = true;
            this.lblNomFichier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomFichier.Location = new System.Drawing.Point(207, 49);
            this.lblNomFichier.Name = "lblNomFichier";
            this.lblNomFichier.Size = new System.Drawing.Size(106, 17);
            this.lblNomFichier.TabIndex = 18;
            this.lblNomFichier.Text = "txtNomFichier";
            this.lblNomFichier.Visible = false;
            // 
            // btnCreateXML
            // 
            this.btnCreateXML.Location = new System.Drawing.Point(352, 13);
            this.btnCreateXML.Name = "btnCreateXML";
            this.btnCreateXML.Size = new System.Drawing.Size(147, 23);
            this.btnCreateXML.TabIndex = 19;
            this.btnCreateXML.Text = "Créer un nouveau XML";
            this.btnCreateXML.UseVisualStyleBackColor = true;
            this.btnCreateXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // btnMajQuest
            // 
            this.btnMajQuest.Location = new System.Drawing.Point(505, 69);
            this.btnMajQuest.Name = "btnMajQuest";
            this.btnMajQuest.Size = new System.Drawing.Size(66, 48);
            this.btnMajQuest.TabIndex = 20;
            this.btnMajQuest.Text = "Modifier question existante";
            this.btnMajQuest.UseVisualStyleBackColor = true;
            this.btnMajQuest.Visible = false;
            this.btnMajQuest.Click += new System.EventHandler(this.btnMajQuest_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(265, 275);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(128, 23);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "Enregistrer modifs";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 563);
            this.Controls.Add(this.btnMajQuest);
            this.Controls.Add(this.btnCreateXML);
            this.Controls.Add(this.lblNomFichier);
            this.Controls.Add(this.grpAddQuest);
            this.Controls.Add(this.btnLoadXML);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.Text = "Generateur questions format XML";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpAddQuest.ResumeLayout(false);
            this.grpAddQuest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRep03;
        private System.Windows.Forms.TextBox txtRep02;
        private System.Windows.Forms.TextBox txtRep01;
        private System.Windows.Forms.CheckBox chkRep04;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbGroupe;
        private System.Windows.Forms.Button btnGenerer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbUp3;
        private System.Windows.Forms.RadioButton rdbUp2;
        private System.Windows.Forms.RadioButton rdbUp1;
        private System.Windows.Forms.GroupBox grpAddQuest;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBonneRep;
        private System.Windows.Forms.Label lblNomFichier;
        private System.Windows.Forms.RadioButton rdbRep03;
        private System.Windows.Forms.RadioButton rdbRep02;
        private System.Windows.Forms.RadioButton rdbRep01;
        private System.Windows.Forms.Button btnCreateXML;
        private System.Windows.Forms.Button btnMajQuest;
        private System.Windows.Forms.Button btnUpdate;
    }
}

