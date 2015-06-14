using Vortex.Core.Logging;
using Vortex.Scenegraph.Components;

namespace asteroids.Messaging
{
    public static class ScriptComponentExtensions
    {
        public static void SubscribeTo(this ScriptComponent scriptComponent, object messageId, MessageHandler messageHandler)
        {
            MessageDispatcher.Instance.AddHandler(messageId, messageHandler);
        }

        public static void Dispatch(this ScriptComponent scriptComponent, object messageId, object data)
        {
            Logger.Write(string.Format("Dispatch message `{0}`", messageId));
            MessageDispatcher.Instance.Dispatch(messageId, data); 
        }

        public static void Dispatch(this ScriptComponent scriptComponent, object messageId)
        {
            Logger.Write(string.Format("Dispatch message `{0}`", messageId));
            MessageDispatcher.Instance.Dispatch(messageId, null);
        }
    }
}