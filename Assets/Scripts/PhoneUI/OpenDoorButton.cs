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
			door.GetComponent<Door>().Open();
			Destroy(gameObject);
		}
	}
}
