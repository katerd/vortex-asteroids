using Vortex.Bootstrap;
using Vortex.Core;
using Vortex.Core.Assets;
using Vortex.Core.Assets.AssetSources;
using Vortex.Core.Enums;

namespace asteroids
{
    static class Program
    {
        static void Main()
        {
            // Creates a new instance of AssetLoader and assigns it to StaticAssetLoader.Instance.
            StaticAssetLoader.Initialize();
            
            // Create a new file system asset source to access Models and SDK assets. All paths
            // are relative to the working directory of the game executable.
            var source = new FileSystemAssetSource();
            source.AddPath(@"Models");
            source.AddPath(@"..\..\..\Data");
            StaticAssetLoader.RegisterSource(source);

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
