using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    class Utility
    {
        public static float Normalize(float min,float max,float v)
        {
            return (v - min) / (max - min);
        }
    }

}
