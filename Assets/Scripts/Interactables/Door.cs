using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
	public GameObject HandScannerPrefab;
	public GameObject DoorTriggerPrefab;
	public bool lockedDoor;

	public EndDemo endDemo;

	private GameObject scanner;
	private GameObject trigger;

	private Vector3 closedPosition;
	private bool shouldOpen;
	private bool dataDoor;

	// Use this for initialization
	void Start()
	{
		closedPosition = transform.position;
		Init(transform.parent);
	}

	public void Init(Transform parent)
	{
		var noScanner = true;
		var noTrigger = true;

		shouldOpen = false;
		dataDoor = true;

		if (lockedDoor) return;

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
		}

		if (DoorTriggerPrefab != null)
		{
			Destroy(trigger);
		}

		if (endDemo != null)
		{
			endDemo.FinishDemo();
		}

		StartCoroutine(Close());
	}

	public IEnumerator Close()
	{
		yield return new WaitForSeconds(10);
		transform.position = closedPosition;
		Init(transform.parent);
	}

	public bool IsDataDoor()
	{
		return dataDoor;
	}
}
