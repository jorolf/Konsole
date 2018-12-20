using Konsole.Graphics.Drawables;
using Konsole.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics.Containers
{
    public class Container : Drawable, IContainer
    {      
        public List<Drawable> Children { get; set; }

        private List<Drawable> DrawableTree = new List<Drawable>();

        public List<Drawable> GetChildren()
        {
            if (Children.Count != 0)
            {
                foreach (Drawable child in Children)
                {
                    if (child is Container)
                    {
                        child.Position.X += Position.X;
                        child.Position.Y += Position.Y;
                        DrawableTree.AddRange((child as Container).GetChildren());
                    }
                    else
                    {
                        child.Position.X += Position.X;
                        child.Position.Y += Position.Y;
                        DrawableTree.Add(child);
                    }
                }
                return DrawableTree;
            }
            else return DrawableTree;
        }
        void UpdateChildren()
        {
            foreach (Drawable child in Children)
            {
                child.Position.X += Position.X;
                child.Position.Y += Position.Y;
            }
        }
        


    }
}
