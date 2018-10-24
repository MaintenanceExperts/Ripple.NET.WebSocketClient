using System;

namespace Ripple.WebSocketClient
{
    internal class TaskInfo
    {
        public Guid TaskId { get; set; }

        public Type Type { get; set; }

        public object TaskCompletionResult { get; set; }

        public bool RemoveUponCompletion { get; set; }

        public bool IsUnsubscribe { get; set; }

        public TaskInfo()
        {
            RemoveUponCompletion = true;
        }
    }
    
}
