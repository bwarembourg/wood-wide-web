using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private GameObject hover;
    [SerializeField] private DragIcon dragIconPrefab;
    [SerializeField] private GameObject disabled;

    public int price;
    public int startingPrice;
    public string type;
    private List<SpriteRenderer> priceTxt = new List<SpriteRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        startingPrice = price;
        UpdatePrice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPrice()
    {
        int multiplierSh = type == "path" ? 1 : 5;
        int multiplierDay = type == "path" ? 0 : 5;
        int days = Score.current.GetDay() > 1 ? Score.current.GetDay() : 1;
        price = startingPrice + multiplierSh * (Score.current.getShroom() - 1) + multiplierDay * (days - 1);
        UpdatePrice();
        UpdateDisabled(Score.current.GetSpore());
    }

    private void UpdatePrice()
    {
        priceTxt.ForEach(p => Destroy(p.gameObject));
        priceTxt.Clear();
        priceTxt = Font.current.WriteLine(price.ToString(), 0.875f, -1.6875f, transform);
    }

    private void OnMouseDown()
    {
        if (!GameMgt.current.gameOn() && Tuto.current.tutoId != 4)
            return;

        if (Score.current.GetSpore() >= price)
        {
            Score.current.addSpore(-price);
            DragIcon dragIcon = Instantiate(dragIconPrefab, transform.position + new Vector3(0.75f, -0.75f, 0), Quaternion.identity, transform);
            dragIcon.Init(type);
        }
    }

    private void OnMouseEnter()
    {
        hover.SetActive(true);
    }

    private void OnMouseExit()
    {
        hover.SetActive(false);
    }

    public void UpdateDisabled(int spores)
    {
        disabled.SetActive(spores < price);
        if (type == "path" && spores < price && Tuto.current.tutoId == 4)
        {
            Tuto.current.EndTuto4();
        }
    } 
}
