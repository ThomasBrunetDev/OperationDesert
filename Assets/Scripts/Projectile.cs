// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject effet;      //déclaration des effets de l'explosion de la balle
    private float _vitesse = 20f;       //déclaration de la vitesse de la balle
    private Vector3 _target;        //déclaration du vector 3 de la cible de la balle
    
    void Start()        //fonction start
    {
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    
    void Update()       //fonction update
    {   
        transform.position = Vector2.MoveTowards(transform.position, _target, _vitesse * Time.deltaTime);
        if(Vector2.Distance(transform.position, _target) < 0.1f)  Die();
    }


    void OnTriggerEnter2D(Collider2D other)     //fontion qui detecte les colisions
    {
        if(other.CompareTag("tank"))
        {
            Destroy(gameObject);
        }
    }
    private void Die()      //fonction qui detruit la balle
    {
        Instantiate(effet, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
