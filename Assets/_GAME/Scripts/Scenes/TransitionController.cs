using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string _sceneToLoad = "";

    public bool canLoad = false;

    private void Awake()
    {
        SceneLoader.transitionController = this;
    }

    public void StartLoad(string sceneName)
    {
        _sceneToLoad = sceneName;
        animator.SetTrigger("Aparecer");
        canLoad = false;
    }

    private void Update()
    {
        if (_sceneToLoad != "" && canLoad)
        {
            SceneLoader.LoadScene(_sceneToLoad);
            animator.SetTrigger("Desaparecer");
            _sceneToLoad = "";
        }
    }
}
