using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    struct WireNode 
    {
        public Transform left;
        public Transform right;
        public LineRenderer line;
    }
    public GameObject text;
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
        Ray myRay = new Ray(cursor, Vector3.forward * 20f);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(1) && Physics.Raycast(myRay, out hit))
        {
            sparks.transform.position = hit.point + Vector3.forward * 10f;
            sparks.Play();
            GameObject currentObj = hit.collider.gameObject;
            if (lastClickedObj != null && CanDrawLine(lastClickedObj, currentObj))
            {
                wireList.Add(new WireNode()
                {
                    line = Instantiate(wire, Vector3.zero, Quaternion.identity, wireParent).GetComponent<LineRenderer>(),
                    left = currentObj.transform,
                    right = lastClickedObj.transform
                });
                if (currentObj.CompareTag("Source"))
                {
                    currentObj.GetComponent<Part>().connectedObjects.Add(lastClickedObj.GetComponent<Part>());
                }
                else
                {
                    lastClickedObj.GetComponent<Part>().connectedObjects.Add(currentObj.GetComponent<Part>());
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

    void DrawWires()
    {
        foreach (WireNode wireNode in wireList)
        {
            wireNode.line.SetPosition(0, wireNode.left.position);
            wireNode.line.SetPosition(1, wireNode.right.position);
        }
    }
}
