using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivatable
{
    bool isActivated { get; set; }

    void Activate();

    void Deactivate();
}
