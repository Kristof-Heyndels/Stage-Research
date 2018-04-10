using UnityEngine;

public class Phone : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (PlayerStorage.isInit() && !gameObject.GetComponent<OVRGrabbable>().isGrabbed)
		{
			transform.position = PlayerStorage.pocketLocation();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (!PlayerStorage.isInit())
		{
			if (other.CompareTag("Body") && gameObject.GetComponent<OVRGrabbable>().isGrabbed)
			{
				PlayerStorage.pocketEnabled(true);
				PlayerStorage.movePocket(gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Body") && gameObject.GetComponent<OVRGrabbable>().isGrabbed)
		{
			PlayerStorage.pocketEnabled(false);
		}
		else if (other.CompareTag("Pocket") && !gameObject.GetComponent<OVRGrabbable>().isGrabbed)
		{
			PlayerStorage.checkInit();
		}
	}
}
