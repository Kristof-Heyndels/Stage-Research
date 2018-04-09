using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
	public GameObject head;
	public GameObject body;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		head.transform.position = Camera.main.transform.position;
		body.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - 0.75f, head.transform.position.z);

		head.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z);
		body.transform.eulerAngles = Vector3.zero;
	}
}
