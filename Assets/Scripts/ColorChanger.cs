using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor()
    {
        if (gameObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material.color = UnityEngine.Random.ColorHSV();
        }
    }
}
