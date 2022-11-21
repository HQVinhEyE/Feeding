using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 expectedPos;
    private float zoomspeed =10f;
    [SerializeField] private float zoomheight = 5f;
    [SerializeField] private float zoomdepth = -5f;
    // Start is called before the first frame update
    void Start()
    {
        expectedPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != expectedPos)
            transform.position = Vector3.MoveTowards(transform.position, expectedPos, Time.deltaTime * zoomspeed);
    }
    public void ZoomView()
    {
        expectedPos = new(expectedPos.x, expectedPos.y + zoomheight, expectedPos.z + zoomdepth);
    }
}
