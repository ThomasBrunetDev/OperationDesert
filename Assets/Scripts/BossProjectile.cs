// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _effet;     //déclaration des effets de l'explosion de l'obus
    private GameObject _perso;      //déclaration du gameobject du perso
    private float _vitesse = 8f;        //déclaration de la vitesse du projectile
    private Vector3 _target;        //déclaration de vector 3 de la cible
    void Start()    //fonction start
    {
        _perso = GameObject.Find("Perso");
        _target = _perso.transform.position;
    }

    void Update()       //fonction update
    {
        transform.position = Vector2.MoveTowards(transform.position, _target, _vitesse * Time.deltaTime);
        if(Vector2.Distance(transform.position, _target) < 0.1f) Die();
    }

    private void OnTriggerEnter2D(Collider2D other)     //fonction qui gere les colisions
    {
        if(other.gameObject.CompareTag("tank") == false && other.gameObject.CompareTag("SalleBoss") == false)
        {
            Destroy(gameObject);
        }
    }
    private void Die()      //fonction qui detruit l'obus
    {
        Instantiate(_effet, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

}
