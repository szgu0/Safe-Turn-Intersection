using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Reflection;
using UnityEngine.EventSystems;
using TMPro;


namespace TalesFromTheRift
{
	public class CanvasKeyboard : MonoBehaviour 
	{
		#region CanvasKeyboard Instantiation

		public enum CanvasKeyboardType
		{
			ASCIICapable
		}

		public static CanvasKeyboard ins;

		public static CanvasKeyboard Open(Canvas canvas, GameObject inputObject = null, CanvasKeyboardType keyboardType = CanvasKeyboardType.ASCIICapable)
		{
			// Don't open the keyboard if it is already open for the current input object
			CanvasKeyboard keyboard = GameObject.FindObjectOfType<CanvasKeyboard>();
			if (keyboard == null || (keyboard != null && keyboard.inputObject != inputObject))
			{
				Close();
				keyboard = Instantiate<CanvasKeyboard>(Resources.Load<CanvasKeyboard>("CanvasKeyboard"));
				keyboard.transform.SetParent(canvas.transform, false);
				keyboard.inputObject = inputObject;
                keyboard.text = ""; // 打開鍵盤時清除顯示的內容
            }
			return keyboard;
		}

		public static void Open(GameObject inputObject=null)
		{
			ins.inputObject = inputObject;
			// return keyboard;
			ins.gameObject.SetActive(true);
		}
		
		public static void Close()
		{
			ins.CloseKeyboard();
		}
		
		public static bool IsOpen 
		{
			get
			{
				return GameObject.FindObjectsOfType<CanvasKeyboard>().Length != 0;
			}
		}

		#endregion

		public GameObject inputObject;

		void Awake()
		{
			ins = this;
            text = ""; // 清除初始化時的數字顯示
            CloseKeyboard();
		}

		public string text 
		{
			get
			{
				if (inputObject != null) 
				{
					Component[] components = inputObject.GetComponents(typeof(Component));
					foreach (Component component in components)
					{
						PropertyInfo prop = component.GetType().GetProperty("text", BindingFlags.Instance | BindingFlags.Public);
						if (prop != null)
						{
							return prop.GetValue(component, null)  as string;;
						}
					}
					return inputObject.name;
				}
				return "";
			}
			
			set 
			{
				if (inputObject != null) 
				{
					Component[] components = inputObject.GetComponents(typeof(Component));
					foreach (Component component in components)
					{
						PropertyInfo prop = component.GetType().GetProperty("text", BindingFlags.Instance | BindingFlags.Public);
						if (prop != null)
						{
							prop.SetValue(component, value, null);
							return;
						}
					}
					inputObject.name = value;
				}
			}
		}

		#region Keyboard Receiving Input

		public void SendKeyString(string keyString)
		{
			var inputField = inputObject.GetComponent<InputField>();
			var tmpInputField = inputObject.GetComponent<TMP_InputField>();

			if (keyString.Length == 1 && keyString[0] == 8/*ASCII.Backspace*/)
			{
				if (text.Length > 0)
				{
					text = text.Remove(text.Length - 1); 
				}
			}
			else
			{
				if (inputField && inputField.selectionAnchorPosition != inputField.selectionFocusPosition)
				{
					text = text.Remove(inputField.selectionFocusPosition, inputField.selectionAnchorPosition - inputField.selectionFocusPosition);
				}
				if (tmpInputField && tmpInputField.selectionAnchorPosition != tmpInputField.selectionFocusPosition)
				{
					text = text.Remove(tmpInputField.selectionFocusPosition, tmpInputField.selectionAnchorPosition - tmpInputField.selectionFocusPosition);
				}
				text += keyString;
			}

			// Workaround: Restore focus to input fields (because Unity UI buttons always steal focus)
			if (inputField)
				ReactivateInputField(inputField);
			// if (tmpInputField)
			// 	ReactivateInputField(tmpInputField);

		}

		public void CloseKeyboard()
		{
			gameObject.SetActive(false);
		}

		#endregion


		#region Steal Focus Workaround

		void ReactivateInputField(InputField inputField)
		{
			if (inputField != null)
			{
				StartCoroutine(ActivateInputFieldWithoutSelection(inputField));
			}
		}

		IEnumerator ActivateInputFieldWithoutSelection(InputField inputField)
		{
			inputField.ActivateInputField();

			// wait for the activation to occur in a lateupdate
			yield return new WaitForEndOfFrame();

			// make sure we're still the active ui
			if (EventSystem.current.currentSelectedGameObject == inputField.gameObject)
			{
				// To remove hilight we'll just show the caret at the end of the line
				inputField.MoveTextEnd(false);
			}
		}

		void ReactivateInputField(TMP_InputField inputField)
		{
			if (inputField != null)
			{
				StartCoroutine(ActivateInputFieldWithoutSelection(inputField));
			}
		}

		IEnumerator ActivateInputFieldWithoutSelection(TMP_InputField inputField)
		{
			inputField.ActivateInputField();

			// wait for the activation to occur in a lateupdate
			yield return new WaitForEndOfFrame();

			// make sure we're still the active ui
			if (EventSystem.current.currentSelectedGameObject == inputField.gameObject)
			{
				// To remove hilight we'll just show the caret at the end of the line
				inputField.MoveTextEnd(false);
			}
		}

		#endregion

	}
}