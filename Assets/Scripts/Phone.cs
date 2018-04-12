using UnityEngine;

public class Phone : MonoBehaviour
{
	private float offset;
	private bool init;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//offset = GameObject.FindGameObjectWithTag("MainCamera").transform.eulerAngles.y;

		//if (PlayerStorage.isInit() && !gameObject.GetComponent<OVRGrabbable>().isGrabbed)
		//{
		//	transform.position = PlayerStorage.pocketLocation();
		//	transform.eulerAngles = new Vector3(0, 90 + offset, 0);
		//}
	}

	void OnTriggerStay(Collider other)
	{
		//if (!PlayerStorage.isInit())
		//{
		//	if (other.CompareTag("Body") && gameObject.GetComponent<OVRGrabbable>().isGrabbed)
		//	{
		//		PlayerStorage.pocketEnabled(true);
		//		PlayerStorage.movePocket(gameObject);
		//	}
		//}
	}

	void OnTriggerExit(Collider other)
	{
		//if (!PlayerStorage.isInit())
		//{
		//	if (other.CompareTag("Body") && gameObject.GetComponent<OVRGrabbable>().isGrabbed)
		//	{
		//		PlayerStorage.pocketEnabled(false);
		//	}
		//	else if (other.CompareTag("Pocket") && !gameObject.GetComponent<OVRGrabbable>().isGrabbed)
		//	{
		//		PlayerStorage.checkInit();
		//	}
		//}
	}
}
