﻿namespace RdbSem
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedList_Tabulky = new System.Windows.Forms.CheckedListBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonKontrola = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedList_Tabulky
            // 
            this.checkedList_Tabulky.FormattingEnabled = true;
            this.checkedList_Tabulky.Items.AddRange(new object[] {
            "Autobus",
            "Jizda",
            "Jizdenka",
            "Klient",
            "Kontakt",
            "Lokalita",
            "Ridic",
            "Trasy",
            "TypKontaktu",
            "Znacka"});
            this.checkedList_Tabulky.Location = new System.Drawing.Point(150, 94);
            this.checkedList_Tabulky.Name = "checkedList_Tabulky";
            this.checkedList_Tabulky.Size = new System.Drawing.Size(125, 169);
            this.checkedList_Tabulky.TabIndex = 1;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(418, 127);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 2;
            this.buttonExport.Text = "buttonExport";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonKontrola
            // 
            this.buttonKontrola.Location = new System.Drawing.Point(363, 214);
            this.buttonKontrola.Name = "buttonKontrola";
            this.buttonKontrola.Size = new System.Drawing.Size(75, 22);
            this.buttonKontrola.TabIndex = 3;
            this.buttonKontrola.Text = "buttonKontrola";
            this.buttonKontrola.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonKontrola);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.checkedList_Tabulky);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckedListBox checkedList_Tabulky;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonKontrola;
    }
}
