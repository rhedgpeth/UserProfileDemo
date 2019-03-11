using System;

namespace UserProfileDemo.Core.ViewModels
{
    public abstract class BaseViewModel : BaseNotify, IDisposable
    {
        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetPropertyChanged(ref _isBusy, value);
        }

        public virtual void Dispose() { } 
    }
}
