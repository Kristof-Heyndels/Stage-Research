using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColourDropdown : HoloUI
{
	public ColourPickerTestManager colourPickerTest;

	private Dropdown colourDrop;
	private bool hidden = true;
	private bool enableToggle = true;


	void Start()
	{
		colourDrop = gameObject.GetComponent<Dropdown>();
	}

	public override void Execute()
	{
		if (!enableToggle) return;

		if (hidden)
		{
			colourDrop.Show();
			hidden = false;
			enableToggle = false;
			StartCoroutine(WaitHalfSecond());
		}
		else
		{
			colourDrop.Hide();
			hidden = true;
			enableToggle = false;
			StartCoroutine(WaitHalfSecond());
		}
	}

	public void ColourSelected()
	{
		if (!enableToggle) return;
		
		colourDrop.Hide();
		hidden = true;
		colourPickerTest.ColourSelected(colourDrop.captionText.text);
	}

	private IEnumerator WaitHalfSecond()
	{
		yield return new WaitForSeconds(0.5f);
		enableToggle = true;
	}
}
