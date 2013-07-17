using System;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace DB6
{
    public class ThemeLoader
    {
        public Theme DefaultTheme { get; set; }
        public List<Theme> Themes = new List<Theme>();

        public ThemeLoader()
        {
            NSDictionary themesDictionary = new NSDictionary(@"./Theme.plist");
            foreach (var item in themesDictionary)
            {
                Theme theme = new Theme((NSDictionary)item.Value);
                if (item.Key.ToString().ToLower() == @"default")
                {
                    DefaultTheme = theme;
                }
                theme.Name = item.Key.ToString();
                Themes.Add(theme);
            }

            foreach (var item in Themes)
            {
                if (item != DefaultTheme)
                {
                    item.ParentTheme = DefaultTheme;
                }
            }
        }

        public Theme ThemeNamed(string themeName)
        {
            foreach (var item in Themes)
            {
                if (item.Name == themeName)
                {
                    return item;
                }
            }
            return null;
        }
    }
}

