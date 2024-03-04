using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private int points=0;
    [SerializeField] private int fruitPoints = 0;

    [Header("GamePlay")]
    [SerializeField] public int maxNumberOfKnifes = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform knifes;
    [SerializeField] private GameObject kniveUI;
    [SerializeField] private float yIndex;

    [Header("GameWin")]
    [SerializeField] private GameObject youWinEffect;
    [SerializeField] private GameObject explotionEffect;

    public static GameManager Instance;
    private List<GameObject> knivesList=new List<GameObject>();
    private int score=0;

    private void Awake()
    {
        Instance = this;
        CreateKnifesUI();
    }
    private void OnEnable()
    {
        Knife.onWoodHit += UpdateScore;
        Knife.onFruitHit += UpdateScoreFruit;
        Knife.onKnifeHit += RestartScene;
        Knife.onThrowKnife += UpdateKnifesUI;
    }
    private void OnDisable()
    {
        Knife.onWoodHit -= UpdateScore;
        Knife.onFruitHit -= UpdateScoreFruit;
        Knife.onKnifeHit -= RestartScene;
        Knife.onThrowKnife -= UpdateKnifesUI;
    }
    private void CreateKnifesUI()
    {
        GameObject currentKnife;
        Vector3 currentPos = Vector3.zero;
        
        for (int i = 0; i < maxNumberOfKnifes; i++)
        {
            currentPos = new Vector3(0, i*yIndex, 0);
            currentKnife = Instantiate(kniveUI, currentPos, Quaternion.identity) as GameObject;
            currentKnife.transform.SetParent(knifes,false);
            knivesList.Add(currentKnife);
        }
    }
    private void UpdateScore()
    {
        score += points;
        scoreText.text = score.ToString();
    }
    private void UpdateScoreFruit()
    {
        score += fruitPoints;
        scoreText.text = score.ToString();
    }
    private void UpdateKnifesUI()
    {
        Destroy(knivesList[knivesList.Count - 1]);
        knivesList.RemoveAt(knivesList.Count - 1);
    }
    public void GameWin()
    {
        StartCoroutine(EffectsOn());
    }
    IEnumerator EffectsOn()
    {
        Vector3 pos = Wood.Instance.transform.position;
        Destroy(Wood.Instance.gameObject);
        Instantiate(explotionEffect, pos, Quaternion.identity);
        yield return new WaitForSeconds(0.25f);
        Instantiate(youWinEffect, pos, Quaternion.identity);
    }
    private void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
