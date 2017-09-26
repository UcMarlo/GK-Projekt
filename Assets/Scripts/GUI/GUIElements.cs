using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    [Serializable]
    public class GUIElements
    {
        public Canvas mainCanvas;
        public GameObject UnitActionMenu;
        public GameObject UnitCreateMenu;
        public UnitsMenu UnitCreateMenuContent;
        public UnitButton UnitCreateButton;
        public GameObject Sources;
        //public Button EndTurnButton;
        public Canvas ProgressTreeCanvas;
    }
}
