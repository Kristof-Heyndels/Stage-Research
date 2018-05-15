using UnityEngine;
using UnityEngine.UI;

public static class UIObjectFinder
{

	public static RaycastHit Hit { get; set; }

	public static void Find(GameObject obj)
	{
		var unityButton = obj.GetComponent<Button>();

		if (unityButton != null && unityButton.interactable)
		{
			if (obj.GetComponent<OpenDoorButton>() != null)
			{
				var script = (OpenDoorButton)obj.GetComponent<HoloUI>();
				script.Execute();
				unityButton.interactable = false;
			}
			else if (obj.GetComponent<StartButton>() != null)
			{
				var script = (StartButton)obj.GetComponent<HoloUI>();
				script.Execute();
				unityButton.interactable = false;
			}
		}
		if (obj.GetComponent<ColourDropdown>() != null)
		{
			var script = (ColourDropdown)obj.GetComponent<HoloUI>();
			script.Execute();
		}
		else if (obj.GetComponent<DropItem>() != null)
		{
			var script = (DropItem)obj.GetComponent<HoloUI>();
			script.Execute();
		}
	}
}
