using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightF : Part, IActivatable
{
    Transform child;
    private void Start()
    {
        child = transform.GetChild(0);
    }

    public bool isActivated { get; set; }

    public void Activate()
    {
        child.gameObject.SetActive(true);
        child.GetComponent<Light2D>().color = GetComponent<SpriteRenderer>().color;
    }
    public void Deactivate()
    {
        child.gameObject.SetActive(false);
    }
}
