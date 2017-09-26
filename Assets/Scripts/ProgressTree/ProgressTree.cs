using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//TODO - change init tree from via previousNodes to via nextNodes, do it in one method for XML and assets

//TODO - links between technology and unlocked units and buildings and etc. (assets)
//TODO - dispaly technology details while pointer is above node
//TODO - gui managing (draw the tree in the middle, create scrollbar when it's to big, open and exit progress tree)

[CreateAssetMenu()]
public class ProgressTree : ScriptableObject
{
    public Canvas ProgressTreeCanvas { get; set; }
    public GameManager GameManager { get; set; }
    //public GameObject infoPanel;
    public TechNodeBehaviour techNodePrefab;
    public Image link;
    public Image corner;
    public ProgressNode[] nodesAssets;

    private static float linkLength = 40f;
    private static float nodeButtonX = 100f;
    private static float nodeButtonY = 30f;
    private static Vector2 startCordinates = new Vector2(200, 200);

    private List<ProgressNode> availableNodes;
    private List<ProgressNode> allNodes;
    private Stack<ProgressNode> requiredNodes;
    private ProgressNode selectedNode;

    private int progressPoints = 50;

    private Color colorUnlocked;
    private Color colorSelected;
    private Color colorAvailable;
    private Color colorNotAvailable;


    public ProgressTree() { }

    public void Setup()
    {
        availableNodes = new List<ProgressNode>();
        allNodes = new List<ProgressNode>();
        requiredNodes = new Stack<ProgressNode>();
        InitColors();
    }

    public void LoadTree(bool xml)
    {
        LoadFromAsset();
    }

    private void SetupNodes(ProgressNode progressNode, ref int tier, ref int number)
    {
        if (progressNode.Details.Name == null)
        {
            allNodes.Add(null);
            number++;
            return;
        }
        if (tier != progressNode.Details.Tier)
        {
            tier = progressNode.Details.Tier;
            number = 0;
        }
        progressNode.GraphicNode = Button.Instantiate(techNodePrefab, new Vector3(startCordinates.x + tier * (2 * linkLength + 2 * nodeButtonX / 2), startCordinates.y + number * (linkLength + nodeButtonY - 10), 0), new Quaternion(), ProgressTreeCanvas.transform) as TechNodeBehaviour;
        progressNode.GraphicNode.name = "node_" + tier + "_" + number;
        progressNode.GraphicNode.GetComponentInChildren<Text>().text = progressNode.Details.Name;
        progressNode.GraphicNode.GetComponent<Image>().color = colorNotAvailable;
        progressNode.GraphicNode.onClick.AddListener(delegate { ActionSelect(progressNode); });
        progressNode.GraphicNode.Node = progressNode; // required for TechNodeBehaviour Node to display info on pointer enter
        if (progressNode.Details.Tier == 0)
        {
            progressNode.Available = true;
            progressNode.GraphicNode.GetComponent<Image>().color = colorAvailable;
            availableNodes.Add(progressNode);
        }
        if (progressNode.Details.Previous != null)
        {

            int k = 0;
            progressNode.PreviousNodes = new List<ProgressNode>();
            for (int j = allNodes.Count - 1; j > -1; j--)
            {
                if (allNodes[j] != null)
                {
                    int i = 1;
                    foreach (int Id in progressNode.Details.Previous)
                    {

                        if (allNodes[j].Details.Id == Id)
                        {
                            progressNode.PreviousNodes.Add(allNodes[j]);
                            allNodes[j].NextNodes.Add(progressNode);
                            k++;
                            Vector3 distance = progressNode.GraphicNode.transform.position - allNodes[j].GraphicNode.transform.position;
                            int y = -(int)Math.Ceiling((float)distance.y / (float)60.0);
                            int sign = 1;
                            if (y != 0)
                            {
                                Quaternion quaternion = new Quaternion();
                                Vector3 additionalScale = new Vector3();
                                float localLinkLength = linkLength;
                                if (y > 0)
                                {
                                    sign = -1;
                                    quaternion = new Quaternion(0, 0, 0, 0);
                                }
                                else
                                {
                                    sign = 1;
                                    quaternion = new Quaternion(0, 0, -1, 1);
                                }
                                if (y == 1 || y == -1)
                                {
                                    localLinkLength = linkLength;
                                    additionalScale = new Vector3(0, 0, 0);
                                }
                                else
                                {
                                    localLinkLength = linkLength * 1.5f;
                                    additionalScale = new Vector3(0.5f, 0, 0);
                                    Vector3 vector = new Vector3(nodeButtonX / 2 + linkLength + 2, sign * (10 + linkLength + localLinkLength / 2 + (-sign * (y + 1) - 2) * localLinkLength), 0);
                                    Image.Instantiate(link, allNodes[j].GraphicNode.transform.position + vector,
                                        new Quaternion(0, 0, 1, 1), ProgressTreeCanvas.transform).transform.localScale += additionalScale;
                                }
                                Vector3 vector1 = new Vector3(nodeButtonX / 2 + linkLength + 2, sign * (10 + linkLength + localLinkLength / 2 + (-sign * y - 2) * localLinkLength), 0);
                                Image.Instantiate(link, allNodes[j].GraphicNode.transform.position + vector1,
                                    new Quaternion(0, 0, 1, 1), ProgressTreeCanvas.transform).transform.localScale += additionalScale;
                                Vector3 vector2 = new Vector3(nodeButtonX / 2 + linkLength + 12 / 2, -y * (linkLength + 12 + 12 - 4) + sign * (-4), 0);
                                Image.Instantiate(corner, allNodes[j].GraphicNode.transform.position + vector2, quaternion, ProgressTreeCanvas.transform);
                                //last = progressNode.GraphicNode.transform.position;
                            }
                        }
                        i++;
                    }
                }

                if (k >= progressNode.Details.Previous.Length) break;
            }

        }
        number++;
        allNodes.Add(progressNode);
    }

    private void DrawHorizontalLinks()
    {
        int i = 0;
        int count = allNodes.Count;
        for (i = 0; i < count; i++)
        {
            if (allNodes[i] != null)
            {
                if (allNodes[i].NextNodes != null && allNodes[i].NextNodes.Count > 0)
                {
                    Image.Instantiate(link, allNodes[i].GraphicNode.transform.position + new Vector3(nodeButtonX / 2 + linkLength * 1.25f / 2, 0, 0),
                        new Quaternion(), ProgressTreeCanvas.transform).transform.localScale += new Vector3(0.25f, 0, 0);
                    if (allNodes[i].GraphicNode.transform.position.y > allNodes[i].NextNodes[0].GraphicNode.transform.position.y)
                        Image.Instantiate(corner, allNodes[i].GraphicNode.transform.position + new Vector3(nodeButtonX / 2 + linkLength - 2, -12 / 2 + 2, 0),
                            new Quaternion(0, 0, 1, 0), ProgressTreeCanvas.transform);

                    if (allNodes[i].GraphicNode.transform.position.y < allNodes[i].NextNodes[allNodes[i].NextNodes.Count - 1].GraphicNode.transform.position.y)
                        Image.Instantiate(corner, allNodes[i].GraphicNode.transform.position + new Vector3(nodeButtonX / 2 + linkLength - 2, 12 / 2 - 2, 0),
                            new Quaternion(0, 0, 1, 1), ProgressTreeCanvas.transform);
                }
                if (allNodes[i].PreviousNodes.Count != 0)
                    Image.Instantiate(link, allNodes[i].GraphicNode.transform.position + new Vector3(-(nodeButtonX / 2 + linkLength * 0.75f / 2), 0, 0),
                        new Quaternion(), ProgressTreeCanvas.transform).transform.localScale -= new Vector3(0.25f, 0, 0);
            }
            else
            {
                allNodes.Remove(allNodes[i]);
                i--;
                count--;
            }
        }
        if (availableNodes.Count > 0)
        {
            selectedNode = availableNodes[0];
            selectedNode.GraphicNode.GetComponentInChildren<Button>().image.color = colorSelected;
        }
        return;
    }

    public void LoadFromAsset()
    {
        //ProgressTreeCanvas = GameObject.Instantiate(ProgressTreeCanvas);
        int tier = 0;
        int number = 0;
        foreach (ProgressNode node in nodesAssets)
        {
            ProgressNodeDetails details = node.Details;
            ProgressNode progressNode = Instantiate(node);
            progressNode.Init();
            progressNode.Details = details;
            SetupNodes(progressNode, ref tier, ref number);
        }

        DrawHorizontalLinks();
    }

    private void InitColors()
    {
        colorUnlocked = Color.yellow;

        colorSelected.r = 0.1f;
        colorSelected.g = 0.4f;
        colorSelected.b = 0.8f;
        colorSelected.a = 1f;

        colorAvailable = Color.green;

        colorNotAvailable = Color.gray;
    }

    public void UnlockNode(ProgressNode unlockedNode)
    {
        unlockedNode.SetUnlocked(colorUnlocked);
        if (unlockedNode.Details.unitAsset.Length != 0)
        {
            foreach(Player player in GameManager.Players)
            {
                player.UnlockUnit(unlockedNode.Details.unitAsset);
            }
        }

        foreach (ProgressNode nextNode in unlockedNode.NextNodes)
        {
            if (CheckPreviousNodes(nextNode))
            {
                availableNodes.Add(nextNode);
                nextNode.Available = true;
                nextNode.GraphicNode.GetComponent<Image>().color = colorAvailable;
            }
        }
        availableNodes.Remove(unlockedNode);
    }

    public void NextTurn()
    {
        Debug.Log("-----------------------------------------\nNext turn:");
        if (selectedNode.Details.Price > progressPoints)
        {
            selectedNode.Details.Price -= progressPoints;
            Debug.Log("Turns to unlock new node: " + (int)Mathf.Ceil((float)selectedNode.Details.Price / (float)progressPoints));
        }
        else
        {
            UnlockNode(selectedNode);
            int diff = progressPoints - selectedNode.Details.Price;
            if (requiredNodes.Count > 0)
            {
                selectedNode.GraphicNode.GetComponent<Image>().color = colorUnlocked;
                selectedNode = requiredNodes.Pop();
                selectedNode.GraphicNode.GetComponent<Image>().color = colorSelected;
            }
            else
            {
                if (availableNodes.Count > 0)
                {
                    selectedNode = availableNodes[0];
                    selectedNode.Details.Price -= diff;
                    selectedNode.GraphicNode.GetComponent<Image>().color = colorAvailable;
                    if (availableNodes.Count > 0)
                    {
                        selectedNode = availableNodes[0];
                        selectedNode.GraphicNode.GetComponentInChildren<Button>().image.color = colorSelected;
                    }
                }
            }
        }
    }

    private void PushPreviousNodes(ProgressNode newSelected)
    {
        foreach (ProgressNode prevNode in newSelected.PreviousNodes)
        {
            if (!prevNode.Unlocked)
            {
                requiredNodes.Push(prevNode);
            }
        }
        foreach (ProgressNode prevNode in newSelected.PreviousNodes)
        {
            if (!prevNode.Unlocked)
            {
                PushPreviousNodes(prevNode);
            }
        }
    }

    private void SetRequiredNodes(ProgressNode newSelected)
    {
        requiredNodes = new Stack<ProgressNode>();
        requiredNodes.Push(newSelected);
        PushPreviousNodes(newSelected);
    }

    private void ActionSelect(ProgressNode newSelected)
    {
        if (!newSelected.Unlocked)
        {
            if (availableNodes.Contains(newSelected) && selectedNode != newSelected)
            {
                Debug.Log("selected new node");
                selectedNode.GraphicNode.GetComponent<Image>().color = colorAvailable;
                newSelected.GraphicNode.GetComponent<Image>().color = colorSelected;
                selectedNode = newSelected;
            }
            else
            {
                SetRequiredNodes(newSelected);
                selectedNode.GraphicNode.GetComponent<Image>().color = colorAvailable;
                selectedNode = requiredNodes.Pop();
                selectedNode.GraphicNode.GetComponent<Image>().color = colorSelected;
            }
        }
    }

    private bool CheckPreviousNodes(ProgressNode nextNode)
    {
        foreach (ProgressNode prevNode in nextNode.PreviousNodes)
        {
            if (!prevNode.Unlocked) return false;
        }
        return true;
    }
}
