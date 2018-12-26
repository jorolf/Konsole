using Konsole.Graphics.Drawables;
using Konsole.Graphics.Enums;
using Konsole.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics.Containers
{
    /// <summary>
    /// A <see cref="Drawable"/> that contains other Drawables, please note that Containers themselves aren't drawn, only their content is.
    /// </summary>
    public class Container : Drawable, IContainer
    {      
        public IList<Drawable> Children { get; set; }

        public Container()
        {
            Children = new List<Drawable>();
        }
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
                        switch (child.RelativeSize)
                        {
                            default:
                                child.DrawSize = child.Size;
                                break;
                            case Axes.X:
                                child.DrawSize.X = Size.X * child.Size.X;
                                child.DrawSize.Y = child.Size.Y;
                                break;
                            case Axes.Y:
                                child.DrawSize.X = child.Size.X;
                                child.DrawSize.Y = Size.Y * child.Size.Y;
                                break;
                            case Axes.Both:
                                child.DrawSize.X = Size.X * child.Size.X;
                                child.DrawSize.Y = Size.Y * child.Size.Y;
                                break;
                        }
                        
                        switch (child.Anchor)
                        {
                            default:
                                child.Position.X += Position.X;
                                child.Position.Y += Position.Y;
                                break;
                            case Anchor.TopCentre:
                                child.Position.X += Position.X + Size.X / 2 - child.DrawSize.X / 2;
                                child.Position.Y += Position.Y;
                                break;
                            case Anchor.TopRight:
                                child.Position.X += Position.X + Size.X - child.DrawSize.X;
                                child.Position.Y += Position.Y;
                                break;
                            case Anchor.Left:
                                child.Position.X += Position.X;
                                child.Position.Y += Position.Y + Size.Y / 2 - child.DrawSize.Y / 2;
                                break;
                            case Anchor.Centre:
                                child.Position.X += Position.X + Size.X / 2 - child.DrawSize.X / 2;
                                child.Position.Y += Position.Y + Size.Y / 2 - child.DrawSize.Y / 2;
                                break;
                            case Anchor.Right:
                                child.Position.X += Position.X + Size.X - child.DrawSize.X;
                                child.Position.Y += Position.Y + Size.Y / 2 - child.DrawSize.Y / 2;
                                break;
                            case Anchor.BottomLeft:
                                child.Position.X += Position.X;
                                child.Position.Y += Position.Y + Size.Y - child.DrawSize.Y;
                                break;
                            case Anchor.BottomCentre:
                                child.Position.X += Position.X + Size.X / 2 - child.DrawSize.X / 2;
                                child.Position.Y += Position.Y + Size.Y - child.DrawSize.Y;
                                break;
                            case Anchor.BottomRight:
                                child.Position.X += Position.X + Size.X - child.DrawSize.X;
                                child.Position.Y += Position.Y + Size.Y - child.DrawSize.Y;
                                break;
                        }                   
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
