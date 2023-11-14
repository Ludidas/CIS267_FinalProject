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
    [SerializeField] private float slowdownSpeed;
    [SerializeField] private float deadZone;

    [Header("References")]
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject interactionZone;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Keep this organized. Update() should contain as few functions as possible.
    private void Update()
    {
        movePlayer();
    }

    #region Movement

    //Handles all of the player's movement and rotation
    public void movePlayer()
    {
        //Gives the player speed in the direction they are moving
        rb.velocity = (rb.velocity + playerMovement) / slowdownSpeed;
        //Rotates the player in the direction they want to face. deadzone() ensure the player keeps facing their direction when they release the joystick
        if (deadzone(playerRotation))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3.up * playerRotation.x + Vector3.left * playerRotation.y));
        }
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
