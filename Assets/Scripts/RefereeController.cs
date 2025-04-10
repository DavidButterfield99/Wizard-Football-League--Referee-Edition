using UnityEngine;

public class RefereeController : MonoBehaviour
{
    public float speed = 250f;
    private float horizontalInput;
    private float verticalInput;

    private bool gameOver = false;
    private bool canMove = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Team"))
        {
            Debug.Log("Game over!");
        }
    }
}
