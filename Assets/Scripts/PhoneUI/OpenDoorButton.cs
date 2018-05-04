using UnityEngine;

public class OpenDoorButton : PhoneUI
{
	private GameObject door;
	public GameObject Door
	{
		get { return door; }

		set
		{
			door = value;
		}
	}

	public void Execute()
	{
		if (!Pauser.isPaused)
		{
			var doorScript = door.GetComponent<Door>();
			doorScript.Open();
			if (doorScript.IsDataDoor())
			{
				World.buttonCounter++;
			}
		}
	}
}
