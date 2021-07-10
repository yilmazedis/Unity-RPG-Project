using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Image healthbar;
    // Start is called before the first frame update

    private void Start()
    {
        healthbar = GetComponent<Image>();
        healthbar.fillAmount = 1f;
    }
    public void UpdateHealth(float fraction)
    {
        healthbar.fillAmount = fraction;
    }
}