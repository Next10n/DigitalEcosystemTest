using UnityEditor;
using UnityEngine;


public class MyWindow : EditorWindow
{
    public static GameObject ObjectInstantiate;
    public static GameObject SightInstance;
    public string _nameObject;
    public string _countryName;
    public int _countryArea;
    public int _countryGDP;
    public int _countryPopulation;

    private void OnEnable()
    {
        ObjectInstantiate = Resources.Load("Prefabs/CountryPrefab") as GameObject;
    }

    private void OnGUI()
    {
        GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
        ObjectInstantiate = EditorGUILayout.ObjectField("Префаб объекта", ObjectInstantiate, typeof(GameObject), true) as GameObject;
        SightInstance = EditorGUILayout.ObjectField("Префаб достопримечательности", SightInstance, typeof(GameObject), true) as GameObject;
        _nameObject = EditorGUILayout.TextField("Имя объекта", _nameObject);
        _countryName = EditorGUILayout.TextField("Название страны", _countryName);
        _countryArea = EditorGUILayout.IntField("Площадь", _countryArea);
        _countryGDP = EditorGUILayout.IntField("ВВП", _countryGDP);
        _countryPopulation = EditorGUILayout.IntField("Население", _countryPopulation);

        if (GUILayout.Button("Создать страну"))
        {
            if (ObjectInstantiate)
            {
                {
                    GameObject temp = Instantiate(ObjectInstantiate, new Vector3(0, 0, 0), Quaternion.identity);
                    temp.name = _nameObject;
                    temp.transform.parent = GameObject.Find("Countries").transform;
                    Country _country = temp.AddComponent<Country>();
                    _country.CountryName = _countryName;
                    _country.CountryArea = _countryArea;
                    _country.CountryGDP = _countryGDP;
                    _country.CountryPopulation = _countryPopulation;
                    temp.transform.Find("CountryName").GetComponent<TextMesh>().text = _country.CountryName;
                    if (SightInstance != null)
                    {
                        DestroyImmediate(temp.transform.Find("Sight").gameObject);
                        GameObject sight = Instantiate(SightInstance, new Vector3(0, 0, 0), Quaternion.identity);
                        sight.name = "Sight";
                        sight.transform.parent = temp.transform;
                        sight.GetComponent<Renderer>().material = Resources.Load("Materials/SightMaterial") as Material;
                    }
                    
                }
            }
        }
    }
}
