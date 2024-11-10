using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Property to hold the color name (e.g., "blue", "red")
    public string colorName;

    // Reference to the Renderer of the model
    private Renderer modelRenderer;

    void Start()
    {
        // Get the Renderer component this will be the arrow or the UFO
        modelRenderer = GetComponent<Renderer>();
    }

    // Public method to change the color
    public void ChangeColor(string colorName)
    {
        if (!string.IsNullOrEmpty(colorName) && gameObject.name == "ArrowModel")
        {
            // Convert the color name to a Unity Color
            if (ColorUtility.TryParseHtmlString(colorName, out Color newColor))
            {
                // Assign the color to the material
                modelRenderer.material.color = newColor;
            }
            else
            {
                Debug.LogWarning($"Color '{colorName}' is not valid! Make sure it's a recognized color name or hex code.");
            }
        }
        else if (!string.IsNullOrEmpty(colorName) && gameObject.name == "UFO")
        {
             // Load the texture from the Resources folder
            Texture newTexture = Resources.Load<Texture>($"Textures/{colorName}");

            if(newTexture != null) {
                modelRenderer.material.mainTexture = newTexture;
            }

        }
    }
}
