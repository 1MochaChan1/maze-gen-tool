using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridElement : MonoBehaviour
{


    int _itemCount=0;
    Sprite _itemImage;
    [SerializeField]
    public int itemCount {
        get { return _itemCount; }
        set { _itemCount = value; SetCount(); }
    }

    [SerializeField]
    public Sprite itemImage
    {
        get { return _itemImage; }
        set { _itemImage = value; SetImage(); }
    }

    public GridElement(int itemCount, Sprite itemImage)
    {
        _itemCount = itemCount;
        _itemImage = itemImage;
    }

    public TextMeshProUGUI tmpText;
    public Image imageHolder;

    private void Start()
    {

        if(_itemImage != null)
        {
            imageHolder.GetComponent<Image>().sprite = _itemImage;
        }

        if(_itemCount > 0)
        {
            tmpText.text = string.Format("x{0}", _itemCount.ToString());
        }


    }

    public void SetCount()
    {
        if (_itemCount > 0)
        {
            tmpText.text = string.Format("x{0}", _itemCount.ToString());
        }
    }

    public void SetImage()
    {
        if (_itemImage != null)
        {
            imageHolder.GetComponent<Image>().sprite = _itemImage;
        }
    }

}
