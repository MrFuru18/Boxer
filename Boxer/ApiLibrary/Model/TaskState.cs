using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    public class TaskState
    {
        public int? id { get; set; }
        public int? task_id { get; set; }
        public string state { get; set; }
        public DateTime time { get; set; }
    }
}
