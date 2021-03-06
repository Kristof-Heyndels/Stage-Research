﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTimer
{
	public float Time;

	public void Next(float deltaTime)
	{
		this.Time += deltaTime;
	}

	public void Reset()
	{
		Time = 0;
	}
}

public class ColourPickerTestManager : MonoBehaviour
{
	public GameObject holoPanel;
	public GameObject colourPickerPanel;
	public Dropdown colourDropdown;
	public Text infoText;

	public ColourCubesContainer colourCubeContainer;

	private Color colourToSelect;

	private TestTimer timer = new TestTimer();
	private float[] dropdownTimers = new float[5];
	private float[] colourCubeTimers = new float[5];
	private int i = 0;

	private bool dropTest;
	private bool preparingNext;
	private bool cubeTest;
	public bool testComplete;

	static System.Random rnd = new System.Random();

	// Update is called once per frame
	void Update()
	{
		if (dropTest || cubeTest)
		{
			timer.Next(Time.deltaTime);
		}

		if (dropTest)
		{
			if (!holoPanel.activeSelf)
			{
				infoText.text = "You may apologise for your short attention span, but please Open your Holo";
			}
		}
		else if (cubeTest)
		{
			if (!holoPanel.activeSelf)
			{
				infoText.text = string.Format("Please, Subject #{0}, keep the Holo open.", World.testID);
			}
		}

		if (testComplete)
		{
			infoText.text = "[DATA GATHERED]";
		}
	}

	public void BeginTest()
	{
		dropTest = true;

		var colours = new List<string>
		{
			"White",
			"Black",
			"Blue",
			"Cyan",
			"Green",
			"Grey",
			"Magenta",
			"Red",
			"Yellow"
		};
		colours.Sort();
		colourDropdown.ClearOptions();
		colourDropdown.AddOptions(colours);

		Pauser.Pause(true);
		colourPickerPanel.SetActive(true);

		colourToSelect = ColourRGBA.colourList[rnd.Next(ColourRGBA.colourList.Count)];
		HoloPanel.LogNotification("Starting test: COLOUR PICKER\nPlease select the colour: {0}", ColourRGBA.ToName(colourToSelect));
	}

	public void NextTest()
	{
		foreach (var cube in colourCubeContainer.colourCubes)
		{
			cube.SetActive(true);
		}
		colourCubeContainer.colourCubes[0].SetActive(false);
		preparingNext = false;
		cubeTest = true;

		colourToSelect = ColourRGBA.colourList[rnd.Next(ColourRGBA.colourList.Count)];
		HoloPanel.LogNotification("Starting test: COLOUR PICKER\nPlease select the colour: {0}", ColourRGBA.ToName(colourToSelect));
	}

	public void ColourSelected(string colour)
	{
		var selectedColour = ColourRGBA.toRGBA(colour);
		holoPanel.GetComponent<HoloPanel>().SetColour(selectedColour);

		if (dropTest || cubeTest)
		{
			if (selectedColour == colourToSelect)
			{
				if (dropTest)
				{
					dropdownTimers[i] = timer.Time;
					i++;
				}
				else if (cubeTest)
				{
					colourCubeTimers[i] = timer.Time;
					i++;
				}
				timer.Reset();

				if (i == 5)
				{
					if (dropTest)
					{
						dropTest = false;
						preparingNext = true;
						i = 0;
						colourPickerPanel.GetComponentInChildren<Dropdown>().Hide();
						colourPickerPanel.SetActive(false);

						HoloPanel.LogNotification("Moving to next stage ...");
						infoText.text = "Engage the holographic cube to begin the next stage.";

						colourCubeContainer.colourCubes[0].SetActive(true);
						colourCubeContainer.transform.position = new Vector3(Camera.main.transform.position.x - 0.6f, Camera.main.transform.position.y - 0.25f, Camera.main.transform.position.z);
					}
					else if (cubeTest)
					{
						//NOTE(Kristof): Finish this test
						HoloPanel.LogNotification("TEST COMPLETE.\nImpressive, for one of your kind.");
						infoText.text = "[DATA GATHERED]";
						colourPickerPanel.SetActive(true);

						Pauser.Pause(false);

						cubeTest = false;
						testComplete = true;

						World.dropdownTimers = dropdownTimers;
						World.colourCubeTimers = colourCubeTimers;
					}
				}
				else
				{
					var index = rnd.Next(ColourRGBA.colourList.Count);
					colourToSelect = ColourRGBA.colourList[index];
					HoloPanel.LogNotification("Please select the next colour: {0}", ColourRGBA.ToName(colourToSelect));
				}
			}
		}
	}

	public bool IsTesting()
	{
		return dropTest || preparingNext || cubeTest;
	}
}
