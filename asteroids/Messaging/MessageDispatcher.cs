using System;
using System.Collections.Generic;

namespace asteroids.Messaging
{
    public class MessageDispatcher
    {
        public static MessageDispatcher Instance = new MessageDispatcher();

        private readonly Dictionary<object, List<MessageHandler>> _handlers;

        public MessageDispatcher()
        {
            _handlers = new Dictionary<object, List<MessageHandler>>();
        }

        public void AddHandler(object id, MessageHandler messageHandler)
        {
            if (!_handlers.ContainsKey(id))
            {
                _handlers[id] = new List<MessageHandler>();
            }
            _handlers[id].Add(messageHandler);
        }

        public void RemoveHandler(object id, MessageHandler messageHandler)
        {
            if (!_handlers.ContainsKey(id))
                return;

            _handlers[id].Remove(messageHandler);
        }

        public void Dispatch(object id, object data)
        {
            if (!_handlers.ContainsKey(id))
                return;

            var handlers = _handlers[id];

            foreach (var handler in handlers)
            {
                handler(id, data);
            }
        }
    }
}