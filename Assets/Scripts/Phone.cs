using System;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
	public GameObject DoorButtonPhonePrefab;
	public GameObject LogTextPrefab;

	private float offset;
	private bool init;
	private GameObject doorButton;

	private static GameObject LogText;

	// Use this for initialization
	void Start()
	{
		LogText = Instantiate(LogTextPrefab, transform.GetComponentInChildren<Canvas>().transform);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("DoorTrigger") && doorButton == null)
		{
			// can unlock gate
			doorButton = Instantiate(DoorButtonPhonePrefab, transform.GetComponentInChildren<Canvas>().transform);
			doorButton.GetComponent<OpenDoorButton>().Door = other.transform.parent.GetComponentInChildren<Door>().gameObject;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("DoorTrigger"))
		{
			Destroy(doorButton);
		}
	}

	public static void Log(object message)
	{
		LogText.GetComponent<Text>().text = message.ToString();
	}

}
