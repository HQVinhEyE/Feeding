using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Vector3 ranDestination;
    private float enemyY = 1.5f;
    private float speed = 2f;
    public int rank = 1;
    private bool metcollider = false;
    private Vector3 metcolliderDestination;
    // Start is called before the first frame update

    void Start()
    {
        RandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (metcollider == false)
        {
            if (transform.position == ranDestination) RandomDestination();
            else transform.position = Vector3.MoveTowards(transform.position, ranDestination, Time.deltaTime * speed);
        }
        else if (metcollider == true)
        {
            if (transform.position != metcolliderDestination)
                transform.position = Vector3.MoveTowards(transform.position, metcolliderDestination, Time.deltaTime * speed * 2);
            else if (transform.position == metcolliderDestination)
                metcollider = false;
        }



    }
    private void RandomDestination()
    {
        float x = UnityEngine.Random.Range(-50, 50);
        float z = UnityEngine.Random.Range(-50, 50);
        ranDestination = new Vector3(x, enemyY, z);
    }
    private Vector3 Runaway(Vector3 colliderPos)
    {
        float xrange = transform.position.x - colliderPos.x;
        float zrange = transform.position.z - colliderPos.z;
        if (xrange < -50) xrange = -50;
        if (zrange < -50) zrange = -50;
        Vector3 Ranaway = new(transform.position.x + xrange, transform.position.y, transform.position.z + zrange);

        return Ranaway;
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.CompareTag("Booster") == true)
            {
                metcollider = true;
                int playerrank = other.GetComponent<PlayerBoost>().rank;
                if (playerrank >= rank)
                {
                    metcolliderDestination = Runaway(other.transform.position);
                }
                else
                {
                    metcolliderDestination = other.transform.position;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") == true)
        {
            int enemyrank = collision.collider.GetComponent<EnemyBehavior>().rank;
            if (enemyrank > rank)
            {
                EnemySpawner.Instance.EnemiesList.Add(this.gameObject);
                this.gameObject.SetActive(false);

            }

            else if (enemyrank < rank)
            {
                EnemySpawner.Instance.EnemiesList.Add(collision.collider.gameObject);
                collision.collider.gameObject.SetActive(false);
            }

        }
        else if (collision.collider.CompareTag("Wall") == true)
        {
            RandomDestination();
            metcolliderDestination = ranDestination;
        }
        else if (collision.collider.CompareTag("Player") == true)
        {
            int playerrank = PlayerController.instance.rank;
            if (rank <= playerrank)
            {
                Growbar.growbarinstance.deactedenemyrank = rank;
                EnemySpawner.Instance.EnemiesList.Add(this.gameObject);
                this.gameObject.SetActive(false);
            }
            else
            {
                PlayerController.instance.GameOverAnnounce();
            }
        }
    }
}
