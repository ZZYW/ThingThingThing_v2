using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThingSpace
{
    public class GetCurrentCamera : MonoBehaviour
    {
        public CameraSwitcher cameraSwitcher;
        Canvas canvas;
        // Start is called before the first frame update
        void Start()
        {
            canvas = GetComponent<Canvas>();
        }

        // Update is called once per frame
        void Update()
        {
            var activeCam = cameraSwitcher.ActiveCam;
            if (activeCam != null)
            {
                canvas.worldCamera = activeCam.GetComponent<Camera>();
            }
        }
    }
}