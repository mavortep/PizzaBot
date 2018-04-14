using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaBot.Models
{
    public enum SizeOptions
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }

    public enum PizzaOptions
    {
        Neapolitan,
        Mozzarella,
        Pepperoni,
        Havaiian,
        FourCheese
    }

    [Serializable]
    public class PizzaQuery
    {
        public SizeOptions? Size;
        public PizzaOptions? Pizza;
        public DateTime? DeliveryDate;
        public string Address;
    }
}