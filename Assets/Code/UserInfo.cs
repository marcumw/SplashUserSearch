using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInfo {

    private GameObject _go;
    private RectTransform _rt;

    private GuiText _textUser;
    private GuiText _textLikes;
    private GuiText _textDescription;

    private Sprite _sprite;
    private Image _image;





    public UserInfo()
    {
        _go = new GameObject();
        _go.name = "UserInfo";
        _go.transform.SetParent(Global.instance.Canvas.transform);

        Vector2 sizeDelta = new Vector2(Screen.height * .2f, Screen.height * .2f);

        //set up image
        GameObject _goImage = new GameObject();
        _goImage.name = "avatar";

        Texture2D textureInit = (Texture2D)Resources.Load("avatars/avatar1");
        _sprite = Sprite.Create(textureInit, new Rect(0, 0, textureInit.width, textureInit.height), new Vector2(0.5f, 0.5f));
        _image = _goImage.AddComponent<Image>();

        _image.rectTransform.sizeDelta = new Vector2(sizeDelta.y, sizeDelta.y);
        _image.sprite = _sprite;

        _goImage.transform.SetParent(_go.transform);

        RectTransform rtImage = _goImage.GetComponent<RectTransform>();
        rtImage.anchoredPosition = new Vector2(0, Screen.height * .1f);

        Vector2 targetName = new Vector2(0, -Screen.height * .03f);
        int fontSizeName = (int)(Screen.height * .04f);
        _textUser = new GuiText(_go.transform, "name", "name", fontSizeName, "ffffff", FontStyle.Normal, TextAnchor.MiddleCenter);
        _textUser.SetPosition(targetName.x, targetName.y);

        int fontSizeLikes = (int)(Screen.height * .03f);
        _textLikes = new GuiText(_go.transform, "likes", "likes", fontSizeLikes, "ffffff", FontStyle.Normal, TextAnchor.MiddleCenter);
        _textLikes.SetPosition(0, targetName.y - _textUser.Height);

        //int fontSizeDescription = (int)(Screen.height * .02f);
        //_textDescription = new GuiText(_go.transform, "desc", "a very long description should go here and it should wrap.a very long description should go here and it should wrap.", fontSizeDescription, "ffffff", FontStyle.Normal, TextAnchor.MiddleLeft, HorizontalWrapMode.Wrap);
        //_textDescription.guiText.resizeTextForBestFit = true;
        //_textDescription.SetPosition(0, -(_textUser.Height + _textLikes.Height));


        //set up back button
        Vector2 sizeDeltaGoBackIcon = new Vector2(sizeDelta.y * .75f, sizeDelta.y * .75f);
        GameObject goBack = new GameObject();
        goBack.name = "GoBack";

        Button buttonDelete = goBack.AddComponent<Button>();
        buttonDelete.onClick.AddListener(delegate { onButtonBackClicked(); });

        goBack.transform.SetParent(_go.transform);

        Texture2D textureBackButton = (Texture2D)Resources.Load("icons/buttonBack");

        Sprite spriteBack = Sprite.Create(textureBackButton, new Rect(0, 0, textureBackButton.width, textureBackButton.height), new Vector2(0.5f, 0.5f));
        Image imageDelete = goBack.AddComponent<Image>();

        imageDelete.rectTransform.sizeDelta = sizeDeltaGoBackIcon;
        imageDelete.sprite = spriteBack;

        RectTransform rtBack = goBack.GetComponent<RectTransform>();
        rtBack.anchoredPosition = new Vector2(0, -Screen.height * .35f);


        _rt = _go.AddComponent<RectTransform>();
        _rt.anchoredPosition = new Vector3(0, 0);

        _go.SetActive(false);
    }

    private void onButtonBackClicked()
    {
        Global.instance.ManagerUsers.onUserInfoBackButtonClicked();
    }

    public void update(GuiUser guiUser)
    {
        float spacer = Screen.height * .1f;

        _textUser.text = guiUser.user.UserName;
        //_textUser.SetPosition(-Screen.width / 2 + _textUser.Width/2 + spacer, 0);

        _textLikes.text = guiUser.user.UserLikes + " LIKES";
        //_textLikes.SetPosition(-Screen.width / 2 + _textLikes.Width / 2 + spacer, -_textUser.Height);

        //_textDescription.text = user.UserLikes + " LIKES";
        //textDescription.SetPosition(-Screen.width / 2 + _textDescription.Width / 2 + spacer, -(_textUser.Height + _textLikes.Height));

        _sprite = Sprite.Create(guiUser.textureAvatar, new Rect(0, 0, guiUser.textureAvatar.width, guiUser.textureAvatar.height), new Vector2(0.5f, 0.5f));
        _image.sprite = _sprite;
    }

    public void toggle(bool value)
    {
        _go.SetActive(value);
    }
}
