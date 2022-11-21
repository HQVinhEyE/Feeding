using UnityEngine;

public class PlayerBoost : MonoBehaviour
{

    private bool boosting = false;
    public int rank { get; private set; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rank = PlayerController.instance.rank;
        transform.position = PlayerController.instance.transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boosting = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            boosting = false;
        }
    }
}
