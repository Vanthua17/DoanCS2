using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMQuanLyKho.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExcute;
        private readonly Action<T> _execute;
        private Func<object, bool> value;
        private string searchKeyword;

        public RelayCommand(Predicate<T> canExcute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _canExcute = canExcute;
            _execute = execute;
        }

        public RelayCommand(Func<object, bool> value)
        {
            this.value = value;
        }

        public RelayCommand(string searchKeyword)
        {
            this.searchKeyword = searchKeyword;
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExcute == null ? true : _canExcute((T)parameter);
            }
            catch
            { return true; }
        }
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        internal void RaiseCanExecuteChanged()
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    

}
