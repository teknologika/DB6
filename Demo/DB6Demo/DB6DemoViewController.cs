using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using DB6;

namespace DB6Demo
{
    public partial class DB6DemoViewController : UIViewController
    {

        Theme theme;
        ThemeLoader applicationThemeLoader;

        public DB6DemoViewController(IntPtr handle) : base (handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }
        #region View lifecycle
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

            // Wire up the button click
            Button.TouchUpInside += HandleTouchUpInside;

        }

        void HandleTouchUpInside (object sender, EventArgs e)
        {
            Label.Text = "Button Clicked";
        }

       
        #endregion

    }
}

