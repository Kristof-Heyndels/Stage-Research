using UnityEngine;
using UnityEngine.UI;

public class HoloPanel : MonoBehaviour
{
	public GameObject LogTextPrefab;
	public GameObject[] panelArray;

	public GameObject LogPanel;
	public ColourPickerTestManager colourPickerTestManager;
	public OpenDoorButton openButton;
	public StartButton startButton;


	private float offset;
	private bool init;


	private static GameObject LogText;

	// Use this for initialization
	void Start()
	{
		LogText = Instantiate(LogTextPrefab, LogPanel.transform);
		openButton.gameObject.GetComponent<Button>().interactable = false;
		startButton.gameObject.GetComponent<Button>().interactable = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("DoorTrigger"))
		{
			// can unlock gate
			openButton.Door = other.transform.parent.GetComponentInChildren<Door>().gameObject;
			openButton.gameObject.GetComponent<Button>().interactable = true;
		}
		else if (other.gameObject.name.Equals("ColourPickerArea") && !colourPickerTestManager.IsTesting())
		{
			ButtonManager.activeButton = ActiveButton.Start;
			startButton.gameObject.GetComponent<Button>().interactable = true;
			startButton.ColourPickerTest = colourPickerTestManager;

			if (!colourPickerTestManager.testComplete)
			{
				colourPickerTestManager.enabled = true;
				colourPickerTestManager.infoText.text = "Engage the button to begin test procedure";
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("DoorTrigger"))
		{
			openButton.Door = null;
			openButton.GetComponent<Button>().interactable = false;
		}
		else if (other.gameObject.name.Equals("ColourPickerArea"))
		{
			ButtonManager.activeButton = ActiveButton.Open;
			startButton.gameObject.GetComponent<Button>().interactable = false;

			colourPickerTestManager.enabled = false;
			startButton.ColourPickerTest = null;
			colourPickerTestManager.infoText.text = "Approach the testing area and open your Holo";
		}
	}

	public void SetColour(Color c)
	{
		foreach (var panel in panelArray)
		{
			panel.GetComponent<Image>().color = c;
		}

		if (c == ColourRGBA.grey)
		{
			LogText.GetComponent<Text>().color = Color.white;
			transform.Find("ColourPickerPanel").GetComponentInChildren<Text>().color = Color.white;
		}
		else
		{
			var newTextColour = new Color(1 - c.r, 1 - c.g, 1 - c.b);
			LogText.GetComponent<Text>().color = newTextColour;
			transform.Find("ColourPickerPanel").GetComponentInChildren<Text>().color = newTextColour;
		}
	}

	public static void LogNotification(string message, params object[] arg)
	{
		LogText.GetComponent<Text>().text = string.Format(message, arg);
	}

	public static void Log(object message)
	{
		LogText.GetComponent<Text>().text = message.ToString();
		Debug.Log(message);
	}
	public static void LogFormat(string message, params object[] arg)
	{
		LogText.GetComponent<Text>().text = string.Format(message, arg);
		Debug.LogFormat(string.Format(message, arg));
	}

}
