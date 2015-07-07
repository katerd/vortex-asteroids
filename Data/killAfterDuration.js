/// <reference path="scriptDefines.js" />

var KillTime;

function onUpdate(delta)
{
	if (Vortex.Core.Timer.GetTime() >= KillTime) {
	    entity.Destroy();
	}
}