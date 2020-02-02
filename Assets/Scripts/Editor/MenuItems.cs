using UnityEditor;


public class MenuItems
{
    [MenuItem("Create/Country ")]
    private static void MenuOption()
    {
        EditorWindow.GetWindow(typeof(MyWindow), false, "CountryCreator");
    }

}
