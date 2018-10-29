using Ripple.WebSocketClient.Requests.Subscription;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ripple.WebSocketClient.Model.Subscription
{
    public class SubscriptionStream
    {
        private BlockingCollection<IDictionary<string, object>> Feed { get; set; } = new BlockingCollection<IDictionary<string, object>>();
        internal SubscriptionRequest Request { get; set; }

        internal SubscriptionStream(SubscriptionRequest request)
        {
            this.Request = request;
        }

        internal void Push(IDictionary<string, object> o)
        {
            this.Feed.Add(o);
        }

        public Task<IDictionary<string, object>> Next(int? timeoutInMiliseconds = null)
        {
            return Task.Run(() =>
            {
                IDictionary<string, object> result;

                if (this.Feed.TryTake(out result, timeoutInMiliseconds ?? -1))
                {
                    return result;
                }

                return null;
            });
        }
    }
}
