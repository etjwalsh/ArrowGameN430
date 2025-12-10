using UnityEngine;

public class Multishot : MonoBehaviour
{
    public void OnupgradeOneClick()
    {
        GameManager.instance.multiShotLevel += 1;
    }
}
