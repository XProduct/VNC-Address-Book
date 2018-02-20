using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VncAddressBook.Models
{
    public class Entry
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Scaling { get; set; }
        public string Comment { get; set; }
        public string UseEncoding { get; set; }
        public string CopyRect { get; set; }
        public string ViewOnly { get; set; }
        public string FullScreen { get; set; }
        public string EightBit { get; set; }
        public string Shared { get; set; }
        public string BellDeiconify { get; set; }
        public string DisableClipboard { get; set; }
        public string SwapMouse { get; set; }
        public string FitWindow { get; set; }
        public string CursorShape { get; set; }
        public string NoRemoteCursor { get; set; }
        public string PreferredEncoding { get; set; }
        public string CompressLevel { get; set; }
        public string Quality { get; set; }
        public string LocalCursor { get; set; }
        public string ScaleDen { get; set; }
        public string ScaleNum { get; set; }
        public string LocalCursorShape { get; set; }
    }
}


/*
TIGHTVNC EXAMPLE
[connection]
host=192.168.70.10
port=5900
password=70685069802a5e92
[options]
use_encoding_1 = 1
copyrect=1
viewonly=0
fullscreen=0
8bit=0
shared=1
belldeiconify=0
disableclipboard=0
swapmouse=0
fitwindow=0 //AUTO on = 1
cursorshape=1
noremotecursor=0
preferred_encoding=7
compresslevel=-1
quality=6
localcursor=1
scale_den=1 // SCALING DENOMINATOR
scale_num=1 // SCALING NUMERATOR
local_cursor_shape=1
*/