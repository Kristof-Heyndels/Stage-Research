using UnityEngine.UI;

public class DropItem : HoloUI
{
	public Dropdown dropdown;

	private float con1;
	private float con2;

	public override void Execute()
	{
		var con1 = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
		var con2 = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

		if (con1 < 0.5f && 0.5f < this.con1 || con2 < 0.5f && 0.5f < this.con2)
		{
			gameObject.GetComponent<Toggle>().Select();
			dropdown.value = transform.GetSiblingIndex() - 1;
			dropdown.gameObject.GetComponent<ColourDropdown>().ColourSelected();
		}

		this.con1 = con1;
		this.con2 = con2;

	}
}
