using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Engine.Menu
{
    public class MenuItem : IMenuItem
    {
        Action action;
        string name;

       public MenuItem(Action action, string name)
        {
            this.action = action;
            this.name = name;
        }

      

        public void Display()
        {
            Console.WriteLine(name);
        }

    

        public void Select(IMenu menuItem)
        {
            action?.Invoke();
            Console.WriteLine($"{name} is succesfull");
            Console.ReadKey();
            menuItem.Select(menuItem.GetPrevMenu());
        }


    }
}
