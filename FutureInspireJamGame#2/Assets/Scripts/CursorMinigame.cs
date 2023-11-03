using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMinigame : Minigame
{
    [SerializeField] private GameObject cursor;

    [SerializeField] private GameObject circlePrefab;

    [SerializeField] private Vector2 boundaries;

    [SerializeField] private int rows;
    [SerializeField] private int columns;

    [SerializeField] private float circleSize;
    
    [SerializeField] private int minCirclesInRow;
    [SerializeField] private int maxCirclesInRow;
    [SerializeField] private int minCirclesInColumn;
    [SerializeField] private int maxCirclesInColumn;
    [SerializeField] private float minAttackTime;
    [SerializeField] private float maxAttackTime;
    [SerializeField] private int minAttackCount;
    [SerializeField] private int maxAttackCount;
    
    private int circlesInRow;
    private int circlesInColumn;
    private float attackTime;
    private int attackCount;

    private Rigidbody2D cursorRB;

    private Rigidbody2D[] currentCircles;

    private Vector2[] circlesMoveFrom;
    private Vector2[] circlesMoveTo;

    private bool rowAttack;

    private int attackIndex;
    private float currentAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        cursorRB = cursor.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!playing || win || lose) return;
        if (intro)
        {
            currentAttackTime += Time.deltaTime;
            if (currentAttackTime < introTime)
            {
                intro = false;
                Attack();
            }
            return;
        }
        cursorRB.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        currentAttackTime += Time.deltaTime;
        if (currentAttackTime < attackTime)
        {
            for (int i = 0; i < currentCircles.Length; i++)
            {
                currentCircles[i].MovePosition(Vector2.Lerp(circlesMoveFrom[i], circlesMoveTo[i], currentAttackTime / attackTime));
                if (Vector2.Distance(cursorRB.position, currentCircles[i].position) < 1.5f) lose = true;
            }
        }
        else
        {
            if (attackIndex < attackCount) Attack();
            else win = true;
        }
    }
    
    public override void PlayMinigame(float focusLevel)
    {
        playing = true;
        circlesInRow = (int)(maxCirclesInRow - (maxCirclesInRow - minCirclesInRow) * focusLevel);
        circlesInColumn = (int)(maxCirclesInColumn - (maxCirclesInColumn - minCirclesInColumn) * focusLevel);
        attackTime = maxAttackTime - (maxAttackTime - minAttackTime) * focusLevel;
        attackCount = (int)(maxAttackCount - (maxAttackCount - minAttackCount) * focusLevel);
        intro = true;
        rowAttack = Random.Range(0, 2) == 0;
        attackIndex = 0;
    }

    private void Attack()
    {
        attackIndex++;
        attackTime = 0;
        rowAttack = !rowAttack;
        int lines = rowAttack ? rows : columns;
        int circleCount = rowAttack ? circlesInColumn : circlesInRow;
        currentCircles = new Rigidbody2D[circleCount];
        circlesMoveFrom = new Vector2[circleCount];
        circlesMoveTo = new Vector2[circleCount];
        bool[] index = new bool[lines];
        for (int i = 0; i < circleCount; i++)
        {
            int line;
            do line = Random.Range(0, lines);
            while (index[line]);
            index[line] = true;
            currentCircles[i] = Instantiate(circlePrefab, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            bool startNegative = Random.Range(0, 1) == 0;
            if (rowAttack)
            {
                circlesMoveFrom[i] = new Vector2(startNegative? -boundaries.x - 0.5f : boundaries.x + 0.5f, boundaries.y * (line + 0.5f - rows / 2f) / rows);
                circlesMoveTo[i] = new Vector2(startNegative ? boundaries.x : -boundaries.x - 0.5f, boundaries.y * (line + 0.5f - rows / 2f) / rows);
            }
            else
            {
                circlesMoveFrom[i] = new Vector2(boundaries.x * (line + 0.5f - columns / 2f) / columns, startNegative ? -boundaries.y - 0.5f : boundaries.y);
                circlesMoveTo[i] = new Vector2(boundaries.x * (line + 0.5f - columns / 2f) / columns, startNegative ? boundaries.y : -boundaries.y - 0.5f);
            }
            currentCircles[i].MovePosition(circlesMoveFrom[i]);
        }
    }
}
