﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using System.Runtime.Serialization;

namespace IMDB
{
    [DataContract]
    public class CounterState
    {
        [DataMember]
        public int Count { get; set; }
    }
}
