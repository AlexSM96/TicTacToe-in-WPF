using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.ViewModels.Base
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
        } 
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string properyName = null!)
        {
            if(Equals(field,value)) return false;
            field = value;
            OnPropertyChanged(properyName); 
            return true;
        }
    }
}
