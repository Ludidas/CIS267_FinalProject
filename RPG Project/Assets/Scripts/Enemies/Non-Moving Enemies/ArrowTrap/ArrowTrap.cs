using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour{

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject arrow;
    private GameObject newArrow;
    private GameObject oldArrow;

    [SerializeField] private float speed;
    [SerializeField] private float shotCooldown;
    [SerializeField] private float reloadCooldown;

    private float _shotCooldown;
    private float _reloadCooldown;

    private Vector2 arrowDirection;


    void Start(){
        _shotCooldown = shotCooldown;
        _reloadCooldown = reloadCooldown;

        // create arrow and shoot instantly
        reloadCooldown = 0;
        reload();

        getArrowDirection();
    }


    void Update(){
        moveArrow();
        shoot();
        reload();

        attemptDespawn();
    }

    private void shoot() {
        if (newArrow == null) {
            return;
        }
        
        // Arrow not moving
        if (!newArrow.GetComponent<Arrow>().Moving && shotCooldown > 0) {
            shotCooldown -= Time.deltaTime;
        } else if (shotCooldown <= 0){
            // Where the magic happens
            GetComponent<AudioSource>().Play();

            newArrow.GetComponent<Arrow>().Moving = true;
            // reset cooldown
            shotCooldown = _shotCooldown;
        }
    }

    private void reload() {
        if (newArrow == null && reloadCooldown > 0) {
            reloadCooldown -= Time.deltaTime;
            return;
        }

        if (reloadCooldown <= 0) {

            if (oldArrow == null) {
                // reset cooldown
                reloadCooldown = _reloadCooldown;

                oldArrow = newArrow;
                newArrow = Instantiate(arrow, transform.position, transform.rotation);

                newArrow.GetComponent<Arrow>().Moving = false;

                if (oldArrow != null) {
                    oldArrow.GetComponent<Arrow>().Moving = true;
                }
            }

            
        } else if (reloadCooldown > 0 && newArrow.GetComponent<Arrow>().Moving){
            reloadCooldown -= Time.deltaTime;
        }
    }

    // why did i do it like this
    private void attemptDespawn() {

        if (targetAbove()) {

            if (oldArrow != null) {
                if (oldArrow.transform.position.y > target.transform.position.y) {
                    Destroy(oldArrow);
                }
            }

            if (newArrow != null) {
                if (newArrow.transform.position.y > target.transform.position.y) {
                    Destroy(newArrow);
                }
            }

        } else { // target below

            if (oldArrow != null) {
                if (oldArrow.transform.position.y < target.transform.position.y) {
                    Destroy(oldArrow);
                }
            }

            if (newArrow != null) {
                if (newArrow.transform.position.y < target.transform.position.y) {
                    Destroy(newArrow);
                }
            }

        }


        if (targetRight()) {

            if (oldArrow != null) {
                if (oldArrow.transform.position.x > target.transform.position.x) {
                    Destroy(oldArrow);
                }
            }

            if (newArrow != null) {
                if (newArrow.transform.position.x > target.transform.position.x) {
                    Destroy(newArrow);
                }
            }

        } else { //target left

            if (oldArrow != null) {
                if (oldArrow.transform.position.x < target.transform.position.x) {
                    Destroy(oldArrow);
                }
            }

            if (newArrow != null) {
                if (newArrow.transform.position.x < target.transform.position.x) {
                    Destroy(newArrow);
                }
            }

        }

    }

    private void moveArrow() {
        if (oldArrow != null && oldArrow.GetComponent<Arrow>().Moving) {
            oldArrow.transform.Translate(arrowDirection * speed * Time.deltaTime, Space.World);
        }

        if (newArrow != null && newArrow.GetComponent<Arrow>().Moving){
            newArrow.transform.Translate(arrowDirection * speed * Time.deltaTime, Space.World);
        }
    }

    private void getArrowDirection() {
        Vector2 vecArrow = newArrow.transform.position;
        Vector2 vecTarget = target.transform.position;

        float adjacent = vecTarget.y - vecArrow.y;
        float opposite = vecTarget.x - vecArrow.x;

        float theta = Mathf.Atan2(adjacent,opposite);

        Debug.Log(adjacent + " " + opposite);

        arrowDirection.y = Mathf.Sin(theta);
        arrowDirection.x = Mathf.Cos(theta);
    }

    #region Clutter
    private bool targetAbove() {
        if(arrowDirection.y > 0) {
            return true;
        }
        return false;
    }
    private bool targetRight() {
        if (arrowDirection.x > 0) {
            return true;
        }
        return false;
    }
    #endregion
}
