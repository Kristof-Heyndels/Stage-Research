using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
	public GameObject head;
	public GameObject body;
	public GameObject pocketPrefab;

	public static GameObject pocket;

	private static bool init;

	// Use this for initialization
	void Start()
	{
		pocket = Instantiate(pocketPrefab, body.transform);
		init = false;
	}

	// Update is called once per frame
	void Update()
	{
		head.transform.position = Camera.main.transform.position;
		body.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - 0.5f, head.transform.position.z);

		head.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z);
		body.transform.eulerAngles = Vector3.zero;
		pocket.transform.eulerAngles = Vector3.zero;
	}

	public static void pocketEnabled(bool enable)
	{
		pocket.SetActive(enable);
	}

	public static void movePocket(GameObject phone)
	{
		pocket.transform.position = phone.GetComponent<OVRGrabbable>().grabbedBy.transform.position;
	}

	public static void checkInit()
	{
		if (pocket.activeSelf)
		{
			init = true;
		}
		else
		{
			init = false;
		}
	}

	public static bool isInit()
	{
		return init;
	}

	public static Vector3 pocketLocation()
	{
		return pocket.transform.position;
	}
}
