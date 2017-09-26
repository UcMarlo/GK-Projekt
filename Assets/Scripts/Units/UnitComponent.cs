using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.VR.WSA;

public class UnitComponent : MonoBehaviour
{
    public Unit UnitLogic { get; set; }
    public void UpdatePosition() {
        Vector3 NewPosition = UnitLogic.Hex.PositionFromCamera(
            Camera.main.transform.position,
            UnitLogic.Hex.HexMap.MapRows,
            UnitLogic.Hex.HexMap.MapColumns
        );

        NewPosition.y = UnitLogic.Hex.hexTerrain.Elevation; //+ (this.GetComponent<MeshRenderer>().bounds.size.y *0.5f);
        this.transform.position = NewPosition;
    }
}
