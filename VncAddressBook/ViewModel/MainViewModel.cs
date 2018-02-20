using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
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
            EditEntryCommand = new RelayCommand(EditEntry, () => SelectedEntry != null);
            SaveEntryCommand = new RelayCommand(SaveEntry);
            ConnectCommand = new RelayCommand(() => { DataService.OpenVncViewer(SelectedEntry); }, () => SelectedEntry != null);

            GetDesignData();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="EditingEntry" /> property's name.
        /// </summary>
        public const string EditingEntryPropertyName = "EditingEntry";

        private Entry _EditingEntry;

        /// <summary>
        /// Sets and gets the EditingEntry property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Entry EditingEntry
        {
            get
            {
                return _EditingEntry;
            }

            set
            {
                if (_EditingEntry == value)
                {
                    return;
                }

                _EditingEntry = value;
                RaisePropertyChanged(EditingEntryPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsEditEntryVisible" /> property's name.
        /// </summary>
        public const string IsEditEntryVisiblePropertyName = "IsEditEntryVisible";

        private bool _IsEditEntryVisible = false;

        /// <summary>
        /// Sets and gets the IsEditEntryVisible property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsEditEntryVisible
        {
            get
            {
                return _IsEditEntryVisible;
            }

            set
            {
                if (_IsEditEntryVisible == value)
                {
                    return;
                }

                _IsEditEntryVisible = value;
                RaisePropertyChanged(IsEditEntryVisiblePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SaveEntryCommand" /> property's name.
        /// </summary>
        public const string SaveEntryCommandPropertyName = "SaveEntryCommand";

        private ICommand _SaveEntryCommand;

        /// <summary>
        /// Sets and gets the SaveEntryCommand property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ICommand SaveEntryCommand
        {
            get
            {
                return _SaveEntryCommand;
            }

            set
            {
                if (_SaveEntryCommand == value)
                {
                    return;
                }

                _SaveEntryCommand = value;
                RaisePropertyChanged(SaveEntryCommandPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="EditEntryCommand" /> property's name.
        /// </summary>
        public const string EditEntryCommandPropertyName = "EditEntryCommand";

        private ICommand _EditEntryCommand;

        /// <summary>
        /// Sets and gets the EditEntryCommand property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ICommand EditEntryCommand
        {
            get
            {
                return _EditEntryCommand;
            }

            set
            {
                if (_EditEntryCommand == value)
                {
                    return;
                }

                _EditEntryCommand = value;
                RaisePropertyChanged(EditEntryCommandPropertyName);
            }
        }

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
            EditingEntry = new Entry()
            {
                Id = Guid.NewGuid().ToString(),
                Host = "192.168.1.1",
                Name = "Test Entry (Auto Added)"
            };

            IsEditEntryVisible = true;

            //Entries.Add(entry);
            //DataService.SaveEntry(entry);
        }

        private void EditEntry()
        {
            if (SelectedEntry != null)
            {
                EditingEntry = SelectedEntry;
                IsEditEntryVisible = true;
            }
        }

        private void SaveEntry()
        {
            var entry = Entries.Where(x => x.Id == EditingEntry.Id).FirstOrDefault();

            if (entry != null)
            {
                entry = EditingEntry;
            }
            else
            {
                Entries.Add(EditingEntry);
            }
            
            DataService.SaveEntry(EditingEntry);
            IsEditEntryVisible = false;
        }

        private void RemoveEntry()
        {
            if (SelectedEntry != null)
            {
                
            }
        }

        private void GetDesignData()
        {
            if (IsInDesignMode)
            {
                IsEditEntryVisible = true;
            }
        }

        #endregion
    }
}