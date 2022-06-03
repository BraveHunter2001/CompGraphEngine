using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Engine.Menu
{
    public interface IMenuItem
    {
        void Display();

        void Select(IMenu menuItem = null);

   
    }
    public interface IMenu : IMenuItem
    {
        IMenu GetPrevMenu();

    }
}
