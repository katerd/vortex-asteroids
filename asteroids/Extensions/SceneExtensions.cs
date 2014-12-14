using System.Linq;
using asteroids.Components;
using Vortex.Scenegraph;

namespace asteroids.Extensions
{
    public static class SceneExtensions
    {
        public static int GetActiveAsteroidCount(this Scene scene)
        {
            return scene.EntitiesWithComponent<Asteroid>().Count();
        }
    }
}