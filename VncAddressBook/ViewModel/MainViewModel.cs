using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VncAddressBook.Model;
using VncAddressBook.Models;

namespace VncAddressBook.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Class Variables

        private readonly IDataService DataService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            DataService = dataService;

            Entries = new ObservableCollection<Entry>(DataService.LoadEntries());

            AddEntryCommand = new RelayCommand(AddEntry);
            ConnectCommand = new RelayCommand(() => { DataService.OpenVncViewer(SelectedEntry); }, () => SelectedEntry != null);

            GetDesignData();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="AddEntryCommand" /> property's name.
        /// </summary>
        public const string AddEntryCommandPropertyName = "AddEntryCommand";

        private ICommand _AddEntryCommand;

        /// <summary>
        /// Sets and gets the AddEntryCommand property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ICommand AddEntryCommand
        {
            get
            {
                return _AddEntryCommand;
            }

            set
            {
                if (_AddEntryCommand == value)
                {
                    return;
                }

                _AddEntryCommand = value;
                RaisePropertyChanged(AddEntryCommandPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ConnectCommand" /> property's name.
        /// </summary>
        public const string ConnectCommandPropertyName = "ConnectCommand";

        private ICommand _ConnectCommand;

        /// <summary>
        /// Sets and gets the ConnectCommand property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ICommand ConnectCommand
        {
            get
            {
                return _ConnectCommand;
            }

            set
            {
                if (_ConnectCommand == value)
                {
                    return;
                }

                _ConnectCommand = value;
                RaisePropertyChanged(ConnectCommandPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedEntry" /> property's name.
        /// </summary>
        public const string SelectedEntryPropertyName = "SelectedEntry";

        private Entry _SelectedEntry;

        /// <summary>
        /// Sets and gets the SelectedEntry property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Entry SelectedEntry
        {
            get
            {
                return _SelectedEntry;
            }

            set
            {
                if (_SelectedEntry == value)
                {
                    return;
                }

                _SelectedEntry = value;
                RaisePropertyChanged(SelectedEntryPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Entries" /> property's name.
        /// </summary>
        public const string EntriesPropertyName = "Entries";

        private ObservableCollection<Entry> _Entries = new ObservableCollection<Entry>();

        /// <summary>
        /// Sets and gets the Entries property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Entry> Entries
        {
            get
            {
                return _Entries;
            }

            set
            {
                if (_Entries == value)
                {
                    return;
                }

                _Entries = value;
                RaisePropertyChanged(EntriesPropertyName);
            }
        }

        #endregion

        #region Methods

        private void AddEntry()
        {
            Entry entry = new Entry()
            {
                Id = Guid.NewGuid().ToString(),
                IpAddress = "192.168.1.1",
                Name = "Test Entry (Auto Added)"
            };

            Entries.Add(entry);
            DataService.AddEntry(entry);
        }

        private void GetDesignData()
        {
            if (IsInDesignMode)
            {
                
            }
        }

        #endregion
    }
}