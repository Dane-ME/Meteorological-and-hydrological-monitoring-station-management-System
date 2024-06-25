using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class ListenActivedEventArgs : EventArgs
    {
        public string UserID { get; }

        public ListenActivedEventArgs(string message)
        {
            UserID = message;
        }
    }
    public partial class EventChanged
    {
        public event EventHandler<ListenActivedEventArgs> ?ListenActivedEvent;
        public event EventHandler<ListenActivedEventArgs> ?LogOutEvent;
        public virtual void OnListenActivedEvent(string UserID)
        {
            ListenActivedEvent?.Invoke(this, new ListenActivedEventArgs(UserID));
        }
        public virtual void OnLogOutEvent(string UserID)
        {
            LogOutEvent?.Invoke(this, new ListenActivedEventArgs(UserID));
        }
    }
}
