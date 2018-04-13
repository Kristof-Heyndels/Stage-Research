using UnityEngine;

public class Scanner : MonoBehaviour
{
	public enum RotationDirection
	{
		Left,
		Right
	}
	public RotationDirection direction;
	public float rotationSpeed;

	public float hoverRange;
	public float hoverSpeed;

	private float pingpong;

	// Use this for initialization
	void Start()
	{
		pingpong = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (Pauser.isPaused) return;

		if (direction == RotationDirection.Left)
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + (rotationSpeed / 10), transform.localEulerAngles.z);
		}
		else
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - (rotationSpeed / 10), transform.localEulerAngles.z);
		}

		pingpong += 1 * hoverSpeed;

		if (transform.localPosition.y < hoverRange)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(pingpong, hoverRange) / 100, transform.localPosition.z);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<OvrAvatarTouchController>() != null && !Pauser.isPaused)
		{
			var door = transform.parent.parent.GetComponentInChildren<Door>().gameObject;
			door.GetComponent<Door>().Open();
			Destroy(transform.parent.gameObject);
		}
	}
}
