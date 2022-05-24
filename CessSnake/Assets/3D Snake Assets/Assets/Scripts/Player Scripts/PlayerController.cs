using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerDirection direction;
    [HideInInspector]
    public float step_length = 0.2f;
    [HideInInspector]
    public float movement_Frequency = 0.1f;

    private float counter;
    private bool move;
    [SerializeField]
    private GameObject tailPrefab;

    private List<Vector3> delta_Position;

    private List<Rigidbody> nodes;

    private Rigidbody main_Body;
    private Rigidbody head_Body;
    private Transform tr;

    private bool create_Node_At_Tail;

    public Text coinDisplayText;
    public Text coinDisplayText1;
    public int currentCoins = 0;

    public Text SnakePartDisplayText;
    public Text SnakePartDisplayText1;
    public int snakePartCount = 0;

    public GameObject fruit_PickUp, bomb_Pickup, rare_drop;

    private float min_X = -4.15f, max_X = 4.15f, min_y = -2.46f, max_Y = 2.16f;
    private float z_Pos = 5.8f;

    public static PlayerController Instance;


    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
        tr = transform;
        main_Body = GetComponent<Rigidbody>();

        InitSnakeNodes();
        InitPlayer();

        delta_Position = new List<Vector3>()
        {
            new Vector3(-step_length, 0f), // -x ... LEFT
            new Vector3(0f, step_length), // y ... UP
            new Vector3(step_length, 0f), // x ... RIGHT
            new Vector3(0f, -step_length),// -y ... DOWN

        };

    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementFrequency();
    }

     void FixedUpdate()
    {
        if (move)
        {
            move = false;
            Move();
        }
    }
    void InitSnakeNodes()
    {
        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());
        

        head_Body = nodes[0];
    }
   
    void InitPlayer()
    {
       

        switch (direction)
        {
            case PlayerDirection.RIGHT:
                
                
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);
              
                break;

            case PlayerDirection.LEFT:
                

                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                
                break;

            case PlayerDirection.UP:
               

                nodes[1].position = nodes[0].position - new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position - new Vector3(0f, Metrics.NODE * 2f, 0f);
                
                break;

            case PlayerDirection.DOWN:

                nodes[1].position = nodes[0].position + new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position + new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;

        }

    }

    void Move()
    {
        Vector3 dPosition = delta_Position[(int)direction];

        Vector3 parentPos = head_Body.position;
        Vector3 prevPosition;

        main_Body.position = main_Body.position + dPosition;
        head_Body.position = head_Body.position + dPosition;

        for(int i = 1; i< nodes.Count; i++)
        {

            prevPosition = nodes[i].position;

            nodes[i].position = parentPos;
            parentPos = prevPosition;

        }

        //Check if we need to create a new node because we ate a fruit

        if (create_Node_At_Tail)
        {
            create_Node_At_Tail = false;

            GameObject newNode = Instantiate(tailPrefab, nodes[nodes.Count - 1].position,
                Quaternion.identity);

            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());
           
        }
    }
    void CheckMovementFrequency()
    {
        counter += Time.deltaTime;
        if(counter >= movement_Frequency)
        {
            counter = 0f;
            move = true;
        }

    }

    public void SetInputDirection(PlayerDirection dir)
    {
        if(dir == PlayerDirection.UP && direction == PlayerDirection.DOWN || 
            dir == PlayerDirection.DOWN && direction == PlayerDirection.UP ||
            dir == PlayerDirection.RIGHT && direction == PlayerDirection.LEFT ||
            dir == PlayerDirection.LEFT && direction == PlayerDirection.RIGHT)
        {
            return;
        }

        direction = dir;

        ForceMove();
    }

    void ForceMove()
    {
        counter = 0;
        move = false;
        Move();

    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {

            currentCoins = PlayerPrefs.GetInt("Coin");

        }

        coinDisplayText.text = "Coins:" + currentCoins;

        if (PlayerPrefs.HasKey("Snake Parts"))
        {
            snakePartCount = PlayerPrefs.GetInt("Snake Parts");
        }

        SnakePartDisplayText.text = "Snake Parts: " + snakePartCount;
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnBombs());
    }

    void MakeInstance()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    IEnumerator SpawnBombs()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        if (Random.Range(0, 10) >= 7)
        {
            Instantiate(bomb_Pickup, new Vector3(Random.Range(min_X, max_X),
                                                 Random.Range(min_y, max_Y),
                                                   z_Pos), Quaternion.identity);
        }

        Invoke("StartSpawning", 0f);
    }

    public void OnTriggerEnter(Collider target)
    {

          

        if(Random.Range(0, 10000) != 666)
        {
            Instantiate(fruit_PickUp,
              new Vector3(Random.Range(min_X, max_X),
              Random.Range(min_y, max_Y), z_Pos),
              Quaternion.identity);
        }

        else
        {
            Instantiate(rare_drop,
              new Vector3(Random.Range(min_X, max_Y),
              Random.Range(min_y, max_Y), z_Pos),
              Quaternion.identity);
        }


        if (target.tag == Tags.FRUIT)
        {
            target.gameObject.SetActive(false);

            create_Node_At_Tail = true;

            currentCoins += 1;
            PlayerPrefs.SetInt("Coin", currentCoins);
            coinDisplayText.text = "Coins:" + currentCoins;
            coinDisplayText1.text = "Coins:" + currentCoins;

            AudioManager.Instance.Play_PickupSound();


        }
         if (target.tag == Tags.WALL || target.tag == Tags.BOMB || target.tag == Tags.TAIL)
        {

            SceneManager.LoadScene(2);

        }

         if(target.tag == Tags.DROP)
        {
            target.gameObject.SetActive(false);
            snakePartCount += 1;
            PlayerPrefs.SetInt("Snake Parts", snakePartCount);
            SnakePartDisplayText.text = "Snake Parts:" + snakePartCount;
           SnakePartDisplayText1.text = "Snake Parts:" + snakePartCount;
            Debug.Log(snakePartCount);
        }
        StartSpawning();
    }


}

       


       
        
    

 // class





















