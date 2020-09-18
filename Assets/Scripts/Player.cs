using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Tweener tweener = null;
    [SerializeField] Animator animatorController = null;
    [SerializeField] AudioSource moveSound = null;
    private List<GameObject> itemList;
    private Vector3 prevTransform;
    GameObject parentObject = null;

    // Start is called before the first frame update
    void Start() {
        parentObject = this.transform.parent.gameObject;
        parentObject.transform.position = new Vector3(0.32f, -0.32f, 0);
        itemList = new List<GameObject>();
        itemList.Add(parentObject);
        Debug.Log(itemList[0]);
        prevTransform = parentObject.transform.position;
    }

    // Update is called once per frame
    void Update() {

        // Check if the player is moving
        if (prevTransform != parentObject.transform.position) {
            animatorController.SetBool("isMoving", true);
        } else {
            animatorController.SetBool("isMoving", false);
        }
        prevTransform = parentObject.transform.position;


        if (parentObject.transform.position == new Vector3(0.32f, -0.32f, 0)) {
            animatorController.SetBool("goingUp", false);
            MovePlayer(new Vector3(1.92f, -0.32f, 0f), 1.5f);
            animatorController.SetBool("goingRight", true);
        }
        if (parentObject.transform.position == new Vector3(1.92f, -0.32f, 0f)) {
            animatorController.SetBool("goingRight", false);
            MovePlayer(new Vector3(1.92f, -1.6f, 0f), 1.5f);
            animatorController.SetBool("goingDown", true);
        }
        if (parentObject.transform.position == new Vector3(1.92f, -1.6f, 0f)) {
            animatorController.SetBool("goingDown", false);
            MovePlayer(new Vector3(0.32f, -1.6f, 0f), 1.5f);
            animatorController.SetBool("goingLeft", true);
        }
        if (parentObject.transform.position == new Vector3(0.32f, -1.6f, 0f)) {
            animatorController.SetBool("goingLeft", false);
            MovePlayer(new Vector3(0.32f, -0.32f, 0), 1.5f);
            animatorController.SetBool("goingUp", true);
        }
    }

    void MovePlayer(Vector3 endPos, float duration) {
        foreach (GameObject item in itemList) {
            bool hasAdded = tweener.AddTween(parentObject.transform, parentObject.transform.position, endPos, duration);
            if (hasAdded) {
                break;
            }
        }
    }
}
