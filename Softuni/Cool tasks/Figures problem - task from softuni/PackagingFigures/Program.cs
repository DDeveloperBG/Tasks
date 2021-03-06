﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace PackagingFigures
{
    class Program
    {
        static List<Figures> packedFigures = new List<Figures>();
        static int biggestCount = 1;

        static void FindCombinationOfFigures(List<Figures> figures, List<Figures> result, int index)
        {
            for (int i = index + 1; i < figures.Count; i++)
            {
                bool figureIsInside = false;

                if (figures[index].FigureType == "rectangle")
                {
                    if (figures[i].FigureType == "rectangle")
                    {
                        if (figures[i].Rectangle.LeftTopX >= figures[index].Rectangle.LeftTopX && figures[i].Rectangle.RightBottomX <= figures[index].Rectangle.RightBottomX &&
                            figures[i].Rectangle.LeftTopY <= figures[index].Rectangle.LeftTopY && figures[i].Rectangle.RightBottomY >= figures[index].Rectangle.RightBottomY)
                        {
                            figureIsInside = true;
                        }
                    }
                    else if (figures[i].FigureType == "square")
                    {
                        if (figures[i].Square.X >= figures[index].Rectangle.LeftTopX && figures[i].Square.X + figures[i].Square.Side <= figures[index].Rectangle.RightBottomX &&
                            figures[i].Square.Y <= figures[index].Rectangle.LeftTopY && figures[i].Square.Y - figures[i].Square.Side >= figures[index].Rectangle.RightBottomY)
                        {
                            figureIsInside = true;
                        }
                    }
                    else
                    {
                        if (figures[i].Circle.X >= figures[index].Rectangle.LeftTopX && figures[i].Circle.X <= figures[index].Rectangle.RightBottomX &&
                            figures[i].Circle.Y <= figures[index].Rectangle.LeftTopY && figures[i].Circle.Y >= figures[index].Rectangle.RightBottomY)
                        {
                            if (figures[i].Circle.X - figures[i].Circle.R >= figures[index].Rectangle.LeftTopX &&
                                figures[i].Circle.X + figures[i].Circle.R <= figures[index].Rectangle.RightBottomX &&
                                figures[i].Circle.Y - figures[i].Circle.R >= figures[index].Rectangle.RightBottomY &&
                                figures[i].Circle.Y + figures[i].Circle.R <= figures[index].Rectangle.LeftTopY)
                            {
                                figureIsInside = true;
                            }
                        }
                    }
                }
                else if (figures[index].FigureType == "square")
                {
                    if (figures[i].FigureType == "rectangle")
                    {
                        if (figures[i].Rectangle.LeftTopX >= figures[index].Square.X && figures[i].Rectangle.RightBottomX <= figures[index].Square.X + figures[index].Square.Side &&
                            figures[i].Rectangle.LeftTopY <= figures[index].Square.Y && figures[i].Rectangle.RightBottomY >= figures[index].Square.Y - figures[index].Square.Side)
                        {
                            figureIsInside = true;
                        }
                    }
                    else if (figures[i].FigureType == "square")
                    {
                        if (figures[i].Square.X >= figures[index].Square.X && figures[i].Square.X + figures[i].Square.X <= figures[index].Square.X + figures[index].Square.Side &&
                            figures[i].Square.Y <= figures[index].Square.Y && figures[i].Square.Y - figures[i].Square.Side >= figures[index].Square.Y - figures[index].Square.Side)
                        {
                            figureIsInside = true;
                        }
                    }
                    else
                    {
                        if (figures[i].Circle.X >= figures[index].Square.X && figures[i].Circle.X <= figures[index].Square.X + figures[index].Square.Side &&
                            figures[i].Circle.Y <= figures[index].Square.Y && figures[i].Circle.Y >= figures[index].Square.Y - figures[index].Square.Side)
                        {
                            if (figures[i].Circle.X - figures[i].Circle.R >= figures[index].Square.X &&
                                figures[i].Circle.X + figures[i].Circle.R <= figures[index].Square.X + figures[index].Square.Side &&
                                figures[i].Circle.Y - figures[i].Circle.R >= figures[index].Square.Y - figures[index].Square.Side &&
                                figures[i].Circle.Y + figures[i].Circle.R <= figures[index].Square.Y)
                            {
                                figureIsInside = true;
                            }
                        }
                    }
                }
                else
                {
                    if (figures[i].FigureType == "rectangle")
                    {
                        int furtherstX = Math.Max(figures[index].Circle.X - figures[i].Rectangle.LeftTopX, figures[i].Rectangle.RightBottomX - figures[index].Circle.X);
                        int furtherstY = Math.Max(figures[index].Circle.Y - figures[i].Rectangle.LeftTopY, figures[i].Rectangle.RightBottomY - figures[index].Circle.Y);

                        if (figures[index].Circle.R * figures[index].Circle.R >= furtherstX * furtherstX + furtherstY * furtherstY)
                        {
                            figureIsInside = true;
                        }
                    }
                    else if (figures[i].FigureType == "square")
                    {
                        double furtherstX = Math.Max(figures[index].Circle.X - figures[i].Square.X, figures[i].Square.X + figures[i].Square.Side - figures[index].Circle.X);
                        double furtherstY = Math.Max(figures[index].Circle.Y - figures[i].Square.Y, figures[i].Square.Y - figures[i].Square.Side - figures[index].Circle.Y);

                        if (figures[index].Circle.R * figures[index].Circle.R >= furtherstX * furtherstX + furtherstY * furtherstY)
                        {
                            figureIsInside = true;
                        }
                    }
                    else
                    {
                        if (figures[i].Circle.X - figures[i].Circle.R >= figures[index].Circle.X - figures[index].Circle.R &&
                            figures[i].Circle.X + figures[i].Circle.R <= figures[index].Circle.X + figures[index].Circle.R &&
                            figures[i].Circle.Y - figures[i].Circle.R >= figures[index].Circle.Y - figures[index].Circle.R &&
                            figures[i].Circle.Y + figures[i].Circle.R <= figures[index].Circle.Y + figures[index].Circle.R)
                        {
                            figureIsInside = true;
                        }
                    }
                }

                if (figureIsInside)
                {
                    result.Add(figures[i]);

                    FindCombinationOfFigures(figures, result, i);
                }
            }

            if (result.Count >= biggestCount)
            {
                if (result.Count == biggestCount && biggestCount > 1)
                {
                    for (int i = 0; i < biggestCount; i++)
                    {
                        if (packedFigures[i].FigureName == result[i].FigureName)
                        {
                            continue;
                        }

                        string firstByAlphabeticOrder = new List<string> { packedFigures[i].FigureName, result[i].FigureName }.OrderBy(x => x).ToList()[0].ToString();

                        if (firstByAlphabeticOrder != result[i].FigureName)
                        {
                            result.RemoveAt(result.Count - 1);
                            return;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                biggestCount = result.Count;

                packedFigures = new List<Figures>();

                foreach (var item in result)
                {
                    packedFigures.Add(item);
                }

                result.RemoveAt(result.Count - 1);
            }
            else if (result.Count > 1)
            {
                result.RemoveAt(result.Count - 1);
            }
        }

        static void Main()
        {
            string[] input = Console.ReadLine().Split(' ');
            List<Figures> figures = new List<Figures>();

            while (input[0] != "End")
            {
                if (input[0] == "rectangle")
                {
                    figures.Add(new Figures(input[0], input[1], int.Parse(input[2]), int.Parse(input[3]), 0, 0, int.Parse(input[4]), int.Parse(input[5])));
                }
                else if (input[0] == "square")
                {
                    figures.Add(new Figures(input[0], input[1], int.Parse(input[2]), int.Parse(input[3]), 0, int.Parse(input[4])));
                }
                else
                {
                    figures.Add(new Figures(input[0], input[1], int.Parse(input[2]), int.Parse(input[3]), int.Parse(input[4])));
                }

                input = Console.ReadLine().Split(' ');
            }

            figures = figures.OrderByDescending(x => x.S).ThenBy(x => x.FigureName).ToList();

            for (int i = 0; i < figures.Count; i++)
            {
                FindCombinationOfFigures(figures, new List<Figures>() { figures[i] }, i);
            }

            if (packedFigures.Count == 0)
            {
                packedFigures.Add(figures[0]);
            }

            Console.WriteLine(string.Join(" < ", packedFigures.Select(x => x.FigureName)));
        }
    }

    class Figures
    {
        public Figures(string figureType, string figureName, int x, int y, int r = 0, int side = 0, int x2 = 0, int y2 = 0)
        {
            if (figureType == "rectangle")
            {
                Rectangle = new Rectangle(x, y, x2, y2);
                S = (x2 - x) * (y - y2);
            }
            else if (figureType == "square")
            {
                Square = new Square(x, y, side);
                S = side * side;
            }
            else
            {
                Circle = new Circle(x, y, r);
                S = Math.PI * Math.Pow(r, 2);
            }

            FigureType = figureType;
            FigureName = figureName;
        }

        public double S { get; set; }
        public string FigureType { get; set; }
        public string FigureName { get; set; }

        public Circle Circle { get; set; }
        public Rectangle Rectangle { get; set; }
        public Square Square { get; set; }
    }

    class Circle
    {
        public Circle(int x, int y, int r)
        {
            X = x;
            Y = y;

            R = r;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int R { get; set; }
    }

    class Square
    {
        public Square(int x, int y, int side)
        {
            X = x;
            Y = y;

            Side = side;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int Side { get; set; }
    }

    class Rectangle
    {
        public Rectangle(int x1, int y1, int x2, int y2)
        {
            LeftTopX = x1;
            LeftTopY = y1;

            RightBottomX = x2;
            RightBottomY = y2;
        }

        public int LeftTopX { get; set; }
        public int LeftTopY { get; set; }

        public int RightBottomX { get; set; }
        public int RightBottomY { get; set; }
    }
}