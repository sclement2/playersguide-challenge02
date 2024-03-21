using System;
using System.Runtime.CompilerServices;

namespace dotnetcore
{
    class Program
    {
        static void Main(string[] args)
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
                if(Enum.TryParse(response, ignoreCase: true, out ChestActions parsedAction))
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
                        if(action == ChestActions.Open)
                            state = ChestState.Open;
                        else if(action == ChestActions.Lock)
                            state = ChestState.Locked;
                        break;
                    case ChestState.Locked:
                        if(action == ChestActions.Unlock)
                            state  = ChestState.Closed;
                        break;
                }
  

                iteration++;
                if(iteration >= maxIteration || action == ChestActions.Exit)
                    break;

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
    }
}
