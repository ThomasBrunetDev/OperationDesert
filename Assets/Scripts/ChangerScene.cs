/*
 *     Auteur: Y. Bourdel
 *     Automne 2019, TIM
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; ////// <<<<< IMPORTANT

/// <summary>
/// Script de bouton de navigation
/// qui permet de charger une nouvelle scene
/// </summary>
public class ChangerScene : MonoBehaviour
{
    private string _destination;
    [SerializeField] private AudioClip _clip;

    public void Aller(string nouvelleScene)
    {
        _destination = nouvelleScene;
        Invoke("Go",0.5f);
        if(_clip!=null) SoundManager.instance.JouerSon(_clip);
    }

    private void Go()
    {
        SceneManager.LoadScene(_destination);
    }
    
}