using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelPattern : MonoBehaviour
{
    [Header("Grounds")]
    [SerializeField] private GameObject[] grounds;
    [Range(0, 100)] [SerializeField] private int chanceGroundNormal;

    [Header("Bricks")]
    [SerializeField] private GameObject brick;
    [SerializeField] private Sprite[] brickSprites;
    [Range(0, 100)] [SerializeField] private int chanceXBrickLargeOrSmallGap;
    [Range(0, 100)] [SerializeField] private int chanceYBricksStayOrChange;
    [Range(0, 100)] [SerializeField] private int chanceYBricksChangeBrickNumbers;

    [Header("Items")]
    [SerializeField] private GameObject itemPointsHigh;
    [SerializeField] private GameObject itemPointsLow;
    [SerializeField] private GameObject itemSpeed;
    [SerializeField] private GameObject itemBomb;
    [SerializeField] private float itemDistanceToBrick;

    [Range(0, 100)] [SerializeField] private int itemFrequency;
    [Range(0, 100)] [SerializeField] private int chanceItemPointsOrSpeedBomb;
    [Range(0, 100)] [SerializeField] private int chanceItemLowOrHighPoints;
    [Range(0, 100)] [SerializeField] private int chanceSpeedOrBomb;

    
    private MainGame mainGame;

    [Header("private ground vars")]
    private List<GameObject> groundCopys = new List<GameObject>();
    private int currentGround;
    [HideInInspector] public float firstGroundYPos;

    [Header("private brick vars")]
    private SpriteRenderer brickSR;
    private List<GameObject> brickCopys = new List<GameObject>();
    public float GetBrickHeight() { return brick.GetComponent<BoxCollider2D>().size.y; }

    private BrickStateMachine.ProcessState processState = BrickStateMachine.ProcessState.StateOne06;
    private BrickStateMachine brickStateMachine = new BrickStateMachine(0, BrickStateMachine.ProcessState.StateOne06);
    private List<BrickStateMachine.ProcessState> stateList = new List<BrickStateMachine.ProcessState>();

    private List<float> actualYPosBricks = new List<float>();
    private const int NOYPOS = 8;
    private const int COMMANDSTAY = 0;
    private float yPos1, yPos2, yPos3;

    private float startXPosBricks;
    private float minXDistanceBetweenBricks;

    private bool waitForLargerXPosBetweenBricks = false;

    [Header("private item vars")]
    [HideInInspector] public List<GameObject> itemsList = new List<GameObject>();
    

    private void Awake()
    {
        mainGame = GetComponent<MainGame>();
        brickSR = brick.GetComponent<SpriteRenderer>();

        firstGroundYPos = grounds[0].GetComponent<PolygonCollider2D>().points[grounds[0].GetComponent<PolygonCollider2D>().points.Length - 1].y;
    }

    public void MyStart()
    {
        InitializeLists();

        FirstBrick();
        FirstTwoGrounds();
    }

    private void FixedUpdate()
    {
        if (mainGame.gameRunning)
        {
            if (groundCopys.Count >= 1)
            {
                Ground();
            }

            if (brickCopys.Count >= 1)
            {
                BrickPattern();
            }

            if (brickCopys.Count > 0 && brickCopys[0].transform.position.x <= -startXPosBricks - minXDistanceBetweenBricks)
            {
                DestroyBrick();
            }

            if (itemsList.Count > 0)
            {
                if (itemsList[0].transform.position.x <= -startXPosBricks)
                {
                    DestroyItem();
                }
            }
        }
    }

    private void InitializeLists()
    {
        groundCopys.Clear();
        brickCopys.Clear();
        itemsList.Clear();

        actualYPosBricks.Clear();

        stateList.Clear();
    }

    public void DestroyAllLevelObjects()
    {
        if (groundCopys.Count > 0)
        {
            for (int i = 0; i < groundCopys.Count; i++)
                Destroy(groundCopys[i]);
            groundCopys.Clear();
        }

        if (brickCopys.Count > 0)
        {
            for (int i = 0; i < brickCopys.Count; i++)
                Destroy(brickCopys[i]);
            brickCopys.Clear();
        }

        if (itemsList.Count > 0)
        {
            for (int i = 0; i < itemsList.Count; i++)
                Destroy(itemsList[i]);
            itemsList.Clear();
        }
    }
    
    #region GroundPattern
    private void FirstTwoGrounds()
    {
        GameObject copy = Instantiate(grounds[0], Vector3.zero, Quaternion.identity);
        groundCopys.Add(copy);

        Vector3 pos = new Vector3(mainGame.GetGroundSpawningPoint(), 0f, 0f);
        copy = Instantiate(grounds[0], pos, Quaternion.identity);
        groundCopys.Add(copy);
    }
    private void Ground()
    {
        for (int i = 0; i < groundCopys.Count; i++)
        {
            if (groundCopys[groundCopys.Count - 1].transform.position.x <= 0f)
            {
                NewGround();
            }

            if (groundCopys[0].transform.position.x <= -mainGame.GetGroundSpawningPoint())
            {
                DestroyGround();
            }
        }
    }
    private void NewGround()
    {
        GameObject copy = null;

        int randomGround = Random.Range(0, grounds.Length);
        int chanceGroundPick = Random.Range(0, 101);
        if (chanceGroundPick <= chanceGroundNormal)
        {
            randomGround = 0;
        }
        else
        {
            randomGround = Random.Range(1, grounds.Length);
        }

        currentGround = randomGround;

        if(stateList.Count == 0)
        {
            brickStateMachine.ChangeDictionary(currentGround, BrickStateMachine.ProcessState.StateOne06);   //StateOne06 = ein Brick auf unterster Stufe
        }
        else
        {
            brickStateMachine.ChangeDictionary(currentGround, stateList[stateList.Count - 1]);
        }

        Vector3 pos = new Vector3(mainGame.GetGroundSpawningPoint(), 0f, 0f);
        copy = Instantiate(grounds[currentGround], pos, Quaternion.identity);
        
        groundCopys.Add(copy);
    }
    private void DestroyGround()
    {
        Destroy(groundCopys[0], 0f);
        groundCopys.RemoveAt(0);
    }
    #endregion

    #region BrickPattern
    private void FirstBrick()
    {
        minXDistanceBetweenBricks = brick.GetComponent<BoxCollider2D>().size.x + 0.1f;
        startXPosBricks = mainGame.GetCameraBorderHorizontal() + minXDistanceBetweenBricks;

        float xPos = startXPosBricks;
        float yPos = brickStateMachine.possibleYPos[brickStateMachine.possibleYPos.Count - 1];

        actualYPosBricks.Add(yPos);
        NewBrick(xPos, yPos);
    }

    private void BrickPattern()
    {
        float lastXPos = brickCopys[brickCopys.Count - 1].transform.position.x;
        float distanceXPos = startXPosBricks - lastXPos;

        if (distanceXPos < minXDistanceBetweenBricks)                                                       //if wait till min distance between stones
            return;

        if (brickCopys.Count == 1)                                                                          //second Stone
        {
            BrickPos(distanceXPos, brickStateMachine.possibleYPos[brickStateMachine.possibleYPos.Count - 2], NOYPOS, NOYPOS);
            brickStateMachine.ChangeDictionary(currentGround, BrickStateMachine.ProcessState.StateOne05);   //manually update stateMachine
            return;
        }

        ChanceLargeOrSmallBrickGap(lastXPos, distanceXPos);
    }
    private void ChanceLargeOrSmallBrickGap(float lastXPos, float distanceXPos)
    {
        float secondLastStoneYPosIndex = actualYPosBricks[actualYPosBricks.Count - 2];
        float lastStoneYPosIndex = actualYPosBricks[actualYPosBricks.Count - 1];

        float secondLastXPos = brickCopys[brickCopys.Count - 2].transform.position.x;
        float distance = lastXPos - secondLastXPos;

        int chanceXPick = Random.Range(0, 100);
        int chanceX = chanceXBrickLargeOrSmallGap;

        if (distance <= minXDistanceBetweenBricks)
            chanceX = chanceXBrickLargeOrSmallGap;
        else
            chanceX = 100 - chanceXBrickLargeOrSmallGap;

        if (chanceXPick >= chanceX && !waitForLargerXPosBetweenBricks)
        {
            if (distanceXPos >= minXDistanceBetweenBricks)
            {
                CallBrickState(out yPos1, out yPos2, out yPos3);
                BrickPos(distanceXPos, yPos1, yPos2, yPos3);
            }
        }
        else
        {
            waitForLargerXPosBetweenBricks = true;
            if (distanceXPos >= 2 * minXDistanceBetweenBricks)
            {
                CallBrickState(out yPos1, out yPos2, out yPos3);

                BrickPos(distanceXPos, yPos1, yPos2, yPos3);
                waitForLargerXPosBetweenBricks = false;
            }
        }
    }
    private void CallBrickState(out float yPos1, out float yPos2, out float yPos3)
    {
        float[] yPos = new float[3];
        processState = BrickStateMachine.ProcessState.StateOne06;
        BrickStateMachine.Command randomCommand;

        bool isInvalidState;
        do
        {
            isInvalidState = false;

            int randomCommandNumber = 0;

            int chanceYBricks = Random.Range(0, 101);
            if (chanceYBricks < chanceYBricksStayOrChange)
                randomCommandNumber = COMMANDSTAY;
            else
            {
                chanceYBricks = Random.Range(0, 101);

                if (chanceYBricks <= chanceYBricksChangeBrickNumbers)
                    randomCommandNumber = Random.Range(15, 43);
                else
                    randomCommandNumber = Random.Range(1, 15);
            }
            
            if (stateList.Count >= 3f)
                if (stateList[stateList.Count - 1] == stateList[stateList.Count - 2])
                    randomCommandNumber = Random.Range(1, 43);

            randomCommand = (BrickStateMachine.Command)randomCommandNumber;

            try
            {
                processState = brickStateMachine.MoveNext(randomCommand);
            }
            catch (System.Exception ex)
            {
                string exeptionMessage = ex.Message;
                isInvalidState = true;
            }
        }
        while (isInvalidState);

        yPos = brickStateMachine.GetYPosFromState(processState);

        yPos1 = yPos[0];
        yPos2 = yPos[1];
        yPos3 = yPos[2];
    }
    private void BrickPos(float distanceXPos, float yPos1, float yPos2, float yPos3)
    {
        float xPos = startXPosBricks;

        if (yPos1 != NOYPOS)
            NewBrick(xPos, yPos1);
        if (yPos2 != NOYPOS)
            NewBrick(xPos, yPos2);
        if (yPos3 != NOYPOS)
            NewBrick(xPos, yPos3);

        stateList.Add(processState);
    }

    private void NewBrick(float xPos, float yPos)
    {
        Vector3 pos = new Vector3(xPos, yPos, 0f);

        GameObject copy = Instantiate(brick, pos, Quaternion.identity);
        brickCopys.Add(copy);
        SpriteRenderer copySR = copy.GetComponent<SpriteRenderer>();

        copy.SetActive(true);

        int randomSprite = Random.Range(0, 4);
        Sprite actualSprite = brickSprites[randomSprite];
        copySR.sprite = actualSprite;

        actualYPosBricks.Add(yPos);

        ItemPattern(xPos, yPos);
    }
    private void DestroyBrick()
    {
        actualYPosBricks.RemoveAt(0);

        if(stateList.Count > 0)
            stateList.RemoveAt(0);

        Destroy(brickCopys[0], 0f);
        brickCopys.RemoveAt(0);
    }
    #endregion

    #region ItemPattern
    private void ItemPattern(float xPos, float yPos)
    {
        Vector2 pos = new Vector2(xPos, yPos + itemDistanceToBrick);

        int itemFrequencyChance = Random.Range(0, 101);

        if (itemFrequencyChance < itemFrequency)
        {
            int pointsOrOtherChancePick = Random.Range(0, 101);

            if (pointsOrOtherChancePick <= chanceItemPointsOrSpeedBomb)
            {
                int lowOrHighPointsChancePick = Random.Range(0, 101);
                GameObject copy = null;

                if (lowOrHighPointsChancePick <= chanceItemLowOrHighPoints)
                    copy = Instantiate(itemPointsLow, pos, Quaternion.identity);
                else
                    copy = Instantiate(itemPointsHigh, pos, Quaternion.identity);

                itemsList.Add(copy);
            }
            else
            {
                int speedOrBombChancePick = Random.Range(0, 101);

                if (speedOrBombChancePick <= chanceSpeedOrBomb)
                {
                    GameObject copy = Instantiate(itemSpeed, pos, Quaternion.identity);
                    itemsList.Add(copy);
                }
                else
                {
                    GameObject copy = Instantiate(itemBomb, pos, Quaternion.identity);
                    itemsList.Add(copy);
                }
            }
        }
    }

    private void DestroyItem()
    {
        Destroy(itemsList[0]);
        itemsList.RemoveAt(0);
    }
    #endregion
}