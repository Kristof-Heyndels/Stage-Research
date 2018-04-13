using UnityEngine;

public class Door : MonoBehaviour
{
	private bool shouldOpen;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (shouldOpen && transform.position.y < 5 && !Pauser.isPaused)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
			Destroy(GameObject.FindGameObjectWithTag("StopGroup"));
		}
	}

	public void Open()
	{
		shouldOpen = true;
		for (int i = 0; i < transform.parent.childCount; i++)
		{
			var child = transform.parent.GetChild(i);
			if (child.CompareTag("DoorTrigger"))
			{
				Destroy(child.gameObject);
			}
		}
	}
}
