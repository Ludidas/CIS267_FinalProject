using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkyrimChecker : MonoBehaviour {

    [SerializeField] int[] stateSolution;
    [SerializeField] GameObject[] keys;

    [SerializeField] GameObject lockedGate;

    public void checkSolution() {
        bool solutionPassed = true;

        if (stateSolution.Length == keys.Length) {
            for (int i = 0; i < stateSolution.Length; i++) {
                if (!stateMatchesAt(i)) {
                    solutionPassed = false;
                }
            }
        }

        if (solutionPassed) {
            openGate();
        } else {
            closeGate();
        }

    }

    private void openGate() {
        lockedGate.SetActive(false);

        Debug.Log("Gate opened");
    }

    private void closeGate() {
        lockedGate.SetActive(true);

        Debug.Log("Gate Closed");
    }

    private bool stateMatchesAt(int i) {
        if (keys.ElementAt(i).GetComponent<SkyrimPuzzle>().state == stateSolution[i]) {
            return true;
        }
        return false;
    }



}
