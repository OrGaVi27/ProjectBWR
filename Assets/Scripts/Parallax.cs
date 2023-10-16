using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    private float _length, _startPos;
    public GameObject MyCamera;
    public float ParallaxEffect;
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<TilemapRenderer>().bounds.size.x;
    }
    void Update()
    {
        float temp = MyCamera.transform.position.x * (1f - ParallaxEffect);
        float dist = (MyCamera.transform.position.x * ParallaxEffect);

        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

        if (temp > _startPos + _length)
            _startPos += _length * 2;
        else if (temp < _startPos - _length)
            _startPos -= _length;
    }
}
