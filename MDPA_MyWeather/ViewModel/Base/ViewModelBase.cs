
namespace MDPA_MyWeather.ViewModel.Base
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string property = "")
        {
            var tmpHandler = PropertyChanged;
            if (tmpHandler != null)
                tmpHandler(this, new PropertyChangedEventArgs(property));
        }
    }
}
