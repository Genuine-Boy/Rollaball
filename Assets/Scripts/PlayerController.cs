using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float jump_power = 1;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    private bool in_air;
    private bool jumps_used = false;
    //Vector3 jump = new Vector3(0.0f, 2 * jump_power, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        in_air = false;

        //Vector3 jump = new Vector3(0.0f, 2 * jump_power, 0.0f);

        SetCountText();
        winTextObject.SetActive(false);
    }

    //Case handling movement for player object
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 8) {
            winTextObject.SetActive(true);
        }
    }

    void OnCollisionStay()
    {
        in_air = false;
        jumps_used = false;
    }

    void OnCollisionExit()
    {
        in_air = true;
    }

    void OnJump()
    {
      if (!jumps_used) {
          Vector3 jump = new Vector3(0.0f, 2 * jump_power, 0.0f);
          rb.AddForce(jump, ForceMode.Impulse);

          if (!in_air) {
              in_air = true;
          }
          else {
              jumps_used = true;
          }
      }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called at end of frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup")) {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
    }
}
