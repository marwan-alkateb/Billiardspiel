using UnityEngine;

/// <summary>
/// Script which, if added to a Unity GameObject, adds a comment field
/// that can be used by developers for documentation purposes.
/// </summary>
public class CommentField : MonoBehaviour
{
    // Do not place your note/comment here,
    // but enter it using the Inspector of the Unity Editor instead.
    [TextArea]
    public string Notes = "Comment here.";
}
