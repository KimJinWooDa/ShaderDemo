using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trMove : MonoBehaviour
{
    public Vector3[] trajectoryPosition;
    public int index;
    Transform tr;
    private void Awake()
    {
        tr = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 nextTracjectroyPos = trajectoryPosition[index];
        float height = tr.transform.localPosition.y;
        float nextHeight = Mathf.Lerp(height, nextTracjectroyPos.y, 0.6f);
        tr.transform.localPosition = Vector3.up * nextHeight;

        Vector3 nextTrPos = new Vector3(nextTracjectroyPos.x, 0, nextTracjectroyPos.z);
        tr.transform.position = Vector3.Lerp(this.transform.position, nextTrPos, 0.6f);

        if (Vector3.Distance(this.transform.position, nextTrPos) < 0.05f)
        {
            if (index < trajectoryPosition.Length - 1) index++;
            else if (trajectoryPosition.Length - 1 < index) index = trajectoryPosition.Length - 1;
        }
    }
}
