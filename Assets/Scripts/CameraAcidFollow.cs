using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAcidFollow : MonoBehaviour
{
    Vector3 targetPos;
    public float interpVelocity;
    public Vector3 offset;
    public GameObject bones;
    public AcidDeath acidDeath;
    public GameObject target;

    void Start()
    {
        Vector3 playerStart = target.transform.position;
        targetPos = transform.position;
        transform.position = new Vector3(playerStart.x, playerStart.y, -1f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (acidDeath.diedByAcid)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = bones.transform.position.z;

            Vector3 targetDirection = (bones.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 15f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        }
    }
}
