using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {
    //INSTANCE
    public static PlayerMovementScript instance;
    public bool thisScriptLoaded = false;

    //OBJECT LINKS
    public GameObject playerHolder;
    public GameObject slideBarContainer;
    public GameObject slideBarContent;

    //MOVEMENT RELATED
    private Vector3 targetPlayerPosition;
    private Vector2[] newTouch;
    private Vector3[] playerPositions;
    private int currendPosIndex = 1;
    public float lastSwipeTime;

    // BEHAVIOUR
    public bool allowSwipe = false;
    public bool allowTab = false;

    void Start() {
        instance = this;
        targetPlayerPosition = ConstantManager.PLAYER_DEFAULT_POSITION_IN_WORLD;
        newTouch = new Vector2[20];
        lastSwipeTime = Time.time;

        var quarterOfScreenWidth = ConstantManager.CAMERA_SCREEN_WIDTH_IN_WORD_SPACE / 4;
        playerPositions = new Vector3[3];
        playerPositions[1] = ConstantManager.PLAYER_DEFAULT_POSITION_IN_WORLD;
        playerPositions[0] = new Vector3(playerPositions[1].x - quarterOfScreenWidth, playerPositions[1].y, playerPositions[1].z);
        playerPositions[2] = new Vector3(playerPositions[1].x + quarterOfScreenWidth, playerPositions[1].y, playerPositions[1].z);
        PlayerScript.instance.playerHolder.transform.position = playerPositions[currendPosIndex];
        thisScriptLoaded = true;
    }

    void Update() {
        //Show or Hide Swipe-Bar 
        if (allowSwipe && slideBarContainer.gameObject.activeSelf == false)
            slideBarContainer.gameObject.SetActive(true);
        else if (!allowSwipe && slideBarContainer.gameObject.activeSelf == true)
            slideBarContainer.gameObject.SetActive(false);
        //Check Inputs
        if (allowSwipe || allowTab) {
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    newTouch[touch.fingerId] = touch.position;
                } else if (touch.phase == TouchPhase.Ended) {
                    var differece = touch.position - newTouch[touch.fingerId];
                    var magnitude = differece.magnitude;
                    if (Mathf.Abs(differece.x) > Mathf.Abs(differece.y) && magnitude >= Screen.width / 4) {
                        //SWIPE
                        if (differece.x > 0 && lastSwipeTime + ConstantManager.PLAYER_MOVEMENT_SLIDE_TIME_COOLDOWN < Time.time && allowSwipe) {
                            //swipe to the right
                            if (currendPosIndex < 2) {
                                currendPosIndex++;
                                lastSwipeTime = Time.time;
                            }
                            targetPlayerPosition = playerPositions[currendPosIndex];
                        } else if (differece.x <= 0 && lastSwipeTime + ConstantManager.PLAYER_MOVEMENT_SLIDE_TIME_COOLDOWN < Time.time && allowSwipe) {
                            //swipe to the left
                            if (currendPosIndex > 0) {
                                lastSwipeTime = Time.time;
                                currendPosIndex--;
                            }
                            targetPlayerPosition = playerPositions[currendPosIndex];
                        }
                    } else if (allowTab) {
                        //NO SWIPE = TAP
                        ShootingController.instance.NewInput(touch.position);
                    }
                }
            }
            if (allowSwipe) {
                UpdateSlideBar();

                //Get free Space to left and right side
                var playerRadius = PlayerScript.instance.currentMass * ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE / 2;
                var freeSpaceLeft = ((ConstantManager.CAMERA_LOWER_LEFT_CORNER_IN_WORD_SPACE.x * -1) +
                    (playerHolder.transform.position.x - playerRadius)) * -1;
                var freeSpaceRight = ConstantManager.CAMERA_UPPER_RIGHT_CORNER_IN_WORLD_SPACE.x -
                    (playerHolder.transform.position.x + playerRadius);

                //Move Player
                Vector3 distance = targetPlayerPosition - playerHolder.transform.position;
                var distToMove = (distance * ConstantManager.PLAYER_MOVEMENT_SPEED) * Time.deltaTime;
                Vector3 newDistToMove = distToMove;

                //Check if Player would get out of Screen, and limit movement if so
                if (freeSpaceLeft > 0 && freeSpaceRight < 0) {
                    newDistToMove = distToMove;
                } else if (distToMove.x > freeSpaceRight) {
                    newDistToMove = new Vector3(freeSpaceRight, distToMove.y, distToMove.z);
                } else if (distToMove.x < freeSpaceLeft) {
                    newDistToMove = new Vector3(freeSpaceLeft, distToMove.y, distToMove.z);
                }

                //distToMove = distToMove.magnitude < ConstantManager.PLAYER_MOVEMENT_MIN_FAKTORT_TO_ABORT_MOVEMENT ? Vector3.zero : distToMove;
                //GetComponent<PlayerMeshGenerator>().ApplayMotionDistortion(distToMove);

                //Apply Movement
                playerHolder.transform.position += newDistToMove;
            }
        }
    }

    private void UpdateSlideBar() {
        var fullWidth = slideBarContainer.GetComponent<RectTransform>().rect.width;
        var timePassedInPercent = 100 * (Time.time - lastSwipeTime) / ConstantManager.PLAYER_MOVEMENT_SLIDE_TIME_COOLDOWN;
        timePassedInPercent = timePassedInPercent > 100 ? 100 : timePassedInPercent;
        var contentRectTransform = slideBarContent.GetComponent<RectTransform>();
        contentRectTransform.sizeDelta = new Vector2((fullWidth / 100 * timePassedInPercent), contentRectTransform.sizeDelta.y);
    }
}