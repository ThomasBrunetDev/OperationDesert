// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private BarreVie _barre;       // déclaration du lien avec le script BarreVie 
    [SerializeField] private GameObject _obus;      //déclaration du gameObject de l'obus du tank
    [SerializeField] private Transform _bout;       //déclaration de la position du bout du canon
    [SerializeField] private GameObject _canon;     //déclaration du gameObject du canon du tank
    [SerializeField] private GameObject _perso;     //déclaration du gameObject du personnage
    [SerializeField] private GameObject _prefabExplosion;       //déclaration du gameObject du prefab de l'explosion
    private Vector3 _cible;     //déclaration duvector 3 de la cible de l'orientation du canon du tank
    [SerializeField] private Transform[] _trajet;       //déclaration du tableau avec le trajet du tank
    [SerializeField] private AudioClip _sonTir;     //déclaration du clip de tir

    private int _numTrajet = 0;     //declaration de numero de trajet au tank
    private Rigidbody2D _rb;        // déclaration du rigidbody
    private float _vitesse = 2.5f;      //déclaration de la vitesse du tank
    private bool _actif =true;      //declaration d'un bool pour sasvoir si c'est actif
    void Start()    //fonction start
    {
        _rb = GetComponent<Rigidbody2D>();
        _cible = _perso.transform.position;
        InvokeRepeating("Tir", 4f, 4f);
        transform.position = _trajet[0].position;
    }


    void Update()       //fonction update
    {
        if(Vector3.Distance(transform.position, _perso.transform.position) < 10f)
        {
            _actif = true;

        }
        else
        {
            _actif =false;
        }
        if(_actif)
        {
            _cible = _perso.transform.position;
            float angles = TrouverAngle(transform.position, _cible);
            _canon.transform.rotation = Quaternion.Euler(0, 0, angles * Mathf.Rad2Deg);
        }
    }

    void FixedUpdate()      //fonction fixedupdate pour le rigidbody
    {
        float angle = TrouverAngle(transform.position, _trajet[(_numTrajet + 1)].position);
        transform.rotation = Quaternion.Euler(0,0,angle*Mathf.Rad2Deg);
        _rb.MovePosition(transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) *  _vitesse * Time.fixedDeltaTime);
        if(Vector3.Distance(transform.position, _trajet[_numTrajet + 1].position) < 0.1f)
        {
            transform.position = _trajet[_numTrajet + 1].position;
            _numTrajet ++;
            if(_numTrajet == _trajet.Length-1)
            {
                _numTrajet = -1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)     //fonction qui detecte les colisions
    {
        if(other.CompareTag("balle"))
        {
            _barre.Modifier(-1f);
        }
    }

    private float TrouverAngle(Vector2 v1, Vector2 v2)      //fonction qui trouve l'angle entre deux objets
    {
        Vector2 v = v2 - v1;
        return Mathf.Atan2(v.y, v.x);
    }

    private void Tir()      //fonction qui instantie un obus
    {
        AudioSource.PlayClipAtPoint(_sonTir, transform.position, 5f);
        Instantiate(_obus, _bout.position, _canon.transform.rotation);
    }

    public void Explosion()     //fonction qui fait exploser le tank
    {
        Destroy(gameObject);
        Instantiate(_prefabExplosion, transform.position, Quaternion.identity);
    }

}
