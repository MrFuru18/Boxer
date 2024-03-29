﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxer.Model
{
    public static class ObjectComparerUtility
    {
        public static bool ObjectsAreEqual<T>(T obj1, T obj2)
        {
            var obj1Serialized = JsonConvert.SerializeObject(obj1);
            var obj2Serialized = JsonConvert.SerializeObject(obj2);

            return obj1Serialized == obj2Serialized;
        }

        public static U Convert<T, U>(T obj1)
        {
            U obj2 = JsonConvert.DeserializeObject<U>(JsonConvert.SerializeObject(obj1));

            return obj2;
        }
    }
}