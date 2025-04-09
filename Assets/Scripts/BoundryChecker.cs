using UnityEngine;

public class BoundryChecker : MonoBehaviour
{
    public float boundryX = 52.5f;
    public float boundryZ = 34f;

    void Update()
    {
        if (transform.position.x > boundryX)
        {
            transform.position.Set(boundryX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -boundryX)
        {
            transform.position.Set(-boundryX, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -boundryZ)
        {
            transform.position.Set(transform.position.x, transform.position.y, -boundryZ);
        }
        if (transform.position.z > boundryZ)
        {
            transform.position.Set(transform.position.x, transform.position.y, boundryZ);
        }
    }


}