using UnityEngine;
using System.Collections;

public class SearchBar {

    private GameObject _go;

    private GuiInputField _usersInputField;
    public SearchBar()
    {

        Vector2 sizeDelta = new Vector2(Screen.width, Screen.height * .085f);
        Vector2 target = new Vector2(0, Screen.height / 2 - sizeDelta.y / 2);

        _usersInputField = new GuiInputField("UserSearch", "user name", target, 
                                                    Global.instance.Canvas.transform, sizeDelta);

        _usersInputField.InputField.onValueChanged.AddListener(delegate { onInputFieldChange(); });
    }

    public void toggle(bool value)
    {
        _usersInputField.go.SetActive(value);
    }

    private void onInputFieldChange()
    {
        Global.instance.ManagerUsers.getUsersByAlpha(_usersInputField.InputField.text);
    }
}
