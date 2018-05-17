public static class Pauser
{
	public static bool isPaused;
	public static void Pause(bool state)
	{
		isPaused = state;
		World.teleporter.ToggleTeleportEnabled(!state);
	}
}
