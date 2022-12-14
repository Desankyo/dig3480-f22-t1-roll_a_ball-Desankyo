using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI livesText;
    public GameObject LoseTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        rb = GetComponent<Rigidbody>();
        lives = 3;

        SetCountText();
        LoseTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 20)

        {
            winTextObject.SetActive(true);
        }

        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            LoseTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pickups
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        // Enemy pickups
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;

            SetCountText();
        }
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
        if (count >= 20)
        {
            GetComponent<PlayerController>().enabled = false;
        }
        // Player teleport
        else if (count == 12)
        {
            transform.position = new Vector3(308.0f, 0.5f, 8.0f);
        }
    }
}
