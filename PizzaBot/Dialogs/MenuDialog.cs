using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using PizzaBot.Models;
using PizzaBot.WebClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PizzaBot.Dialogs
{
    [Serializable]
    public class MenuDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();
            var products = await ApiClient.GetProductAsync("/api/Products");
            await context.PostAsync("There are products");

            foreach (Product product in products)
            {
                List<CardAction> cardButtons = new List<CardAction>();
                HeroCard plCard = new HeroCard()
                {
                    Title = $"{product.Name} {product.Category}\n\n {product.Price}$\n\n",
                    Text = $"{product.Description}",
                    Buttons = cardButtons,
                    Images = new List<CardImage> { new CardImage(product.ImagePath)}
                };
                Attachment plAttachment = plCard.ToAttachment();
                message.Attachments.Add(plAttachment);
            }
            await context.PostAsync(message);
            context.Done<object>(null);
        }
    }
}