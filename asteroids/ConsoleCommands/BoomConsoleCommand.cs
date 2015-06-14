using asteroids.Components;
using Vortex.Core.Console.Commands;
using Vortex.Scenegraph;

namespace asteroids.ConsoleCommands
{
    public class BoomConsoleCommand : ConsoleCommand
    {
        public Scene Scene { get { return GetContextItem<Scene>(); } }

        public override string CommandName
        {
            get { return "boom"; }
        }

        public override void Execute(params string[] parameters)
        {
            var roids = Scene.GetEntitiesWithComponent<Asteroid>();
            foreach (var roid in roids)
            {
                roid.GetComponent<Asteroid>().Nuke();
            }
        }
    }
}