using UnityEngine.SceneManagement;

public static class Loader
{
    private static string targetScene;

    public static void Load(string targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene("Loading");
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene);
    }
}
