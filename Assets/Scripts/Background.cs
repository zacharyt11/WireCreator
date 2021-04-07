using UnityEngine;

public class Background : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
