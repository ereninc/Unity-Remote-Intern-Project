using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public bool isGrounded = true;
    public bool takeWood = false;
    [SerializeField] private Transform woodStackedPosition;
    [SerializeField] private Transform placedWoodPosition;
    [SerializeField] private GameObject _player;
    private int _stackedWood = 0;
    private float _yOffset = 0;
    private List<GameObject> _woodList = new List<GameObject>();
    private int _index = 1;
    private float _deltaTime = 0.0f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            takeWood = true;
            TakeWood(other);
        }
    }

    private void TakeWood(Collider wood)
    {
        _yOffset += 1.25f;
        _stackedWood++;
        wood.transform.position = woodStackedPosition.transform.position;
        wood.transform.parent = woodStackedPosition.transform;
        wood.transform.rotation = woodStackedPosition.transform.rotation;
        LeanTween.scale(wood.gameObject, new Vector3(0.6375f, 0.9563f, 0.6375f), 0.0f);
        LeanTween.moveLocalY(wood.gameObject, _yOffset, 0.0f);
        _woodList.Add(wood.transform.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Wood"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
            
        }
    }
    private void Update()
    {
        _deltaTime += Time.deltaTime;
        if (!isGrounded && _deltaTime > 0.15f)
        {
            PlaceWoods();
            _deltaTime = 0.0f;
        }
    }

    private void PlaceWoods()
    {
        if (_woodList.Count > 0)
        {
            GameObject placedWood = _woodList[_stackedWood-1];
            _woodList.RemoveAt(_stackedWood-1);
            placedWood.tag = "empty";
            LeanTween.scale(placedWood, new Vector3(1, 0.06f, 0.7f), 0.0f);
            LeanTween.move(placedWood.transform.gameObject, placedWoodPosition.transform, 0.025f);
            _yOffset -= 1.25f;
            placedWood.GetComponent<BoxCollider>().isTrigger = false;
            placedWood.transform.rotation = placedWoodPosition.transform.rotation;
            placedWood.transform.SetParent(null);
            _stackedWood--;
        }
        else
        {
            _player.GetComponent<Rigidbody>().isKinematic = false;
            //_player.GetComponent<Rigidbody>().AddForce(Vector3.up * 50f);
        }
        
    }
}