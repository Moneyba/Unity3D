using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    public GameObject uiPrefab;
    public Transform target;
    float visibleTime = 5f;
    float lastVisibletime;
    Transform ui;
    Image healthBar;
    Transform cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main.transform;
		foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if(c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthBar = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
           
        }

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
        
	}


    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if(ui != null)
        {
            ui.gameObject.SetActive(true);
            lastVisibletime = Time.time;
            float healthPercent = currentHealth / (float)maxHealth;
            healthBar.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastVisibletime > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }
        }
      
    }
}
