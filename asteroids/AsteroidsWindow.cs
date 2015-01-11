using asteroids.Components;
using asteroids.Scenes;
using SlimMath;
using Vortex.Bootstrap;
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

            GameConsole.CommandBindings.Bind("lives", "Set number of lives remaining", SetLivesHandler);
            GameConsole.CommandBindings.Bind("level", "Load a specific level", LoadLevelHandler);
            GameConsole.CommandBindings.Bind("boom", "Destroy all asteroids", BoomHandler);
        }

        private void BoomHandler(params string[] parameters)
        {
            var roids = Scene.GetEntitiesWithComponent<Asteroid>();
            foreach (var roid in roids)
            {
                roid.GetComponent<Asteroid>().Nuke();
            }
        }

        private void LoadLevelHandler(params string[] parameters)
        {
            int level;
            if (!int.TryParse(parameters[1], out level))
                return;

            var director = Scene.GetComponent<GameDirector>();
            director.StartLevel(level);
        }

        private void SetLivesHandler(params string[] parameters)
        {
            int lives;
            if (!int.TryParse(parameters[1], out lives))
                return;

            var director = Scene.GetComponent<GameDirector>();

            director.LivesRemaining = lives;
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