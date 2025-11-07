using UnityEngine;

public class CompletedUI : UIWindow
{
    public GameObject StarGrid;
    public GameObject StarsPrefab;
    public override void Initialize()
    {
        base.Initialize();
    }

    public void ShowStars(int obtainedstars)
    {
        foreach (Transform child in StarGrid.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < obtainedstars; i++)
        {
            GameObject stars = Instantiate(StarsPrefab, StarGrid.transform);
        }
    }
    
}
