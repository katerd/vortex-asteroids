var MovementVector = new SlimMath.Vector3(0, 0, 0);
var MovementSpeed = 0;

function onUpdate(delta) {

    log(MovementSpeed);

    var mv = SlimMath.Vector3.Add(
        entity.LocalPosition,
        SlimMath.Vector3.Multiply(MovementVector, MovementSpeed * delta));

    entity.LocalPosition = mv;
}
