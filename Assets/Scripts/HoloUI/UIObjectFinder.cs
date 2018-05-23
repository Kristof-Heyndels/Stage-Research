using UnityEngine;
using UnityEngine.UI;

public class UIObjectFinder
{

	public static void Find(GameObject obj)
	{
		var unityButton = obj.GetComponent<Button>();

		if (unityButton != null && unityButton.interactable)
		{
			if (obj.GetComponent<OpenDoorButton>() != null)
			{
				var script = (OpenDoorButton)obj.GetComponent<HoloUI>();
				script.Execute();
			}
			else if (obj.GetComponent<StartButton>() != null)
			{
				var script = (StartButton)obj.GetComponent<HoloUI>();
				script.Execute();
			}
			unityButton.interactable = false;
		}
		if (obj.GetComponent<PatternSphere>() != null)
		{
			obj.GetComponent<PatternSphere>().Execute();
		}
		if (obj.GetComponent<PinButton>() != null)
		{
			obj.GetComponent<PinButton>().Execute();
		}
		if (obj.GetComponent<OkButton>() != null)
		{
			obj.GetComponent<OkButton>().Execute();
		}
		if (obj.GetComponent<HoloUI>() != null)
		{
			obj.GetComponent<HoloUI>().Execute();
		}
	}

}
