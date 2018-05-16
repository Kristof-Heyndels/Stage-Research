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

	private Color colourToSelect;

	private TestTimer timer = new TestTimer();
	private float[] dropdownTimers = new float[5];
	private float[] colourCubeTimers = new float[5];
	private int i = 0;

	private bool dropTest;
	private bool preparingNext;
	private bool cubeTest;

	static System.Random rnd = new System.Random();

	// Update is called once per frame
	void Update()
	{
		if (dropTest || cubeTest)
		{
			timer.Next(Time.deltaTime);
		}

		if (preparingNext)
		{
			if (!holoPanel.activeSelf)
			{
				infoText.text = "Engage the holographic cube to begin the next stage.";
			}
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
		colourDropdown.ClearOptions();
		colourDropdown.AddOptions(colours);

		Pauser.Pause(true);
		colourPickerPanel.SetActive(true);

		colourToSelect = ColourRGBA.magenta;
		HoloPanel.LogNotification("Starting test: COLOUR PICKER\nPlease select the colour: {0}", ColourRGBA.ToName(colourToSelect));
	}

	public void NextTest()
	{
		
	}

	public void ColourSelected(string colour)
	{
		var selectedColour = ColourRGBA.toRGBA(colour);
		holoPanel.GetComponent<HoloPanel>().SetColour(selectedColour);

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
					HoloPanel.LogNotification("Moving to next stage: COLOUR CUBES\nPlease close your Holo to continue.", ColourRGBA.ToName(colourToSelect));
				}
				else if (cubeTest)
				{
					//NOTE(Kristof): Finish this test
				}
			}
			else
			{
				colourToSelect = ColourRGBA.colourList[rnd.Next(ColourRGBA.colourList.Count)];
				HoloPanel.LogNotification("Impressive for one of your kind.\nPlease select the next colour: {0}", ColourRGBA.ToName(colourToSelect));
			}
		}
	}

}
