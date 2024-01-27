using System;
using System.Text.RegularExpressions;

namespace Work6_6
{
    class Program
    {
        private Menu menu;
        private Repository _repository;
        
        public static void Main(string[] args)
        {
            Program task = new Program();
        }

        public Program()
        {
            _repository = new Repository();
            menu = new Menu();
            menu.ShowMenu();
        }
    }

    class Menu
    {
        public static Action onAddNewWorker;
        public static Action onAddTestWorker;
        public static Action onShowAllWorkers;
        public static Action onSelectWorkers;
        public static Action onRemoveWorker;
        
        private static string help = "Menu:\n" +
            "1 - add new worker\n" +
            "2 - Show all worker\n" +
            "3 - add default worker\n" +
            "4 - remove worker\n" +
            "5 - select workers\n";
        
        public void ShowMenu()
        {
            int command = GetParam(help);

            switch (command)
            {
                case 1:
                    onAddNewWorker?.Invoke();
                    break;
                case 2:
                    onShowAllWorkers?.Invoke();
                    break;
                case 3:
                    onAddTestWorker?.Invoke();
                    break;
                case 4:
                    onRemoveWorker?.Invoke();
                    break;
                case 5:
                    onSelectWorkers?.Invoke();
                    break;
            }
            
            ShowMenu();
        }

        public static string GetStringFromConsole(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        
        private int GetParam(string message)
        {
            
            int command = -1;
            try
            {
                command = Convert.ToInt32(GetStringFromConsole(message));
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid command.");
            }

            return command;
        }
    }
}