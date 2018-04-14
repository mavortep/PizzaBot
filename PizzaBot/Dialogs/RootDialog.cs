using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace PizzaBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string MenuOption = "Menu";

        private const string ProductOption = "Order";

        private const string TrackingOption = "Tracking";

        private const string DiscountOption = "Discount";

        private const string RecommendationOption = "Recommendation";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to the pizza bot!");

            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text.ToLower().Contains("help") || message.Text.ToLower().Contains("support"))
            {
                await context.Forward(new OrderDialog(), this.ResumeAfterProductDialog, message, CancellationToken.None);
            }
            else
            {
                this.ShowOptions(context);
            }
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { MenuOption, ProductOption, TrackingOption, DiscountOption, RecommendationOption }, "Please choose the option from the following: ", "Not a valid option", 6);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            string optionSelected = await result;

            switch (optionSelected)
            {
                //case MenuOption:
                //    context.Call(new MenuDialog(), this.ResumeAfterOptionDialog);
                //    break;

                case MenuOption:
                    context.Call(new ProductDialog(), this.ResumeAfterOptionDialog);
                    break;

                    //case TrackingOption:
                    //    context.Call(new TrackingDialog(), this.ResumeAfterOptionDialog);
                    //    break;

                    //case DiscountOption:
                    //    context.Call(new DiscountDialog(), this.ResumeAfterOptionDialog);
                    //    break;

                    //case RecommendationOption:
                    //    context.Call(new RecommendationDialog(), this.ResumeAfterOptionDialog);
                    //    break;
            }
        }

        private async Task ResumeAfterProductDialog(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("resume after product dialog");
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}