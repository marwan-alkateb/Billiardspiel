using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Tracking.Intern.Sender;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCameraTracking
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestCameraTrackingIntern()
    {
        CameraTrackingIntern intern = new CameraTrackingIntern();

        Assert.AreEqual(intern.ColorTexture2D.width, intern.DepthTexture2D.width);
        Assert.AreEqual(intern.ColorTexture2D.height, intern.DepthTexture2D.height);
        
        Assert.AreEqual(intern.ColorTexture2D.width, intern.ColorTextureRect.width);
        Assert.AreEqual(intern.ColorTexture2D.height, intern.ColorTextureRect.height);

        Assert.AreEqual(intern.ColorTextureRect.width, intern.DepthTextureRect.width);
        Assert.AreEqual(intern.ColorTextureRect.height, intern.DepthTextureRect.height);
    }
}
