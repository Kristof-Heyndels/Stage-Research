﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PinLock : MonoBehaviour
{
	public List<GameObject> buttonsRaw;
	public int[] buttons;
	public GameObject clearButton;
	public GameObject confirmButton;
	public Text info;
	public bool setter;
	public bool needsConfirmation;

	private static List<int> pin = new List<int>();
	private static List<int> attempt = new List<int>();

	// Use this for initialization
	void Start()
	{
		buttons = Enumerable.Range(0, 10).ToArray();

		//Shuffle();
	}


	// Update is called once per frame
	void Update()
	{
		/*
		if (attempt.Count > 0)
		{
			info.text = new string('*', attempt.Count);
		}
		else if (needsConfirmation)
		{
			info.text = "Please insert again";
		}
		*/
	}

	private void Shuffle()
	{
		if (buttons.Length == 0 || buttonsRaw.Count == 0) return;

		var rng = new Random();
		int n = buttons.Length;

		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			var value = buttons[k];
			buttons[k] = buttons[n];
			buttons[n] = value;
		}

		for (var i = 0; i < buttonsRaw.Count; i++)
		{
			var button = buttonsRaw[i];
			button.GetComponentInChildren<Text>().text = buttons[i].ToString();
		}

		World.Record("pincode:shuffle:[{0}]", string.Join(",", buttons.Select(a => a.ToString()).ToArray()));
	}

	/// <summary>
	/// Check if pin and attempt are equal
	/// </summary>
	/// <returns></returns>
	private static bool CheckPin()
	{
		if (pin.Count != attempt.Count) return false;

		for (var i = 0; i < pin.Count; i++)
		{
			if (pin[i] != attempt[i])
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Add a button press to the login attempt
	/// </summary>
	/// <param name="pinButton">The button being pressed</param>
	/// <param name="parent">the parent of the button</param>
	public static void Click(PinButton pinButton, PinLock parent)
	{
		var input = Int32.Parse(pinButton.GetComponentInChildren<Text>().text);
		attempt.Add(input);
		parent.info.text = new string('*', attempt.Count);
		//parent.Shuffle();
	}

	public static void Ok(PinLock parent)
	{
		if (attempt.Count == 0) return;
		World.Record("pincode:attempt:[{0}]", string.Join(",", attempt.Select(a => a.ToString()).ToArray()));
		var check = CheckPin();

		if (!parent.setter)
		{
			if (check)
			{
				//open door 
			}
		}
		else
		{
			if (parent.needsConfirmation)
			{
				if (check)
				{
					parent.needsConfirmation = false;
					parent.setter = false;
					parent.info.text = "pin confirmed"; 
					parent.GetComponentInParent<Door>().Open();
				}
			}
			else
			{
				if (3 < attempt.Count && attempt.Count < 9)
				{
					pin = attempt.ToList(); // TODO(Lander): check if this copies by value, not reference
					parent.needsConfirmation = true;
					parent.info.text = "Confirm pin";
				}
				else
				{
					parent.info.text = "pin is too short!";
				}
			}
		}
		attempt.Clear();
	}

	public static void Clear(PinLock parent)
	{
		attempt.Clear();
		parent.info.text = "[CLEARED]";
	}
}
