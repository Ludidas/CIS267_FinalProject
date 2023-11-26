using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//=======================================
// The bat enemy will fly in random directions 
// it will only exist 
//=======================================

public class Bat : MonoBehaviour{

    [SerializeField] private float xPatrolRange;
    [SerializeField] private float yPatrolRange;
    [SerializeField] private float minTimeBeforeTurn;
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float damage;

    private float xMax;
    private float yMax;
    private float xMin;
    private float yMin;

    private bool playerPresent;
    private bool waitTurn;
    private float time;

    private Vector2 batPos;
    private Vector2 batDirection;
    private Vector2 batHome;

    private GameObject player;


    void Start(){

        batPos = new Vector2(transform.position.x, transform.position.y);
        // batHome will be the location it goes tword when it exits its area
        batHome = batPos;

        xMax = batHome.x + (xPatrolRange / 2f);
        yMax = batHome.y + (yPatrolRange / 2f);
        xMin = batHome.x - (xPatrolRange / 2f);
        yMin = batHome.y - (yPatrolRange / 2f);

        playerPresent = false;
        time = 0;
        waitTurn = false;

        choosePath();
    }

    void Update(){
        fly();
    }

    private void fly() {
        transform.Translate(batDirection * speed * Time.deltaTime);

        batPos.x += batDirection.x * speed * Time.deltaTime;
        batPos.y += batDirection.y * speed * Time.deltaTime;

        time += Time.deltaTime;

        if (time > minTimeBeforeTurn || batOutsideRange() > 0) {
            // Time is set in choosePath()
            choosePath();
        }
    }

    private void choosePath() {
        time = 0;

        float xNew;
        float yNew;

        if (playerPresent && !waitTurn && batOutsideRange() == 0) {

            xNew = player.transform.position.x - batPos.x;
            yNew = player.transform.position.y - batPos.y;

            float theta = math.atan2(yNew, xNew);

            xNew = math.cos(theta);
            yNew = math.sin(theta);

            // make the bat do something else if it just went after the player
            waitTurn = true;

        } else if (batOutsideRange() == 0){ // Bat is inside it's range
            float theta = UnityEngine.Random.Range(0f, 2 * Mathf.PI);

            xNew = math.cos(theta);
            yNew = math.sin(theta);

            // we waited our turn so we turn it off
            if (waitTurn) {
                waitTurn = false;
            }

        } else {
            // Bat is outside it's range
            xNew = batHome.x - batPos.x; 
            yNew = batHome.y - batPos.y;

            float theta = math.atan2(yNew, xNew);

            xNew = math.cos(theta);
            yNew = math.sin(theta);

            // same as above
            if (waitTurn) {
                waitTurn = false;
            }
        }

        batDirection = new Vector2 (xNew, yNew);
        // We don't have to normalize this because we are using cos and sin
        //batDirection.Normalize();
    }

    private int batOutsideRange() {

        if (batPos.x < xMin) {
            return 1; // x too small
        }else if (batPos.x > xMax) {
            return 2; // x too big
        }else if (batPos.y < yMin) {
            return 3; // y too small
        }else if (batPos.y > yMax) {
            return 4; // y too big
        }

        return 0;
    }

    private void doDamage() {
        Debug.Log("The player takes some damage");
    }

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // So the bat can seek the player
            player = collision.gameObject;
            playerPresent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerPresent = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            doDamage();
        }
    }
    #endregion
}
