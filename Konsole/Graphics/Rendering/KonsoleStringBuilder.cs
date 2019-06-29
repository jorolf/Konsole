using System;
using System.Text;
using Konsole.Extensions;
using Konsole.Graphics.Colour;

namespace Konsole.Graphics.Rendering
{
    public static class KonsoleStringBuilder
    {
        private static readonly StringBuilder Output = new StringBuilder();

        public static string CreateConsoleString(Charsel[,] backbuffer, Charsel[,] frontbuffer = null)
        {
            lock (Output)
            {

                if (frontbuffer != null && (backbuffer.GetLength(0) != frontbuffer.GetLength(0) || backbuffer.GetLength(1) != frontbuffer.GetLength(1)))
                    throw new ArgumentException("Backbuffer and frontbuffer have different sizes!");

                Output.Clear();
                Output.Append("\u001b[H");

                int width = backbuffer.GetLength(1), height = backbuffer.GetLength(0);

                Colour3? prevColour = null;
                int skipped = 0;
                bool skippedLine = false;

                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        Charsel back = backbuffer[i, j];
                        if (frontbuffer != null)
                        {
                            Charsel front = frontbuffer[i, j];

                            if (back == front)
                            {
                                skipped++;
                                continue;
                            }

                            if (skipped != 0)
                            {
                                Output.Append("\u001b[");
                                if (skippedLine)
                                {
                                    if (i != 0)
                                        Output.Append(i + 1);
                                    Output.Append(';');
                                    if (j != 0)
                                        Output.Append(j + 1);
                                    Output.Append('H');

                                    skippedLine = false;
                                }
                                else
                                {
                                    if (skipped != 1)
                                        Output.Append(skipped);
                                    Output.Append('C');
                                }

                                skipped = 0;
                            }
                        }

                        if (back.Colour != prevColour)
                        {
                            Output.Append("\u001b[38;2;");
                            Output.Append(back.Colour.R.ToByte());
                            Output.Append(';');
                            Output.Append(back.Colour.G.ToByte());
                            Output.Append(';');
                            Output.Append(back.Colour.B.ToByte());
                            Output.Append('m');
                            prevColour = back.Colour;
                        }

                        Output.Append(back.Char == 0 ? ' ' : back.Char);
                    }

                    if (skipped != 0)
                        skippedLine = true;

                    if (i != height - 1)
                        Output.AppendLine();
                }

                return Output.ToString();
            }
        }
    }
}
