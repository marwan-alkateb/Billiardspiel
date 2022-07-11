using Tracking.Intern.Sender;
using UnityEngine;

//behaviour which should lie on the same gameobject as the main camera
public class DepthPostprocessing : MonoBehaviour
{
    //material that's applied when doing postprocessing
    [SerializeField]
    private Material postprocessMaterial;

    private void Start()
    {
    }

    private void Update()
    {
    }

    //method which is automatically called by unity after the camera is done rendering
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
        
        if(CameraTrackingIntern.Tracking == null) return;

        Graphics.Blit(source,CameraTrackingIntern.Tracking.DepthTexture, postprocessMaterial);

        var intern = CameraTrackingIntern.Tracking.Intern;

        RenderTexture.active = destination;
        intern.ColorTexture2D.ReadPixels(intern.ColorTextureRect, 0,0);

        RenderTexture.active = CameraTrackingIntern.Tracking.DepthTexture;
        intern.DepthTexture2D.ReadPixels(intern.DepthTextureRect, 0,0);

        RenderTexture.active = null;
    }
}