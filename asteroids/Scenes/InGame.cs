using asteroids.Components;
using SlimMath;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Gui;

namespace asteroids.Scenes
{
    public static class InGame
    {
        public static void LoadInto(Scene scene)
        {
            scene.CreateEntityFromComponent(new GameDirector());
            scene.CreateEntityFromComponent(new ScoreKeeper());
            CreateHudComponents(scene);
        }

        private static void CreateHudComponents(Scene scene)
        {
            var hudControllerEntity = scene.CreateEntity("hud_controller_entity");
            hudControllerEntity.CreateComponent<GuiRootComponent>();

            // ---- Progress Status Text
            var labelEntity = scene.CreateEntity("label_entity", hudControllerEntity);
            labelEntity.LocalPosition = new Vector3(30, 30, 0);
            labelEntity.CreateComponent<LabelWidgetComponent>(component =>
            {
                component.FontSize = 22;
            });
            

            // ---- Ship Health Bar
            var shipHealthEntity = scene.CreateEntity("shiphealth_entity", hudControllerEntity);
            shipHealthEntity.LocalPosition = new Vector3(450, 10, 0);
            shipHealthEntity.CreateComponent<ImageWidgetComponent>(component =>
            {
                component.ImageName = "Textures/healthbar.png";
                component.Size = new Vector2(200, 20);
            });

            // ---- Game Over Label
            var gameEndLabelEntity = scene.CreateEntity("gameover_entity", hudControllerEntity);
            gameEndLabelEntity.LocalPosition = new Vector3(270, 270, 0);
            gameEndLabelEntity.CreateComponent<LabelWidgetComponent>(component =>
            {
                component.Text = "";
                component.FontSize = 50;
                component.Visible = false;
            });

            var scoreLabelEntity = scene.CreateEntity("score_entity", hudControllerEntity);
            scoreLabelEntity.LocalPosition = new Vector3(450, 30, 0);
            scoreLabelEntity.CreateComponent<LabelWidgetComponent>(component =>
            {
                component.Text = "";
                component.FontSize = 22;
                component.Visible = true;
            });

            // ---- Controlling entity
            hudControllerEntity.CreateComponent<HudController>(component =>
            {
                component.ShipHealth = scene.GetEntityByName("shiphealth_entity").GetComponent<ImageWidgetComponent>();
                component.StatusLabel = scene.GetEntityByName("label_entity").GetComponent<LabelWidgetComponent>();
                component.GameOverLabel = scene.GetEntityByName("gameover_entity").GetComponent<LabelWidgetComponent>();
                component.ScoreLabel = scene.GetEntityByName("score_entity").GetComponent<LabelWidgetComponent>();
            });



            var camera = scene.CreateEntity("camera");

            camera.CreateComponent<CameraComponent>(component =>
            {
                CameraComponent.Main.Zoom = 80;
            });
        }
    }
}