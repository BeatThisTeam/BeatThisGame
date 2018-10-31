using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProBuilder.Addons;
using ProBuilder.Core;
using ProBuilder.MeshOperations;

public class GroundSections : MonoBehaviour {

    [System.Serializable]
    public class Ring {

        public List<Transform> faces;
    }

    public List<Ring> rings = new List<Ring>();
}
