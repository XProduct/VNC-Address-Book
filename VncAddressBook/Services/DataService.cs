using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using VncAddressBook.Models;

namespace VncAddressBook.Model
{
    public class DataService : IDataService
    {
        readonly string vncEntriesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\XProduct\Address Book\";

        public void AddEntry(Entry entry)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Entry));

            using (var stringWriter = new Utf8StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter))
                {
                    serializer.Serialize(writer, entry);
                    string xml = stringWriter.ToString(); // Your XML
                    File.WriteAllText(vncEntriesPath + entry.Id + ".xml", xml);
                }
            }
        }

        public List<Entry> LoadEntries()
        {
            List<Entry> vncEntries = new List<Entry>();
            
            foreach (string vncEntryFile in Directory.GetFiles(vncEntriesPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Entry));
                Entry resultingMessage = (Entry)serializer.Deserialize(new XmlTextReader(vncEntryFile));

                vncEntries.Add(resultingMessage);
            }
            
            return vncEntries;
        }

        public void OpenVncViewer(Entry entry)
        {
            List<string> knownVncViewers = new List<string>() { @"C:\Program Files\RealVNC\VNC Viewer\vncviewer.exe" };

            string vncViewer = knownVncViewers[0];

            System.Diagnostics.Process.Start(vncViewer, entry.IpAddress);
        }
    }

    #region Helper Methods

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    #endregion
}