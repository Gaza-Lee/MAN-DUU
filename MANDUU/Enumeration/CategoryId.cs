using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Enumeration
{ 
    //Not currenly in use
    public enum CategoryId : short
    {
        // Fashion
        Fashion = 1,
        Clothing = 2,
        Footwear = 3,
        Hats = 4,
        Accessories = 5,

        // Grocery
        Grocery = 10,
        FruitsAndVegetables = 11,
        DairyProducts = 12,

        // Beauty
        Beauty = 20,
        Makeup = 21,
        HairCare = 22,
        SkinCare = 23,
        Nails = 24,
        Fragrances = 25,
        BeautyToolsAndAccessories = 26,
        BathAndBody = 27,
        HealthAndWellness = 28,

        // Food
        Food = 30,
        LocalFoods = 31,
        Continental = 32,
        Drinks = 33,
        Pastries = 34,
        Snacks = 35,

        // Electronics
        Electronics = 40,
        MobilePhones = 41,
        Laptops = 42,
        ElectronicsAccessories = 43,
        Appliances = 44,
        Gaming = 45,
        Tablets = 46
    }
}

