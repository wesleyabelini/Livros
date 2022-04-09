namespace Livros
{
    partial class fLivros
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvBook = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.excluirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uRLDownloadFormatoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.livroEPUBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.livroMOBIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.livroTodosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tituloAutorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picCapa = new System.Windows.Forms.PictureBox();
            this.txtDescricao = new System.Windows.Forms.RichTextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblAutor = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tsStatusDownload = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsUpdateDb = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTxtTitulo = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTxtAutor = new System.Windows.Forms.ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBook)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCapa)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBook
            // 
            this.dgvBook.AllowUserToAddRows = false;
            this.dgvBook.AllowUserToDeleteRows = false;
            this.dgvBook.AllowUserToResizeRows = false;
            this.dgvBook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBook.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvBook.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvBook.Location = new System.Drawing.Point(0, 276);
            this.dgvBook.Name = "dgvBook";
            this.dgvBook.RowHeadersVisible = false;
            this.dgvBook.RowHeadersWidth = 51;
            this.dgvBook.RowTemplate.Height = 24;
            this.dgvBook.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBook.Size = new System.Drawing.Size(1297, 206);
            this.dgvBook.TabIndex = 1;
            this.dgvBook.SelectionChanged += new System.EventHandler(this.dgvBook_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excluirToolStripMenuItem,
            this.selectToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 52);
            // 
            // excluirToolStripMenuItem
            // 
            this.excluirToolStripMenuItem.Name = "excluirToolStripMenuItem";
            this.excluirToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.excluirToolStripMenuItem.Text = "Excluir";
            this.excluirToolStripMenuItem.Click += new System.EventHandler(this.excluirToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uRLDownloadFormatoToolStripMenuItem,
            this.livroEPUBToolStripMenuItem,
            this.livroMOBIToolStripMenuItem,
            this.livroTodosToolStripMenuItem,
            this.tituloAutorToolStripMenuItem});
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.selectToolStripMenuItem.Text = "Select";
            // 
            // uRLDownloadFormatoToolStripMenuItem
            // 
            this.uRLDownloadFormatoToolStripMenuItem.Name = "uRLDownloadFormatoToolStripMenuItem";
            this.uRLDownloadFormatoToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.uRLDownloadFormatoToolStripMenuItem.Text = "Livro PDF";
            // 
            // livroEPUBToolStripMenuItem
            // 
            this.livroEPUBToolStripMenuItem.Name = "livroEPUBToolStripMenuItem";
            this.livroEPUBToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.livroEPUBToolStripMenuItem.Text = "Livro EPUB";
            // 
            // livroMOBIToolStripMenuItem
            // 
            this.livroMOBIToolStripMenuItem.Name = "livroMOBIToolStripMenuItem";
            this.livroMOBIToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.livroMOBIToolStripMenuItem.Text = "Livro MOBI";
            // 
            // livroTodosToolStripMenuItem
            // 
            this.livroTodosToolStripMenuItem.Name = "livroTodosToolStripMenuItem";
            this.livroTodosToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.livroTodosToolStripMenuItem.Text = "Livro - Todos";
            // 
            // tituloAutorToolStripMenuItem
            // 
            this.tituloAutorToolStripMenuItem.Name = "tituloAutorToolStripMenuItem";
            this.tituloAutorToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.tituloAutorToolStripMenuItem.Text = "Titulo - Autor";
            // 
            // picCapa
            // 
            this.picCapa.Location = new System.Drawing.Point(10, 59);
            this.picCapa.Name = "picCapa";
            this.picCapa.Size = new System.Drawing.Size(156, 211);
            this.picCapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCapa.TabIndex = 0;
            this.picCapa.TabStop = false;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(178, 103);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(1117, 167);
            this.txtDescricao.TabIndex = 1;
            this.txtDescricao.Text = "";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(173, 59);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(43, 16);
            this.lblTitulo.TabIndex = 6;
            this.lblTitulo.Text = "Título:";
            // 
            // lblAutor
            // 
            this.lblAutor.AutoSize = true;
            this.lblAutor.Location = new System.Drawing.Point(175, 77);
            this.lblAutor.Name = "lblAutor";
            this.lblAutor.Size = new System.Drawing.Size(41, 16);
            this.lblAutor.TabIndex = 7;
            this.lblAutor.Text = "Autor:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.tsStatusDownload});
            this.statusStrip1.Location = new System.Drawing.Point(0, 485);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1307, 26);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 18);
            // 
            // tsStatusDownload
            // 
            this.tsStatusDownload.Name = "tsStatusDownload";
            this.tsStatusDownload.Size = new System.Drawing.Size(0, 20);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1307, 28);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsUpdateDb});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // tsUpdateDb
            // 
            this.tsUpdateDb.Name = "tsUpdateDb";
            this.tsUpdateDb.Size = new System.Drawing.Size(208, 26);
            this.tsUpdateDb.Text = "Update Database";
            this.tsUpdateDb.Click += new System.EventHandler(this.tsUpdateDb_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTxtTitulo,
            this.toolStripLabel2,
            this.toolStripTxtAutor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1307, 27);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(47, 24);
            this.toolStripLabel1.Text = "Título";
            // 
            // toolStripTxtTitulo
            // 
            this.toolStripTxtTitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTxtTitulo.Name = "toolStripTxtTitulo";
            this.toolStripTxtTitulo.Size = new System.Drawing.Size(100, 27);
            this.toolStripTxtTitulo.TextChanged += new System.EventHandler(this.toolStripTxtTitulo_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(46, 24);
            this.toolStripLabel2.Text = "Autor";
            // 
            // toolStripTxtAutor
            // 
            this.toolStripTxtAutor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTxtAutor.Name = "toolStripTxtAutor";
            this.toolStripTxtAutor.Size = new System.Drawing.Size(100, 27);
            this.toolStripTxtAutor.TextChanged += new System.EventHandler(this.toolStripTxtAutor_TextChanged);
            // 
            // fLivros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1307, 511);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvBook);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblAutor);
            this.Controls.Add(this.picCapa);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fLivros";
            this.Text = "Livros";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBook)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCapa)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvBook;
        private System.Windows.Forms.PictureBox picCapa;
        private System.Windows.Forms.RichTextBox txtDescricao;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblAutor;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusDownload;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsUpdateDb;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTxtTitulo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem excluirToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTxtAutor;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uRLDownloadFormatoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem livroEPUBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem livroMOBIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem livroTodosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tituloAutorToolStripMenuItem;
    }
}

