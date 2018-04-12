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
		if (shouldOpen && transform.position.y < 5)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.002f, transform.position.z);
			Destroy(GameObject.FindGameObjectWithTag("StopGroup"));
		}
	}

	public void Open()
	{
		shouldOpen = true;
	}
}
