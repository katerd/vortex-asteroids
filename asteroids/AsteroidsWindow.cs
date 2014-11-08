using asteroids.Components;
using asteroids.Spawners;
using SlimMath;
using Vortex.Bootstrap;
using Vortex.Graphics.Enums;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Gui;

namespace asteroids
{
    internal class AsteroidsWindow : GameWindow
    {
        protected override void OnResourceLoad()
        {
            LoadScene();

            base.OnResourceLoad();
        }

        private void CreateHudComponents()
        {
            var hudControllerEntity = Scene.CreateEntity("hud_controller_entity");
            hudControllerEntity.CreateComponent<HudController>(component =>
            {
                component.ShipHealth = Scene.EntityByName("shiphealth_entity").GetComponent<ImageWidgetComponent>();
                component.StatusLabel = Scene.EntityByName("label_entity").GetComponent<LabelWidgetComponent>();
                component.GameDirector = Scene.EntityWithComponent<GameDirector>().GetComponent<GameDirector>();
            });

            var root = new GuiRootComponent();
            hudControllerEntity.AddComponent(root);
            var labelEntity = Scene.CreateEntity("label_entity");
            labelEntity.Parent = hudControllerEntity;
            labelEntity.CreateComponent<LabelWidgetComponent>();
            labelEntity.LocalPosition = new Vector3(30, 30, 0);

            var shipHealthEntity = Scene.CreateEntity("shiphealth_entity", hudControllerEntity);
            shipHealthEntity.LocalPosition = new Vector3(450, 10, 0);
            shipHealthEntity.CreateComponent<ImageWidgetComponent>(component =>
            {
                component.ImageName = "Textures/healthbar.png";
                component.Size = new Vector2(200, 20);
            });

            var camera = Scene.CreateEntity("camera");

            camera.CreateComponent<CameraComponent>(component =>
            {
                //component.Zoom = 60;
                CameraComponent.Main.Zoom = 80;
            });
        }

        private void LoadScene()
        {
            Scene.CreateEntityFromComponent(new GameDirector());

            CreateHudComponents();

            ShipSpawner.SpawnIn(Scene, new Vector3(5, 0, 0));

            SetSceneLighting();
        }

        private void SetSceneLighting()
        {
            GraphicsContext.ClearColour = new Color4(1.0f, 0.01f, 0.21f, 0.21f);

            Scene.AmbientLight = new Color4(1.0f, 0.5f, 0.5f, 0.5f);

            //var light = Scene.CreateEntity();
            //light.AddComponent(new LightComponent
            //{
            //    Colour = new Color4(1.0f, 0.0f, 0.1f, 0.2f),
            //    Intensity = 0.5f,
            //    LightType = LightType.Directional,
            //});
            //light.LocalRotation = new Vector3(1, 0, 0.6f);
        }
    }
}