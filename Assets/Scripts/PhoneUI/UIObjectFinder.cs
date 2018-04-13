using UnityEngine;

public class UIObjectFinder {

	public static void Find(GameObject obj)
	{
		if (obj.GetComponent<OpenDoorButton>() != null)
		{
			var script = (OpenDoorButton) obj.GetComponent<PhoneUI>();
			script.Execute();
		}
	}

}
