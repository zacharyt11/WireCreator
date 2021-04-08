using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Part : MonoBehaviour
{
    public bool isSource;
    public bool sourceOn;
    public List<Part> connectedObjects;
    //public bool isDraggable = true;
    private bool shouldDrag;
    Color col;


    public void HandleDrag()
    {
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray myRay = new Ray(cursor, Vector3.forward * 20f);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(2) && Physics.Raycast(myRay, out hit) && hit.collider.gameObject == gameObject)
        {
            shouldDrag = true;
        }
        if (shouldDrag)
        {
            transform.position = new Vector3(cursor.x, cursor.y, transform.position.z);
        }
        if (Input.GetMouseButtonUp(2))
        {
            shouldDrag = false;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (isSource)
        {
            foreach (Part connectedObject in connectedObjects)
            {
                IActivatable a = connectedObject.GetComponent(typeof(IActivatable)) as IActivatable;
                if (a != null)
                {
                    if (a.isActivated)
                    {
                        a.Deactivate();
                    }
                    else
                    {
                        a.Activate();
                    }
                }
                a.isActivated = !a.isActivated;
                //connectedObject.GetComponent(connectedObject.GetComponent<MonoBehaviour>().GetType().Name).GetType().GetMethod("Deactivate").Invoke(connectedObject.GetComponent(connectedObject.GetComponent<MonoBehaviour>().GetType().Name), null);
            }
            sourceOn = !sourceOn;
        }
    }

    private void Update()
    {
        HandleDrag();
    }
}
