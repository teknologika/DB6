DB6
===

DB6 is a Monotouch port of [DB5](https://github.com/quartermaster/DB5) by [Q Branch](http://qbranch.co/) as used in their app app [Vesper](http://vesperapp.co/).

***AT THIS TIME THIS CODE SHOULD BE CONSIDERED ALPHA AND UNTESTED.***

DB6 allows theming information to be stored in a plist, alowing design to be seperated from code. DB6 is quite simple to implement and requires a fre conventions.

### How it works

A demo application DB6Demo is included. It uses two classes ThemeLoader and Theme. Included also is a Theme.plist to store the theming information.

You load the file via ThemeLoader, then access values via methods in Theme. The demo has a single view so this is don in view did load, however in a real app you would probably want to do that in the appdelegate.

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
  		
            // Perform any additional setup after loading the view, typically from a nib.
            applicationThemeLoader = new ThemeLoader();
            theme = applicationThemeLoader.DefaultTheme;

            // Theme the view
            View.BackgroundColor = theme.ColorForKey("ViewBackgroundColor");

            // Theme the label
            Label.Font = theme.FontForKey("DefaultLabelFont");

            // Theme the button
            Button.BackgroundColor = theme.ColorForKey("ButtonBackgroundColor");
            Button.SetTitleColor(theme.ColorForKey("ButtonTextColor"), UIControlState.Normal);
            Button.Layer.CornerRadius = theme.IntegerForKey("ButtonCornerRadius");
            Button.ClipsToBounds = true;

        }

