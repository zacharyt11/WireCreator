using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightF : Part, IActivatable
{
    public bool isActivated { get; set; }

    public void Activate()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
