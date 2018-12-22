using Konsole.Graphics.Drawables;
using Konsole.Graphics.Enums;
using Konsole.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics.Containers
{
    public class Container : Drawable, IContainer
    {      
        public List<Drawable> Children { get; set; }

        List<Drawable> DrawableTree = new List<Drawable>();

        public List<Drawable> GetChildren()
        {
            if (Children.Count != 0)
            {
                foreach (Drawable child in Children)
                {
                    if (child is Container)
                    {
                        SetProperties();
                        DrawableTree.AddRange((child as Container).GetChildren());
                    }
                    else
                    {
                        SetProperties();
                        DrawableTree.Add(child);
                    }
                    void SetProperties()
                    {
                        
                        switch (child.Anchor)
                        {
                            default:
                                child.Position.X += Position.X;
                                child.Position.Y += Position.Y;
                                break;
                            case Anchor.TopCentre:
                                child.Position.X += Position.X + Size.X / 2 - child.Size.X / 2;
                                child.Position.Y += Position.Y;
                                break;
                            case Anchor.TopRight:
                                child.Position.X += Position.X + Size.X - child.Size.X;
                                child.Position.Y += Position.Y;
                                break;
                            case Anchor.Left:
                                child.Position.X += Position.X;
                                child.Position.Y += Position.Y + Size.Y / 2 - child.Size.Y / 2;
                                break;
                            case Anchor.Centre:
                                child.Position.X += Position.X + Size.X / 2 - child.Size.X / 2;
                                child.Position.Y += Position.Y + Size.Y / 2 - child.Size.Y / 2;
                                break;
                            case Anchor.Right:
                                child.Position.X += Position.X + Size.X - child.Size.X;
                                child.Position.Y += Position.Y + Size.Y / 2 - child.Size.Y / 2;
                                break;
                            case Anchor.BottomLeft:
                                child.Position.X += Position.X;
                                child.Position.Y += Position.Y + Size.Y - child.Size.Y;
                                break;
                            case Anchor.BottomCentre:
                                child.Position.X += Position.X + Size.X / 2 - child.Size.X / 2;
                                child.Position.Y += Position.Y + Size.Y - child.Size.Y;
                                break;
                            case Anchor.BottomRight:
                                child.Position.X += Position.X + Size.X - child.Size.X;
                                child.Position.Y += Position.Y + Size.Y - child.Size.Y;
                                break;
                        }
                        
                        //child.Position.X += Position.X;
                        //child.Position.Y += Position.Y;
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
