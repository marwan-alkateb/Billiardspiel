using UnityEngine;

namespace StaticUI
{
	public class PopupController : MonoBehaviour
{

	//public static GameObject BringPrefabToScene(string prefab, float x, float y)
	//{
	//	var obj = GameObject.Instantiate(Resources.Load("SimpleMessageBox/Prefabs/" + prefab) as GameObject);
	//	obj.transform.position = new Vector3(x, y, obj.transform.position.z);
	//	return obj;
	//}

	public static PopupController Instance;

	public Transform TrickshotCanvas;

	public OkPopup CreateOkPopup()
	{
		GameObject popUpGo = GameObject.Instantiate(Resources.Load("PopupBox/Prefabs/" + "OkPopup") as GameObject);
		OkPopup popUpBox = popUpGo.GetComponent<OkPopup>();
		return popUpBox;
	}

	// Start is called before the first frame update
	void Start()
	{
		// if we have an instance, 
		if (Instance != null)
		{
			GameObject.Destroy(this.gameObject);
			return;
		}

		Instance = this;

	}
}
}
