using HtmlAgilityPack;
using Livros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Livros
{
    public partial class fLivros : Form
    {
        public static string livrosURL = ConfigurationManager.AppSettings["url"];

        public List<string> urlReaded = new List<string>();
        public List<string> urlToRead = new List<string>();

        public List<Book> books = new List<Book>();
        public List<Book> savedBooks = new List<Book>();

        public Book currentBook = null;

        BackgroundWorker worker = new BackgroundWorker();

        public string lastURL = String.Empty;

        public List<string> urlProblema = new List<string>()
        {
            livrosURL + "/book/15813/",
        };

        public fLivros()
        {
            InitializeComponent();
            SetGridLayout();

            //ClearDesc();
            //HTMLCharacterConverter();
            //InsertMatrixOnDataBase();
        }

        private void SetGridLayout()
        {
            using (LivroEntities context = new LivroEntities())
            {
                savedBooks = (from p in context.Book
                              select new BookMin()
                              {
                                  id = p.id,
                                  NomeLivro = p.NomeLivro,
                                  NomeAutor = p.NomeAutor,
                                  Descricao = p.Descricao,
                                  URL_CAPA = p.URL_CAPA,
                                  URL_BOOK = p.URL_BOOK,
                              }).ToList().Select(x => new Book
                              {
                                  id = x.id,
                                  NomeLivro = x.NomeLivro,
                                  NomeAutor = x.NomeAutor,
                                  Descricao = x.Descricao,
                                  URL_CAPA = x.URL_CAPA,
                                  URL_BOOK = x.URL_BOOK,
                              }).ToList();

                urlReaded = savedBooks.Select(x => x.URL_BOOK).ToList();
                urlReaded.AddRange(urlProblema);
                dgvBook.DataSource = savedBooks;
            }

            foreach (DataGridViewColumn grid in dgvBook.Columns)
            {
                if (new List<string> { "id", "NomeLivro", "NomeAutor" }.Contains(grid.Name)) continue;
                grid.Visible = false;
            }

            dgvBook.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBook.Columns["NomeLivro"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvBook.Columns["NomeAutor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private List<string> FindURL(List<string> ml)
        {
            List<string> urls = new List<string>();
            int count = 1;
            foreach (string url in ml)
            {
                urls.AddRange(GetNodeFromURL(url));

                int total = urlReaded.Count + urlToRead.Count;
                int percorrido = urlReaded.Count + count;

                tsStatusDownload.Text = url;
                worker.ReportProgress((percorrido * 100) / total);

                if (url.Contains(livrosURL + "/book/") || url.Contains(livrosURL + "/page/") ||
                url.Contains(livrosURL + "/categoria")) Thread.Sleep(5000);

                count++;
            }

            urlReaded.AddRange(ml);

            urls = urls.Where(x => !urlReaded.Contains(x)).Distinct().ToList();
            urlToRead.Clear();

            return urls;
        }

        private List<string> GetNodeFromURL(string url)
        {
            lastURL = url;

            if (url.Contains(livrosURL + "/book/") || url.Contains(livrosURL + "/page/") ||
                url.Contains(livrosURL + "/categoria"))
            {
                if (url.Contains(livrosURL + livrosURL.Replace(".love", ""))) url = url.Replace(livrosURL + livrosURL, livrosURL);
                HtmlWeb web = new HtmlWeb();

                HtmlAgilityPack.HtmlDocument doc = null;

                try { doc = web.Load(url); }
                catch
                {
                    Thread.Sleep(180000);
                    doc = web.Load(url);
                }

                List<string> nodes = new List<string>();

                if (doc.DocumentNode.SelectNodes("//a") != null)
                {
                    nodes = doc.DocumentNode.SelectNodes("//a").ToList().Where(x => x.Attributes["href"] != null)
                    .Select(x => x.Attributes["href"].Value).ToList().ToList().Distinct().ToList()
                    .Where(x => x.Contains(livrosURL)).ToList();

                    nodes.AddRange(doc.DocumentNode.SelectNodes("//a").ToList().Where(x => x.Attributes["href"] != null)
                    .Select(x => x.Attributes["href"].Value).ToList().ToList().Distinct().ToList()
                    .Where(x => x.Contains("categoria") && !x.Contains("/page/")).ToList().Select(x => livrosURL + x));

                    if (url.Contains(livrosURL + "/book/") && url.Length > (livrosURL + "/book/").Length &&
                        !url.Contains(livrosURL + "/book/page/"))
                    {
                        string titulo = doc.DocumentNode.SelectNodes("//h1[@class='product_title entry-title']").First().InnerText.Replace(" &#8211; ", "|").Replace(" – ", "|");

                        List<HtmlAttributeCollection> urlDownbook = doc.DocumentNode.SelectNodes("//div[@class='links-download']").First().ChildNodes.Where(x => x.Name == "a").ToList()
                            .Select(x => x.Attributes).ToList();

                        string epub = String.Empty;
                        string pdf = String.Empty;
                        string mobi = String.Empty;

                        foreach (HtmlAttributeCollection attr in urlDownbook)
                        {
                            if (attr[1].OwnerNode.InnerHtml.Contains("epub.png")) epub = attr[1].Value;
                            else if (attr[1].OwnerNode.InnerHtml.Contains("pdf.png")) pdf = attr[1].Value;
                            else if (attr[1].OwnerNode.InnerHtml.Contains("mobi.png")) mobi = attr[1].Value;
                        }

                        string capa = String.Empty;

                        try
                        {
                            capa = doc.DocumentNode.SelectNodes("//img[@src]").ToList()
                                .Where(x => x.OuterHtml.Contains("attachment-medium")).First().Attributes
                                .Where(x => x.Name == "src").First().Value;
                        }
                        catch { capa = "error"; }

                        Book newBook = new Book()
                        {
                            NomeLivro = titulo.Split('|').First(),
                            NomeAutor = titulo.Split('|').Last(),
                            Descricao = doc.GetElementbyId("tab-description").InnerText,
                            Categoria = "",
                            URL_BOOK = url,
                            URL_CAPA = capa,
                        };

                        if (ValidateBook(newBook))
                        {
                            using (LivroEntities context = new LivroEntities())
                            {
                                context.Book.Add(newBook);
                                context.SaveChanges();
                            }
                            //books.Add(newBook);
                        }
                        else
                        {
                            //PONTO IMPORTANTE PARA FAZER REPARO MANUAL DO BOOK PARA EVITAR FALHAS AO SALVAR POSTERIORMENTE
                        }
                    }
                }
                else if (url.Contains(livrosURL + "/book/"))
                {
                    SaveDataBooks();
                    MessageBox.Show("Alerta, o firewall ativado no servidor");
                }
                else return new List<string>();

                return nodes;
            }

            return new List<string>();
        }

        private bool ValidateBook(Book book)
        {
            if (String.IsNullOrEmpty(book.NomeLivro)) return false;
            if (String.IsNullOrEmpty(book.NomeAutor)) return false;
            if (String.IsNullOrEmpty(book.Descricao)) return false;
            if (String.IsNullOrEmpty(book.URL_CAPA)) return false;
            if (String.IsNullOrEmpty(book.URL_BOOK)) return false;

            return true;
        }

        private void tsUpdateDb_Click(object sender, EventArgs e)
        {
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) MessageBox.Show("Foi encontrado algum erro durante o processo.");
            SaveDataBooks();
        }

        private void SaveDataBooks()
        {
            if (books.Count > 0)
            {
                using (LivroEntities context = new LivroEntities())
                {
                    context.Book.AddRange(books);
                    context.SaveChanges();

                    SetGridLayout();
                }
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

            List<string> nodes = GetNodeFromURL(livrosURL);

            urlToRead.Add(livrosURL + "/book/");
            urlToRead.AddRange(nodes.Where(x => x.Contains("http")));
            urlToRead.AddRange(nodes.Where(x => !x.Contains("http")).Select(x => livrosURL + x));

            while (urlToRead.Count > 0) urlToRead.AddRange(FindURL(urlToRead));
            //try
            //{

            //}
            ////catch (Exception ex) { worker.CancelAsync(); }
        }

        private void dgvBook_SelectionChanged(object sender, EventArgs e)
        {
            currentBook = CurrentBook();

            if (currentBook != null)
            {
                lblTitulo.Text = "Título: " + currentBook.NomeLivro;
                lblAutor.Text = "Autor: " + currentBook.NomeAutor;
                txtDescricao.Text = currentBook.Descricao;

                picCapa.ImageLocation = currentBook.URL_CAPA;
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgvBook.SelectedRows)
            {
                int id = Convert.ToInt32(r.Cells[0].Value);
                using (LivroEntities context = new LivroEntities())
                {
                    currentBook = context.Book.Where(x => x.id == id).FirstOrDefault();
                    context.Book.Remove(currentBook);
                    context.SaveChanges();
                }
            }

            SetGridLayout();
        }

        private Book CurrentBook()
        {
            int id = Convert.ToInt32(dgvBook.CurrentRow.Cells[0].Value);
            using (LivroEntities context = new LivroEntities())
                return context.Book.Where(x => x.id == id).FirstOrDefault();
        }

        private void toolStripTxtAutor_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(toolStripTxtAutor.Text)) SetGridLayout();
            else
            {
                using (LivroEntities context = new LivroEntities())
                {
                    List<Book> allbooks = context.Book.Where(x => x.NomeAutor.Contains(toolStripTxtAutor.Text)).ToList();
                    dgvBook.DataSource = allbooks;
                }
            }
        }

        private void toolStripTxtTitulo_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(toolStripTxtTitulo.Text)) SetGridLayout();
            else
            {
                using (LivroEntities context = new LivroEntities())
                {
                    List<Book> allbooks = context.Book.Where(x => x.NomeLivro.Contains(toolStripTxtTitulo.Text)).ToList();
                    dgvBook.DataSource = allbooks;
                }
            }
        }

        private void ClearDesc()
        {
            using (LivroEntities context = new LivroEntities())
            {
                List<Book> allbooks = context.Book.ToList();

                foreach (Book book in allbooks)
                {
                    Book b = context.Book.Where(x => x.id == book.id).FirstOrDefault();
                    b.Descricao = b.Descricao.Replace("\n\n                            Descrição do livro\n                            ", "");

                    context.SaveChanges();
                }
            }
        }

        private void HTMLCharacterConverter()
        {
            List<string> htmlCodes = new List<string>() { "&#8211;", "&#038;", "&#8220;", "&#8221;", "&#8230;", "&#8216;", "&#8217;",
                "&#8212;", "&#x2714;", "&#8243;", "&#x2665;", "&#8243;", "&#8243;" };
            using (LivroEntities context = new LivroEntities())
            {
                List<Book> allbooks = context.Book.Where(x => htmlCodes.Where(y => x.Descricao.Contains(y)).Count() > 0).ToList();

                foreach (Book book in allbooks)
                {
                    Book b = context.Book.Where(x => x.id == book.id).FirstOrDefault();
                    b.Descricao = b.Descricao.Replace("&#8211;", "-").Replace("&#038;", "&").Replace("&#8220;", "'")
                        .Replace("&#8221;", "'").Replace("&#8230;", "...").Replace("&#8216;", "'").Replace("&#8217;", "")
                        .Replace("&#8212;", "-").Replace("&#x2714;", "").Replace("&#8243;", "'").Replace("&#x2665;", "-")
                        .Replace("&#8243;", "'").Replace("&#8243;", "'");

                    context.SaveChanges();
                }
            }
        }

        private void InsertMatrixOnDataBase()
        {
            using (LivroEntities context = new LivroEntities())
            {
                List<Book> allbooks = context.Book.Where(x => String.IsNullOrEmpty(x.Matrix)).OrderBy(x => x.id).ToList();

                int cont = 1;
                int myfilecount = 1;
                int firstId = allbooks[0].id;

                foreach (var line in File.ReadLines(@"C:\Users\w_vic\pythonfiles\foo.csv"))
                {
                    if (myfilecount < firstId)
                    {
                        myfilecount++;
                        continue;
                    }

                    List<string> lstToForat = line.Split(',').ToList().Select(x => "'" + x + "'").ToList();
                    string myMx = "[[" + String.Join(" ", lstToForat) + "]]";

                    allbooks[cont - 1].Matrix = myMx;
                    context.SaveChanges();

                    cont++;
                }
            }
        }
    }

    public class BookMin
    {
        public int id { get; set; }
        public string NomeLivro { get; set; }
        public string NomeAutor { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string URL_CAPA { get; set; }
        public string URL_BOOK { get; set; }
        public string EPUB { get; set; }
        public string PDF { get; set; }
        public string MOBI { get; set; }
    }
}
