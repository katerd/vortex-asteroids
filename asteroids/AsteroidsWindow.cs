using asteroids.Components;
using asteroids.ConsoleCommands;
using asteroids.Scenes;
using SlimMath;
using Vortex.Bootstrap;
using Vortex.Core.Console.Commands;
using Vortex.Graphics.Enums;
using Vortex.Scenegraph.Components;

namespace asteroids
{
    internal class AsteroidsWindow : GameWindow
    {
        protected override void OnResourceLoad()
        {
            base.OnResourceLoad();

            ConsoleRenderer.Lines = 5;

            SetSceneLighting();
            InGame.LoadInto(Scene);

            GameConsole.SetContextItem(Scene);
            GameConsole.RegisterAllCommands();
        }

        private void SetSceneLighting()
        {
            GraphicsContext.ClearColour = new Color4(1.0f, 0, 0.01f, 0.02f);
            Scene.AmbientLight = new Color4(1.0f, 0.2f, 0.2f, 0.2f);

            var light = Scene.CreateEntity();
            light.AddComponent(new LightComponent
            {
                Colour = new Color4(1.0f, 0.0f, 0.1f, 0.2f),
                Intensity = 0.5f,
                LightType = LightType.Directional,
            });
            light.LocalRotation = new Vector3(1, 0, 0.6f);
        }
    }
}