using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VncAddressBook.Models;

namespace VncAddressBook.Model
{
    public class DataService : IDataService
    {
        readonly string vncEntriesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\XProduct\VNC Address Book\";

        public void SaveEntry(Entry entry)
        {
            try
            {
                using (var stringWriter = new Utf8StringWriter())
                {
                    stringWriter.WriteLine("[connection]");
                    stringWriter.WriteLine("host=" + entry.Host);
                    stringWriter.WriteLine("port=" + entry.Port);
                    //stringWriter.WriteLine("username=" + entry.Username);
                    stringWriter.WriteLine("password=" + entry.Password);
                    stringWriter.WriteLine("[options]");
                    stringWriter.WriteLine("use_encoding_1=" + entry.UseEncoding);
                    stringWriter.WriteLine("copyrect=" + entry.CopyRect);
                    stringWriter.WriteLine("viewonly=" + entry.ViewOnly);
                    stringWriter.WriteLine("fullscreen=" + entry.FullScreen);
                    stringWriter.WriteLine("8bit=" + entry.EightBit);
                    stringWriter.WriteLine("shared=" + entry.Shared);
                    stringWriter.WriteLine("belldeiconify=" + entry.BellDeiconify);
                    stringWriter.WriteLine("disableclipboard=" + entry.DisableClipboard);
                    stringWriter.WriteLine("swapmouse=" + entry.SwapMouse);
                    stringWriter.WriteLine("fitwindow=" + entry.FitWindow);
                    stringWriter.WriteLine("cursorshape=" + entry.CursorShape);
                    stringWriter.WriteLine("noremotecursor=" + entry.NoRemoteCursor);
                    stringWriter.WriteLine("preferred_encoding=" + entry.PreferredEncoding);
                    stringWriter.WriteLine("compresslevel=" + entry.CompressLevel);
                    stringWriter.WriteLine("quality=" + entry.Quality);
                    stringWriter.WriteLine("localcursor=" + entry.LocalCursor);
                    stringWriter.WriteLine("scale_den=" + entry.ScaleDen);
                    stringWriter.WriteLine("scale_num=" + entry.ScaleNum);
                    stringWriter.WriteLine("local_cursor_shape=" + entry.LocalCursorShape);
                    string vncText = stringWriter.ToString(); // Text to save in .vnc file
                    File.WriteAllText(vncEntriesPath + entry.Name + ".vnc", vncText);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception thrown: SaveEntry() failed.", e.ToString());
            }
        }

        public List<Entry> LoadEntries()
        {
            List<Entry> vncEntries = new List<Entry>();
            string line;
            string lineData;
            int position;
            
            try
            {
                var vncFiles = Directory.EnumerateFiles(vncEntriesPath, "*.vnc", SearchOption.AllDirectories);
                foreach (string currentFile in vncFiles)
                {
                    using (StreamReader file = new StreamReader(currentFile))
                    {
                        Entry loadResult = new Entry();
                        while ((line = file.ReadLine()) != null)
                        {
                            System.Console.WriteLine(line);
                            if (line.StartsWith("["))
                            {
                                continue;
                            }
                            position = line.IndexOf("=");
                            if (position < 0)
                            {
                                continue;
                            }
                            lineData = line.Substring(position + 1);
                            lineData = lineData.Trim();
                            if (line.StartsWith("host"))
                            {
                                loadResult.Host = lineData;
                            }
                            else if (line.StartsWith("port"))
                            {
                                loadResult.Port = lineData;
                            }
                            else if (line.StartsWith("password"))
                            {
                                loadResult.Password = lineData;
                            }
                            else if (line.StartsWith("use_encoding_1"))
                            {
                                loadResult.UseEncoding = lineData;
                            }
                            else if (line.StartsWith("copyrect"))
                            {
                                loadResult.CopyRect = lineData;
                            }
                            else if (line.StartsWith("viewonly"))
                            {
                                loadResult.ViewOnly = lineData;
                            }
                            else if (line.StartsWith("fullscreen"))
                            {
                                loadResult.FullScreen = lineData;
                            }
                            else if (line.StartsWith("8bit"))
                            {
                                loadResult.EightBit = lineData;
                            }
                            else if (line.StartsWith("shared"))
                            {
                                loadResult.Shared = lineData;
                            }
                            else if (line.StartsWith("belldeiconify"))
                            {
                                loadResult.BellDeiconify = lineData;
                            }
                            else if (line.StartsWith("disableclipboard"))
                            {
                                loadResult.DisableClipboard = lineData;
                            }
                            else if (line.StartsWith("swapmouse"))
                            {
                                loadResult.SwapMouse = lineData;
                            }
                            else if (line.StartsWith("fitwindow"))
                            {
                                loadResult.FitWindow = lineData;
                            }
                            else if (line.StartsWith("cursorshape"))
                            {
                                loadResult.CursorShape = lineData;
                            }
                            else if (line.StartsWith("noremotecursor"))
                            {
                                loadResult.NoRemoteCursor = lineData;
                            }
                            else if (line.StartsWith("preferred_encoding"))
                            {
                                loadResult.PreferredEncoding = lineData;
                            }
                            else if (line.StartsWith("compresslevel"))
                            {
                                loadResult.CompressLevel = lineData;
                            }
                            else if (line.StartsWith("quality"))
                            {
                                loadResult.Quality = lineData;
                            }
                            else if (line.StartsWith("localcursor"))
                            {
                                loadResult.LocalCursor = lineData;
                            }
                            else if (line.StartsWith("scale_den"))
                            {
                                loadResult.ScaleDen = lineData;
                            }
                            else if (line.StartsWith("scale_num"))
                            {
                                loadResult.ScaleNum = lineData;
                            }
                            else if (line.StartsWith("local_cursor_shape"))
                            {
                                loadResult.LocalCursorShape = lineData;
                            }
                        }
                        loadResult.Name = Path.GetFileNameWithoutExtension(currentFile);
                        vncEntries.Add(loadResult);
                    }
                }
            }
            catch (FileNotFoundException f)
            {
                Console.WriteLine("Exception: A file was not found in LoadEntries().", f.ToString());
            }
            catch (DirectoryNotFoundException d)
            {
                Console.WriteLine("Exception: Directory not found in LoadEntries().", d.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: LoadEntries() failed.", e.ToString());
            }

            return vncEntries;
        }

        public void OpenVncViewer(Entry entry)
        {
            List<string> knownVncViewers = new List<string>() { @"C:\Program Files\TightVNC\tvnviewer.exe" };

            string vncViewer = knownVncViewers[0];

            //System.Diagnostics.Process.Start(vncViewer, "-host=" + entry.Host + " -password=" + entry.Password);
            System.Diagnostics.Process.Start(vncViewer, " -optionsfile = " + vncEntriesPath + entry.Name + ".vnc");
        }
    }

    #region Helper Methods

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    #endregion
}