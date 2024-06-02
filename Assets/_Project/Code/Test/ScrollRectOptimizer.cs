using UnityEngine;
using UnityEngine.UI;

public class ScrollRectOptimizer : MonoBehaviour
{
    [SerializeField] private ScrollRectOtp _scrollRectOtp;
    [SerializeField] private RectTransform _contentRt;
    [SerializeField] private Item[] _items;
    private Vector2 _contentStartPos;
    private float _itemHeight;
    private int _topIndex;
    private bool _stop;

    

    private void Start()
    {
        _items = _contentRt.GetComponentsInChildren<Item>();
        _contentStartPos = _contentRt.anchoredPosition;
        _itemHeight = _items[0].GetComponent<RectTransform>().rect.height;
    }


    private void Update()
    {
        if (_stop) return;
        
        Debug.Log(_contentRt.position + "  " + _contentRt.anchoredPosition + "  " + _itemHeight);

        var delta = _contentRt.anchoredPosition.y - _contentStartPos.y;
        if (delta >= _itemHeight)
        {
            _items[_topIndex].GetComponent<RectTransform>().SetAsLastSibling();
            _topIndex++;
            if (_topIndex == _items.Length) _topIndex = 0;


            // _contentRt.anchoredPosition = _contentStartPos;
            var contentPos = _contentRt.anchoredPosition;
            contentPos.y -= _itemHeight;
            _contentRt.anchoredPosition = contentPos;
            
            _scrollRectOtp.SetContentStartPosition(contentPos);

            _stop = false;
        }
    }
}