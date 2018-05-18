using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyTest : MonoBehaviour
{
	public GameObject activationCube;
	public GameObject lighting;
	public Text infoText;

	public bool testComplete;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init()
	{
		lighting.SetActive(false);
		infoText.text = "Your test is in another lab";
		testComplete = true;
		StartCoroutine(TurnOnLights());
	}
	private IEnumerator TurnOnLights()
	{
		yield return new WaitForSeconds(5);
		lighting.SetActive(true);

	}
}
