using System.Windows.Forms;
using SlimMath;
using Vortex.Core.Extensions;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Collision;

namespace asteroids.Components
{
    public class ShipMovement : ScriptComponent
    {
        public float ForwardThrustPower { get; set; }
        public float ReverseThrustPower { get; set; }
        public float TurnPower { get; set; }
        public float TurnDamping { get; set; }

        private bool _leftKeyDown;
        private bool _rightKeyDown;
        private float _rotateDirection;
        private bool _upKeyDown;
        private bool _downKeyDown;
        private Vector3 _movement;
        private RigidbodyComponent _rigidBodyComponent;
        private LightComponent _engineLight;

        public ShipMovement()
        {
            _leftKeyDown = false;
            _rightKeyDown = false;
            _upKeyDown = false;
            _downKeyDown = false;
            _rotateDirection = 0;
            _movement = new Vector3();

            TurnPower = 0.04f;
            TurnDamping = 0.09f;
            ForwardThrustPower = 0.8f;
            ReverseThrustPower = 0.002f;
        }

        public override void Initialize()
        {
            base.Initialize();
            _rigidBodyComponent = Entity.GetComponent<RigidbodyComponent>();
            _engineLight = Entity.GetComponentInSelfOrChildren<LightComponent>();
        }

        public override void OnKeyDown(Keys keyCode)
        {
            base.OnKeyDown(keyCode);

            switch (keyCode)
            {
                case Keys.Left:
                    _leftKeyDown = true;
                    break;
                case Keys.Right:
                    _rightKeyDown = true;
                    break;
                case Keys.Up:
                    _upKeyDown = true;
                    break;
                case Keys.Down:
                    _downKeyDown = true;
                    break;
            }
        }

        public override void OnKeyUp(Keys keyCode)
        {
            base.OnKeyUp(keyCode);

            switch (keyCode)
            {
                case Keys.Left:
                    _leftKeyDown = false;
                    break;
                case Keys.Right:
                    _rightKeyDown = false;
                    break;
                case Keys.Up:
                    _upKeyDown = false;
                    break;
                case Keys.Down:
                    _downKeyDown = false;
                    break;

            }
        }

        public void Stop()
        {
            _movement = new Vector3();
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            _rigidBodyComponent = Entity.GetComponent<RigidbodyComponent>();

            float rotateImpulse = 0;
            if (_leftKeyDown)
            {
                rotateImpulse -= TurnPower;
            }
            else if (_rightKeyDown)
            {
                rotateImpulse += TurnPower;
            }
            _rotateDirection *= (1 - TurnDamping);
            _rotateDirection += rotateImpulse;
            Entity.LocalRotation += new Vector3(0, 0, _rotateDirection);

            var v = Vector3.Transform(new Vector3(1.0f, 0, 0), Entity.LocalRotationMatrix).AsVector3();

            var movementImpulse = 0f;
            if (_upKeyDown)
            {
                movementImpulse += ForwardThrustPower;
            }
            else if (_downKeyDown)
            {
                movementImpulse -= ReverseThrustPower;
            }

            _engineLight.Range = 16;//*(movementImpulse/ForwardThrustPower);
            _engineLight.Intensity = 0.3f;//0.1f*StaticRng.Random.NextFloat(0.5f, 1.0f);

            _movement = movementImpulse * v;

            _rigidBodyComponent.Velocity -= _movement;
        }
    }
}