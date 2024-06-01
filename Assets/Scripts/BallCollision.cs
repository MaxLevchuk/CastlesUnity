using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameObject DestroyedWallPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            DestroyWall(collision.gameObject);
        }
    }

    private void DestroyWall(GameObject wall)
    {
        Instantiate(DestroyedWallPrefab, wall.transform.position, wall.transform.rotation);
        Destroy(wall);
    }
}
