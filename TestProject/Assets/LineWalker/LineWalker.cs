using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWalker : MonoBehaviour
{
    public float stepDistance;
    public float waitingPeriod;
    LineRenderer lineRenderer;
    float timer;
    Vector2 UVoffset;
    float totalDistance;
    float frame;
    // Use this for initialization
    void Start()
    {
        frame = 0;
        UVoffset.x = 1;
        lineRenderer = GetComponent<LineRenderer>();
        //Vector3[] points = lineRenderer.get;
        totalDistance = 0;
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            totalDistance += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }
        lineRenderer.material.SetTextureScale("_MainTex", new Vector2(totalDistance / stepDistance, 0.25f));
        StartCoroutine(switchFrame());
    }

    // Update is called once per frame
    void Update()
    {
        //if(UVoffset)
        if (timer < 0)
        {
            UVoffset.x += Time.deltaTime;
            if (UVoffset.x > (totalDistance / stepDistance))
            {
                UVoffset.x = -totalDistance / stepDistance;
                timer = waitingPeriod;
            }
            lineRenderer.material.SetTextureOffset("_MainTex", UVoffset);
        }
        else
        {

            timer -= Time.deltaTime;
        }

    }

    IEnumerator switchFrame()
    {
        while (true)
        {
            UVoffset.y = frame;
            frame += 0.25f;
            frame = frame % 1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
