using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] public  int SceneIndex;

    public int Return()
    {
        return SceneIndex;
    }


}
