using UnityEngine.UI;

public class DropItem : HoloUI
{
	public Dropdown dropdown;
	public override void Execute()
	{
		gameObject.GetComponent<Toggle>().Select();
		dropdown.value = transform.GetSiblingIndex() - 1;
		dropdown.gameObject.GetComponent<ColourDropdown>().ColourSelected();
	}
}
