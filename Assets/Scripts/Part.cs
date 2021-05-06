using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Part : MonoBehaviour
{
    public bool isSource;
    public bool sourceOn;
    public List<Part> wiredObjects;
    public List<Part> cabledObjects;
    //public bool isDraggable = true;
    private bool shouldDrag;
    Color col;

    private void OnTriggerStay(Collider other)
    {
        Vector3 opposite = transform.position - other.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + opposite, Time.deltaTime);
        if (!GetComponent<SpriteRenderer>().isVisible)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void HandleDrag()
    {
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider;
        collider = Physics2D.OverlapCircle(cursor, 0.1f);
        if (/*(!collider.gameObject.CompareTag("Immobile") && */Input.GetKeyDown(KeyCode.C) && collider != null)
        {
            shouldDrag = true;
        }
        if (shouldDrag && collider != null && !collider.CompareTag("Wire"))
        {
            Vector3 ogPos = collider.transform.position;
            collider.transform.position = new Vector3(cursor.x, cursor.y, collider.transform.position.z);
            Vector3 deltaPos = collider.transform.position - ogPos;
            foreach (Part part in cabledObjects)
            {
                part.transform.position += deltaPos;
            }
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            shouldDrag = false;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (isSource)
        {
            foreach (Part connectedObject in wiredObjects)
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
