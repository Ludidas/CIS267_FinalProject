using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDialogue : MonoBehaviour {

    private DialogueTrigger dialogueTrigger;

    public Dialogue[] oneDone;
    public Dialogue[] twoDone;
    public Dialogue[] threeDone;

    // get how many dungeons are complete and change the dialogue attached to the NPC accordingly
    private void Start() {
        dialogueTrigger = this.GetComponent<DialogueTrigger>();
        int dungeonsComplete = GameManager.getTotalDungeonsCompleted();

        if (dungeonsComplete == 1) {

            if (oneDone.Length != 0) {
                dialogueTrigger.setConversation(oneDone);
            }

        }else if (dungeonsComplete == 2) {

            if (twoDone.Length != 0) {
                dialogueTrigger.setConversation(oneDone);
            }

        } else if (dungeonsComplete == 3) {

            if (threeDone.Length != 0) {
                dialogueTrigger.setConversation(oneDone);
            }

        }
    }

}
