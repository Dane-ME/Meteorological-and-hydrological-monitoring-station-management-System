using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Services
{
    public class EventChanged
    {
        public static EventChanged _instance;
        public static EventChanged Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventChanged();
                }
                return _instance;
            }
        }
        public event EventHandler Loaded;
        public virtual void OnLoaded()
        {
            Loaded?.Invoke(this, EventArgs.Empty);
        }
    }
}
