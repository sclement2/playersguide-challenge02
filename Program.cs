using System;

namespace dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            //ChestChallenge();
            SoupChallenge();
        }

        // Simula's Test Challenge
        private static void ChestChallenge()
        {
            ChestState state = ChestState.Open;
            ChestActions action;
            int iteration = 0;
            int maxIteration = 8;

            while (true)
            {
                Console.Write($"The chest is {state}. What do you want to do? ");
                string response = (Console.ReadLine()).ToUpper();

                // Convert response to action
                if (Enum.TryParse(response, ignoreCase: true, out ChestActions parsedAction))
                    action = parsedAction;
                else
                {
                    Console.WriteLine("That is not a valid action. Try again.");
                    continue;
                }

                switch (state)
                {
                    case ChestState.Open:
                        if (action == ChestActions.Close)
                            state = ChestState.Closed;
                        break;
                    case ChestState.Closed:
                        if (action == ChestActions.Open)
                            state = ChestState.Open;
                        else if (action == ChestActions.Lock)
                            state = ChestState.Locked;
                        break;
                    case ChestState.Locked:
                        if (action == ChestActions.Unlock)
                            state = ChestState.Closed;
                        break;
                }

                iteration++;
                if (iteration >= maxIteration || action == ChestActions.Exit)
                    break;
            }
        }

        // Simula's Soup Challenge
        private static void SoupChallenge()
        {
            (Seasonings seasoning, MainIngredients mainIngredient, Recipes recipe) meal;
            int iteration = 0;
            int maxIteration = 1;

            Console.WriteLine("Let's make a special meal just for you. ");
            Console.Write("I have many different meals to choose from. ");

            while (true)
            {

                Console.WriteLine("In order to make your meal I need to know 3 things.");
                Console.WriteLine("(1) - Type of seasoning you want");
                Console.WriteLine("(2) - Main ingredient you want");
                Console.WriteLine("(3) - Recipe you want");

                meal.seasoning = GetEnumSelection<Seasonings>("seasoning");
                meal.mainIngredient = GetEnumSelection<MainIngredients>("main ingredient");
                meal.recipe = GetEnumSelection<Recipes>("recipe");

                /*
                while (true)
                {
                    Console.WriteLine($"\nTell me what seasoning you would like. ");
                    Console.Write("Your choices are as follows: ");
                    Console.Write(GetEnumAsString(typeof(Seasonings)));

                    string seasoningResponse = Console.ReadLine();
                    if (Enum.TryParse(seasoningResponse, ignoreCase: true, out Seasonings parsedSeasoning))
                    {
                        meal.seasoning = parsedSeasoning;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that's not a valid option. Let's try this again.");
                        continue;
                    }
                }


                while (true)
                {
                    Console.WriteLine($"\nTell me what main ingedient you would like. ");
                    Console.Write("Your choices are as follows: ");
                    Console.Write(GetEnumAsString(typeof(MainIngredients)));

                    string mainIngredientResponse = Console.ReadLine();
                    if (Enum.TryParse(mainIngredientResponse, ignoreCase: true, out MainIngredients parsedMainIngredient))
                    {
                        meal.mainIngredient = parsedMainIngredient;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that's not a valid option. Let's try this again.");
                        continue;
                    }
                }

                while (true)
                {
                    Console.WriteLine($"\nTell me what recipe you would like. ");
                    Console.Write("Your choices are as follows: ");
                    Console.Write(GetEnumAsString(typeof(Recipes)));

                    string recipeResponse = Console.ReadLine();
                    if (Enum.TryParse(recipeResponse, ignoreCase: true, out Recipes parsedRecipe))
                    {
                        meal.recipe = parsedRecipe;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that's not a valid option. Let's try this again.");
                        continue;
                    }
                }
                */

                Console.Write("\nHere's your special meal - ");
                Console.Write($"{meal.seasoning} {meal.mainIngredient} {meal.recipe}\n");
                iteration++;
                if (iteration == maxIteration) break;
            }
        }

        private static string GetEnumAsString(Type enumType)
        {
            Array enumValues = Enum.GetValues(enumType);
            string enumString = "";
            for (int i = 0; i < enumValues.Length - 1; i++)
            {
                enumString += enumValues.GetValue(i).ToString() + ", ";
            }
            enumString += enumValues.GetValue(enumValues.Length - 1).ToString() + ": ";
            return enumString;
        }

        private static TEnum GetEnumSelection<TEnum>(string attributeName) where TEnum : struct, Enum
        {
            int tries = 1;
            int maxTries = 2;
            int randomIndex = 0;
            while (true)
            {
                Console.WriteLine($"\nTell me what {attributeName} you would like. ");
                Console.Write("Your choices are as follows: ");
                Console.Write(GetEnumAsString(typeof(TEnum)));

                string response = Console.ReadLine();
                if (Enum.TryParse<TEnum>(response, ignoreCase: true, out TEnum parsedValue))
                {
                    return parsedValue;
                }
                else if (tries > maxTries)
                {
                    Array enumValues = Enum.GetValues(typeof(TEnum));
                    Console.WriteLine($"OK, this does not seem to be working. I will choose a {attributeName} for you.");
                    TEnum randomChoice = (TEnum)enumValues.GetValue(randomIndex);
                    Console.WriteLine($"Your {attributeName} is {randomChoice}");
                    return randomChoice;
                }
                else
                {
                    Console.WriteLine("Sorry, that's not a valid option. Let's try this again.");
                    tries++;
                    continue;
                }
            }
        }

        enum ChestState
        {
            Open,
            Closed,
            Locked
        }

        enum ChestActions
        {
            Close,
            Open,
            Lock,
            Unlock,
            Exit
        }

        enum Recipes
        {
            Soup,
            Stew,
            Gumbo
        }

        enum MainIngredients
        {
            Mushroom,
            Chicken,
            Carrot,
            Potatoe
        }

        enum Seasonings
        {
            Spicy,
            Salty,
            Sweet
        }
    }
}
