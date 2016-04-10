
namespace MDPA_MyWeather.ViewModel.Base
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        private Action execute;

        public DelegateCommand(Action execute)
        {
            this.execute = execute;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.execute();
        }
    }
}