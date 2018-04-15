using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace PizzaBot.Dialogs
{
    [Serializable]
    public class DiscountDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();
            await context.PostAsync("How lucky are you today?");
            var randomDiscount = new Random().Next(0, 20);
            string discountText = null;

            if (randomDiscount > 10)
            {
                discountText = $"Congratulations! Your luck is high today. We give you a discount in {randomDiscount}% for the next order! ";
            }
            else
            {
                discountText = $"Next time it will be better. We give you a discount in {randomDiscount}% for the next order. ";
            }

            List<CardAction> cardButtons = new List<CardAction>();
            HeroCard plCard = new HeroCard()
            {
                Title = $"Your discount",
                Text = discountText,
                Buttons = cardButtons,
                Images = new List<CardImage> { new CardImage("https://i.pinimg.com/564x/cd/f3/a2/cdf3a2a17d5da7b27c62436fdc3a56db.jpg") }
            };
            Attachment plAttachment = plCard.ToAttachment();
            message.Attachments.Add(plAttachment);

            await context.PostAsync(message);
            context.Done<object>(null);
        }
    }
}