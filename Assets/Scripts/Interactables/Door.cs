using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
	public GameObject HandScannerPrefab;
	public GameObject DoorTriggerPrefab;
	public GameObject PatternLockPrefab;

	private GameObject scanner;
	private GameObject trigger;
	private GameObject patternLock;

	private Vector3 closedPosition;
	private bool shouldOpen;
	private bool dataDoor;

	// Use this for initialization
	void Start()
	{
		closedPosition = transform.position;
		Init(transform.parent);
	}

	private void Init(Transform parent)
	{
		var noScanner = true;
		var noTrigger = true;

		shouldOpen = false;
		dataDoor = true;

		if (HandScannerPrefab != null)
		{
			scanner = Instantiate(HandScannerPrefab, parent);
			noScanner = false;
		}

		if (DoorTriggerPrefab != null)
		{
			trigger = Instantiate(DoorTriggerPrefab, parent);
			noTrigger = false;
		}
		if (PatternLockPrefab != null)
		{
			patternLock = Instantiate(PatternLockPrefab, parent.Find("DoorCube"));
		}

		if (noScanner || noTrigger)
		{
			dataDoor = false;
		}
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

		if (HandScannerPrefab != null)
		{
			Destroy(scanner);
			StartCoroutine(Close());
		}

		if (DoorTriggerPrefab != null)
		{
			Destroy(trigger);
			StartCoroutine(Close());
		}
		if (PatternLockPrefab != null)
		{
			Destroy(patternLock);
		}
	}

	public IEnumerator Close()
	{
		yield return new WaitForSeconds(20);
		transform.position = closedPosition;
		Init(transform.parent);
	}

	public bool IsDataDoor()
	{
		return dataDoor;
	}
}
