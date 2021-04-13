using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutArrow : MonoBehaviour
{
    [SerializeField] Transform scrollView;
    bool openingScrollView;
    bool currentlyPopping;

    public void ArrowButton()
    {
        if (!currentlyPopping)
        {
            StartCoroutine(TweenPopOut());
        }      
    }

    IEnumerator TweenPopOut()
    {
        float startX = scrollView.position.x, endX = startX * -1f;
        float startTime = Time.time;
        float endTime = startTime + 1f;
        currentlyPopping = true;
        openingScrollView = !openingScrollView;
        transform.localScale = new Vector3(openingScrollView ? 1f : -1f, 1f, 1f);
        while (Time.time <= endTime)
        {
            scrollView.position = new Vector3(Mathf.Lerp(startX, endX, Time.time - startTime), scrollView.position.y, scrollView.position.z);
            yield return null;
        }
        currentlyPopping = false;
    }
}
