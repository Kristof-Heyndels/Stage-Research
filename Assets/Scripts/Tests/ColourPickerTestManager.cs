using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTimer
{
	public float timer;

	public void Next(float time)
	{
		timer += time;
	}
}

public class ColourPickerTestManager : MonoBehaviour
{
	public GameObject holoPanel;
	public GameObject colourPickerPanel;
	public Dropdown colourDropdown;
	public Text infoText;

	private Color colourToSelect;
	private float[] dropdownTimers = new float[5];
	private float[] colourCubeTimers = new float[5];

	static System.Random rnd =  new System.Random();

	// Update is called once per frame
		void Update()
	{
		if (!holoPanel.activeSelf)
		{
			infoText.text = "You may apologise for your short attention span, but please Open your Holo";
		}
	}

	public void BeginTest()
	{
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

	public void ColourSelected(string colour)
	{
		var selectedColour = ColourRGBA.toRGBA(colour);
		holoPanel.GetComponent<HoloPanel>().SetColour(selectedColour);

		if (selectedColour == colourToSelect)
		{
			colourToSelect = ColourRGBA.colourList[rnd.Next(ColourRGBA.colourList.Count)];
			HoloPanel.LogNotification("Impressive for one of your kind.\nPlease select the next colour: {0}", ColourRGBA.ToName(colourToSelect));
		}
	}
	
}
