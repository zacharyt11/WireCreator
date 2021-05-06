using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartButtonManager : MonoBehaviour
{
    [SerializeField] List<string> names;
    [SerializeField] GameObject prefab;
    [SerializeField] ColorPicker colPick;

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
        GameObject clonedPart = Instantiate(Resources.Load(n) as GameObject, new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f), Quaternion.identity);
        clonedPart.GetComponent<SpriteRenderer>().color = colPick.GetColor();
        if (clonedPart.transform.childCount > 0)
        {
            for (int i = 0; i < clonedPart.transform.childCount; i++)
            {
                SpriteRenderer childSpriteRen = clonedPart.transform.GetChild(i).GetComponent<SpriteRenderer>();
                if (childSpriteRen != null)
                {
                    childSpriteRen.color = colPick.GetColor();
                }               
            }
        }
    }
}
