// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzle : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;        //déclaration du lien avec le script Playermovement
    [SerializeField] private Text _txtIndice;       //déclaration du champ de texte des indices
    [SerializeField] private int _valeurCible;      //déclaration de la valeur de la cible
    [SerializeField] private GameObject _porte;     //déclaration du gameobject de la porte
    private int[] _valeurs = {1,2,3,4};     //déclaration du tableau de valeur
    private List<int> _bonneCombi;      //declaration dela liste de la bonne combinaison
    private List<int> _listeJoueur;     //declaration dela liste du joeur
    void Start()        //fonction start
    {
        CreerCombi();
        _listeJoueur = new List<int>();
    }

    
    public void VerifierCombi()     //fonction qui verifie la combinaison
    {
        string p = string.Join(",", _bonneCombi);
        string j = string.Join(",", _playerMovement._listeJoueur);
        if(p==j)
        {
            
            Debug.Log("gg");
            _porte.SetActive(false);
            Invoke("FermerPorte", 3f);
        }
        else
        {
            Debug.Log("C'est pas la bonne combinaison");
            CreerCombi();
        }
    }

    private void FermerPorte()      //fontion qui ferme la porte
    {
        _porte.SetActive(true);
    }

    private void CreerCombi()       //fonction qui creer la combi
    {
        _bonneCombi = new List<int>();
        List<int> temp = new List<int>(_valeurs);
        for (int i = 0; i < _valeurs.Length; i++)
        {
            int pos = Random.Range(0, temp.Count);
            int item = temp[pos];
            _bonneCombi.Add(item);
            temp.RemoveAt(pos);
        }
        string combi = string.Join(",", _bonneCombi);
        _txtIndice.text = $"La bonne combinaison est {combi} ";
    }

    void OnTriggerEnter2D(Collider2D other)     //fonction qui detecte les colisions
    {
        if(other.CompareTag("balle"))
        {
            _playerMovement._listeJoueur.Add(_valeurCible);
            GameManager.instance.AfficherListe(_valeurCible);
            Debug.Log(string.Join(",", _playerMovement._listeJoueur));
            Debug.Log(_valeurCible);
        }
    }
}
