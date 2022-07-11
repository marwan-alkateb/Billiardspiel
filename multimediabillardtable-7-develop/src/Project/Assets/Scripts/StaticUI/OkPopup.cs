using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace StaticUI
{
	public class OkPopup : MonoBehaviour
{
	[SerializeField] Text	_popupText;
	[SerializeField] Text	_okButtonText;

	public Action onOkAction;

	public void Init (Transform canvas, string popupText, string okButtonText, Action action)
	{
		_popupText.text = popupText;
		_okButtonText.text = okButtonText;


		transform.SetParent(null);			// turns child into a top-level object in the hierarchy

		transform.localScale = Vector3.one / 15;     // adjust scale of Popup to fit canvas
	}

		public void SetMessage(string mes)
	{
		_popupText.text = mes;
	}
	public void MainMessageBoxClicked()
	{
		Debug.Log("main message touched");
	}
	public void OkClicked()
	{
		Debug.Log("ok clicked");
		if (onOkAction != null)
			onOkAction();
		Dissapear();
	}
	public void OutOfMessageClicked()
	{
		Debug.Log("Out Of Message clicked");
		Dissapear();
	}
	public void CloseClicked()
	{
		Debug.Log("Close clicked. For me it is a no.");
		Dissapear();
	}
	public void Dissapear()
	{
		Debug.Log("message dissapear");
		GetComponent<Animator>().Play("MessageBoxDissappear");
		Destroy(this.gameObject, 1f);
	}
}
}
