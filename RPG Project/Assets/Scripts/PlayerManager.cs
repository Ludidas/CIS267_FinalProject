using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private Vector2 playerMovement = Vector2.zero;
    private Vector2 playerRotation = Vector2.zero;
    private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float deadZone;

    [Header("References")]
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject interactionZone;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    //Handles all of the player's movement
    public void movePlayer()
    {
        //Moves the player in their requested direction
        rb.velocity = playerMovement * speed;
    }

    //Handles all of the player's rotations
    public void rotatePlayer()
    {
        //Rotates the player's interactionZone in the direction they want to face. deadzone() ensure the player keeps facing their direction when they release the joystick
        if (deadzone(playerRotation))
        {
            interactionZone.transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3.up * playerRotation.x + Vector3.left * playerRotation.y));
        }
        //Rotates the interactionZone of the player if the user is not using the rotate joystick
        else if (deadzone(playerMovement))
        {
            interactionZone.transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3.up * playerMovement.x + Vector3.left * playerMovement.y));
        }
    }

    //Handles the movement of the camera
    public void moveCamera()
    {
        //Forces the camera to follow the player without having to parent it.
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
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

    #region Animations

    #warning THIS ANIMATION CODE DOES NOT WORK YET

    //This will handle walking animations and directional animations
    public void animatePlayer()
    {
        float rotation = Quaternion.LookRotation(Vector3.forward, (Vector3.up * playerMovement.x + Vector3.left * playerMovement.y)).eulerAngles.z;
        if (rb.velocity != Vector2.zero)
        {
            //The player in here is walking. We'll need to use walk animations
            if ((rotation >= 0 && rotation <= 45) || (rotation <= 360 && rotation >= 315))
            {
                //Player is facing right. Use right walking animations
                Debug.Log("Walking Right");
            }
            else if (rotation >= 45 && rotation <= 135)
            {
                //Player is facing up. Use upwards walking animations
                Debug.Log("Walking Up");
            }
            else if (rotation >= 135 && rotation <= 225)
            {
                //Player is facing left. Use left walking animations
                Debug.Log("Walking Left");
            }
            else if (rotation >= 225 && rotation <= 315)
            {
                //Player is facing down. Use downwards walking animations
                Debug.Log("Walking Down");
            }
        }
        else
        {
            //The player in here is standing still. We'll need to use idle animations
            if ((rotation >= 0 && rotation <= 45) || (rotation <= 360 && rotation >= 315))
            {
                //Player is facing right. Use right idle animations
                Debug.Log("Standing Right");
            }
            else if (rotation >= 45 && rotation <= 135)
            {
                //Player is facing up. Use upwards idle animations
                Debug.Log("Standing Up");
            }
            else if (rotation >= 135 && rotation <= 225)
            {
                //Player is facing left. Use left idle animations
                Debug.Log("Standing Left");
            }
            else if (rotation >= 225 && rotation <= 315)
            {
                //Player is facing down. Use downwards idle animations
                Debug.Log("Standing Down");
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
            //Currently just displays the text to the log. In the future we will have textboxes that appear, but we haven't made those yet.
            Debug.Log("Interacted Text: " + inter.GetComponent<InteractionText>().getText());
        }
    }

    #endregion
}
