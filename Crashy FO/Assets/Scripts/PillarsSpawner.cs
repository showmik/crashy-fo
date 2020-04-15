using UnityEngine;

public class PillarsSpawner : MonoBehaviour
{
    public float maxTime = 1f;
    private float timeCounter = 0f;
    public float maxHeight = 1f;
    public GameObject pillars;

    void Start()
    {
        
    }

    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter > maxTime)
        {
            GameObject currentPillars = Instantiate(pillars);
            currentPillars.transform.position = transform.position + new Vector3(0, Random.Range(-maxHeight, maxHeight), 0);
            Destroy(currentPillars, 5f);

            timeCounter = 0;
        }
    }
}
