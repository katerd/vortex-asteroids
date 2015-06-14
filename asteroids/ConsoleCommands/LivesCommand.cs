using asteroids.Components;
using Vortex.Core.Console.Commands;
using Vortex.Scenegraph;

namespace asteroids.ConsoleCommands
{
    public class LivesCommand : ConsoleCommand
    {
        public Scene Scene { get { return GetContextItem<Scene>(); } }

        public override string CommandName
        {
            get { return "lives"; }
        }

        public override string[] HelpText
        {
            get { return new[] {"Sets the number of lives remaining"}; }
        }

        public override void Execute(params string[] parameters)
        {
            int lives;
            if (!int.TryParse(parameters[1], out lives))
                return;

            var director = Scene.GetComponent<GameDirector>();

            director.LivesRemaining = lives;
        }
    }
}