using UnityEngine;

public class OpenDoorButton : HoloUI
{
	public GameObject Door { get; set; }

	public override void Execute()
	{
		if (!Pauser.isPaused)
		{
			var doorScript = Door.GetComponent<Door>();
			doorScript.Open();
			if (doorScript.IsDataDoor())
			{
				World.buttonCounter++;
			}
		}
	}
}
