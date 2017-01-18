# WPF ColoredTextBlock custom control
Custom WPF control for indicating colored text


1. Include reference to `BastardFat.ColoredTextBlock.Controls` in your project

2. Include namespace to your XAML:

```xml
<Window ... xmlns:ctb="clr-namespace:BastardFat.ColoredTextBlock.Controls;assembly=BastardFat.ColoredTextBlock.Controls">
```

3. Place control into your window or control and bind his `RichText` property to your ViewModel:

```xml
<ctb:ColoredTextBlock RichText="{Binding ColoredRepresentation}"/>
```

There is `ColoredTextString` class in namespace `BastardFat.ColoredTextBlock.Controls`. You can bind (or directly set) property `RichText` with instance of this class. Example:

```cs
    ColoredTextString coloredString = new ColoredTextString();
    coloredString.AppendText("default color ");
    coloredString.AppendText("red ", "#FF0000");
    coloredString.AppendText("yellow ", new Color(255,255,0));
    coloredString.AppendText("green ", 0, 255, 0);
```
Also you can create ColoredTextString object from string. Example:

```cs
ColoredTextString coloredString = new ColoredTextString("`FF00FF`Some `0088FF`awesome `FF0000`color!");
```

You can further define the following color using ``` ` ``` characters and 6-digit hexadecimal. Examle above will produce something like this:

![Screenshot](https://raw.githubusercontent.com/BastardFat/wpf-coloredtextblock/master/temp.png)


If you want to bind just string to control you must use converter: `BastardFat.ColoredTextBlock.Controls.StringToColoredStringConverter`

Like this:
```xml
<Window.Resources>
	<ctb:StringToColoredStringConverter x:Key="StringToColoredStringConverter"/>
</Window.Resources>
...
<ctb:ColoredTextBlock RichText="{Binding StringRepresentation, Converter={StaticResource StringToColoredStringConverter}}"/>
```

*I hope you will enjoy*
