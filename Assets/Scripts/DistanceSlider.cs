using UnityEngine;
using UnityEngine.UI;

public class DistanceSlider : MonoBehaviour
{
    [SerializeField] private Slider distSlider;
    [SerializeField] private Transform target;
    [SerializeField] private Transform player;

    private float _currDist;

    private float _fullDistance;

    private void Awake()
    {
        _fullDistance = Vector3.Distance(player.position, target.position);
    }

    private void Update()
    {
        _currDist = Vector3.Distance(player.position, target.position);
        distSlider.value = 1 - _currDist / _fullDistance;

    }
}
