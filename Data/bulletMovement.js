/// <reference path="scriptDefines.js" />

var MovementVector = new SlimMath.Vector3(0, 0, 0);

var MovementSpeed = 0;

function onUpdate(delta) {

    var mv = SlimMath.Vector3.Add(
        entity.TransformComponent.LocalPosition,
        SlimMath.Vector3.Multiply(MovementVector, MovementSpeed * delta));

    entity.TransformComponent.LocalPosition = mv;
}
