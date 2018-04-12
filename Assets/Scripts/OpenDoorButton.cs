using UnityEngine;

public class OpenDoorButton : PhoneUI
{
	public GameObject door;

	public void Execute()
	{
		Debug.Log("Opening door");

		door = GameObject.FindGameObjectWithTag("DoorTrigger").transform.parent.gameObject;
		door.GetComponent<Door>().Open();
	}
}
