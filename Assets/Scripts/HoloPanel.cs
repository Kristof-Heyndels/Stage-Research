using System;
using UnityEngine;
using UnityEngine.UI;

public class HoloPanel : MonoBehaviour
{
	public GameObject LogTextPrefab;
	public GameObject button;

	private float offset;
	private bool init;

	private static GameObject LogText;

	// Use this for initialization
	void Start()
	{
		LogText = Instantiate(LogTextPrefab, transform.Find("LogPanel"));
		button.GetComponent<Button>().interactable = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("DoorTrigger"))
		{
			// can unlock gate
			var openButton = GetComponentInChildren<OpenDoorButton>();
			openButton.Door = other.transform.parent.GetComponentInChildren<Door>().gameObject;
			button.GetComponent<Button>().interactable = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("DoorTrigger"))
		{
			button.GetComponent<Button>().interactable = false;
		}
	}

	public static void Log(object message)
	{
		LogText.GetComponent<Text>().text = message.ToString();
	}
	public static void LogFormat(string message, params object[] arg)
	{
		LogText.GetComponent<Text>().text = string.Format(message, arg);
	}

}
