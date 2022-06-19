using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;

public class Player : MonoBehaviour
{
    public int speed = 10;
    private Rigidbody rb;
    private float movementVectorX;
    private float movementVectorZ;
    private int count ;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winGameText;
    public TextMeshProUGUI LoseGameText;
    private bool FailedStatus = false; 
    private GameObject[] pickupParent;
    public GameObject restartButton;
    private PlayerInput playerInputComponent;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();   
      count = 0 ;
      
      pickupParent = GameObject.FindGameObjectsWithTag("PickUp");
      playerInputComponent = GetComponent<PlayerInput>();
      
      SetText();

    }

    private void Update()
    {
        if(count == 38 && FailedStatus == false)
        {
            winGameText.gameObject.SetActive(true);
            playerInputComponent.enabled = false;

        }
    }

    private void OnMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();
        movementVectorX = movementVector.x ;
        movementVectorZ = movementVector.y;

        
    }

    private void SetText()
    {
        scoreText.text = "Score : " + count.ToString(); 
    }

    private void FixedUpdate() {
        Vector3 movement;
     
        movement.x = movementVectorX ;
        movement.y =  0.0f;
        movement.z = movementVectorZ;
        

         rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false); 
            count += 1;
            SetText();
        }   

        if(other.gameObject.CompareTag("DeadlyWalls"))
        {
            
            if(count != 38)
            {
                LoseGameText.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);
                FailedStatus = true;
                playerInputComponent.enabled = false;
                
            }

        }
    }

    public void RestartGame()
    {
        count = 0;
        SetText();
       
        foreach(var obj in pickupParent)
        {
            obj.SetActive(true);
        }

        playerInputComponent.enabled = true;
        transform.position = new Vector3(0.0f , 0.5f, -5.5f);
        LoseGameText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
       

        
    }
}
