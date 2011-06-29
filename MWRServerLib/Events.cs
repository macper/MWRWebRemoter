using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWRCommonTypes;
using System.Diagnostics;

namespace MWRServerLib
{
    public delegate void Notifier(IMWRObject obj, ProcessResult result);
    public delegate void TextNotifier(string message, TraceEventType type, string additionalInfo);
}
