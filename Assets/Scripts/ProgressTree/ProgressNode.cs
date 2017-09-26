using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class ProgressNode : ScriptableObject
{

    public ProgressNodeDetails Details;

    private bool unlocked;
    private bool available;

    private TechNodeBehaviour graphicNode;

    private List<ProgressNode> nextNodes;
    private List<ProgressNode> previousNodes;

    public ProgressNode()
    {
        NextNodes = new List<ProgressNode>();
        PreviousNodes = new List<ProgressNode>();
        unlocked = false;
        available = false;
    }

    public void Init()
    {
        NextNodes = new List<ProgressNode>();
        PreviousNodes = new List<ProgressNode>();
        unlocked = false;
        available = false;
    }

    public void SetUnlocked(Color colorUnlocked)
    {
        unlocked = true;
        graphicNode.GetComponent<Image>().color = colorUnlocked;
        //TODO - unlocking linked with node stuff
    }

    public List<ProgressNode> NextNodes
    {
        get
        {
            return nextNodes;
        }

        set
        {
            nextNodes = value;
        }
    }

    public List<ProgressNode> PreviousNodes
    {
        get
        {
            return previousNodes;
        }

        set
        {
            previousNodes = value;
        }
    }

    public bool Unlocked
    {
        get
        {
            return unlocked;
        }

        set
        {
            unlocked = value;
        }
    }

    public TechNodeBehaviour GraphicNode
    {
        get
        {
            return graphicNode;
        }

        set
        {
            graphicNode = value;
        }
    }

    public bool Available
    {
        get
        {
            return available;
        }

        set
        {
            available = value;
        }
    }
}
