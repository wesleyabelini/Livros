using LivrosBot.CognitiveModels;
using LivrosBot.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LivrosBot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        //private readonly FlightBookingRecognizer _luisRecognizer;
        private readonly BookRecognizer _luisRecognizer;
        protected readonly ILogger Logger;

        List<Book> allBook = new List<Book>();
        private string searshBy = String.Empty;

        public MainDialog(BookRecognizer luisRecognizer, BookDialog bookDialog, ILogger<MainDialog> logger)
            : base(nameof(MainDialog))
        {
            _luisRecognizer = luisRecognizer;
            Logger = logger;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(bookDialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                SearchByAutor,
                WriteSinopseToSearch,
                CompareSearchSinopse
            }));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> SearchByAutor(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var luisResult = await _luisRecognizer.RecognizeAsync<BookIntent>(stepContext.Context, cancellationToken);

            using (LivroContext context = new LivroContext())
            {
                allBook = context.Books.Where(x=> x.NomeAutor.Contains(luisResult.Text)).ToList();
            }

            if (allBook.Count() == 0)
            {
                var messageText = stepContext.Options?.ToString() ?? "Não consegui encontrar nenhum o autor '" + luisResult.Text + "'. Por favor, tente por nome ou sobrenome.";
                var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
                await stepContext.Context.SendActivityAsync(promptMessage, cancellationToken);
                stepContext.ActiveDialog.State["stepIndex"] = (int)stepContext.ActiveDialog.State["stepIndex"] - 2;
            }
            else if (allBook.Select(x => x.NomeAutor.Replace(" ", "")).ToList().Distinct().Count() > 1)
            {
                
            }
            else
            {
                var adaptiveCardJson = File.ReadAllText(@".\Cards/findBooks.json");
                JObject json = JObject.Parse(adaptiveCardJson);

                JArray body = (JArray)json["body"];

                for(int i = 0; i < allBook.Count(); i++)
                {
                    string s = "{" +
                                    "\"type\": \"ColumnSet\"," +
                                    "\"columns\": [" +
                                        "{" +
                                            "\"type\": \"Column\"," +
                                            "\"width\": \"10px\"" +
                                        "}," +
                                        "{" +
                                            "\"type\": \"Column\"," +
                                            "\"width\": \"50px\"," +
                                            "\"items\": [" +
                                                "{" +
                                                    "\"type\": \"Image\"," +
                                                    "\"url\": \"" + allBook[i].UrlCapa + "\"," +
                                                    "\"horizontalAlignment\": \"Center\"," +
                                                    "\"size\": \"Small\"" +
                                                "}" +
                                            "]," +
                                            "\"height\": \"stretch\"," +
                                            "\"horizontalAlignment\": \"Center\"" +
                                        "}," +
                                        "{" +
                                            "\"type\": \"Column\"," +
                                            "\"width\": \"stretch\"," +
                                            "\"items\": [" +
                                                "{" +
                                                    "\"type\": \"TextBlock\"," +
                                                    "\"text\": \"" + allBook[i].NomeLivro + "\"," +
                                                    "\"wrap\": true," +
                                                    "\"weight\": \"Bolder\"" +
                                                "}," +
                                                "{" +
                                                    "\"type\": \"TextBlock\"," +
                                                    "\"text\": \"" + allBook[i].NomeAutor + "\"," +
                                                    "\"wrap\": true," +
                                                    "\"isSubtle\": true," +
                                                    "\"size\": \"Small\"" +
                                                "}" +
                                            "]" +
                                        "}" +
                                     "]," +

                                     "\"separator\": true," +
                                     "\"selectAction\": {" +
                                        "\"type\" : \"Action.Submit\"," +
                                        "\"data\": \"" + allBook[i].Id + "\"," +
                                     "}" +
                                  "}";

                    JToken k = JObject.Parse(s);
                    json["body"][i].AddAfterSelf(k);
                }

                var adaptiveCardAttach = new Attachment()
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(json.ToString()),
                };

                stepContext.Values.Add("autor", allBook.First().NomeAutor);

                var response = MessageFactory.Attachment(adaptiveCardAttach, ssml: "Lista Book Criada");
                await stepContext.Context.SendActivityAsync(response, cancellationToken);
            }

            return new DialogTurnResult(DialogTurnStatus.Waiting);
        }

        private async Task<DialogTurnResult> WriteSinopseToSearch(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            int idBook;
            bool IsSelectedBook = int.TryParse(stepContext.Context.Activity.Text, out idBook);

            if (!IsSelectedBook)
            {
                var adaptiveCardJson = File.ReadAllText(@".\Cards/wroteSinopse.json");
                JObject json = JObject.Parse(adaptiveCardJson);

                var adaptiveCardAttach = new Attachment()
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(json.ToString()),
                };

                var response = MessageFactory.Attachment(adaptiveCardAttach, ssml: "Write Sinopse");
                await stepContext.Context.SendActivityAsync(response, cancellationToken);

                return new DialogTurnResult(DialogTurnStatus.Waiting);
            }
            else if(idBook > 0)
            {
                Book myBook = allBook.Where(x=> x.Id == idBook).FirstOrDefault();

                var adaptiveCardJson = File.ReadAllText(@".\Cards/book.json");
                JObject json = JObject.Parse(adaptiveCardJson);

                JArray body = (JArray)json["body"];

                body[0]["columns"][0]["items"][0]["url"] = myBook.UrlCapa;
                body[0]["columns"][1]["items"][0]["text"] = myBook.NomeLivro;
                body[0]["columns"][1]["items"][1]["text"] = myBook.NomeAutor;
                body[1]["text"] = myBook.Descricao;

                var adaptiveCardAttach = new Attachment()
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(json.ToString()),
                };

                var response = MessageFactory.Attachment(adaptiveCardAttach, ssml: "Lista Book");
                await stepContext.Context.SendActivityAsync(response, cancellationToken);
            }

            return new DialogTurnResult(DialogTurnStatus.Waiting);
        }

        private async Task<DialogTurnResult> CompareSearchSinopse(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string msg = stepContext.Context.Activity.Text;
            string matrix = String.Empty;
            List<Sequencia> mySeq = new List<Sequencia>();

            using (var conn = new SqlConnection("Data Source = DellNote; Initial Catalog = Livro; Integrated Security = True"))
            {
                conn.Open();
                using (var cmd = new SqlCommand("Exec dbo.GETMATRIX '" + msg + "';", conn))
                {
                    do
                    {
                        Task t = Task.Run(async () => { matrix = await some(cmd); });
                        t.Wait();
                    }
                    while (String.IsNullOrEmpty(matrix));
                }

                conn.Close();
            }

            using (var conn2 = new SqlConnection("Data Source = DellNote; Initial Catalog = Livro; Integrated Security = True"))
            {
                conn2.Open();
                using (var cmdSimilarity = new SqlCommand("Exec dbo.Similarity '" + matrix + "'", conn2))
                {
                    do
                    {
                        Task j = Task.Run(async () => { mySeq = await getSeq(cmdSimilarity); });
                        j.Wait();
                    }
                    while (mySeq.Count == 0);
                }

                conn2.Close();
            }

            if(mySeq.Count > 0)
            {
                List<Book> newlsBook = new List<Book>();

                foreach(Sequencia sequencia in mySeq) newlsBook.Add(allBook[sequencia.id]);

                allBook = new List<Book>();
                allBook.Add(newlsBook.FirstOrDefault());

                Book myBook = allBook.First();

                var adaptiveCardJson = File.ReadAllText(@".\Cards/book.json");
                JObject json = JObject.Parse(adaptiveCardJson);

                JArray body = (JArray)json["body"];

                body[0]["columns"][0]["items"][0]["url"] = myBook.UrlCapa;
                body[0]["columns"][1]["items"][0]["text"] = myBook.NomeLivro;
                body[0]["columns"][1]["items"][1]["text"] = myBook.NomeAutor;
                body[1]["text"] = myBook.Descricao;

                var adaptiveCardAttach = new Attachment()
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(json.ToString()),
                };

                var response = MessageFactory.Attachment(adaptiveCardAttach, ssml: "Lista Book");
                await stepContext.Context.SendActivityAsync(response, cancellationToken);

                return new DialogTurnResult(DialogTurnStatus.Waiting);
            }

            return new DialogTurnResult(DialogTurnStatus.Waiting);
        }

        private async Task<string> some(SqlCommand cmd)
        {
            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (reader.Read())
            {
                string matrix = reader["Matrix"].ToString().Replace("' '", ",").Replace("'\n  '", ",").Replace("'", "");
                matrix = matrix.Remove(matrix.Length - 1);

                foreach (Book b in allBook)
                {
                    string l = b.Matrix.ToString().Remove(b.Matrix.Length - 1).Remove(0, 1).Replace("' '", ",").Replace("'\n  '", ",").Replace("'", "");
                    matrix += ";" + l;
                }

                matrix += "]";

                return matrix;
            }

            return String.Empty;
        }

        private async Task<List<Sequencia>> getSeq(SqlCommand cmdSimilarity)
        {
            List<Sequencia> mySeq = new List<Sequencia>();

            SqlDataReader readerSim = await cmdSimilarity.ExecuteReaderAsync();

            while (readerSim.Read())
            {
                if (String.IsNullOrEmpty(readerSim["key"].ToString())) continue;

                Sequencia seq = new Sequencia()
                {
                    id = Convert.ToInt16(readerSim["key"]),
                    valor = Convert.ToDouble(readerSim["value"]),
                };

                mySeq.Add(seq);
            }

            return mySeq;
        }
    }
}

public class Sequencia
{
    public int id { get; set; }
    public double valor { get; set; }
}
