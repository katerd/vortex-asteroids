var Extents;

function onUpdate(delta)
{
    var vx = entity.RigidbodyComponent.Velocity.X;
    var vy = entity.RigidbodyComponent.Velocity.Y;

    var x = entity.LocalPosition.X;
    var y = entity.LocalPosition.Y;

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

    entity.RigidbodyComponent.Velocity = new SlimMath.Vector3(vx, vy, 0);
}