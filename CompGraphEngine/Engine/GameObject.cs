using CompGraphEngine.Render;
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
        public Transform Transform { get; set; }
        public RenderObject renderObject;
       
        public abstract void Init();
        public abstract void Update();
        
        
    }
}
