using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BastardFat.ColoredTextBlock.Controls
{
    public class ColoredTextBlock : RichTextBox
    {
        public const string RichTextPropertyName = nameof(RichText);

        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.Register(RichTextPropertyName,
                                        typeof(ColoredTextString),
                                        typeof(RichTextBox),
                                        new FrameworkPropertyMetadata(
                                            default(ColoredTextString),
                                            FrameworkPropertyMetadataOptions.AffectsMeasure |
                                            FrameworkPropertyMetadataOptions.AffectsRender,
                                            new PropertyChangedCallback
                                                (RichTextPropertyChanged)));

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            return base.ArrangeOverride(arrangeBounds);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            using (var image = new System.Drawing.Bitmap(1, 1))
            {
                using (var g = System.Drawing.Graphics.FromImage(image))
                {
                    var size = g.MeasureString(RawText, new System.Drawing.Font("Arial", 10, System.Drawing.GraphicsUnit.Pixel));
                    return new Size(size.Width * 1.5, size.Height);
                }
            }
        }

        public ColoredTextBlock()
        {
            IsReadOnly = true;
            IsHitTestVisible = false;
            Focusable = false;
            Background = new SolidColorBrush { Opacity = 0 };
            BorderThickness = new Thickness(0);
        }

        private string RawText;

        public ColoredTextString RichText
        {
            get { return (ColoredTextString) GetValue(RichTextProperty); }
            set { SetValue(RichTextProperty, value); }
        }

        private static void RichTextPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            string RTF = ((ColoredTextString) dependencyPropertyChangedEventArgs.NewValue).ToString();
            ((ColoredTextBlock) dependencyObject).RawText = ((ColoredTextString) dependencyPropertyChangedEventArgs.NewValue).RawText;
            ((RichTextBox) dependencyObject).SelectAll();
            ((RichTextBox) dependencyObject).Selection.Text = String.Empty;
            using (var stream = new MemoryStream(Encoding.Default.GetBytes(RTF)))
                ((RichTextBox) dependencyObject).Selection.Load(stream, DataFormats.Rtf);
        }
    }
}
