using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wire : MonoBehaviour
{
    struct WireNode 
    {
        public Vector3 left;
        public Vector3 right;
        public LineRenderer line;
    }
    public GameObject text;
    [SerializeField] Toggle wireToggle;
    [SerializeField] GameObject wire;
    [SerializeField] Transform wireParent;
    [SerializeField] ParticleSystem sparks;
    List<WireNode> wireList = new List<WireNode>();
    GameObject lastClickedObj;

    bool CanDrawLine(GameObject left, GameObject right)
    {
        bool hasSource = false;
        bool hasNonSource = false;
        if (left.CompareTag("Source") || right.CompareTag("Source"))
        {
            hasSource = true;
        }
        if (!left.CompareTag("Source") || !right.CompareTag("Source"))
        {
            hasNonSource = true;
        }
        return hasSource && hasNonSource;
    }

    void Update()
    {
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(cursor, 0.1f);
        if (Input.GetMouseButtonDown(1) && colliders.Length > 0)
        {
            sparks.transform.position = colliders[0].transform.position + Vector3.forward * 10f;
            sparks.Play();
            GameObject currentObj = colliders[0].gameObject;
            if (lastClickedObj != null/* && CanDrawLine(lastClickedObj, currentObj)*/)
            {
                wireList.Add(new WireNode()
                {
                    line = Instantiate(wire, Vector3.zero, Quaternion.identity, currentObj.transform).GetComponent<LineRenderer>(),
                    left = currentObj.transform.position,
                    right = lastClickedObj.transform.position
                });
                if (currentObj.CompareTag("Source"))
                {
                    if (currentObj != lastClickedObj)
                    {
                        Connect(currentObj.GetComponent<Part>(), lastClickedObj.GetComponent<Part>());
                    }                    
                }
                else
                {
                    if (lastClickedObj != currentObj && !lastClickedObj.CompareTag("Wire"))
                    {
                        Connect(lastClickedObj.GetComponent<Part>(), currentObj.GetComponent<Part>());
                    }
                }
                lastClickedObj = null;
                text.SetActive(false);
            }
            else if( lastClickedObj == null )
            {
                lastClickedObj = currentObj;
                text.SetActive(true);
            }
        }
        //Destroy Wires
       /* if (Input.GetKeyDown(KeyCode.C) && ogObj.GetComponent<Part>().connectedObjects.Count > 0)
        {
            ogObj.GetComponent<Part>().connectedObjects.RemoveAt(ogObj.GetComponent<Part>().connectedObjects.Count - 1);
            Destroy(wireParent.GetChild(wireParent.childCount - 1).gameObject);
        }*/
        DrawWires();
    }

    void Connect(Part g, Part c)
    {
        if (wireToggle.isOn)
        {
            if (!g.wiredObjects.Contains(c))
            {
                g.wiredObjects.Add(c);
            }
        }
       /* else if (wireToggle.isOn)
        {
            if (!c.wiredObjects.Contains(g))
            {
                c.wiredObjects.Add(g);
            }
        }*/
        else if (!g.cabledObjects.Contains(c))
        {                        
            g.cabledObjects.Add(c);
        }/*
        else if (!c.cabledObjects.Contains(g))
        {
            c.cabledObjects.Add(g);
        }*/
    }

    void DrawWires()
    {
        try
        {
            foreach (WireNode wireNode in wireList)
            {
                wireNode.line.SetPosition(0, wireNode.left);
                wireNode.line.SetPosition(1, wireNode.right);
                wireNode.line.GetComponent<EdgeCollider2D>().SetPoints(new List<Vector2>() { wireNode.right, wireNode.left });
            }
        }
        catch
        {
            Debug.Log("There was an error on line 104");
        }
    }
}
