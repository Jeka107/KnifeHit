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
        if(target.tag=="Wood")
        {
            gameObject.transform.SetParent(target.transform);//rotate the knife with the wood.
            rb.velocity = Vector2.zero;
            onWood = true;
            onWoodHit?.Invoke();
        }
        if(target.tag=="Fruit")
        {
            Destroy(target.gameObject);
            onFruitHit?.Invoke();
        }
        if (target.tag == "Knife")//Game over restart the scene.
        {
            onKnifeHit?.Invoke();
        }
    }
}
