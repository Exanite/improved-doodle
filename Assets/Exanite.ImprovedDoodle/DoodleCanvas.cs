using UnityEngine;
using UnityEngine.UI;

public class DoodleCanvas : MonoBehaviour
{
    public new Camera camera;
    public RawImage display;

    private RenderTexture captureTexture;
    private RenderTexture displayTexture;

    private Vector2Int currentResolution;

    public Plane Plane => new Plane(transform.forward, transform.position);

    private void Start()
    {
        var resolution = GetScreenResolution();

        UpdateRenderTextureResolution(resolution);

        camera.enabled = false;
    }

    private void Update()
    {
        var resolution = GetScreenResolution();
        if (currentResolution != resolution)
        {
            UpdateRenderTextureResolution(resolution);
        }
    }

    public void Paint(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var brush = Instantiate(prefab, position, rotation, transform);

        camera.Render();
        Graphics.Blit(captureTexture, displayTexture);

        Destroy(brush);
    }

    private Vector2Int GetScreenResolution()
    {
        return new Vector2Int(Screen.width, Screen.height);
    }

    private void UpdateRenderTextureResolution(Vector2Int resolution)
    {
        currentResolution = resolution;

        var options = new RenderTextureDescriptor(resolution.x, resolution.y);

        captureTexture = new RenderTexture(options);
        displayTexture = new RenderTexture(options);

        display.texture = displayTexture;
        camera.targetTexture = captureTexture;
    }
}