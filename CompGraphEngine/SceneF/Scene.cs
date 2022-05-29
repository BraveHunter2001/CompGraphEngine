using CompGraphEngine.Engine;
using CompGraphEngine.Render;

using System.Collections.Generic;


namespace CompGraphEngine.SceneF
{
    public class Scene
    {
        public readonly Window window;

        public Renderer Renderer;

        private List<GameObject> GameObjects = new List<GameObject>();
        public Camera Camera { get; set; } = new Camera();
        public Scene(Window window) { this.window = window; }
        public Scene(Window window, List<GameObject> objects)
        {
            this.window = window;
           
            foreach (GameObject o in objects)
                AddObjectToScene(o);
        }
        public Scene(Window window, List<GameObject> objects, Renderer renderer)
        {
            this.window = window;
            Renderer = renderer;
            renderer.Camera = Camera;
            foreach (GameObject o in objects)
                AddObjectToScene(o);
        }

        public virtual void Init()
        {

            foreach(GameObject obj in GameObjects)
                if(obj != null && !obj.IsInited)
                    obj.Init();
        }

        public virtual void Update()
        {

            foreach (GameObject obj in GameObjects)
                if (obj != null && obj.IsInited)
                    obj.Update();
            //Camera.updateCumeraVectors();
        }

        public void Render()
        {
            foreach (GameObject obj in GameObjects)
                if (obj != null && obj.IsInited)
                    Renderer.Draw(obj.renderObject);
            
        }

        public void AddObjectToScene(GameObject obj)
        {
            GameObjects.Add(obj);
        }

        public void RemoveObjectFromScene(GameObject obj)
        {
            GameObjects.Remove(obj);
        }

        
    }
}
