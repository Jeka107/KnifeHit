using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Knife : MonoBehaviour
{
    public delegate void OnWoodHit();
    public static event OnWoodHit onWoodHit;
    public delegate void OnFruitHit();
    public static event OnFruitHit onFruitHit;
    public delegate void OnKnifeHit();
    public static event OnKnifeHit onKnifeHit;
    public delegate void OnThrowKnife();
    public static event OnThrowKnife onThrowKnife;
     
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject woodHitffect;
    [SerializeField] private GameObject fruitHitffect;
    [SerializeField] private GameObject youWinExplotion;


    private Rigidbody2D rb;
    private bool onWood=false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&!onWood)
        {
            rb.velocity = new Vector2(0f, speed);
            onThrowKnife?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Fruit")
        {
            float x = Random.Range(-0.5f, 0.5f);
            float y = Random.Range(-0.5f, 0.5f);

            GameObject currentEffect = Instantiate(fruitHitffect, new Vector3(target.transform.position.x + x,
                    target.transform.position.y - y, target.transform.position.z), Quaternion.identity);
            Destroy(currentEffect, 2f);

            Destroy(target.gameObject);
            onFruitHit?.Invoke();
        }
        else if (target.tag=="Wood")
        {
            GameObject currentEffect =Instantiate(woodHitffect, new Vector3(transform.position.x,
                transform.position.y+0.5f,transform.position.z), Quaternion.identity);
            currentEffect.transform.SetParent(target.transform);

            gameObject.transform.SetParent(target.transform);//rotate the knife with the wood.
            rb.velocity = Vector2.zero;
            onWood = true;
            onWoodHit?.Invoke();
        }
        else if (target.tag == "Knife")//Game over restart the scene.
        {
            onKnifeHit?.Invoke();
        }
    }
    private void FruitHitEffect()
    {
        
    }
}
