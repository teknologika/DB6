using System;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;


namespace DB6
{

    public class Theme
    {
        private NSDictionary ThemeDictionary { get; set; }
        private NSCache _colorCache { get; set; }
        private NSCache _fontCache { get; set; }

        public string Name { get; set; }
        public Theme ParentTheme { get; set; }

        public Theme(NSDictionary themeDictionary)
        {
            ThemeDictionary = themeDictionary;
            _colorCache = new NSCache();
            _fontCache = new NSCache();
        }

        public NSObject ObjectForKey(string key)
        {
            NSString nskey = new NSString(key);
            NSObject obj = null;

            obj = ThemeDictionary.ObjectForKey((NSObject)nskey);


            if (obj == null)
            {
                if (ParentTheme != null)
                {
                    obj = ParentTheme.ObjectForKey(key);
                }
            }
               
            return obj;
        }

        public bool BoolForKey(string key)
        {
            var obj = ObjectForKey(key);
            if (obj == null)
            {
                return false;
            }
            return Convert.ToBoolean(obj);
        }

        public string StringForKey(string key)
        {
            var obj = ObjectForKey(key);
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }

        public int IntegerForKey(string key)
        {
            var obj = ObjectForKey(key);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                NSNumber tmp = (NSNumber)obj;
                return (int)tmp;
            }
        }

        public float FloatForKey(string key)
        {
            var obj = ObjectForKey(key);
            if (obj == null)
            {
                return 0.0f;
            }
            else
            {
                return float.Parse(obj.ToString());
            }
        }

        public double TimeIntervalForKey(string key)
        {
            var obj = ObjectForKey(key);
            if (obj == null)
            {
                return 0.0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        public UIImage ImageForKey(string key)
        {
            var imageName = StringForKey(key);
            if (string.IsNullOrEmpty(imageName))
            {
                return null;
            }
            else
            {
                return new UIImage(imageName);
            }
        }

        public UIColor ColorForKey(string key)
        {
            NSString stringKey = new NSString(key);
               
            UIColor cachedColor = (UIColor)_colorCache.ObjectForKey(stringKey);
            if (cachedColor != null)
            {
                return cachedColor;
            }
            else
            {
                string colorString = StringForKey(key);
                UIColor color = ColorWithHexString(colorString);
                if (color == null)
                {
                    color = UIColor.Black;
                }
                else
                {
                    _colorCache.SetObjectforKey(color, new NSString(key));
                }
                return color;
            }
        }

        public UIEdgeInsets EdgeInsetsForKey (string key)
        {
            float left = FloatForKey(key + @"Left");
            float top = FloatForKey(key + @"Top");
            float right = FloatForKey(key + @"Right");
            float bottom = FloatForKey(key + @"Bottom");
            UIEdgeInsets edgeInsets = new UIEdgeInsets(top,left,bottom,right);
            return edgeInsets;
        }

        public UIFont FontForKey(string key)
        {
            NSString stringKey = new NSString(key);

            UIFont cachedFont = (UIFont)_fontCache.ObjectForKey(stringKey);
            if (cachedFont != null)
            {
                return cachedFont;
            }

            string fontName = StringForKey(key);
            float fontSize = FloatForKey(key+ @"Size");

            if (fontSize < 1.0f)
            {
                fontSize = 15.0f;
            }

            UIFont font;
            if (string.IsNullOrEmpty(fontName))
            {
                font = UIFont.SystemFontOfSize(fontSize);
            }
            else
            {
                font = UIFont.FromName(fontName,fontSize); 
            }

            if (font == null)
            {
                font = UIFont.SystemFontOfSize(fontSize);
            }
            _fontCache.SetObjectforKey(font, new NSString(key));

            return font;
        }

        public SizeF SizeForKey(string key)
        {
            float width = FloatForKey(key + @"Width");
            float height = FloatForKey(key + @"Height");

            SizeF size = new SizeF(width, height);
            return size;
        }

        public UIViewAnimationOptions CurveForKey(string key)
        {
            string curveString = StringForKey(key).ToLower();


            switch (curveString)
            {
                case "easeinout":
                    return UIViewAnimationOptions.CurveEaseInOut;

                case "easein":
                    return UIViewAnimationOptions.CurveEaseIn;
   
                case "easeout":
                    return UIViewAnimationOptions.CurveEaseOut;

                case "linear":
                    return UIViewAnimationOptions.CurveLinear;

                default:
                    return UIViewAnimationOptions.CurveEaseInOut;
            }
        }

        public AnimationSpecifier AnimationSpecifierForKey(string key)
        {
            AnimationSpecifier animationSpecifier = new AnimationSpecifier();
            animationSpecifier.Duration = TimeIntervalForKey(key + @"Duration");
            animationSpecifier.Delay = TimeIntervalForKey(key + @"Delay");
            animationSpecifier.Curve = CurveForKey(key + @"Curve");

            return animationSpecifier;
        }

        public void AnimateWithAnimationSpecifierKey(string key, NSAction animation, NSAction completion)
        {
            AnimationSpecifier animationSpecifier = AnimationSpecifierForKey(key);
            UIView.Animate(animationSpecifier.Duration, animationSpecifier.Delay, animationSpecifier.Curve, animation, completion);
        }


        static UIColor ColorWithHexString(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                return UIColor.Black;
            }
            string s = hexString.Replace("#","").Trim();
            string redString = s.Substring(0, 2);
            string greenString = s.Substring(2, 2);
            string blueString = s.Substring(4, 2);

            int red = Convert.ToInt32(redString, 16);
            int green = Convert.ToInt32(greenString, 16);
            int blue = Convert.ToInt32(blueString, 16);

            return new UIColor((float)red / 255.0F, (float)green / 255.0F, (float)blue / 255.0F, 1.0F);

        }
    }

    public class AnimationSpecifier
    {
        public double Delay { get; set; }
        public double Duration { get; set; }
        public UIViewAnimationOptions Curve  { get; set; }
    }
}

