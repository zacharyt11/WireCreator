using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Part, IActivatable
{
    [SerializeField] Transform arm;
    Part part;
    public bool isActivated { get; set; }

    private void Start()
    {
        part = GetComponent<Part>();
    }

    public void Activate()
    {
        if (arm.localRotation.z == 180)
        {
            Physics2D.OverlapBoxAll(arm.position - Vector3.up, Vector2.one, 0f);
            foreach (Part cabledObject in cabledObjects)
            {
                cabledObject.gameObject.transform.position = Vector3.down * 5f;
            }
        }
        else if (arm.localRotation.z == 0)
        {
            Debug.Log("Activated");
            Physics2D.OverlapBoxAll(arm.position + Vector3.up, Vector2.one, 0f);
            foreach (Part cabledObject in cabledObjects)
            {
                cabledObject.gameObject.transform.position = Vector3.up * 5f;
            }
        }
        else if (arm.localRotation.z == 90)
        {
            Physics2D.OverlapBoxAll(arm.position + Vector3.right, Vector2.one, 0f);
            foreach (Part cabledObject in cabledObjects)
            {
                cabledObject.gameObject.transform.position = Vector3.right * 5f;
            }
        }
        else if (arm.localRotation.z == 270)
        {
            Physics2D.OverlapBoxAll(arm.position - Vector3.right, Vector2.one, 0f);
            foreach (Part cabledObject in cabledObjects)
            {
                cabledObject.gameObject.transform.position = Vector3.left * 5f;
            }
        }
        Debug.Log(arm.localRotation.z);
    }
    public void Deactivate()
    {

    }
}
