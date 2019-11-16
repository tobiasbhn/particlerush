using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {
    //INSTANCE
    [HideInInspector] public static PlayerMovementScript instance;

    //OBJECT LINKS
    public GameObject playerHolder;

    //MOVEMENT RELATED
    private Vector3 targetPlayerPosition;
    private Vector2[] newTouch;
    private Vector3[] playerPositions;
    private int currendPosIndex = 1;
    [HideInInspector] public float lastSwipeTime;

    // BEHAVIOUR
    [HideInInspector] public bool allowSwipe = false;
    [HideInInspector] public bool allowTab = false;
    [HideInInspector] public bool forceCenterPosition = false;

    void Awake() {
        instance = this;
    }

    void Start() {
        targetPlayerPosition = ConstantManager.PLAYER_DEFAULT_POSITION_IN_WORLD;
        newTouch = new Vector2[20];
        lastSwipeTime = Time.time;

        var quarterOfScreenWidth = ConstantManager.CAMERA_SCREEN_WIDTH_IN_WORD_SPACE / 4;
        playerPositions = new Vector3[3];
        playerPositions[1] = ConstantManager.PLAYER_DEFAULT_POSITION_IN_WORLD;
        playerPositions[0] = new Vector3(playerPositions[1].x - quarterOfScreenWidth, playerPositions[1].y, playerPositions[1].z);
        playerPositions[2] = new Vector3(playerPositions[1].x + quarterOfScreenWidth, playerPositions[1].y, playerPositions[1].z);
        playerHolder.transform.position = playerPositions[currendPosIndex];
    }

    void Update() {
        UpdateSlideBar();
        // Check Inputs and Update Position Regulary
        if (allowSwipe || allowTab) {
            foreach (Touch touch in Input.touches) {
                if (touch.fingerId >= newTouch.Length)
                    return;

                if (touch.phase == TouchPhase.Began) {
                    newTouch[touch.fingerId] = touch.position;
                } else if (touch.phase == TouchPhase.Ended) {
                    var differece = touch.position - newTouch[touch.fingerId];
                    var magnitude = differece.magnitude;
                    if (Mathf.Abs(differece.x) > Mathf.Abs(differece.y) && magnitude >= Screen.width / 4) {
                        RuntimeDataManager.value.inputSwipeCount++;
                        //SWIPE
                        if (differece.x > 0 && lastSwipeTime + ConstantManager.PLAYER_MOVEMENT_SLIDE_TIME_COOLDOWN < Time.time && allowSwipe) {
                            //swipe to the right
                            if (currendPosIndex < 2) {
                                currendPosIndex++;
                                lastSwipeTime = Time.time;
                                targetPlayerPosition = playerPositions[currendPosIndex];
                            }
                        } else if (differece.x <= 0 && lastSwipeTime + ConstantManager.PLAYER_MOVEMENT_SLIDE_TIME_COOLDOWN < Time.time && allowSwipe) {
                            //swipe to the left
                            if (currendPosIndex > 0) {
                                lastSwipeTime = Time.time;
                                currendPosIndex--;
                                targetPlayerPosition = playerPositions[currendPosIndex];
                            }
                        }
                    } else if (allowTab) {
                        //NO SWIPE = TAP
                        RuntimeDataManager.value.inputTabCount++;
                        ShootingController.instance.NewInput(touch.position);
                    }
                }
            }
        }
        
        // Update Position if forced
        if (forceCenterPosition) {
            currendPosIndex = 1;
            targetPlayerPosition = playerPositions[currendPosIndex];
            forceCenterPosition = false;
        }

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

        //Apply Movement
        playerHolder.transform.position += newDistToMove;

    }

    public void DenyInput(bool denyInput) {
        StartCoroutine(DenyInputHelper(denyInput));
    }
    private IEnumerator DenyInputHelper(bool denyInput) {
        yield return null;
        if (denyInput) {
            allowSwipe = false;
            allowTab = false;
        } else {
            allowSwipe = true;
            allowTab = true;
        }
    }

    private void UpdateSlideBar() {        
        var timePassedInPercent = 100 * (Time.time - lastSwipeTime) / ConstantManager.PLAYER_MOVEMENT_SLIDE_TIME_COOLDOWN;
        if (allowSwipe && timePassedInPercent <= 100) {
            // Set Active if it should be activd but isnt
            if (UiObjectReferrer.instance.ingameSlideContainer.gameObject.activeSelf == false)
                UiObjectReferrer.instance.ingameSlideContainer.gameObject.SetActive(true);
            var fullWidth = UiObjectReferrer.instance.ingameSlideContainer.rect.width;
            // Calculate and set width of Bar
            timePassedInPercent = timePassedInPercent > 100 ? 100 : timePassedInPercent;
            var contentRectTransform = UiObjectReferrer.instance.ingameSlideContent;
            contentRectTransform.sizeDelta = new Vector2((fullWidth / 100 * timePassedInPercent), contentRectTransform.sizeDelta.y);
            // Position Bar to Player
            var yOffset = (PlayerScript.instance.currentMass * ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE * 0.5) - 0.5;
            var pos = Camera.main.WorldToScreenPoint(playerHolder.transform.position + new Vector3(0, (float)yOffset, 0));
            UiObjectReferrer.instance.ingameSlideContainer.transform.position = pos;
        } else if (UiObjectReferrer.instance.ingameSlideContainer.gameObject.activeSelf == true) {
            UiObjectReferrer.instance.ingameSlideContainer.gameObject.SetActive(false);
        }
    }
}