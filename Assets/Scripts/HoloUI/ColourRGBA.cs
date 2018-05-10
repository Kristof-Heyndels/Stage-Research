
using System;
using UnityEngine;

public class ColourRGBA
{
	public static Color white = SetColour(Color.white);
	public static Color blue = SetColour(Color.blue);
	public static Color cyan = SetColour(Color.cyan);
	public static Color grey = SetColour(Color.grey);
	public static Color green = SetColour(Color.green);
	public static Color magenta = SetColour(Color.magenta);
	public static Color red = SetColour(Color.red);
	public static Color yellow = SetColour(Color.yellow);
	public static Color black = SetColour(Color.black);

	private static Color SetColour(Color c)
	{
		c.a = 100;
		return new Color(c.r, c.g, c.b, c.a);
	}
}
