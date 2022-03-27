using CompGraphEngine.Engine;
using CompGraphEngine.Render;

using System.Collections.Generic;


namespace CompGraphEngine.SceneF
{
    public class Scene
    {
        private Renderer renderer  = new Renderer();
        private List<GameObject> GameObjects = new List<GameObject>();
        public Scene() { }
        public Scene(List<GameObject> objects)
        {
            renderer = new Renderer();
            foreach (GameObject o in objects)
                AddObjectToScene(o);
        }

        public virtual void Init()
        {

            foreach(GameObject obj in GameObjects)
                if(!obj.IsInited)
                    obj.Init();
        }

        public virtual void Update()
        {

            foreach (GameObject obj in GameObjects)
                if (obj.IsInited)
                    obj.Update();
        }

        public void Render()
        {
            foreach (GameObject obj in GameObjects)
                if (obj.IsInited && obj is IRenderable renderable)
                    renderer.Draw(renderable);
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
