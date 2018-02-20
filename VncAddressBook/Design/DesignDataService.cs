using System;
using System.Collections.Generic;
using VncAddressBook.Model;
using VncAddressBook.Models;

namespace VncAddressBook.Design
{
    public class DesignDataService : IDataService
    {
        public void SaveEntry(Entry entry)
        {
            
        }

        public List<Entry> LoadEntries()
        {
            return new List<Entry>()
                {
                    new Entry()
                    {
                        Name = "WS0001 - Ethan Office",
                        Host = "192.168.1.1"
                    },
                    new Entry()
                    {
                        Name = "WS0001 - Warehouse",
                        Host = "192.168.1.3"
                    }
                };
        }

        public void OpenVncViewer(Entry entry)
        {
            throw new NotImplementedException();
        }
    }
}