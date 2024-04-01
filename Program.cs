using System;

namespace dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            //ChestChallenge();
            //SoupChallenge();
            ArrowChallenge();
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

        private static void ArrowChallenge()
        {
            ArrowHeadMaterials arrowHeadMaterial = GetEnumSelection<ArrowHeadMaterials>("arrowhead material");
            FletchingMaterials fletchingMaterial = GetEnumSelection<FletchingMaterials>("fletching material");
            int shaftLength = GetIntInputFromRange(10, 60, "shaft length");
            Arrow arrow = new Arrow(arrowHeadMaterial, fletchingMaterial, shaftLength);

            arrow.GetArrowDescription();
            Console.WriteLine($"The cost of the arrow is {arrow.GetArrowCost()}");
        }

        private static int GetIntInputFromRange(int min, int max, string message)
        {
            int maxTries = 2;
            int count = 0;
            while (count < maxTries)
            {
                Console.Write($"\nProvide a number for the {message}. The number should be in between {min} and {max}. ");
                if (int.TryParse(Console.ReadLine(), out int response))
                {
                    if (response <= max && response >= min)
                    {
                        return response;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that's not a valid input");
                        count++;
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, that's not a valid input");
                    count++;
                    continue;
                }
            }
            Console.WriteLine("Let me choose a value for you.");
            int randomNumber = 45;
            Console.Write($"The {message} is {randomNumber}.\n");
            return randomNumber;
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


        class Arrow
        {
            // Tuples for the arrowhead, fletching and shaft. Idea is to capture the material and cost of each component.
            (ArrowHeadMaterials arrowHeadMaterial, float arrowHeadCost) _arrowHead;
            (FletchingMaterials fletchingMaterial, float fletchingCost) _fletching;
            (int shaftLength, float shaftLengthCostPerCM) _shaft;

            // Default constructor. Sets the default values for the arrowhead, fletching and shaft.
            public Arrow() : this(ArrowHeadMaterials.Steel, FletchingMaterials.Plastic, 45)
            { }

            // Constructor with parameters to set the arrowhead, fletching and shaft.
            public Arrow(ArrowHeadMaterials arrowHeadMaterial, FletchingMaterials fletchingMaterial, int shaftLength)
            {
                _arrowHead.arrowHeadMaterial = arrowHeadMaterial;
                _arrowHead.arrowHeadCost = SetArrowHeadCost(_arrowHead.arrowHeadMaterial);

                _fletching.fletchingMaterial = fletchingMaterial;
                _fletching.fletchingCost = SetFletchingCost(_fletching.fletchingMaterial);

                _shaft.shaftLength = shaftLength;
                _shaft.shaftLengthCostPerCM = 0.05f;

            }

            // Set the cost of the arrowhead based on the material.
            public float SetArrowHeadCost(ArrowHeadMaterials arrowHeadMaterial)
            {
                return arrowHeadMaterial switch
                {
                    ArrowHeadMaterials.Steel => 10f,
                    ArrowHeadMaterials.Wood => 3f,
                    ArrowHeadMaterials.Obsidian => 5f,
                    _ => throw new ArgumentOutOfRangeException(nameof(arrowHeadMaterial), arrowHeadMaterial, "Invalid arrow head material.")
                };
            }

            // Set the cost of the fletching based on the material.
            public float SetFletchingCost(FletchingMaterials fletchingMaterial)
            {
                return fletchingMaterial switch
                {
                    FletchingMaterials.Plastic => 10f,
                    FletchingMaterials.Turkey_Feather => 5f,
                    FletchingMaterials.Goose_Feather => 3f,
                    _ => throw new ArgumentOutOfRangeException(nameof(fletchingMaterial), fletchingMaterial, "Invalid fletching material.")
                };
            }

            // Calculate the cost of the arrow based on the cost of the arrowhead, fletching and shaft.
            public float GetArrowCost()
            {
                return _arrowHead.arrowHeadCost + _fletching.fletchingCost + _shaft.shaftLength * _shaft.shaftLengthCostPerCM;
            }

            public void GetArrowDescription()
            {
                Console.WriteLine($"The arrow is constructed as follows:\n{(_arrowHead.arrowHeadMaterial)} arrowhead\n{_fletching.fletchingMaterial} fletching\n{_shaft.shaftLength} cm shaft.");
            }
        }

        enum ArrowHeadMaterials
        {
            Steel,
            Wood,
            Obsidian
        }

        enum FletchingMaterials
        {
            Plastic,
            Turkey_Feather,
            Goose_Feather
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
