using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Growbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    [SerializeField] private int currentpoint = 0;
    [SerializeField] private int maxpoint = 100;
    public int deactedenemyrank = -1;
    private int point = 0;
    public TextMeshProUGUI maxvalue;
    public TextMeshProUGUI currentvalue;
    #region s
    public static Growbar growbarinstance;
    #endregion
    private void Awake()
    {
        growbarinstance = this;
    }
    public void SetMaxGrowPoint(int maxgrowpoint)
    {
        slider.maxValue = maxgrowpoint;
        fill.color = gradient.Evaluate(1f);
        maxvalue.text = maxgrowpoint.ToString();
    }
    public void SetGrowPoint(int growpoint)
    {
        slider.value = growpoint;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        currentvalue.text = growpoint.ToString();
    }
    private void Start()
    {
        SetMaxGrowPoint(maxpoint);
        SetGrowPoint(currentpoint);
    }

    private void Update()
    {
        if (deactedenemyrank != -1)
        {
            currentpoint += (deactedenemyrank + 1) * 10;
            deactedenemyrank = -1;
            SetGrowPoint(currentpoint);
        }
        if (currentpoint >= maxpoint)
        {
            PlayerController.instance.Zooming();
            currentpoint = 0;
            SetGrowPoint(0);
        }
    }

}
