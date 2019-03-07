using System;
using System.Collections.Generic;
using System.Text;

namespace FormsAdmin.Core.Utilities
{
    public class Utils
    {
        public static string NewGuid => Guid.NewGuid().ToString().ToLower();
    }
}
