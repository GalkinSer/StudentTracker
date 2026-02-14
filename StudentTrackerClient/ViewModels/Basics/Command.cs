using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentTrackerClient.ViewModels.Basics
{
    public class Command<T> : ICommand where T : class
    {
        private readonly Action<T> _action;

        internal Command(Action<T> action)
        {
            _action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is null)
            {
                _action?.Invoke(null);
            }
            else if (parameter is T param)
            {
                _action?.Invoke(param);
            }
            else
            {
                throw new ArgumentException($"Параметр команды не является {nameof(T)}", nameof(parameter));
            }

        }
    }
}
