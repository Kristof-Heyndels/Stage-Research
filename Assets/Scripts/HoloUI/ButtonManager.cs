using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveButton
{
	Open,
	Start
}

public class ButtonManager : MonoBehaviour
{
	public GameObject open;
	public GameObject start;

	public static ActiveButton activeButton;

	private List<GameObject> buttons;

	// Use this for initialization
	void Start()
	{
		buttons.Add(open);
		buttons.Add(start);
	}

	// Update is called once per frame
	void Update()
	{
		if (activeButton == ActiveButton.Open && !open.activeSelf)
		{
			foreach (var b in buttons)
			{
				b.SetActive(false);
			}
			open.SetActive(true);
		}
		else if (activeButton == ActiveButton.Start)
		{
			foreach (var b in buttons)
			{
				b.SetActive(false);
			}
			start.SetActive(true);
		}
	}
}
