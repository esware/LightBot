
using UnityEditor;
using UnityEngine;

namespace Dev.Scripts
{
#if UNITY_EDITOR
    
    public class LevelGenerator : EditorWindow
    {

        public enum DirectionType
        {
            Forward,
            Backward,
            Left,
            Right,
            Up,
            Down
        }

        private GameObject _prefab;
        private Vector3 _direction;
        private DirectionType _directionType;
        private GameObject _platformPrefab;

        private GameObject _platform;
        private static GameObject _lastCube;
        private static GameObject _cubePreview;

        private bool _canCreateCube = false;

        [MenuItem("Custom Tools/Generate Level")]
        private static void OpenWindow()
        {
            LevelGenerator window = GetWindow<LevelGenerator>("Level Generator");
            window.titleContent = new GUIContent("Generate Level");
        }

        private void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {

            if (_platform != null)
            {
                Transform[] allChildren = _platform.GetComponentsInChildren<Transform>();
                for (int i = 0; i < allChildren.Length; i++)
                {
                    if (allChildren[i] != _platform.transform)
                        DestroyImmediate(allChildren[i].gameObject);
                }

                DestroyImmediate(_platform);
            }

            DestroyImmediate(_cubePreview);
            DestroyImmediate(_lastCube.transform);
        }

        private void Init()
        {
            _direction = Vector3.forward;
            _lastCube = new GameObject("TempCube");
            _lastCube.transform.position = Vector3.zero;
            _cubePreview = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _cubePreview.name = "Cube Preview";
            var c = Color.green;
            c.a = 0.1f;
            _cubePreview.GetComponent<Renderer>().sharedMaterial.color = c;
        }

        private void ChangeDirection()
        {
            switch (_directionType)
            {
                case DirectionType.Forward:
                    _direction = Vector3.forward;
                    break;
                case DirectionType.Backward:
                    _direction = Vector3.back;
                    break;
                case DirectionType.Left:
                    _direction = Vector3.left;
                    break;
                case DirectionType.Right:
                    _direction = Vector3.right;
                    break;
                case DirectionType.Up:
                    _direction = Vector3.up;
                    break;
                case DirectionType.Down:
                    _direction = Vector3.down;
                    break;
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Cube  Prefab ");
            _prefab = (GameObject)EditorGUILayout.ObjectField(_prefab, typeof(GameObject), true);

            EditorGUILayout.LabelField("Platform  Prefab ");
            _platformPrefab = (GameObject)EditorGUILayout.ObjectField(_platformPrefab, typeof(GameObject), true);

            EditorGUILayout.Space();
            _directionType = (DirectionType)EditorGUILayout.EnumPopup("Direction", _directionType);
            ChangeDirection();

            if (GUILayout.Button("Create Platform"))
            {
                _platform = Instantiate(_platformPrefab, new Vector3(0, 0f, 0), Quaternion.identity);
            }

            if (GUILayout.Button("Create Cube"))
            {
                _canCreateCube = true;
            }

            var levelName = "";
            levelName = EditorGUILayout.TextField(levelName);

            if (GUILayout.Button("Save Level"))
            {
                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(_platform.gameObject,
                    "Assets/Art/Levels/" + levelName + ".prefab");
                Debug.Log("Prefab saved at: " + AssetDatabase.GetAssetPath(prefab));
            }

        }

        private void CreateCube(Vector3 spawnPosition)
        {

            if (_prefab == null)
            {
                Debug.LogWarning("Please select a prefab before generating cubes!");
                return;
            }

            string objectName = _prefab.name;

            GameObject clone = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            clone.name = objectName;
            clone.transform.parent = _platform.transform;
            _lastCube = clone;

            Selection.activeGameObject = clone;
            _canCreateCube = false;
        }

        private void Update()
        {
            if (_lastCube != null)
            {
                var spawnPosition = _lastCube.transform.position + _direction;
                _cubePreview.transform.position = spawnPosition;
                if (_canCreateCube)
                {
                    CreateCube(spawnPosition);
                }
            }
        }
    }
#endif
}

    