using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TG4LabWPF.Model.Attractions;
using TG4LabWPF.MVVM;

namespace TG4LabWPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Attraction> Attractions { get; set; }

        private Attraction selectedAttraction;
        public Attraction SelectedAttraction
        {
            get { return selectedAttraction; }
            set 
            { 
                selectedAttraction = value;
                OnPropertyChanged(nameof(SelectedAttraction));
            }
        }

    }
}