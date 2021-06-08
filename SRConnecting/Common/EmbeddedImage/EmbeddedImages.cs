using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SRConnecting.Common.EmbeddedImage
{
    [ContentProperty (nameof(source))]
    class EmbeddedImages : IMarkupExtension
    {
        public string source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (source == null) return null;

            return ImageSource.FromResource(source, typeof(EmbeddedImages).GetTypeInfo().Assembly);
        }
    }
}
