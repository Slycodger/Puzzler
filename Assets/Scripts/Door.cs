using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Door : MonoBehaviour
{
    public Animator anim;
    private int counter = 0;
    [HideInInspector]
    public bool FirstDoor;
    [HideInInspector]
    public bool SecoundDoor;
    [HideInInspector]
    public GameObject FirstDoorGameObject;
    [HideInInspector]
    public GameObject SecoundDoorGameObject;
    [HideInInspector]
    public GameObject FirstDoorMaterial;
    [HideInInspector]
    public GameObject SecoundDoorMaterial;
    public Color FirstColor;
    public Color SecoundColor;
    [HideInInspector]
    public GameObject player;
    public bool LevelComplete;
    public bool Operator;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (counter == 1)
        {
            anim.SetTrigger("Open");
            if (FirstDoor)
            {
                SecoundDoorGameObject.GetComponent<Animator>().SetTrigger("Open");
            }
            counter = 2;
        }
        if (counter == 3)
        {
            anim.SetTrigger("Close");
            if (FirstDoor)
            {
                SecoundDoorGameObject.GetComponent<Animator>().SetTrigger("Close");
            }
            counter = 0;
        }
        
        if (FirstDoor)
        {
            FirstDoorMaterial.GetComponent<MeshRenderer>().material.SetColor("_MainColor", FirstColor);
            SecoundDoorMaterial.GetComponent<MeshRenderer>().material.SetColor("_MainColor", SecoundColor);
            FirstDoorMaterial.GetComponent<Teleport>().EndPostion = SecoundDoorGameObject.transform;
            SecoundDoorMaterial.GetComponent<Teleport>().EndPostion = FirstDoorGameObject.transform;
            FirstDoorMaterial.GetComponent<Teleport>().Active = true;
            SecoundDoorMaterial.GetComponent<Teleport>().Active = true;

            if (FirstDoorMaterial.GetComponent<Teleport>().TPed)
            {
                player = FirstDoorMaterial.GetComponent<Teleport>().ObjectIn;
                player.transform.localEulerAngles = new Vector3(0, FirstDoorMaterial.GetComponent<Teleport>().RotationGoingIn.y + (FirstDoorGameObject.transform.localEulerAngles.y + SecoundDoorGameObject.transform.localEulerAngles.y), 0);
                player.GetComponent<Rigidbody>().velocity = SecoundDoorMaterial.transform.TransformDirection(FirstDoorMaterial.GetComponent<Teleport>().Velocity);
                FirstDoorMaterial.GetComponent<Teleport>().RotationGoingIn = new Vector3(0, 0, 0);
                FirstDoorMaterial.GetComponent<Teleport>().TPed = false;
            }
            if (SecoundDoorMaterial.GetComponent<Teleport>().TPed)
            {
                player = SecoundDoorMaterial.GetComponent<Teleport>().ObjectIn;
                player.transform.localEulerAngles = new Vector3(0, SecoundDoorMaterial.GetComponent<Teleport>().RotationGoingIn.y + (FirstDoorGameObject.transform.localEulerAngles.y + SecoundDoorGameObject.transform.localEulerAngles.y), 0);
                player.GetComponent<Rigidbody>().velocity = FirstDoorMaterial.transform.TransformDirection(SecoundDoorMaterial.GetComponent<Teleport>().Velocity);
                SecoundDoorMaterial.GetComponent<Teleport>().RotationGoingIn = new Vector3(0, 0, 0);
                SecoundDoorMaterial.GetComponent<Teleport>().TPed = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (LevelComplete)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (counter == 0)
                {
                    counter = 1;
                }
                if (counter == 2)
                {
                    counter = 3;
                }
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Door)), CanEditMultipleObjects]
    public class Toggler : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Door Toggler = (Door)target;
            if (!Toggler.SecoundDoor)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("FirstDoor", GUILayout.MaxWidth(0));
                Toggler.FirstDoor = EditorGUILayout.Toggle("FirstDoor", Toggler.FirstDoor);
                EditorGUILayout.EndHorizontal();
            }
            if (!Toggler.FirstDoor)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("SecoundDoor", GUILayout.MaxWidth(0));
                Toggler.SecoundDoor = EditorGUILayout.Toggle("SecoundDoor", Toggler.SecoundDoor);
                EditorGUILayout.EndHorizontal();
            }
            if (Toggler.FirstDoor)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("FirstDoorGameObject", GUILayout.MaxWidth(0));
                Toggler.FirstDoorGameObject = (GameObject)EditorGUILayout.ObjectField("FirstDoorGameObject", Toggler.FirstDoorGameObject, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("SecoundDoorGameObject", GUILayout.MaxWidth(0));
                Toggler.SecoundDoorGameObject = (GameObject)EditorGUILayout.ObjectField("SecoundDoorGameObject", Toggler.SecoundDoorGameObject, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("FirstDoorMaterial", GUILayout.MaxWidth(0));
                Toggler.FirstDoorMaterial = (GameObject)EditorGUILayout.ObjectField("FirstDoorMaterial", Toggler.FirstDoorMaterial, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("SecoundDoorGameObject", GUILayout.MaxWidth(0));
                Toggler.SecoundDoorMaterial = (GameObject)EditorGUILayout.ObjectField("SecoundDoorMaterial", Toggler.SecoundDoorMaterial, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("DoorOne", GUILayout.MaxWidth(0));
                EditorGUILayout.EndHorizontal();
            }
            if (Toggler.Operator)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("LevelComplete", GUILayout.MaxWidth(500));
                Toggler.LevelComplete = EditorGUILayout.Toggle(false);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
#endif
}

