using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Bounce : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void Run(Vector2 pos)
    {
        transform.position = pos;

        transform.DOKill();
        transform.DOScale(3, 0.3f).From(1);

        _image.DOKill();
        _image.DOFade(0, 0.3f).From(1);
    }
}