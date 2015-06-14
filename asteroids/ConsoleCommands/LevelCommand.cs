using asteroids.Components;
using Vortex.Core.Console.Commands;
using Vortex.Scenegraph;

namespace asteroids.ConsoleCommands
{
    public class LevelCommand : ConsoleCommand
    {
        public Scene Scene { get { return GetContextItem<Scene>(); } }

        public override void Execute(params string[] parameters)
        {
            int level;
            if (!int.TryParse(parameters[1], out level))
                return;

            var director = Scene.GetComponent<GameDirector>();
            director.StartLevel(level);
        }

        public override string[] HelpText
        {
            get { return new []{"Loads a new level"}; }
        }

        public override string CommandName
        {
            get { return "level"; }
        }
    }
}