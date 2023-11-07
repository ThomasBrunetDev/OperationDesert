// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public ChangerScene _changerScene;     //déclaration du lien avec le script ChangerScene
    [SerializeField] private Vies _vies;        //déclaration du lien avec le script Vies
    [SerializeField] private Piege[] _sortirPiege;      //déclaration du lien avec les objets qui ont le script Piege
    [SerializeField] private GameObject[] _spawnPiege;      //déclaration du tableau du spawn des pieges
    [SerializeField] private GameObject _spawnPuzzle;   //déclaration du gameobject de l'endroit du spawn a la piece des puzzles
    [SerializeField] private GameObject _spawnBoss;     //déclaration du gameobject de l'endroit du spawn a la salle du boss
    [SerializeField] private GameObject _joueur;        //déclaration de game object du perso
    [SerializeField] private Text _txtRecomencer;       //déclaration de champ de texte recommencer
    [SerializeField] private Text _txtListe;        //déclaration de champ de texte de la liste
    private List<int> _listeJoueur;     //déclaration de la liste du joueur
    private AudioSource _audio;     //déclaration de l'audisource
    private static bool _partieGagne;       //declaration d'un bool static
    public static bool partieGagne{     
        get{return _partieGagne;}
    }
    private static int _nbVie = 3;      //déclaration d'un int static
    public static int nbVie{
        get{return _nbVie;}
    }
    private static GameManager _instance;       //instance

    public static GameManager instance      
    {
        get { return _instance; }
    }


    // Singleton
    void Awake()        //fonction awake
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()        //fonction start
    {
        _nbVie = 3;
        _audio = GetComponent<AudioSource>();
        if (_audio != null) _audio.Play();
        PartirPiege();
        _listeJoueur = new List<int>();
    }

    public void AfficherVie()       //fonction qui affiche les vies
    {
        _nbVie--;
        _vies.Afficher(_nbVie);
        if(_nbVie <= 0)
        {
            _partieGagne = false;
            FinPartie(true);
        }
    }

    public void AfficherListe(int ajout)        //fonction qui affiche la liste        
    { 
        _listeJoueur.Add(ajout);
        string a = string.Join(",", _listeJoueur);
        _txtListe.text = $"Votre combinaison : {a}";
    }

    public void ResetListe()        //fonction qui reset la liste
    {
        _listeJoueur = new List<int>();
        _txtListe.text = $"Votre combinaison :";
    }

    private void PartirPiege()      //fonction qui part les pieges
    {
        for (int i = 0; i < _sortirPiege.Length; i++)
        {
            _sortirPiege[i].DebutPartie();
        }
    }

    private void Update()       //fonction update
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _joueur.transform.position = _spawnPiege[Random.Range(0, _spawnPiege.Length)].transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _joueur.transform.position = _spawnPuzzle.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _joueur.transform.position = _spawnBoss.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void FinPartie(bool mort)        //fonction de la fin de partie
    {
        if(mort)
        {
            _changerScene.Aller("Fin");
        }
        else
        {
            _partieGagne = true;
            _changerScene.Aller("Fin");
        }
    }
}

