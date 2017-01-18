using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BastardFat.ColoredTextBlock.Controls
{
    public class ColoredTextString
    {
        private const string HeaderBeforeColors = @"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033{\fonttbl{\f0\fnil\fcharset0 Arial;}}{\colortbl ;";
        private const string HeaderAfterColors = @";}\viewkind4\uc1\pard\sa200\sl276\slmult1\fs";//22 ";
        private const string Ender = @"\lang9}";

        public ColoredTextString() : this(10) { }
        private ColoredTextString(int fontSize)
        {
            FontSize = fontSize;
        }

        public ColoredTextString(string InlineFormated) : this(10)
        {
            var matches = Regex.Matches(InlineFormated, "(`(.*?)`)")
                                .Cast<Match>()
                                .Select(match => new { Start = match.Index, End = match.Index + match.Length, Color = match.Groups[2].Value }).ToList();
            if (matches.Count == 0)
            {
                AppendText(InlineFormated);
                return;
            }
            AppendText(InlineFormated.Substring(0, matches[0].Start));
            for (int i = 0; i < matches.Count - 1; i++)
            {
                AppendText(InlineFormated.Substring(matches[i].End, matches[i + 1].Start - matches[i].End), matches[i].Color);
            }
            AppendText(InlineFormated.Substring(matches.Last().End), matches.Last().Color);
        }

        public int FontSize { get; set; }

        public void AppendText(string text, Color color)
        {
            if (!ColorTable.Contains(color))
                ColorTable.Add(color);
            int colorIndex = ColorTable.IndexOf(color);
            List.Add(new ColoredText() { Text = EscapeText(text), ColorIndex = colorIndex });
        }
        public void AppendText(string text) =>
            AppendText(text, (Color) null);
        public void AppendText(string text, string hexColor) =>
            AppendText(text, new Color(hexColor));
        public void AppendText(string text, byte r, byte g, byte b) =>
            AppendText(text, new Color(r, g, b));

        public void Clear()
        {
            List.Clear();
            ColorTable.Clear();
        }


        private string GenerateRTF()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var str in List)
            {
                sb.Append($"\\cf{str.ColorIndex} ");
                sb.Append(str.Text);
            }
            return sb.ToString();
        }
        private string GenerateColorTable() => String.Join(";", ColorTable.Skip(1).Select(color => color.ToString()));


        private string EscapeText(string text) => text.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}");

        private List<Color> ColorTable = new List<Color>() { null };
        private List<ColoredText> List = new List<ColoredText>();

        public static string ToRTF(string inlineFormatedString) =>
            new ColoredTextString(inlineFormatedString).ToString();


        public string RawText => String.Join(String.Empty, List.Select(c => c.Text));
        public override string ToString() =>
            $"{HeaderBeforeColors}{GenerateColorTable()}{HeaderAfterColors}{FontSize * 2} {GenerateRTF()}{Ender}";

    }
}
