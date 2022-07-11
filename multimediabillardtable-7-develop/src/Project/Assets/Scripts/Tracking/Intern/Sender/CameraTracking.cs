using System;
using UnityEngine;

namespace Tracking.Intern.Sender
{
    /// <summary>
    /// This class holds all relevant unity objects to use for camera tracking which is not exposed to the Editor
    /// </summary>
    public class CameraTrackingIntern
    {
        /// <summary>
        /// <see cref="ColorTexture2D"/> contains the Texture2D for the color image
        /// </summary>
        public Texture2D ColorTexture2D = new Texture2D(1920, 1080, TextureFormat.BGRA32, false);
        
        /// <summary>
        /// <see cref="ColorTextureRect"/> contains the Rect for the color image
        /// </summary>
        public Rect ColorTextureRect = new Rect(0, 0, 1920, 1080);

        /// <summary>
        /// <see cref="DepthTexture2D"/> contains the Texture2D for the depth image
        /// </summary>
        public Texture2D DepthTexture2D = new Texture2D(1920, 1080, TextureFormat.R16, false);
        
        /// <summary>
        /// <see cref="DepthTextureRect"/> contains the Rect for the depth image
        /// </summary>
        public Rect DepthTextureRect = new Rect(0, 0, 1920, 1080);
        
        /// <summary>
        /// <see cref="Tracking"/> contains the static CameraTracking reference for use in DepthPostprocessing
        /// </summary>
        public static CameraTracking Tracking = null;
    }

    /// <summary>
    /// This class holds all relevant unity objects to use for camera tracking
    /// </summary>
    [Serializable]
    public class CameraTracking
    {
        /// <summary>
        /// <see cref="Camera"/> contains the Camera-GameObject with which the color and depth image are captured
        /// </summary>
        public Camera Camera;

        /// <summary>
        /// <see cref="DepthShader"/> contains the Shader used for the depth capture
        /// </summary>
        public Shader DepthShader;

        /// <summary>
        /// <see cref="ColorTexture"/> contains the RenderTexture the color image is captured to
        /// </summary>
        public RenderTexture ColorTexture;

        /// <summary>
        /// <see cref="DepthTexture"/> contains the RenderTexture the depth image is captured to
        /// </summary>
        public RenderTexture DepthTexture;

        /// <summary>
        /// <see cref="Intern"/> contains all relevant unity objects to use for camera tracking which are not exposed to the editor
        /// </summary>
        [HideInInspector]
        public CameraTrackingIntern Intern = null;
    }
}