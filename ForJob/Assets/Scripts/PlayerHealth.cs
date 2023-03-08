using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public float health;
    public float healthMax;

    public CanvasManager canvasManager;
    public ScoreManager score;

    private void Start()
    {
        health = healthMax;
        Refresh();
    }

    public void HealthChange(float count)
    {
        health += count;
        health = Mathf.Clamp(health,0,healthMax);


        if (health == 0)
        {
            Death();
        }

        Refresh();
    }

    private void Death()
    {
        canvasManager.DeathMenu(score.GetScore());
    }

    private void Refresh()
    {
        healthSlider.value = health / healthMax;
    }
}
