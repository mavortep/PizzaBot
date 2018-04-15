using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using PizzaBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PizzaBot.Dialogs
{
    [Serializable]
    public class OrderDialog : IDialog<object>
    {
        private readonly BuildFormDelegate<PizzaQuery> MakePizzaForm;

        public OrderDialog()
        {

        }

        internal OrderDialog(BuildFormDelegate<PizzaQuery> makePizzaForm)
        {
            this.MakePizzaForm = makePizzaForm;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var pizzaFormDialog = FormDialog.FromForm(this.BuildForm, FormOptions.PromptInStart);

            context.Call(pizzaFormDialog, this.ResumeAfterPizzaFormDialog);
        }

        private async Task ResumeAfterPizzaFormDialog(IDialogContext context, IAwaitable<PizzaQuery> result)
        {
            await context.PostAsync("Type anything to go to the main menu");
            context.Done<object>(null);
        }

        private IForm<PizzaQuery> BuildForm()
        {
            OnCompletionAsyncDelegate<PizzaQuery> processOrder = async (context, state) =>
            {
                await context.PostAsync($"We are processing your query.");
            };

            return new FormBuilder<PizzaQuery>()
                .Message("Which size do you want?")
                .Field(nameof(PizzaQuery.Size))
                .Field(nameof(PizzaQuery.Pizza))
                .Field(nameof(PizzaQuery.DeliveryDate))
                .Field(nameof(PizzaQuery.Address))
                .Confirm("Do you want to order your {Size} {Pizza} pizza to be sent to {Address} at {DeliveryDate}?")
                .AddRemainingFields()
                .Message("Thanks for ordering!")
                .OnCompletion(processOrder)
                .Build();
        }
    }
}