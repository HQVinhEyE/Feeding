using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Camera cam;
    float verticalcontrol;
    float horizontalcontrol;
    [SerializeField] GameObject boostcollider;
    private float movingspeed = 4f;
    public Growbar growbar;
    [SerializeField] private int growpoint = 0;
    [SerializeField] private int pointpergrow = 1;
    [SerializeField] private int maxgrowpoint = 100;
    bool grow = false;
    public int rank { get; private set; }
    private Vector3 mousePos;


    #region s
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion


    void Start()
    {
        cam = Camera.main;
        rank = 1;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z);
        //keyboard
        verticalcontrol = Input.GetAxis("Vertical");
        horizontalcontrol = Input.GetAxis("Horizontal");
        float leftright = transform.position.x + horizontalcontrol * Time.deltaTime * movingspeed;
        float updown = transform.position.z + verticalcontrol * Time.deltaTime * movingspeed;
        if (leftright <= 50 && leftright >= -50 && updown >= -50 && updown <= 50)
        {
            transform.position = new Vector3(leftright, transform.position.y, updown);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boostcollider.SetActive(true);
            movingspeed = movingspeed * 2;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            boostcollider.SetActive(false);
            movingspeed = movingspeed / 2;
        }
        //mouse
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //if(Physics.Raycast(ray, out var hit))
        //{
        //    mousePos =new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //    if(mousePos.x<= 50 && mousePos.x>= -50 && mousePos.z <= 50 && mousePos.z >=-50)
        //    transform.position= Vector3.MoveTowards(transform.position, mousePos, Time.deltaTime* movingspeed);  
        //}        
    }
    
    public void Zooming()
    {
        transform.localScale += new Vector3(4f, 4f, 4f);
        transform.position += new Vector3(0, 2f, 0);
        cam.GetComponent<CameraController>().ZoomView();
        rank++;
    }

    public void GameOverAnnounce()
    {
        Debug.Log("GameOver");
    }
}
