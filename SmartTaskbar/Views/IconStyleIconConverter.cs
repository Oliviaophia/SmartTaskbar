using System;
using System.Drawing;
using ReactiveUI;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Properties;

namespace SmartTaskbar.Views
{
    internal class IconStyleIconConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            return fromType != typeof(IconStyle) || toType != typeof(Icon) ? 0 : 10;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            if (from is IconStyle style)
                switch (style)
                {
                    case IconStyle.Black:
                        result = Resources.Logo_Black;
                        return true;
                    case IconStyle.Blue:
                        result = Resources.Logo_Blue;
                        return true;
                    case IconStyle.Pink:
                        result = Resources.Logo_Pink;
                        return true;
                    case IconStyle.White:
                        result = Resources.Logo_White;
                        return true;
                    case IconStyle.Auto:
                        result = InvokeMethods.IsLightTheme()
                            ? Resources.Logo_Black
                            : Resources.Logo_White;
                        return true;
                    default:
                        result = Resources.Logo_Blue;
                        return false;
                }

            result = Resources.Logo_Blue;
            return false;
        }
    }
}