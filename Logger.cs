using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueueingSystem
{
    class Logger
    {
        public ListBox log { get; set; }
        public void AddLog(string s)
        {
            log.Items.Add(s);
        }
    }
}
