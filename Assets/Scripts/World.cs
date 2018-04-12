using UnityEngine;

public class World : MonoBehaviour
{

	public enum Handedness
	{
		Right,
		Left
	}

	public GameObject localAvatar;
	public GameObject phone;

	public GameObject avatarGrabberLeft;
	public GameObject avatarGrabberRight;

	public Handedness handedness;

	private float oldPrimaryStick;
	private float oldSecondaryStick;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//NOTE(Kristof): Rotating the avatar with the joysticks
		{
			var turnAngle = 0f;

			if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > 0.8f && oldPrimaryStick < 0.8f)
			{
				turnAngle = 30;
			}
			else if (oldPrimaryStick > -0.8f && OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < -0.8f)
			{
				turnAngle = -30;
			}

			if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x > 0.8f && oldSecondaryStick < 0.8f)
			{
				turnAngle = 30;
			}
			else if (oldSecondaryStick > -0.8f && OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x < -0.8f)
			{
				turnAngle = -30;
			}

			oldPrimaryStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
			oldSecondaryStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

			localAvatar.transform.localEulerAngles = new Vector3(localAvatar.transform.localEulerAngles.x,
				localAvatar.transform.localEulerAngles.y + turnAngle, localAvatar.transform.localEulerAngles.z);
		}

		//NOTE(Kristof): Grabbing the phone
		{
			if (handedness == Handedness.Right)
			{
				//NOTE(Kristof): phone child left hand
				phone.transform.parent = avatarGrabberLeft.transform.GetChild(0).transform;
				phone.transform.localPosition = Vector3.zero;
				phone.transform.localEulerAngles = new Vector3(45, 0, 0);
			}
			else
			{
				//NOTE(Kristof): phone child right hand
				phone.transform.parent = avatarGrabberRight.transform.GetChild(0).transform;
				phone.transform.localPosition = Vector3.zero;
				phone.transform.localEulerAngles = new Vector3(45, 0, 0);
			}
		}
	}
}