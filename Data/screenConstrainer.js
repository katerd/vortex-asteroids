var Extents;

function OnUpdate(delta)
{
    var vx = Entity.RigidbodyComponent.Velocity.X;
    var vy = Entity.RigidbodyComponent.Velocity.Y;

    var x = Entity.LocalPosition.X;
    var y = Entity.LocalPosition.Y;

    var ex = Extents.X;
    var ey = Extents.Y;

    if ((x > ex && vx > 0) || (x < -ex && vx < 0))
    {
        vx = -vx;
    }

    if ((y > ey && vy > 0) || (y < -ey && vy < 0))
    {
        vy = -vy;
    }

    Entity.RigidbodyComponent.Velocity = new SlimMath.Vector3(vx, vy, 0);
}