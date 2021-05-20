using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelData
{
    public static ArrayList[] levelOrders = new ArrayList[]
    {
        new ArrayList () { "DishWithRice" }, //level 0
        new ArrayList () { "FriedEggWithDish_State2", "FriedEggWithDish_State3" }, //level 1
        new ArrayList () { "FriedEggWithRice_State2", "FriedEggWithRice_State3" }, //level 2
        new ArrayList () { "DishWithRice" , "FriedEggWithDish_State2", "FriedEggWithDish_State3" , "FriedEggWithRice_State2" , "FriedEggWithRice_State3" }, //level 3
        new ArrayList () { "KaPrao" }, //level 4
        new ArrayList () { "KaPrao" , "KaPraoEgg_State2" , "KaPraoEgg_State3" }, //level 5
        new ArrayList () { "KaPrao" , "KaPraoEgg_State2" , "KaPraoEgg_State3", "DishWithRice" , "FriedEggWithDish_State2" , "FriedEggWithDish_State3", "FriedEggWithRice_State2" , "FriedEggWithRice_State3" }, //level 6
        new ArrayList () { "BowlofTomYumKung" }, //level 7
        new ArrayList () { "KaPrao" , "KaPraoEgg_State2" , "KaPraoEgg_State3", "DishWithRice" , "FriedEggWithDish_State2" , "FriedEggWithDish_State3", "FriedEggWithRice_State2" , "FriedEggWithRice_State3" , "BowlofTomYumKung" }, //level 8
        new ArrayList () { "KaPrao" , "KaPraoEgg_State2" , "KaPraoEgg_State3", "DishWithRice" , "FriedEggWithDish_State2" , "FriedEggWithDish_State3", "FriedEggWithRice_State2" , "FriedEggWithRice_State3" , "BowlofTomYumKung" }, //level 9
    };

    public static ArrayList[] levelDetails = new ArrayList[]
    {
        //      {level pass score , time remain}
        new ArrayList () {20f, 60f},
        new ArrayList () {120f, 170f}, // level 1
        new ArrayList () {200f, 190f}, // level 2
        new ArrayList () {140f, 200f}, // level 3
        new ArrayList () {225f, 205f}, // level 4
        new ArrayList () {250f, 225f}, // level 5
        new ArrayList () {200f, 215f}, // level 6
        new ArrayList () {125f, 160f}, // level 7
        new ArrayList () {205f, 190f}, // level 8
        new ArrayList () {0f, 15f}, // level 9
    };

    public static Dictionary<string, float> orderTimeRemain = new Dictionary<string, float>()
    {
        {"DishWithRice", 35f},
        {"FriedEggWithDish_State2", 60f},
        {"FriedEggWithDish_State3", 60f},
        {"FriedEggWithRice_State2", 70f},
        {"FriedEggWithRice_State3", 70f},
        {"KaPrao", 80f},
        {"KaPraoEgg_State2", 100f},
        {"KaPraoEgg_State3", 100f},
        {"BowlofTomYumKung", 100f},
    };

    public static Dictionary<string, float> orderScores = new Dictionary<string, float>()
    {
        {"FriedEggWithDish_State2(Clone)", 30f},
        {"FriedEggWithDish_State3(Clone)", 30f},
        {"FriedEggWithRice_State2(Clone)", 50f},
        {"FriedEggWithRice_State3(Clone)", 50f},
        {"DishWithRice(Clone)", 5f},
        {"KaPrao", 75f },
        {"KaPraoEgg_State2(Clone)", 100f },
        {"KaPraoEgg_State3(Clone)", 100f },
        {"BowlofTomYumKung", 50f},
    };

    public static Dictionary<string, string> foodNameTranslator = new Dictionary<string, string>()
    {
        {"Egg_State1(Clone)", "ไข่ดิบ"},
        {"Egg_State2(Clone)", "ไข่ดาวไม่สุก"},
        {"Egg_State3(Clone)", "ไข่ดาวสุก" },
        {"Egg_State4(Clone)", "ไข่ดาวไหม้" }
    };

    public static ArrayList[][] levelOrdersFixed = new ArrayList[][]
    {
        new ArrayList [] //level 0
        {             //     {TimeComing, List of Order, TimeCountdown of this order}
            new ArrayList () {0f, new List<string> { "DishWithRice"}, 35f } ,
        },
        new ArrayList [] //level 1
        {
            new ArrayList () {0f, new List<string> { "FriedEggWithDish_State2" }, 60f } ,
            new ArrayList () {10f, new List<string> { "FriedEggWithDish_State3" }, 60f } ,
            new ArrayList () {20f, new List<string> { "FriedEggWithDish_State2" }, 60f } ,
            new ArrayList () {30f, new List<string> { "FriedEggWithDish_State3" }, 60f } ,
            new ArrayList () {40f, new List<string> { "FriedEggWithDish_State2", "FriedEggWithDish_State2" }, 120f } ,
            new ArrayList () {50f, new List<string> { "FriedEggWithDish_State3", "FriedEggWithDish_State3", }, 120f } ,
        },
        new ArrayList [] //level 2
        {
            new ArrayList () {0f, new List<string> { "FriedEggWithRice_State2" }, 70f } ,
            new ArrayList () {10f, new List<string> { "FriedEggWithRice_State3" }, 70f } ,
            new ArrayList () {20f, new List<string> { "FriedEggWithRice_State2" }, 70f } ,
            new ArrayList () {30f, new List<string> { "FriedEggWithRice_State3" }, 70f } ,
            new ArrayList () {40f, new List<string> { "FriedEggWithRice_State2" , "FriedEggWithRice_State2" }, 140f } ,
            new ArrayList () {50f, new List<string> { "FriedEggWithRice_State3" , "FriedEggWithRice_State3" }, 140f } ,
        },
        new ArrayList [] // level 3
        {
            new ArrayList () {0f, new List<string> { "DishWithRice", "DishWithRice" }, 70f } ,
            new ArrayList () {10f, new List<string> { "FriedEggWithDish_State2" }, 60f } ,
            new ArrayList () {20f, new List<string> { "FriedEggWithDish_State3" }, 60f } ,
            new ArrayList () {30f, new List<string> { "FriedEggWithDish_State2", "FriedEggWithDish_State3" }, 120f } ,
            new ArrayList () {50f, new List<string> { "FriedEggWithRice_State3" }, 70f } ,
            new ArrayList () {60f, new List<string> { "FriedEggWithRice_State3", "FriedEggWithRice_State2" }, 140f } ,
        },
        new ArrayList [] // level 4
        {
            new ArrayList () {0f, new List<string> { "KaPrao" }, 80f } ,
            new ArrayList () {15f, new List<string> { "KaPrao" }, 80f } ,
            new ArrayList () {30f, new List<string> { "KaPrao" }, 80f } ,
            new ArrayList () {45f, new List<string> { "KaPrao", "KaPrao" }, 160f } ,
            new ArrayList () {55f, new List<string> { "KaPrao" }, 80f } ,
        },
        new ArrayList [] // level 5
        {
            new ArrayList () {0f, new List<string> { "KaPrao" }, 80f } ,
            new ArrayList () {15f, new List<string> { "KaPrao" }, 80f } ,
            new ArrayList () {30f, new List<string> { "KaPrao", "KaPraoEgg_State2" }, 180f } ,
            new ArrayList () {45f, new List<string> { "KaPrao", "KaPraoEgg_State2" }, 180f } ,
        },
        new ArrayList [] // level 6
        {
            new ArrayList () {0f, new List<string> { "DishWithRice", "DishWithRice" }, 70f } ,
            new ArrayList () {10f, new List<string> { "FriedEggWithRice_State2" , "KaPrao" }, 150f } ,
            new ArrayList () {30f, new List<string> { "FriedEggWithDish_State3", "KaPraoEgg_State2" }, 160f } ,
            new ArrayList () {45f, new List<string> { "DishWithRice", "DishWithRice", "KaPraoEgg_State3" }, 170f } ,
            new ArrayList () {60f, new List<string> { "FriedEggWithDish_State3" }, 60f } ,
        },
        new ArrayList [] // level 7
        {
            new ArrayList () {0f, new List<string> { "BowlofTomYumKung" }, 100f } ,
            new ArrayList () {15f, new List<string> { "BowlofTomYumKung" }, 100f } ,
            new ArrayList () {30f, new List<string> { "BowlofTomYumKung" }, 100f } ,
            new ArrayList () {45f, new List<string> { "BowlofTomYumKung" }, 100f } ,
            new ArrayList () {60f, new List<string> { "BowlofTomYumKung" }, 100f } ,
        },
        new ArrayList [] // level 8
        {
            new ArrayList () {0f, new List<string> { "BowlofTomYumKung" , "DishWithRice" }, 135f } ,
            new ArrayList () {15f, new List<string> { "FriedEggWithRice_State2" , "KaPrao" }, 150f } ,
            new ArrayList () {30f, new List<string> { "KaPraoEgg_State3" , "FriedEggWithDish_State2" }, 160f } ,
            new ArrayList () {45f, new List<string> { "BowlofTomYumKung" }, 100f } ,
            new ArrayList () {60f, new List<string> { "BowlofTomYumKung" }, 100f } ,
        },
        new ArrayList [] // level 9 All Random
        {
            new ArrayList () {0f, new List<string> { "DishWithRice" }, 35f } ,
        },
    };
}

