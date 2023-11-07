// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private puzzle _puzzle;        //declaration du lien avec puzzle
    [SerializeField] private GameManager _gm;       //declaration du lien avec gamemanager
    [SerializeField] private Transform _playerGO;       //declaration du transform du perso
    [SerializeField] private GameObject _perso;     //declaration du gameobject du perso
    [SerializeField] private GameObject _boutPistol;        //declaration du gameobject du bout du fusil
    [SerializeField] private GameObject _projectile;        //declaration du gameobject du projectile
    [SerializeField] private GameObject[] _spawnPiege;      //declaration du tableau de gameobject du spawn dans la salle des piege
    [SerializeField] private GameObject _spawnPuzzle;       //declaration du gameobject du spawn dans la salle des puzzle
    [SerializeField] private GameObject _spawnBoss;     //declaration du gameobject du spawn dans la salle du boss
    [SerializeField] private GameObject _barreVieBoss;      //declaration du gameobject de la barre de vie
    [SerializeField] private AudioClip _sonTir;     //declaration du clip de tir
    [SerializeField] private AudioClip _sonMort;        //declaration du clip de mort
    [SerializeField] private AudioClip _sonRevive;      //declaration du clip de revive
    [SerializeField] private GameObject _txtListe;      //declaration du gameobject de la liste
    private Animator _anim;     //declaration de l'animator
    private bool _checkpointPuzzle = false;     //declaration du bool pour savoir si le personnage peut spawn sur le checkpoint
    private bool _checkpointBoss = false;       //declaration du bool pour savoir si le personnage peut spawn sur le checkpoint 
    public List<int> _listeJoueur;      //declaration de la liste de combinaison du joueur
    private float _vitesse = 6f;        //declaration de la vitesse du personnage    
    private Rigidbody2D _rb;        //declaration du rigid body
    private Vector2 _moveVector;        //declaration de vector 2 du personnage


    void Start()        //fontion start
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        PointApparition();
        _listeJoueur = new List<int>();
    }

    private void PointApparition()      //fonction qui gere les spawns
    {
        AudioSource.PlayClipAtPoint(_sonRevive, transform.position, 5f);
        _perso.SetActive(true);
        if (_checkpointPuzzle)
        {
            transform.position = _spawnPuzzle.transform.position;
        }
        else if (_checkpointBoss)
        {
            transform.position = _spawnBoss.transform.position;
        }
        else
        {
            int spawn = Random.Range(0, _spawnPiege.Length);
            transform.position = _spawnPiege[spawn].transform.position;
        }
    }

    void Update()       //fonction update
    {
        _moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveVector = _moveVector.normalized;
        if(_moveVector.x != 0 && _moveVector.y != 0)
        {
            _anim.SetTrigger("Marche");
        }
        else
        {
            _anim.SetTrigger("Idle");
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = TrouverAngle(transform.position, mousePos) * Mathf.Rad2Deg;
        _playerGO.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0)) Fire();
    }

    private void FixedUpdate()      //fonction fixedUpdate rigidbody
    {
        _rb.MovePosition(_rb.position + _moveVector * _vitesse * Time.fixedDeltaTime);
    }

    private void Fire()     //fonction qui instantie une balle
    {
        AudioSource.PlayClipAtPoint(_sonTir, transform.position, 5f);
        Instantiate(_projectile, _boutPistol.transform.position, _playerGO.rotation);
    }

    private float TrouverAngle(Vector2 v1, Vector2 v2)      //fonction qui trouve l'angle entre deux objets
    {
        Vector2 v = v2 - v1;
        return Mathf.Atan2(v.y, v.x);
    }

    private void JouerSonMort()     //fonction qui joue le son du perso quand il perd une vie
    {
        AudioSource.PlayClipAtPoint(_sonMort, transform.position, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)     // fontion qui detecte les colisions
    {
        if (other.CompareTag("piege"))
        {
            _gm.AfficherVie();
            _perso.SetActive(false);
            Invoke("PointApparition", 2f);
            JouerSonMort();
        }
        if (other.CompareTag("obus"))
        {
            _gm.AfficherVie();
            _perso.SetActive(false);
            Invoke("PointApparition", 2f);
            JouerSonMort();
        }
        if (other.CompareTag("tank"))
        {
            _gm.AfficherVie();
            _perso.SetActive(false);
            Invoke("PointApparition", 2f);
            JouerSonMort();
        }
        if (other.CompareTag("porte"))
        {
            _puzzle.VerifierCombi();
            _listeJoueur = new List<int>();
            _gm.ResetListe();
        }
        if (other.CompareTag("RespawnPuzzle"))
        {
            Debug.Log("Vous avez atteint le checkpoint du puzzle");
            _checkpointPuzzle = true;
            _checkpointBoss = false;
            _txtListe.SetActive(true);
        }
        if (other.CompareTag("RespawnBoss"))
        {
            Debug.Log("Vous avez atteint le checkpoint du Boss");
            _checkpointPuzzle = false;
            _checkpointBoss = true;
            _barreVieBoss.SetActive(true);
            _txtListe.SetActive(false);
        }
    }
}
