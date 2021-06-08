using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {

        void Update()
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }

}
