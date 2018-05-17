using UnityEngine;

public class ColourCube : MonoBehaviour
{

	public ColourPickerTestManager colourTestManager;

	public RotationDirection direction;
	public float rotationSpeed;

	public float hoverRange;
	public float hoverSpeed;

	public bool isStartCube;

	private float pingpong = 1;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (isStartCube)
		{
			pingpong += 1 * hoverSpeed;

			if (transform.localPosition.y < hoverRange)
			{
				transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(pingpong, hoverRange) / 100, transform.localPosition.z);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<OvrAvatarTouchController>() != null)
		{
			if (isStartCube)
			{
				colourTestManager.NextTest();
			}
			else
			{
				var colour = gameObject.GetComponent<Renderer>().material.color;
				colourTestManager.ColourSelected(ColourRGBA.ToName(colour));
			}
		}
	}
}
