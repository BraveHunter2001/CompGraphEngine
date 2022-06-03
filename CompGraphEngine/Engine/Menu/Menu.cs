using System;
using System.Collections.Generic;

namespace CompGraphEngine.Engine.Menu
{
    public class Menu : IMenu
    {
        readonly string selectAgainText = "Incorrect input. Try again.",
            selectText = "Please select option using number in range:",
            toPrevMenuText = "Return to previous menu",
            exitText = "Exit";
        IMenuItem[] items;
        protected string name;
        protected IMenu PrevMenu;
        public IMenu GetPrevMenu() => PrevMenu;
        public Menu(string name)
        {
            this.name = name;
        }
        public Menu(string name, IMenuItem[] items)
        {
            this.name = name;
            this.items = items;
        }

        public void Display()
        {
            
            Console.WriteLine(name);
        
        }

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine(name);
            Console.WriteLine();
            int counter = 1;
            foreach (IMenuItem item in items)
            {
                Console.Write($"[{counter++}]");
                item.Display();
            }
            Console.WriteLine();
            Console.WriteLine("[0]. " + (PrevMenu == null ? exitText : toPrevMenuText));
            Console.WriteLine();
            Console.WriteLine(selectText.Replace("FROM", "1")
                .Replace("TO", items.Length.ToString()));

        }

        public void Select(IMenu menuItem = null)
        {
            if (menuItem != null)
                PrevMenu = menuItem;
            DisplayMenu();
            int selection = GetUserSelectedItemIndex();
            if (selection != 0)
            {
                items[selection - 1]?.Select(this);
            }
            else
            {

                PrevMenu?.Select(null);
            }
        }

        int GetUserSelectedItemIndex()
        {
            int selectedIndex = -1;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out selectedIndex)
                    || selectedIndex < 0 || selectedIndex > items.Length)
                {
                    selectedIndex = -1;
                    Console.WriteLine("[Error] select Again");
                    Console.ReadKey();
                    Display();
                }
            } while (selectedIndex < 0);
            return selectedIndex;
        }
    }
}
