using UnityEngine;

public class World : MonoBehaviour
{
	public GameObject playerStorage;

	public GameObject leftController;
	public GameObject rightController;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.8f)
		{

		}

		if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.8f)
		{

		}
	}
}
