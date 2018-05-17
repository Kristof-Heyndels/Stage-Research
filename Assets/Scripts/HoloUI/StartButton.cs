public class StartButton : HoloUI
{
	public ColourPickerTestManager ColourPickerTest { get; set; }

	public override void Execute()
	{
		if (!Pauser.isPaused)
		{
			if (ColourPickerTest != null)
			{
				if (!ColourPickerTest.testComplete) ColourPickerTest.BeginTest();
				ColourPickerTest = null;
			}
		}
	}
}
