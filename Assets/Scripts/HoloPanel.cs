using UnityEngine;
using UnityEngine.UI;

public class HoloPanel : MonoBehaviour
{
	public GameObject LogTextPrefab;
	public GameObject button;
	public GameObject[] panelArray;

	public Color colour = ColourRGBA.white;

	private ColourPickerTestManager colourPickerTestManager;

	private float offset;
	private bool init;

	private static GameObject LogText;

	// Use this for initialization
	void Start()
	{
		LogText = Instantiate(LogTextPrefab, transform.Find("LogPanel"));
		button.GetComponent<Button>().interactable = false;

		colourPickerTestManager = transform.root.Find("ColourPickerTestManager").GetComponent<ColourPickerTestManager>();
		SetColour(colour);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Equals("DoorTrigger"))
		{
			// can unlock gate
			var openButton = GetComponentInChildren<OpenDoorButton>();
			openButton.Door = other.transform.parent.GetComponentInChildren<Door>().gameObject;
			button.GetComponent<Button>().interactable = true;
		}
		else if (other.gameObject.name.Equals("ColourPickerArea"))
		{
			var startButton = GetComponentInChildren<StartButton>();
			startButton.ColourPickerTest = colourPickerTestManager;
			colourPickerTestManager.errorText.text = "Engage the button to begin test procedure";
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("DoorTrigger"))
		{
			var openButton = GetComponentInChildren<OpenDoorButton>();
			openButton.Door = null;
			button.GetComponent<Button>().interactable = false;
		}
		else if (other.gameObject.name.Equals("ColourPickerArea"))
		{
			var startButton = GetComponentInChildren<StartButton>();
			startButton.ColourPickerTest = null;
			colourPickerTestManager.errorText.text = "Approach the testing area and open your Holo";
		}
	}

	public void SetColour(Color c)
	{
		foreach (var panel in panelArray)
		{
			panel.GetComponent<Image>().color = c;
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
