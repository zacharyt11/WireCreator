using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteManager : MonoBehaviour
{
    List<Part> objDeleted;
    List<Part> objConnectedToDeleted;

    private void Update()
    {
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.X))
        {
            Collider2D[] array = Physics2D.OverlapCircleAll(cursor, 1f);
            if (array.Length > 0)
            {
                Debug.Log("Called");
                foreach(Collider2D collider in array)
                {
                    if (collider.GetComponent<Part>())
                    {
                        GameObject temp = collider.gameObject;
                        objDeleted = collider.GetComponent<Part>().wiredObjects;
                        objConnectedToDeleted = temp.GetComponent<Part>().wiredObjects;
                        foreach (Part prt in objDeleted)
                        {
                            Destroy(prt);
                            prt.wiredObjects.Remove(prt);
                        }
                    }
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
