using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourPickerTestManager : MonoBehaviour
{
	public GameObject holoPanel;
	public GameObject colourPickerPanel;
	public Dropdown colourDropdown;
	public Text errorText;

	// Update is called once per frame
	void Update()
	{
		if (holoPanel.activeSelf)
		{
			ButtonManager.activeButton = ActiveButton.Start;
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
		HoloPanel.Log("Starting test: COLOUR PICKER");
	}
	
}
