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
    private Vector2 _avatarSizeDelta;
    
    public UserInfo()
    {
        _go = new GameObject();
        _go.name = "UserInfo";
        _go.transform.SetParent(Global.instance.Canvas.transform);

        _avatarSizeDelta = new Vector2(Screen.height * .2f, Screen.height * .2f);

        //---------------------------
        //set up image
        //---------------------------
        GameObject _goImage = new GameObject();
        _goImage.name = "avatar";

        Texture2D textureInit = (Texture2D)Resources.Load("avatars/avatar1");
        _sprite = Sprite.Create(textureInit, new Rect(0, 0, textureInit.width, textureInit.height), new Vector2(0.5f, 0.5f));
        _image = _goImage.AddComponent<Image>();

        _image.rectTransform.sizeDelta = new Vector2(_avatarSizeDelta.y, _avatarSizeDelta.y);
        _image.sprite = _sprite;

        _goImage.transform.SetParent(_go.transform);

        RectTransform rtImage = _goImage.GetComponent<RectTransform>();
        rtImage.anchoredPosition = new Vector2(0, Screen.height * .2f);

        //---------------------------
        //set up name likes and desc
        //---------------------------
        Vector2 targetName = new Vector2(0, rtImage.anchoredPosition.y - _avatarSizeDelta.y * .7f);
        int fontSizeName = (int)(Screen.height * .04f);
        _textUser = new GuiText(_go.transform, "name", "name", fontSizeName, "ffffff", FontStyle.Normal, TextAnchor.MiddleCenter);
        _textUser.SetPosition(targetName);

        Vector2 targetLikes = new Vector2(0, targetName.y - _textUser.Height);
        int fontSizeLikes = (int)(Screen.height * .03f);
        _textLikes = new GuiText(_go.transform, "likes", "likes", fontSizeLikes, "ffffff", FontStyle.Normal, TextAnchor.MiddleCenter);
        _textLikes.SetPosition(targetLikes);

        int fontSizeDescription = (int)(Screen.height * .02f);
        
        _textDescription = new GuiText(_go.transform, "description", "description", fontSizeDescription, "ffffff", FontStyle.Normal, TextAnchor.MiddleLeft);
        _textDescription.guiText.horizontalOverflow = HorizontalWrapMode.Wrap;

        //---------------------------
        //set up back button
        //---------------------------
        Vector2 sizeDeltaGoBackIcon = new Vector2(_avatarSizeDelta.y * .75f, _avatarSizeDelta.y * .75f);
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
        _textLikes.text = guiUser.user.UserLikes + " LIKES";

        string userNameFormat = "<b>" + _textUser.text + "</b>";
        string description = string.Format("A very long description for {0} should go here and it should wrap. A very long description for {0} should go here and it should wrap. A very long description for {0} should go here and it should wrap.", userNameFormat);
        _textDescription.text = description;

        int fontSizeDescription = (int)(Screen.height * .02f);
        Vector2 sizeDeltaDesc = new Vector2(_avatarSizeDelta.x * 2, fontSizeDescription);
        Vector2 targetDesc = new Vector2(0, -Screen.height * .1f);
        _textDescription.guiText.rectTransform.sizeDelta = sizeDeltaDesc;
        _textDescription.SetPosition(targetDesc);

        _sprite = Sprite.Create(guiUser.textureAvatar, new Rect(0, 0, guiUser.textureAvatar.width, guiUser.textureAvatar.height), new Vector2(0.5f, 0.5f));
        _image.sprite = _sprite;
    }

    public void toggle(bool value)
    {
        _go.SetActive(value);
    }
}
