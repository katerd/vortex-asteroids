using Vortex.Bootstrap;
using Vortex.Core;
using Vortex.Core.Enums;

namespace asteroids
{
    static class Program
    {
        static void Main()
        {
            var startupAttributes = new StartupAttributes
            {
                Height = 600,                       // Height in pixels
                Width = 800,                        // Width in pixels
                RenderFrequency = 60,              // Render frequency in frames per second
                UpdateFrequency = 30,               // Update frequency in frames per second
                VSync = true,                       // Vertical sync
                QualityLevel = QualityLevel.High    // Graphics quality level - used by shaders and engine internals
            };

            // Application.Create takes the default window class and the startup attributes.
            var application = Application.Create<AsteroidsWindow>(startupAttributes);

            // Launch the application, this will return when the game exits.
            application.Run();
        }
    }
}
