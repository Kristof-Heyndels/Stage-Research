using UnityEngine;

public class ColourCube : MonoBehaviour
{

	public ColourPickerTestManager colourTestManager;

	public RotationDirection direction;
	public float rotationSpeed;

	public float hoverRange;
	public float hoverSpeed;

	public bool isStartCube;
	public bool fakeCube;

	private float pingpong = 1;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (isStartCube || fakeCube)
		{
			pingpong += 1 * hoverSpeed;

			if (transform.localPosition.y < hoverRange)
			{
				transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(pingpong, hoverRange) / 100,
					transform.localPosition.z);
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
			else if (fakeCube)
			{
				transform.parent.parent.parent.GetComponent<EmptyTest>().Init();
				Destroy(gameObject);
			}
			else
			{
				var colour = gameObject.GetComponent<Renderer>().material.color;
				colourTestManager.ColourSelected(ColourRGBA.ToName(colour));
				gameObject.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (isStartCube) return;
		gameObject.GetComponent<Renderer>().enabled = true;
	}
}
