using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfApp1
{
    internal class MainWindowViewModel : NotifiableObjectBase
    {
        private string firstName;
        private string lastName;

        public MainWindowViewModel()
        {
            ClearCommand = new RelayCommand
                (
                (obj) =>
                {
                    return !string.IsNullOrEmpty(FirstName) || !string.IsNullOrEmpty(LastName);
                },
                (obj) =>
                {
                    FirstName = string.Empty;
                    LastName = string.Empty;
                }
                );
            FirstName = "Jannis";
            LastName = "Pietza";
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(FullName));
                    ClearCommand.RaiseCanExecuteChange();
                }
            }
        }

        public string LastName
        {
            get => lastName; set
            {
                if (value != lastName)
                {
                    lastName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(FullName));
                    ClearCommand.RaiseCanExecuteChange();
                }
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public RelayCommand ClearCommand { get; }
    }
}
