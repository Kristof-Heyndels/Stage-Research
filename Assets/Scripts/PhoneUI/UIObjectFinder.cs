using UnityEngine;
using UnityEngine.UI;

public class UIObjectFinder {

	public static void Find(GameObject obj)
	{
		if (obj.GetComponent<OpenDoorButton>() != null)
		{
			var script = (OpenDoorButton) obj.GetComponent<PhoneUI>();
			var unityButton = script.gameObject.GetComponent<Button>();
			if (unityButton.interactable)
			{
				script.Execute();
				unityButton.interactable = false;
			}
		}
	}

}
