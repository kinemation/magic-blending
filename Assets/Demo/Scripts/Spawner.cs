using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private int width = 1;
    [SerializeField] private int length = 1;
    [SerializeField] private float step = 1f;
    
    private void Start()
    {
        Vector3 position = transform.position;

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var instance = Instantiate(prefab, position, Quaternion.identity);
                instance.transform.parent = transform;
                position.x += step;
            }

            position.x = transform.position.x;
            position.z += step;
        }
    }
}
