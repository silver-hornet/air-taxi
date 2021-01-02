using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public static GameManager instance;
    [SerializeField] Text deliveriesLeftText;

    [Header("Config")]
    [SerializeField] int totalDeliveries;

    void Awake()
    {
        instance = this;
    }

    public void UpdateDeliveriesLeft()
    {
        if (totalDeliveries != 0)
        {
            totalDeliveries--;
            deliveriesLeftText.text = totalDeliveries.ToString();
        }
        else
            deliveriesLeftText.text = "YOU WIN!";
    }
}