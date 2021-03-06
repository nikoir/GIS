﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;


namespace GIS
{
    public class Parser
    {
        private static System.Drawing.Color IntToColor(int argb)
        {
            var r = ((argb >> 16) & 0xff);
            var g = ((argb >> 8) & 0xff);
            var b = (argb & 0xff);
            return System.Drawing.Color.FromArgb(r, g, b);
        }
        public static Layer Parse(string FileName)
        {
            Layer lr = new Layer();
            StreamReader sr = new StreamReader((System.IO.Stream)File.OpenRead(FileName), System.Text.Encoding.Default);
            string str = null;
            str = sr.ReadLine();
            while (str.IndexOf("data", StringComparison.CurrentCultureIgnoreCase) == -1)
                str = sr.ReadLine();
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                str = sr.ReadLine();
                if (str.Contains("Point"))
                {
                    string[] Coords = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    str = sr.ReadLine();
                    Point p;
                    if (str.Contains("Symbol"))
                    {
                        string[] Symbol = str.Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        p = new Point(Convert.ToDouble(Coords[1]), Convert.ToDouble(Coords[2]), Convert.ToChar(Convert.ToInt32(Symbol[1]) + 1));
                        Color color = IntToColor(Convert.ToInt32(Symbol[2]));
                        p.SolidBrush = new SolidBrush(color);
                        p.Font = new Font("MapInfo Symbols", Convert.ToInt32(Symbol[3]));
                    }
                    else
                    {
                        p = new Point(Convert.ToDouble(Coords[1]), Convert.ToDouble(Coords[2]), '*');
                        p.SolidBrush = new SolidBrush(Color.Black);
                        p.Font = new Font("MapInfo Symbols", 18);
                    }
                    p.Visibility = true;
                    lr.AddMapObject(p);
                }
                else
                    if (str.Contains("Line"))
                    {
                        string[] Coords = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        str = sr.ReadLine();
                        Line l = new Line(Convert.ToDouble(Coords[1]), Convert.ToDouble(Coords[2]), Convert.ToDouble(Coords[3]), Convert.ToDouble(Coords[1]));
                        if (str.Contains("Pen"))
                        {
                            string[] Pen = str.Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                            int Width = Convert.ToInt32(Pen[1]);
                            l.Visibility = true;
                            Color color = IntToColor(Convert.ToInt32(Pen[3]));
                            l.Pen = new Pen(color, Width);
                            switch (Pen[2])
                            {
                                case "1":
                                    l.Pen.Color = Color.FromArgb(0);
                                    break;
                                case "2":
                                    l.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                                    break;
                                case "3":
                                    l.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                    break;
                                case "4":
                                    l.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                    break;
                                case "5":
                                    l.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                                    break;
                                default:
                                    l.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                                    break;
                            }
                        }
                        else
                        {
                            l.Pen = new Pen(Color.Black, 5);
                            l.Visibility = true;
                        }
                        lr.AddMapObject(l);
                    }
                    else
                        if (str.Contains("Pline"))
                        {
                            string[] Pline = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            string[] Coords;
                            List<Polyline> list = new List<Polyline>();
                            Polyline pl = null;
                            Pen p = null;
                            int numsections;
                            if (Pline.Length == 3)
                                numsections = Convert.ToInt32(Pline[2]); //Количество секций мультиполилинии
                            else
                                numsections = 1;
                            int counter = 0;
                            int numpts; //Количество точек в текущей секции
                            string[] ar;
                            while (counter < numsections)
                            {
                                if (Pline.Length == 2)
                                    numpts = Convert.ToInt32(Pline[1]);
                                else
                                {
                                    str = sr.ReadLine();
                                    ar = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    numpts = Convert.ToInt32(ar[0]);
                                }
                                counter++;
                                pl = new Polyline();
                                pl.Visibility = true;
                                for (int i = 0; i < numpts; i++)
                                {
                                    str = sr.ReadLine();
                                    Coords = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    pl.AddNode(Convert.ToDouble(Coords[0]), Convert.ToDouble(Coords[1]));
                                }
                                list.Add(pl);
                            }
                            str = sr.ReadLine();
                            if (str.Contains("Pen"))
                            {
                                string[] Pen = str.Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                                int Width = Convert.ToInt32(Pen[1]);
                                Color color = IntToColor(Convert.ToInt32(Pen[3]));
                                p = new Pen(color, Width);
                                switch (Pen[2])
                                {
                                    case "1":
                                        p.Color = Color.FromArgb(0);
                                        break;
                                    case "2":
                                        p.DashStyle= System.Drawing.Drawing2D.DashStyle.Solid;
                                        break;
                                    case "3":
                                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                        break;
                                    case "4":
                                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                        break;
                                    case "5":
                                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                                        break;
                                    default:
                                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                                        break;
                                }
                            }
                            else
                            {
                                p = new Pen(Color.Black, 5);
                            }
                            foreach (Polyline polyline in list)
                            {
                                polyline.Pen = p;
                                lr.AddMapObject(polyline);
                            }
                        }
                        else
                            if (str.Contains("Region"))
                            {
                                Polygon pg = null;
                                System.Drawing.Brush br = null;
                                string[] Region = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                string[] Coords;
                                string[] Brush;
                                Color ForegroundColor;
                                Color BackgroundColor;
                                Pen p = null;
                                List<Polygon> list = new List<Polygon>();
                                int numsections = Convert.ToInt32(Region[1]); //Количество секций мультиполигона
                                int counter = 0;
                                string[] ar;
                                int numpts; //Количество вершин полигона в текущей секции
                                while (counter < numsections)
                                {
                                    str = sr.ReadLine();
                                    ar = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    numpts = Convert.ToInt32(ar[0]);
                                    pg = new Polygon();
                                    pg.Visibility = true;
                                    for (int i = 0; i < numpts; i++)
                                    {
                                        str = sr.ReadLine();
                                        Coords = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        pg.AddNode(Convert.ToDouble(Coords[0]), Convert.ToDouble(Coords[1]));

                                    }
                                    counter++;
                                    list.Add(pg);
                                }
                                str = sr.ReadLine();
                                if (str.Contains("Pen"))
                                {
                                    string[] Pen = str.Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                                    int Width = Convert.ToInt32(Pen[1]);
                                    Color color = IntToColor(Convert.ToInt32(Pen[3]));
                                    p = new Pen(color, Width);
                                    switch (Pen[2])
                                    {
                                        case "1":
                                            p.Color = Color.FromArgb(0);
                                            break;
                                        case "2":
                                            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                                            break;
                                        case "3":
                                            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                            break;
                                        case "4":
                                            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                            break;
                                        case "5":
                                            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                                            break;
                                        default:
                                            p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                                            break;
                                    }
                                    str = sr.ReadLine();
                                }
                                else
                                    p = new Pen(Color.Black, 5);
                                if (str.Contains("Brush"))
                                {
                                    Brush = str.Split(new char[] { ' ', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    ForegroundColor = IntToColor(Convert.ToInt32(Brush[2]));
                                    if (Brush.Length == 4)
                                        BackgroundColor = IntToColor(Convert.ToInt32(Brush[3]));
                                    else
                                        BackgroundColor = Color.White;
                                    switch (Brush[1])
                                    {
                                        case "1":
                                            br = new System.Drawing.SolidBrush(Color.FromArgb(0));
                                            break;
                                        case "2":
                                            br = new System.Drawing.SolidBrush(ForegroundColor);
                                            break;
                                        case "3":
                                            br = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Horizontal, ForegroundColor, BackgroundColor);
                                            break;
                                        case "4":
                                            br = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Vertical, ForegroundColor, BackgroundColor);
                                            break;
                                        case "5":
                                            br = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal, ForegroundColor, BackgroundColor);
                                            break;
                                        case "6":
                                            br = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal, ForegroundColor, BackgroundColor);
                                            break;
                                        default:
                                            br = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.LargeGrid, ForegroundColor, BackgroundColor);
                                            break;
                                    }
                                }
                                else
                                    br = new System.Drawing.SolidBrush(Color.Green);
                                foreach (Polygon polygon in list)
                                {
                                    polygon.Brush = br;
                                    polygon.Pen = p;
                                    lr.AddMapObject(polygon);
                                }

                            }
                            else
                                if (str.Contains("Text"))
                                {
                                    string[] Text = str.Split(new char[] { ' ', '"' }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] Font;
                                    string text;
                                    string[] Coords;
                                    Text txt;
                                    SolidBrush sb;
                                    Font font;
                                    if (Text.Length == 2)
                                        text = Text[1];
                                    else
                                    {
                                        str = sr.ReadLine();
                                        Text = str.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);
                                        text = Text[0];
                                    }
                                    str = sr.ReadLine();
                                    Coords = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    str = sr.ReadLine();
                                    if (str.IndexOf("font", StringComparison.CurrentCultureIgnoreCase) != -1)
                                    {
                                        FontStyle fs = FontStyle.Regular;
                                        Font = str.Split(new char[] { ' ', '"', ',', ')', '(', ']', '[' }, StringSplitOptions.RemoveEmptyEntries);
                                        sb = new SolidBrush(IntToColor(Convert.ToInt32(Font[4])));
                                        switch (Font[2])
                                        {
                                            case "1":
                                                fs = FontStyle.Bold;
                                                break;
                                            case "2":
                                                fs = FontStyle.Italic;
                                                break;
                                            case "3":
                                                fs = FontStyle.Underline;
                                                break;
                                        }
                                        font = new Font(Font[1], Convert.ToInt32(Font[3]), fs);
                                    }
                                    else
                                    {
                                        font = new Font("TimesNewRoman", 14);
                                        sb = new SolidBrush(Color.Black);
                                    }
                                    txt = new Text(text, font, Convert.ToDouble(Coords[0]), Convert.ToDouble(Coords[1]));
                                    txt.SolidBrush = sb;
                                    txt.Visibility = true;
                                    lr.AddMapObject(txt);
                                }
            }
            lr.MapObjects.Sort(delegate(MapObject mo1, MapObject mo2)
            { return mo1.Priority.CompareTo(mo2.Priority); });
            return lr;
        }
    }
}
