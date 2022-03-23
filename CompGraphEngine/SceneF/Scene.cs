using CompGraphEngine.Render;

using System.Collections.Generic;


namespace CompGraphEngine.SceneF
{
    public class Scene
    {
        private Renderer renderer  = new Renderer();
        private List<Figure> Figures = new List<Figure>();
        public Scene() { }
        public Scene(List<Figure> figure)
        {
            renderer = new Renderer();
            foreach (Figure f in figure)
                AddObjectToScene(f);
        }

        public virtual void Init()
        {

            foreach(Figure f in Figures)
                f.Init();
        }

        public virtual void Update()
        {
            foreach (Figure f in Figures)
                renderer.Draw(f);
        }

        public void AddObjectToScene(Figure figure)
        {
            Figures.Add(figure);
        }

        public void RemoveObjectFromScene(Figure figure)
        {
            Figures.Remove(figure);
        }
    }
}
