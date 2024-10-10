using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using UnityEngine.UI;
public class LoadSprites : MonoBehaviour 
{
    public Image[] images;         
    public string atlasKey;       
    public string[] spriteNames;  

    public void Load()
    {
        StartCoroutine(LoadSpriteAtlas());
    }

    IEnumerator LoadSpriteAtlas()
    {
        var atlasLoadTask = Addressables.LoadAssetAsync<SpriteAtlas>(atlasKey);
        yield return atlasLoadTask;

        if (atlasLoadTask.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            SpriteAtlas atlas = atlasLoadTask.Result;

            for (int i = 0; i < images.Length; i++)
            {
                Sprite sprite = atlas.GetSprite(spriteNames[i]);

                if (sprite != null)
                {
                    images[i].sprite = sprite;
                }
                else
                {
                    Debug.LogError($"Спрайт с именем {spriteNames[i]} не найден в атласе");
                }
            }
        }
        else
        {
            Debug.LogError("Ошибка при загрузке атласа");
        }
    }
}
