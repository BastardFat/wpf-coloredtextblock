using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastardFat.ColoredTextBlock.Controls
{
    public class Color
    {
        public Color(string HexRepresentation)
        {
            if (HexRepresentation.StartsWith("#")) HexRepresentation = HexRepresentation.Substring(1);
            if (HexRepresentation.Length != 6) throw new Exception("Invalid hexadecimal representation of color");
            byte parsedR, parsedG, parsedB;
            if (Byte.TryParse(HexRepresentation.Substring(0, 2), NumberStyles.AllowHexSpecifier, new CultureInfo("en-US"), out parsedR) &&
                Byte.TryParse(HexRepresentation.Substring(2, 2), NumberStyles.AllowHexSpecifier, new CultureInfo("en-US"), out parsedG) &&
                Byte.TryParse(HexRepresentation.Substring(4, 2), NumberStyles.AllowHexSpecifier, new CultureInfo("en-US"), out parsedB))
            {
                R = parsedR;
                G = parsedG;
                B = parsedB;
            }
            else
                throw new Exception("Invalid hexadecimal representation of color");
        }

        public Color(byte r, byte g, byte b)
        { R = r; G = g; B = b; }


        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public static bool operator ==(Color c1, Color c2) => (c1.R == c2.R) && (c1.G == c2.G) && (c1.B == c2.B);
        public static bool operator !=(Color c1, Color c2) => !(c1 == c2);
        public override bool Equals(object obj) => (obj is Color) ? (Color) obj == this : false;
        public override int GetHashCode() => ((R << 16) + (G << 8) + B);
        public override string ToString() =>
            $"\\red{R}\\green{G}\\blue{B}";
    }
}
