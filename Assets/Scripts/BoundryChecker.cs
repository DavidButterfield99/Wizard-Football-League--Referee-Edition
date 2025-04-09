using UnityEngine;

public class BoundryChecker : MonoBehaviour
{
    float boundryX = 520f;
    float boundryZ = 270f;

    void Update()
    {
        if (transform.position.x > boundryX)
        {
            // Debug.Log($"Violated boundry x: {transform.position.x} > {boundryX}");
            transform.position = new Vector3(boundryX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -boundryX)
        {
            // Debug.Log($"Violated boundry -x: {transform.position.x} < {-boundryX}");
            transform.position = new Vector3(-boundryX, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -boundryZ)
        {
            // Debug.Log($"Violated boundry -z: {transform.position.z} < {-boundryZ}");
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundryZ);
        }
        if (transform.position.z > boundryZ)
        {
            // Debug.Log($"Violated boundry z: {transform.position.z} > {boundryZ}");
            transform.position = new Vector3(transform.position.x, transform.position.y, boundryZ);
        }
    }


}