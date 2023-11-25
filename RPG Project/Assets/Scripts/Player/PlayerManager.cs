using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerManager : MonoBehaviour
{
    private Vector2 playerMovement = Vector2.zero;
    private Vector2 playerRotation = Vector2.zero;
    private Vector2 iceMovementHold = Vector2.zero;
    private Rigidbody2D rb;

    //This is for animations to tell which directional system should be used
    private bool playerIsRotating;

    //this is not control schemes, this is for moving as the player.
    //Update this as needed
    //0 is regular, 1 is ice, 2 is for being stopped on ice
    //-1 can be used to prevent any movement
    [NonSerialized] public int movementType = 0;

    private bool cameraLock;

    //These are both used for falling while in the cave dungeon
    private float isFalling;
    private bool touchingCaveUnder;

    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float deadZone;
    [SerializeField] private float fallingLength;

    [Header("References")]
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject interactionZone;
    [SerializeField] private GameObject dialogueManager;
    [SerializeField] private GameObject inputManagerGO;

    private InputManager inputManager;
    private Animator playerAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        inputManager = inputManagerGO.GetComponent<InputManager>();
        inputManager.swapMap("PlayerControls");

        playerAnimator = GetComponent<Animator>();

        transform.position = GameManager.getSpawnLocation();

        cameraLock = false;

        isFalling = -1f;
    }

    //Keep this organized. Update() should contain as few functions as possible, and it should be obvious what the functions do.
    private void Update()
    {
        updatePlayer();
        animatePlayer();
    }

    #region Movement

    //updatePlayer() does three things. Moves the player, rotates the player, and moves the camera. Each of these actions can be found in their respective functions.
    public void updatePlayer()
    {
        movePlayer();
        rotatePlayer();
        moveCamera();

        //Custom falling animation for when the player falls off a ledge
        caveGroundFall();
    }

    //Handles all of the player's movement
    public void movePlayer()
    {
        //We need to account for custom movement methods
        //movementType 0 is for regular movement
        if (movementType == 0)
        {
            rb.velocity = playerMovement * speed;
        }
        //movementType 1 and 2 is for ice movement
        else if (movementType == 1 || movementType == 2)
        {
            iceMovement();
        }
    }

    //Handles all of the player's rotations
    public void rotatePlayer()
    {
        //Rotates the player's interactionZone in the direction they want to face. deadzone() ensure the player keeps facing their direction when they release the joystick
        if (deadzone(playerRotation))
        {
            interactionZone.transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3.up * playerRotation.x + Vector3.left * playerRotation.y));
            playerIsRotating = true;
        }
        //Rotates the interactionZone of the player if the user is not using the rotate joystick
        else if (deadzone(playerMovement))
        {
            interactionZone.transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3.up * playerMovement.x + Vector3.left * playerMovement.y));
            playerIsRotating = false;
        }
    }

    //deadzone() returns false when the joystick is too close to the middle, and true if it's far enough out
    public bool deadzone(Vector2 pDirection)
    {
        if ((pDirection.x > -deadZone && pDirection.x < deadZone) && (pDirection.y > -deadZone && pDirection.y < deadZone))
        {
            return false;
        }
        return true;
    }

    #endregion

    #region Camera

    //Handles the movement of the camera
    public void moveCamera()
    {
        //Forces the camera to follow the player without having to parent it.
        if (!cameraLock)
        {
            camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
        
    }

    //Locks or unlocks the camera based on l (true for lock, false for unlock)
    public void lockCamera(bool l)
    {
        cameraLock = l;
    }

    #endregion

    #region Animations

    //This will handle walking animations and directional animations
    public void animatePlayer()
    {
        float rotation;
        if (playerIsRotating && deadzone(playerMovement))
        {
            rotation = interactionZone.transform.eulerAngles.z;
        }
        else
        {
            rotation = Quaternion.LookRotation(Vector3.forward, (Vector3.up * playerMovement.x + Vector3.left * playerMovement.y)).eulerAngles.z;
        }
        
        float rotationStill = interactionZone.transform.eulerAngles.z;
        if (rb.velocity != Vector2.zero && movementType != -1)
        {
            //For direction, 0 is facing down, 1 is facing up, 2 is facing right, 3 is facing left

            playerAnimator.SetBool("isWalking", true);
            //The player in here is walking. We'll need to use walk animations
            if (rotation >= 45 && rotation <= 135)
            {
                //Player is facing up. Use upwards walking animations
                //Debug.Log("Walking Up");
                playerAnimator.SetInteger("direction", 1);
            }
            else if (rotation >= 225 && rotation <= 315)
            {
                //Player is facing down. Use downwards walking animations
                //Debug.Log("Walking Down");
                playerAnimator.SetInteger("direction", 0);
            }
            else if (rotation >= 135 && rotation <= 225)
            {
                //Player is facing left. Use left walking animations
                //Debug.Log("Walking Left");
                playerAnimator.SetInteger("direction", 3);
            }
            else if ((rotation >= 0 && rotation <= 45) || (rotation <= 360 && rotation >= 315))
            {
                //Player is facing right. Use right walking animations
                //Debug.Log("Walking Right");
                playerAnimator.SetInteger("direction", 2);
            }
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);

            //The player in here is standing still. We'll need to use idle animations
            if (rotationStill >= 45 && rotationStill <= 135)
            {
                //Player is facing up.
                playerAnimator.SetInteger("direction", 1);
            }
            else if (rotationStill >= 225 && rotationStill <= 315)
            {
                //Player is facing down.
                playerAnimator.SetInteger("direction", 0);
            }
            else if (rotationStill >= 135 && rotationStill <= 225)
            {
                //Player is facing left.
                playerAnimator.SetInteger("direction", 3);
            }
            else if ((rotationStill >= 0 && rotationStill <= 45) || (rotationStill <= 360 && rotationStill >= 315))
            {
                //Player is facing right.
                playerAnimator.SetInteger("direction", 2);
            }
        }
    }

    #endregion

    #region Inputs

    //Gets the player's movement inputs
    public void onMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        playerMovement = inputMovement;
    }

    //Gets the player's rotation inputs
    public void onTurn(InputAction.CallbackContext value)
    {
        Vector2 inputTurn = value.ReadValue<Vector2>();
        playerRotation = inputTurn;
    }

    //Interacts with whatever the player is looking at
    public void onInteract(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            //Gets the gameobject stored in the InteractionZone object.
            GameObject inter = interactionZone.GetComponent<InteractionZone>().getInteractionObject();
            //Ensures that the InteractionZone is currently storing a gameobject
            if (inter != null)
            {
                //Checks all of the interactions that could occur
                checkInteractions(inter);
            }
        }
    }

    #endregion

    #region Interact Functions

    public void checkInteractions(GameObject inter)
    {
        //This is where any interactions will occur. For example, talking to characters, looking at signs, picking up items, etc.
        //A significant amount of this code is for testing, and will most likely be majorly overhauled later on.
        if (inter.CompareTag("Text"))
        {
            //Trigger a dialogue box to appear and interact with the user
            //This takes the dialogueManager script, and uses the startDialogue function with the parameter of whatever is stored in the DialogueTrigger of the interacted with object.
            dialogueManager.GetComponent<DialogueManager>().startDialogue(inter.GetComponent<DialogueTrigger>().triggerDialogue());
        }
    }

    #endregion

    #region Collisions

    //Rather than having collisions attached to the PlayerManager class, each one is attached to its
    //respective hitbox to prevent incorrect colliders from sending incorrect collisions.
    //You can find each script for each hitbox attached to the player's children.

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Ice Collision
    //    iceTriggerEnter(collision);

    //    //Cave Under Collision
    //    caveUnderTriggerEnter(collision);

    //    Debug.Log("Collision: ");
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    //Ice Block Collision
    //    iceBlockCollisionStay(collision);

    //    Debug.Log("Collision: " + collision.contacts[0].otherCollider.name);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    //Ice Collision
    //    iceTriggerExit(collision);
        
    //    //Cave Ground Collision
    //    caveGroundTriggerExit(collision);

    //    //Cave Under Collision
    //    caveUnderTriggerExit(collision);
    //}

    #endregion

    #region Ice

    private void iceMovement()
    {
        if (movementType == 1)
        {
            rb.velocity = iceMovementHold * speed;
        }
        //movementType 2 is for ice movement when stopped on ice
        else if (movementType == 2)
        {
            rb.velocity = playerMovement * speed;
            //When the player starts moving again, swap back to regular ice movement.
            if (rb.velocity != Vector2.zero)
            {
                iceMovementHold = playerMovement;
                movementType = 1;
            }
        }
    }

    public void iceTriggerEnter(Collider2D collision)
    {
        //Ice Collision
        if (collision.gameObject.CompareTag("Ice"))
        {
            //We need to save what playerMovement was upon entering the ice, and set it back after swapping the map
            iceMovementHold = playerMovement;
            movementType = 1;
        }
    }

    public void iceTriggerExit(Collider2D collision)
    {
        //Ice Collision
        if (collision.gameObject.CompareTag("Ice"))
        {
            //Let the player start moving naturally again
            movementType = 0;
            iceMovementHold = Vector2.zero;
        }
    }

    public void iceBlockCollisionStay(Collision2D collision)
    {
        //Ice Block Collision
        if (collision.gameObject.CompareTag("Iceblock"))
        {
            //If the iceblock has stopped, assume the user has too, and let the user move once
            if (movementType == 1 && collision.gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                movementType = 2;
                iceMovementHold = Vector2.zero;
            }
        }
    }

    #endregion

    #region Cave Platforms

    public void caveGroundTriggerExit(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CaveGround") && isFalling <= 0)
        {
            Debug.Log("FALL");
            
            lockCamera(true);
            movementType = -1;
            isFalling = fallingLength;
            Debug.Log("Health: " + GameManager.getHealth());
        }
    }

    public void caveUnderTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CaveUnder"))
        {
            Debug.Log("Enter CaveUnder");
            touchingCaveUnder = true;
        }
    }

    public void caveUnderTriggerExit(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CaveUnder"))
        {
            Debug.Log("Exit CaveUnder");
            touchingCaveUnder = false;
        }
    }

    private void caveGroundFall()
    {
        if (isFalling > 0)
        {
            GetComponent<SpriteRenderer>().sortingOrder = (touchingCaveUnder ? 0 : -2);

            rb.velocity = Vector2.down * speed * 4;

            isFalling -= Time.deltaTime;
        }
        else if (isFalling != -1)
        {
            isFalling = -1;
            movementType = 0;
            GameManager.changeHealth(-1.0f);
            transform.position = GameManager.getSpawnLocation();
            lockCamera(false);
            GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        
    }

    #endregion
}
