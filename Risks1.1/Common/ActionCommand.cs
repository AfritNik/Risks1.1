/////////////////////////////////////////////////////
// *** UTF-8 encoding ∞ ☼ *** Кодировка UTF-8 ∞ ☼ ***
/////////////////////////////////////////////////////

using System;
using System.Windows.Input;

namespace WpfApplication1
{
    public delegate bool CanExecuteDelegate(object parameter);
    public delegate void ExecuteDelegate(object parameter);

    public class ActionCommand : ICommand
    {
        #region fields
        private bool prevCanExecute = false;
        private CanExecuteDelegate canExecute;
        private ExecuteDelegate execute;
        #endregion

        #region .ctors
        public ActionCommand(ExecuteDelegate execute) : this(execute, o => { return true; }) { }
        public ActionCommand(ExecuteDelegate execute, CanExecuteDelegate canExecute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            bool can = canExecute(parameter);

            if (can != prevCanExecute)
            {
                prevCanExecute = can;
                CommandManager.InvalidateRequerySuggested();
            }

            return can;
        }
        public void Execute(object parameter)
        {
            execute(parameter);
        }
        #endregion
    }
}
