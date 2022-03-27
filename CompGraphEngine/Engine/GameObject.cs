using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphEngine.Engine
{
    public abstract class GameObject
    {
        public bool IsInited { get; set; }
        public abstract void Init();
        public abstract void Update();
        
    }
}
