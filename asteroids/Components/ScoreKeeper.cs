using asteroids.Enums;
using asteroids.Messaging;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class ScoreKeeper : ScriptComponent
    {
        public GameDirector GameDirector { get; set; }
        
        public override void Initialize()
        {
            base.Initialize();

            LoadGameDirector();

            this.SubscribeTo(EventType.AsteroidDestroyed, (id, data) => IncreaseScore(1000));
        }

        public override void OnUpdate(float delta)
        {                                                                  
            base.OnUpdate(delta);

            if (!LoadGameDirector())
            {
                Log("No game director!");
            }
        }

        private void IncreaseScore(int score)
        {
            if (LoadGameDirector())
            {
                GameDirector.Score += score;
            }
        }

        private bool LoadGameDirector()
        {
            if (GameDirector != null)
                return true;
            GameDirector = Scene.GetComponent<GameDirector>();
            return GameDirector != null;
        }
    }
}