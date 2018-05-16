
using System;
using System.Collections.Generic;
using UnityEngine;

public static class ColourRGBA
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

	public static List<Color> colourList = new List<Color>
		{
			white,
			blue,
			cyan,
			grey,
			green,
			magenta,
			red,
			yellow,
			black
		};

	private static Color SetColour(Color c)
	{
		c.a = 0.4f;
		return new Color(c.r, c.g, c.b, c.a);
	}

	public static Color toRGBA(string s)
	{
		if (s.ToLower().Equals("white"))
		{
			return white;
		}
		else if (s.ToLower().Equals("blue"))
		{
			return blue;
		}
		else if (s.ToLower().Equals("cyan"))
		{
			return cyan;
		}
		else if (s.ToLower().Equals("grey"))
		{
			return grey;
		}
		else if (s.ToLower().Equals("green"))
		{
			return green;
		}
		else if (s.ToLower().Equals("magenta"))
		{
			return magenta;
		}
		else if (s.ToLower().Equals("red"))
		{
			return red;
		}
		else if (s.ToLower().Equals("yellow"))
		{
			return yellow;
		}
		else if (s.ToLower().Equals("black"))
		{
			return black;
		}
		else
		{
			return white;
		}
	}

	public static string ToName(Color c)
	{
		if (c == white)
		{
			return "white";
		}
		else if (c == blue)
		{
			return "blue";
		}
		else if (c == cyan)
		{
			return "cyan";
		}
		else if (c == grey)
		{
			return "grey";
		}
		else if (c == green)
		{
			return "green";
		}
		else if (c == magenta)
		{
			return "magenta";
		}
		else if (c == red)
		{
			return "red";
		}
		else if (c == yellow)
		{
			return "yellow";
		}
		else if (c == black)
		{
			return "black";
		}
		else
		{
			return "invalid RGBA colour";
		}
	}
}
