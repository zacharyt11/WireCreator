using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class WireCableToggler : MonoBehaviour
{
    [SerializeField] Text txt;

    public void ConnectorSwitch()
    {
        txt.text = GetComponent<Toggle>().isOn ? "Wire" : "Cable";
    }
}
