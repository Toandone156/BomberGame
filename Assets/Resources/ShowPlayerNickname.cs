using Photon.Pun;
using TMPro;

public class ShowPlayerNickname : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<TMP_Text>().text = photonView.Owner.NickName;
    }
}
