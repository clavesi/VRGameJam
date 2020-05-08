using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCubeColliders : MonoBehaviour
{
    public Vector3 box = new Vector3(4.5f, 4, 4.5f);
    public int clearCondition;
    public bool condition;
    public Collider[] hitColliders;

    void Update()
    {
        hitColliders = Physics.OverlapBox(gameObject.transform.position, box / 2, Quaternion.identity);

        if (hitColliders.Length == clearCondition)
        {
            condition = true;
        } else
        {
            condition = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(transform.position, box);
    }
}
