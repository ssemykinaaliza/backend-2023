using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FoodDelivery.Models.Enum
{
    public enum DishCategory
    {
        Wok = 0,
        Pizza = 1,
        Soup = 2,
        Dessert = 3,
        Drink = 4
    }
}
