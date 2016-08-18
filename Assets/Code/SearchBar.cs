using UnityEngine;
using System.Collections;

public class SearchBar {

    private GuiInputField _usersInputField;
    public SearchBar()
    {
        Vector2 sizeDelta = new Vector2(Screen.width, Screen.height * .1f);
        Vector2 target = new Vector2(0, Screen.height / 2 - sizeDelta.y / 2);

        _usersInputField = new GuiInputField("UserSearch", "user name", target, 
                                                    Global.instance.canvas.transform, sizeDelta);

        _usersInputField.InputField.onValueChanged.AddListener(delegate { onInputFieldChange(); });
    }

    private void onInputFieldChange()
    {
        Global.instance.managerUsers.getUsersByAlpha(_usersInputField.InputField.text);
    }
}
