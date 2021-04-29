using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private Transform woodStackedPosition;
    [SerializeField] private Transform placedWoodPosition;
    private int _stackedWood = 0;
    private float _yOffset = 0;
    private List<GameObject> _woodList = new List<GameObject>();
    private int _index = 1;
    public bool isOver = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            _yOffset += 1.25f;
            _stackedWood++;
            Debug.Log("Wood : " + _stackedWood);
            other.transform.position = woodStackedPosition.transform.position;
            other.transform.parent = woodStackedPosition.transform;
            other.transform.rotation = woodStackedPosition.transform.rotation;
            LeanTween.scale(other.gameObject, new Vector3(0.6375f,0.9563f, 0.6375f), 0.0f);
            LeanTween.moveLocalY(other.gameObject, _yOffset, 0.0f);
            _woodList.Add(other.transform.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Player is on water.");
            if (_woodList.Count > 0)
            {
                GameObject placedWood = _woodList[_woodList.Count - _index];
                placedWood.GetComponent<BoxCollider>().isTrigger = false;
                LeanTween.scale(placedWood, new Vector3(1, 0.06f, 0.7f), 0.0f);
                LeanTween.move(placedWood.transform.gameObject, placedWoodPosition.transform, 0.0f);
                placedWood.transform.rotation = placedWoodPosition.transform.rotation;
                placedWood.transform.SetParent(null);
                _index++;
                _yOffset -= 1.25f;
            }
            else
            {
                isOver = true;
                _woodList.RemoveAll(null);
            }
        }
    }
}