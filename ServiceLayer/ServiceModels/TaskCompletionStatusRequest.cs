﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceModels
{
    public class TaskCompletionStatusRequest
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
    }

}
