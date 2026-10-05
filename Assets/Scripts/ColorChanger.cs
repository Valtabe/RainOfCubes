using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void SetRandomColor()
    {
        if (gameObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material.color = UnityEngine.Random.ColorHSV();
        }
    }

    public void SetCustomColor(Color newColor)
    {
        if (gameObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material.color = newColor;
        }
    }
}
