var MovementVector = new SlimMath.Vector3(0, 0, 0);
var MovementSpeed = 0;

function OnUpdate(delta)
{
	var mv = SlimMath.Vector3.Add(Entity.LocalPosition, SlimMath.Vector3.Multiply(MovementVector, MovementSpeed * delta));
	Entity.LocalPosition = mv;
}
