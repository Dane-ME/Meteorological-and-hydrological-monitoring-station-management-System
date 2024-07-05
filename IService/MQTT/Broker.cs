using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT
{
    public class Broker : Client
    {
        public static Broker instance;
        public static Broker Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Broker();
                }
                return instance;
            }
        }

        public Broker() : base(null, "broker.emqx.io", 1883, "hhdangev02", "dangevil4") { }

        public void Send(string topic, Document doc)
        {
            Publish(topic, doc?.ToString() ?? "{}");
        }

        protected override void RaiseDataRecieved(string topic, byte[] message)
        {
            var content = message.UTF8();
            var doc = Document.Parse(content);
            if (topicCallbacks.TryGetValue(topic, out var callback))
            {
                callback?.Invoke(doc);
            }
            base.RaiseDataRecieved(topic, message);
        }

        public Dictionary<string, Action<Document>> topicCallbacks = new Dictionary<string, Action<Document>>();

        public void Listen(string topic, Action<Document> received_callback)
        {
            if (!topicCallbacks.ContainsKey(topic))
            {
                topicCallbacks[topic] = received_callback;
                Subscribe(topic);
                EventChanged.Instance.OnStatusChanged();
            }
            else
            {
                topicCallbacks[topic] += received_callback;
            }
        }

        public void StopListening(string topic, Action<Document>? received_callback = null)
        {
            if (topicCallbacks.ContainsKey(topic))
            {
                if (received_callback == null)
                {
                    topicCallbacks.Remove(topic);
                    Unsubscribe(topic);
                    EventChanged.Instance.OnStatusChanged();
                }
                else
                {
                    topicCallbacks[topic] -= received_callback;
                    if (topicCallbacks[topic] == null)
                    {
                        topicCallbacks.Remove(topic);
                        Unsubscribe(topic);
                    }
                }
            }
        }
    }
}