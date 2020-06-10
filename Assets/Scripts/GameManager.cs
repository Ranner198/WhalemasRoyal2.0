using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numberOfPlayers;
    public int numberOfBots;
    public bool practice = false;

    public List<Vector3> Paths = new List<Vector3>();
    public List<Whaleburt> players = new List<Whaleburt>();

    public float terminalVelocity = 20;

    public Animator StartingGate;

    public static GameManager instance;

    //  UI/GUI
    public TextMeshProUGUI countDownText;
    public GameObject finishBanner;

    public Queue<Destroyable> Destroyables = new Queue<Destroyable>();

    public GameObject Whaleburt;

    public void Awake()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject newplayer = Instantiate(Whaleburt, Vector3.zero, Quaternion.identity);
            newplayer.transform.rotation = Quaternion.Euler(8.6f, 0, 0);
            newplayer.name = "Player_" + i;
            Whaleburt temp = newplayer.GetComponent<Whaleburt>();
            players.Add(temp);
            temp.hatBehavior.GenerateRandomHat();
        }
        for (int i = 0; i < numberOfBots; i++)
        {
            GameObject newplayer = Instantiate(Whaleburt, Vector3.zero, Quaternion.identity);
            newplayer.transform.rotation = Quaternion.Euler(8.6f, 0, 0);
            newplayer.name = "Player_" + i;
            Whaleburt temp = newplayer.GetComponent<Whaleburt>();
            players.Add(temp);
            temp.hatBehavior.GenerateRandomHat();
            temp.IsBot();
        }

        instance = this;
    }

    // Camera Logic
    public Vector3 GetLeader()
    {
        if (players.Count == 1 && !practice)
            Finish();

        Whaleburt whaleburt = players[0];
        
        foreach (Whaleburt player in players)
        {
            if (whaleburt == null && player != null)
                whaleburt = player;

            if (player != null && whaleburt != null && player.transform.position.z > whaleburt.transform.position.z)
                whaleburt = player;                    
        }

        return whaleburt.transform.position;
    }

    public void GetPositions()
    {
        List<Whaleburt> positions = new List<Whaleburt>(players.OrderByDescending(e => e.transform.position.z));

        for (int i = 0; i < positions.Count; i++)
        {
            positions[i].SetPosition(i+1);
        }
    }

    public void Finish()
    {
        finishBanner.SetActive(true);                
    }

    public void Start()
    {
        StartCoroutine(C_StartDelay());

        int i = 0;
        foreach (Whaleburt player in players)
        {
            player.rb.isKinematic = true;
            player.terminalVelocity = 0;
            player.Index = i;
            player.positions.Index = player.Index;
            Vector3 GatePosition = new Vector3(player.positions.XPos[player.Index], 3.667168f, -21.99012f);
            player.transform.position = GatePosition;
            i++;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("GameScene");

        GetPositions();
    }

    public void AddDestoryable(GameObject RaceTrack, List<GameObject> AdSigns, GameObject Object)
    {
        Destroyables.Enqueue(new Destroyable(RaceTrack, AdSigns, Object));
    }
    public void DestoryDestroyable()
    {
        Destroyable temp = Destroyables.Peek();
        Destroy(temp.RaceTrack, 3f);
        foreach (GameObject sign in temp.adSigns)
            Destroy(sign, 3f);
        Destroy(temp.Object, 3f);
        Destroyables.Dequeue();
    }

    #region Race Start Logic
    IEnumerator C_StartDelay()
    {
        while (WhalemasRoyalNetworkManager.instance.GetTimer() > 0)
        {
            float timer = WhalemasRoyalNetworkManager.instance.GetTimer();
            countDownText.text = Mathf.Round(timer + 1).ToString();
            countDownText.color = Color.Lerp(Color.red, Color.green, 1/ timer);
            yield return new WaitForEndOfFrame();
        }

        countDownText.text = "";

        StartingGate.SetTrigger("GateDrop");
        yield return new WaitForSeconds(.25f);
        StartRace();
    }

    public void StartRace()
    {
        players[0].anim.SetTrigger("Start");

        foreach (Whaleburt player in players)
        {
            player.terminalVelocity = terminalVelocity;
            player.rb.isKinematic = false;
        }
    }
    #endregion
}

public class Destroyable
{
    public GameObject RaceTrack;
    public List<GameObject> adSigns = new List<GameObject>();
    public GameObject Object;

    public Destroyable(GameObject RaceTrack, List<GameObject> adSigns, GameObject Object)
    {
        this.RaceTrack = RaceTrack;
        this.adSigns = new List<GameObject>(adSigns);
        this.Object = Object;
    }
}
