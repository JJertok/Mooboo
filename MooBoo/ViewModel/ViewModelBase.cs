using System.ComponentModel;
using System.Runtime.CompilerServices;
using MooBoo.Annotations;

namespace MooBoo.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields

        protected readonly ViewModelBase Parent;

        #endregion

        #region C-tor

        protected ViewModelBase(ViewModelBase parent)
        {
            this.Parent = parent;
        }

        #endregion

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
