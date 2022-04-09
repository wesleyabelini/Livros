using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace LivrosBot.Dialogs
{
    public class BookDialog : CancelAndHelpDialog
    {
        private const string whatBook = "Que livro você gostaria de ler?\r\nDigite o nome do Autor do livro ou conte alguma coisa que tentaremos encontrar o livro para você.";

        public BookDialog(): base(nameof(BookDialog))
        {
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                FindBookStepAsync,
                //OriginStepAsync,
                //TravelDateStepAsync,
                //ConfirmStepAsync,
                //FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> FindBookStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var bookDetails = (BookDetails)stepContext.Options;

            if (bookDetails.NomeLivro == null)
            {
                var promptMessage = MessageFactory.Text(whatBook, whatBook, InputHints.ExpectingInput);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            }

            return await stepContext.NextAsync(bookDetails.NomeLivro, cancellationToken);
        }
    }
}
