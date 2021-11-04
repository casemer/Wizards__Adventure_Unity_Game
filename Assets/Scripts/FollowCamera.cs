using UnityEngine;
using System.Collections;
using System.Runtime;
using System.Security.Cryptography;

public class FollowCamera : MonoBehaviour {

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;

    public GameObject bones;
    public AcidDeath acidDeath;

    // Use this for initialization
    void Start()
    {
        Vector3 playerStart = target.transform.position;
        targetPos = transform.position;
        transform.position = new Vector3(playerStart.x, playerStart.y, -1f);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 15f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        }

        if(acidDeath.diedByAcid)
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
