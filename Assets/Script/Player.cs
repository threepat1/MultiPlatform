using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 4;
    private Rigidbody rb;

    public float thrust = 10;
    public float maxJumpTime = 0.1f;
    public bool jumping;
    public float jumpTimer;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        if(transform.position.y <= startPos.y - 5)
        {
            string thisScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(thisScene);
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && jumpTimer <= maxJumpTime)
        {
            jumping = true;
            rb.AddForce(Vector2.up * thrust);
        }
        if (jumping)
        {
            jumpTimer += Time.deltaTime;
        }
        if (rb.velocity.y < -0.1f || jumpTimer >= maxJumpTime)
        {
            Physics.gravity = new Vector3(0, -25f, 0);
        }
        if (jumpTimer > 2f)
        {
            ResetJump();
        }
    }
    private void ResetJump()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
        jumping = false;
        jumpTimer = 0;
    }
    private void OnCollisionEnter(Collision col)
    {
       if(col.collider.gameObject.tag == "Floor")
        {
            ResetJump();
        }      
    }
    public void Jump()
    {
        jumping = true;
        rb.AddForce(Vector2.up * thrust);
    }
}
