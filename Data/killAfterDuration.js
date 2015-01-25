var KillTime;

function OnUpdate(delta)
{
	if (Vortex.Core.Timer.GetTime() >= KillTime)
	{
		Entity.Destroy();
	}
}