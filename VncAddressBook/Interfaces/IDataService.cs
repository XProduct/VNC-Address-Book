﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VncAddressBook.Models;

namespace VncAddressBook.Model
{
    public interface IDataService
    {
        List<Entry> LoadEntries();
        void SaveEntry(Entry entry);
        void OpenVncViewer(Entry entry);
    }
}
