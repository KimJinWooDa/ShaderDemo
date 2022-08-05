using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    RaycastHit raycastHit;
    float shootDistance = 777f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Transform gunHole;
    Vector3 dir;
    [SerializeField] Transform gunTransform;
    public void Shooting()
    {
        dir = gunHole.position - transform.position;
        if (Physics.Raycast(transform.position, dir, out raycastHit, shootDistance))
        {
            
            if (raycastHit.collider.CompareTag("BUG"))
            {
                Debug.Log("버그다!!");
                //버그 쉐이더 On -> 슈루룩 사라지게끔 하고 죽는소리 꽥;;
                raycastHit.transform.GetComponent<DissolveController>().go = true;
                GameObject effect = Instantiate(hitEffect, raycastHit.transform.position, Quaternion.identity);
                Destroy(effect, 1f);
            }
            else
            {
                Debug.Log("그것도 못맞추냐?ㅋㅋ");
            }
            
        }
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, dir * 30f, Color.red);
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
}
