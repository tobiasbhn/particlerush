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
    private Vector2[] touchPositions;
    private float[] touchStartTime;
    private Vector3[] playerPositions;
    private int currendPosIndex = 1;
    [HideInInspector] public float lastSwipeTime;
    private float lastTapTime;

    // BEHAVIOUR
    [HideInInspector] public bool allowSwipe = false;
    [HideInInspector] public bool allowTab = false;
    [HideInInspector] public bool forceCenterPosition = false;
    [HideInInspector] public float shootItemEffect = 0f;

    void Awake() {
        instance = this;
    }

    void Start() {
        targetPlayerPosition = ConstantManager.PLAYER_DEFAULT_POSITION_IN_WORLD;
        touchPositions = new Vector2[20];
        touchStartTime = new float[20];
        lastSwipeTime = Time.time;
        lastTapTime = Time.realtimeSinceStartup;

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
                if (touch.fingerId >= touchPositions.Length)
                    return;

                if (touch.phase == TouchPhase.Began) {
                    touchPositions[touch.fingerId] = touch.position;
                    touchStartTime[touch.fingerId] = Time.realtimeSinceStartup;
                } else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                    if (shootItemEffect != 0f) {
                        TabAndShoot(touch.position, true);
                    }
                } else if (touch.phase == TouchPhase.Ended) {
                    var duration = Time.realtimeSinceStartup - touchStartTime[touch.fingerId];
                    if (duration < ConstantManager.INPUT_BREAKPOINT_TAP_SWIPE) {
                        // Defenitely a Tab
                        TabAndShoot(touch.position, false);
                    } else if (duration <= ConstantManager.INPUT_BREAKPOINT_SHOOT_SWIPE) {
                        // Depends on SwipeAngle and Magnitude
                        var differece = touch.position - touchPositions[touch.fingerId];
                        var magnitude = differece.magnitude;
                        if (Mathf.Abs(differece.x) > Mathf.Abs(differece.y) && magnitude >= Screen.width / 4) {
                            // Input Movement Long enouth && Angle more Horizontally than Vertically
                            RuntimeDataManager.value.inputSwipeCount++;
                            //SWIPE
                            if (differece.x > 0 && lastSwipeTime + ItemPool.instance.swipeItemDefinition.getCurrendEffect() < Time.time && allowSwipe) {
                                //swipe to the right
                                if (currendPosIndex < 2) {
                                    currendPosIndex++;
                                    lastSwipeTime = Time.time;
                                    targetPlayerPosition = playerPositions[currendPosIndex];
                                }
                            } else if (differece.x <= 0 && lastSwipeTime + ItemPool.instance.swipeItemDefinition.getCurrendEffect() < Time.time && allowSwipe) {
                                //swipe to the left
                                if (currendPosIndex > 0) {
                                    lastSwipeTime = Time.time;
                                    currendPosIndex--;
                                    targetPlayerPosition = playerPositions[currendPosIndex];
                                }
                            }
                        } else {
                            TabAndShoot(touch.position, false);
                        }
                    } else {
                        TabAndShoot(touch.position, false);
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

    private void TabAndShoot(Vector2 position, bool auto) {
        if (allowTab && ((shootItemEffect != 0f && auto) || !auto)) {
            var delay = shootItemEffect == 1f ? ConstantManager.INPUT_SHOOT_ITEM_TIER_1_DELAY : ConstantManager.INPUT_SHOOT_ITEM_TIER_2_DELAY;
            if ((Time.realtimeSinceStartup - lastTapTime >= delay && auto) || !auto) {
                RuntimeDataManager.value.inputTabCount++;
                ShootingController.instance.NewInput(position);
                lastTapTime = Time.realtimeSinceStartup;
            }
        }
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
        var timePassedInPercent = 100 * (Time.time - lastSwipeTime) / ItemPool.instance.swipeItemDefinition.getCurrendEffect();
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