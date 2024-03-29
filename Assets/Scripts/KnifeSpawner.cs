using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;

    private Vector3 spawnPoint;
    private int numberOfKnifesSpawn = 1;

    private void Awake()
    {
        spawnPoint = knifePrefab.transform.position;
        Instantiate(knifePrefab, spawnPoint, Quaternion.identity); 
    }
    private void OnEnable()
    {
        Knife.onWoodHit += SpawnKnife;
    }
    private void OnDisable()
    {
        Knife.onWoodHit -= SpawnKnife;
    }
    private void SpawnKnife()
    {
        if (numberOfKnifesSpawn < GameManager.Instance.maxNumberOfKnifes)
        {
            Instantiate(knifePrefab, spawnPoint, Quaternion.identity);
            numberOfKnifesSpawn++;
        }
        else
        {
            GameManager.Instance.GameWin();
            Debug.Log("You Win");
        }
    }
}
