using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartButtonManager : MonoBehaviour
{
    [SerializeField] List<string> names;
    [SerializeField] GameObject prefab;

    private void Awake()
    {
        names.Sort((string a, string b) =>
        {
            return string.Compare(a, b);
        });
        foreach (string n in names)
        {
            GameObject clonedPrefab = Instantiate(prefab, transform);
            clonedPrefab.GetComponentInChildren<Text>().text = n;
            clonedPrefab.GetComponent<Button>().onClick.AddListener(() => SummonPart(n));
        }       
    }

    void SummonPart(string n)
    {
        Instantiate(Resources.Load(n));
    }
}
