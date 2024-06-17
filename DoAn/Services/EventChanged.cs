using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public event EventHandler StationListChanged;
        public event EventHandler UserListChanged;
        public event EventHandler StationProfileChanged;
        public event EventHandler IDChanged;
        public virtual void OnLoaded()
        {
            Loaded?.Invoke(this, EventArgs.Empty);
        }
        public virtual void OnStationListChanged()
        {
            StationListChanged?.Invoke(this, EventArgs.Empty);
        }
        public virtual void OnUserListChanged()
        {
            UserListChanged?.Invoke(this, EventArgs.Empty);
        }
        public virtual void OnStationProfileChanged()
        {
            StationProfileChanged?.Invoke(this, EventArgs.Empty);
        }
        public virtual void OnIDChanged()
        {
            IDChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

}
