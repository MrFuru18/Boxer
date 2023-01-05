using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model
{
    class TaskState
    {
        public Guid task_id { get; set; }
        public string state { get; set; }
        public DateTime time { get; set; }
    }
}
