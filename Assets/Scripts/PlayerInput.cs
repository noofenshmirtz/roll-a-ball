using System.Collections;
using System . Collections . Generic ;
using UnityEngine ;
using UnityEngine . InputSystem ;

public class PlayerController : MonoBehaviour
{

    public Vector2 moveValue;
    public float speed;
    private int count;

    void start{
        count = 0;
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.
        fixedDeltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == " PickUp ")
        {
            other.gameObject.SetActive(false);
            count= count + 1;
        }
    }

    


    Add a private int count variable to count how many pick-ups are collected.
• Add the method Start, and initialise this variable to 0.
• Add 1 to this value every time a pick-up collides with the player.
}