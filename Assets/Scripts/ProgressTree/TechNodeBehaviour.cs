using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechNodeBehaviour : Button {

	public ProgressNode Node { get; set; }

	public void ShowInfo()
	{
        //string message = "This is info about: " + Node.Details.Name;
        //if(Node.units != null)
        //{
        //    message += "\nUnits: ";
        //    foreach(Unit unit in Node.units)
        //    {
        //        //message += unit.Name + " | ";
        //    }
        //}
        //else
        //{
        //    message += "\nNo units";
        //}
        //Debug.Log(message);
	}
}
