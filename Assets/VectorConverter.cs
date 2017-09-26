using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets {
    public class VectorConverter {
        //<summary> 
        // After conversion Y array is lost!
        //</summary>
        public static List<Vector2> convertVec3ToVec2(List<Vector3> vectors3) {
            List<Vector2> vectors2 = new List<Vector2>();
            foreach (Vector3 vec in vectors3) {
                vectors2.Add(new Vector2(vec.x, vec.z));
            }
            return vectors2;
        }
        //<summary>
        //returned Y array members are allways = 0
        //</summary>
        public static List<Vector3> convertVec2ToVec3(List<Vector2> vectors2) {
            List<Vector3> vectors3 = new List<Vector3>();
            foreach (Vector2 vec in vectors2) {
                vectors3.Add(new Vector3(vec.x, 0, vec.y));
            }
            return vectors3;
        }

    }
}
