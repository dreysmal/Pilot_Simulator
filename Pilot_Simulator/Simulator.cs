using System;
using static System.Console;

namespace Pilot_Simulator
{
    class Simulator
    {
        static bool flag1, flag2;
        static int penaltyAll = 0;
        bool thousand;
        Plane plane = new Plane();
        
        public void Start()
        {
            SetWindowSize(70, 25);
            SetBufferSize(70, 25);
            Title = "Pilot Simulator";
            
            WriteLine("\t\t\tWelcome to Pilot Simulator\n\n\nPlese provide two dispatchers:");
            Write("\nDispatcher #1: ");
            Dispatch dispatcher1 = new Dispatch(ReadLine());
            Write("\nDispatcher #2: ");
            Dispatch dispatcher2 = new Dispatch(ReadLine());

            SetCursorPosition(10, 10);
            WriteLine("Controls: arrow right, left or with SHIFT - speed");
            SetCursorPosition(10, 11);
            WriteLine("Controls: arrow up, down or with SHIFT - height");
            SetCursorPosition(21, 13);
            WriteLine("Press any key to start");
            ConsoleKeyInfo keypressd = ReadKey();

            Clear();
            CursorVisible = false;
            SetCursorPosition(29, 12);
            WriteLine("Speed: " + plane.Speed + "   km per hour");
            SetCursorPosition(29, 13);
            WriteLine("Height: " + plane.Height + "   meters");

            plane.SetDispatchersToTheirPositions(dispatcher1);
            plane.SetDispatchersToTheirPositions(dispatcher2);

            /////////////////////////////////////////////
            foreach (Dispatch item in plane.dispatchers)
            {
                plane.Moving += item.Recomended_Height;

                item.Dispatch_penalties += (name, penalty) =>
                {
                    int i = 9;
                    if (!flag2)
                    {
                        i = 9;
                        flag2 = true;
                    }
                    else
                    {
                        i = 10;
                        flag2 = false;
                    }
                    SetCursorPosition(13, i);
                    Write("Penalties from " + name + ": ");
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(penalty);
                    ForegroundColor = ConsoleColor.White;
                };


                item.Dispatch_message += (name, Height_recomended) =>
                {
                    int i = 5;
                    if (!flag1)
                    {
                        i = 5;
                        flag1 = true;
                    }
                    else
                    {
                        i = 6;
                        flag1 = false;
                    }
                    SetCursorPosition(15, i);
                    Write("Recommended Height from dispatch " + name + ": ");
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(Height_recomended);
                    ForegroundColor = ConsoleColor.White;
                };

                item.Dispatch_excesses += (mes) =>
                {
                    Clear();
                    SetCursorPosition(20, 15);
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("The Simulation terminated!!!\n");
                    SetCursorPosition(20, 16);
                    WriteLine(mes);
                    SetCursorPosition(20, 24);
                    Environment.Exit(-1);
                };

                item.Dispatch_alert += () =>
                {
                    SetCursorPosition(12, 8);
                    ForegroundColor = ConsoleColor.Red;
                    Write("Your Speed is too High!!! Slow down immediately!!!");
                    ForegroundColor = ConsoleColor.White;
                };
            }
            /////////////////////////////////////////////
            while (true)
            {
                if (KeyAvailable)
                {
                    Clear();
                    ConsoleKeyInfo keypress = ReadKey();

                    switch (keypress.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if ((ConsoleModifiers.Shift & keypress.Modifiers) != 0)
                                plane.Shift(Direction.SPEEDEXTRADOWN);
                            else
                                plane.Shift(Direction.SPEEDDOWN);
                            break;
                        case ConsoleKey.RightArrow:
                            if ((ConsoleModifiers.Shift & keypress.Modifiers) != 0)
                                plane.Shift(Direction.SPEEDEXTRAUP);
                            else
                                plane.Shift(Direction.SPEEDUP);
                            break;
                        case ConsoleKey.DownArrow:
                            if ((ConsoleModifiers.Shift & keypress.Modifiers) != 0)
                                plane.Shift(Direction.HETGHTEXTRADOWN);
                            else
                                plane.Shift(Direction.HEIGHTDOWN);
                            break;
                        case ConsoleKey.UpArrow:
                            if ((ConsoleModifiers.Shift & keypress.Modifiers) != 0)
                                plane.Shift(Direction.HEIGHTEXTRAUP);                            
                            else
                                plane.Shift(Direction.HEIGHTUP);
                            break;
                        case ConsoleKey.Escape:
                            Clear();
                            SetCursorPosition(29, 11);
                            WriteLine("Good bye");
                            SetCursorPosition(20, 24);
                            Environment.Exit(-1);
                            break;
                        case ConsoleKey.D1:
                            penaltyAll += plane.dispatchers[0].Penalty;
                            Clear();
                            CursorVisible = true;
                            Write("\nEnter new dispatcher #1: "); 
                            plane.KillDispatch(1, ReadLine());
                            CursorVisible = false;
                            Clear();
                            break;
                        case ConsoleKey.D2:
                            penaltyAll += plane.dispatchers[1].Penalty;
                            Clear();
                            CursorVisible = true;
                            Write("\nEnter new dispatcher #2: ");
                            plane.KillDispatch(2, ReadLine());
                            CursorVisible = false;
                            Clear();
                            break;
                        default:
                            break;
                    }
                }

                Display_Plane_Parametrs();
                if (plane.Speed >= 1000)
                    thousand = true;
                if(thousand && plane.Speed == 0 && plane.Height == 0)
                {
                    Clear();
                    SetCursorPosition(23, 11);
                    WriteLine("Congratulations Pilot!");
                    foreach (Dispatch item in plane.dispatchers)
                    {
                        penaltyAll += item.Penalty;
                    }
                    SetCursorPosition(23, 12);
                    WriteLine("All the penalties: " + penaltyAll);
                    SetCursorPosition(20, 24);
                    Environment.Exit(-1);
                }
            }
        }

        public void Display_Plane_Parametrs()
        {
            SetCursorPosition(29, 12);
            WriteLine("Speed:  " + plane.Speed + "   km per hour");
            SetCursorPosition(29, 13);
            WriteLine("Height: " + plane.Height + "   meters");
            SetCursorPosition(19, 21);
            WriteLine("Press 1 to kill dispatcher " + plane.dispatchers[0].Name);
            SetCursorPosition(19, 22);
            WriteLine("Press 2 to kill dispatcher " + plane.dispatchers[1].Name);
            SetCursorPosition(19, 23);
            WriteLine("Press Esc to finish training");
        }
    }
}
