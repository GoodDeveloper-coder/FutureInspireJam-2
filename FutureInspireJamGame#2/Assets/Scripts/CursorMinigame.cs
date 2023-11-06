using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMinigame : Minigame
{
    [SerializeField] private Rigidbody2D cursor;

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

    private Rigidbody2D[] currentCircles;

    private Vector2[] circlesMoveFrom;
    private Vector2[] circlesMoveTo;

    private bool rowAttack;

    private int attackIndex;
    private float currentAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(Camera.main.transform);
        cursor.transform.SetParent(Camera.main.transform);
        cursor.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!playing || win || lose) return;
        cursor.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (intro)
        {
            currentAttackTime += Time.deltaTime;
            if (currentAttackTime >= introTime)
            {
                intro = false;
                Attack();
            }
            return;
        }
        currentAttackTime += Time.deltaTime;
        if (currentAttackTime < attackTime)
        {
            for (int i = 0; i < currentCircles.Length; i++)
            {
                currentCircles[i].MovePosition(Vector2.Lerp(circlesMoveFrom[i], circlesMoveTo[i], currentAttackTime / attackTime));
                if (!lose && Vector2.Distance(cursor.position, currentCircles[i].position) < (circleSize + 0.5f) / 2)
                {
                    cursor.gameObject.SetActive(false);
                    lose = true;
                }
            }
            if (lose)
            {
                foreach (Rigidbody2D c in currentCircles) Destroy(c.gameObject);
                currentCircles = null;
            }
        }
        else
        {
            if (attackIndex < attackCount) Attack();
            else
            {
                cursor.gameObject.SetActive(false);
                foreach (Rigidbody2D c in currentCircles) Destroy(c.gameObject);
                currentCircles = null;
                win = true;
            }
        }
    }
    
    public override void PlayMinigame(float focusLevel)
    {
        cursor.gameObject.SetActive(true);
        playing = true;
        win = false;
        lose = false;
        circlesInRow = (int)(maxCirclesInRow - (maxCirclesInRow - minCirclesInRow) * focusLevel);
        circlesInColumn = (int)(maxCirclesInColumn - (maxCirclesInColumn - minCirclesInColumn) * focusLevel);
        attackTime = minAttackTime + (maxAttackTime - minAttackTime) * focusLevel;
        attackCount = (int)(maxAttackCount - (maxAttackCount - minAttackCount) * focusLevel);
        intro = true;
        rowAttack = Random.Range(0, 2) == 0;
        attackIndex = 0;
    }

    private void Attack()
    {
        attackIndex++;
        currentAttackTime = 0;
        rowAttack = !rowAttack;
        int lines = rowAttack ? rows : columns;
        int circleCount = rowAttack ? circlesInColumn : circlesInRow;
        if (currentCircles != null)
        {
            foreach (Rigidbody2D c in currentCircles) Destroy(c.gameObject);
        }
        currentCircles = new Rigidbody2D[circleCount];
        circlesMoveFrom = new Vector2[circleCount];
        circlesMoveTo = new Vector2[circleCount];
        bool[] index = new bool[lines];
        bool startNegative = Random.Range(0, 2) == 0;
        for (int i = 0; i < circleCount; i++)
        {
            int line = Random.Range(0, lines);
            while (index[line]) line = (line + 1) % lines;
            index[line] = true;
            currentCircles[i] = Instantiate(circlePrefab, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            currentCircles[i].transform.localScale = (Vector3.right + Vector3.up) * circleSize + Vector3.forward;
            if (rowAttack)
            {
                float b = Mathf.Abs(boundaries.x) * (columns + 1f) / columns;
                circlesMoveFrom[i] = new Vector2(startNegative? -b : b, boundaries.y * (line * 2f + 1f - rows) / rows);
                circlesMoveTo[i] = new Vector2(startNegative ? b : -b, boundaries.y * (line * 2f + 1f - rows) / rows);
            }
            else
            {
                float b = Mathf.Abs(boundaries.y) * (rows + 1f) / rows;
                circlesMoveFrom[i] = new Vector2(boundaries.x * (line * 2f + 1f - columns) / columns, startNegative ? -b : b);
                circlesMoveTo[i] = new Vector2(boundaries.x * (line * 2f + 1f - columns) / columns, startNegative ? b : -b);
            }
            currentCircles[i].MovePosition(circlesMoveFrom[i]);
        }
    }
}
