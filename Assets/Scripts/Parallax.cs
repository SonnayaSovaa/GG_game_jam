using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
   [SerializeField] private Transform target;
   [SerializeField] private float force;

   private Vector3 _targetPos;

   private void Awake()
   {
      _targetPos = target.position;
   }

   private void Update()
   {
      Vector3 delta = _targetPos - target.position;

      delta.y = 0;

      _targetPos = target.position;
      transform.position += delta * force;
   }
}
