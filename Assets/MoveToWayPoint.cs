using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWayPoint : MonoBehaviour
{
    [SerializeField] GameObject WayPointGroup;
    [SerializeField] Transform[] wayPoints;

    Transform tr;
    public int nextIdx = 1;
    public float speed = 1f;
    public float damping = 3f;
    private void Awake()
    {
        wayPoints = WayPointGroup.GetComponentsInChildren<Transform>();
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveToPoint();
    }

    void MoveToPoint()
    {
        Vector3 dir = wayPoints[nextIdx].position - tr.position;

        Quaternion rot = Quaternion.LookRotation(dir);

        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WAYPOINT"))
        {
            nextIdx = (++nextIdx >= wayPoints.Length) ? 1 : nextIdx;
        }
    }
}
