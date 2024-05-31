using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    void Start()
    {
        CheckCanvasSize();
    }

    void CheckCanvasSize()
    {
        var gameObjectTransform = transform;
   
        if (canvas.position.x <= 1000)
        {
            gameObjectTransform.position = new Vector2(6, gameObjectTransform.position.y);
        }
        else if (canvas.position.x is > 1000 and <= 1290)
        {
            gameObjectTransform.position = new Vector2(8, gameObjectTransform.position.y);
        }
        else if(canvas.position.x is > 1290 and < 1350)
        {
            gameObjectTransform.position = new Vector2(12, gameObjectTransform.position.y);
        }
        else
        {
            gameObjectTransform.position = new Vector2(13, gameObjectTransform.position.y);
        }
    }
}
