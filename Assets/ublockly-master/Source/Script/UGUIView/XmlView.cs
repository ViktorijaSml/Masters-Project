/****************************************************************************

Copyright 2016 sophieml1989@gmail.com

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

****************************************************************************/


using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UBlockly.UGUI
{
    /// <summary>
    /// Deal with Workspace - XML save and load
    /// </summary>
    public class XmlView : MonoBehaviour
    {
        [SerializeField] protected Button m_SaveBtn;
        [SerializeField] protected Button m_LoadBtn;
        [SerializeField] protected Button m_EditBtn;
        [SerializeField] protected Button m_DeleteBtn;
        [SerializeField] protected Button m_OpenNewBtn;

        [SerializeField] protected GameObject m_SavePanel;
        [SerializeField] protected InputField m_SaveNameInput;
        [SerializeField] protected Button m_SaveOkBtn;
        [SerializeField] protected GameObject m_SaveErrorObject;

        [SerializeField] protected GameObject m_LoadPanel;
        [SerializeField] protected RectTransform m_ScrollContent;
        [SerializeField] protected GameObject m_XmlOptionPrefab;
        [SerializeField] protected GameObject m_ConfirmDialogPrefab;


        private bool mIsEdit = false;
        private string mOriginalName;
        protected string mSavePath;
        private TMP_Text mErrorMsg;

        private static readonly Regex FilenameRegex = new Regex("^[a-zA-Z0-9_]*$", RegexOptions.Compiled);

        protected bool IsEdit
        {
            get { return mIsEdit; }
        }
        protected bool mIsSavePanelShow
        {
            get { return m_SavePanel.activeInHierarchy; }
        }

        protected bool mIsLoadPanelShow
        {
            get { return m_LoadPanel.activeInHierarchy; }
        }
        

        protected string GetSavePath()
        {
            if (string.IsNullOrEmpty(mSavePath))
            {
                mSavePath = System.IO.Path.Combine(Application.persistentDataPath, "XmlSave");
                if (!System.IO.Directory.Exists(mSavePath))
                    System.IO.Directory.CreateDirectory(mSavePath);
            }
            return mSavePath;
        }

        private void Awake()
        {
            HideSavePanel();
            HideLoadPanel();

            m_SaveBtn.onClick.AddListener(() =>
            {
                mIsEdit = false;
                if (!mIsSavePanelShow) ShowSavePanel();
                else HideSavePanel();
            });

            m_LoadBtn.onClick.AddListener(() =>
            {
                if (!mIsLoadPanelShow) ShowLoadPanel();
                else HideLoadPanel();
            });

            m_OpenNewBtn.onClick.AddListener(() => {
                HideLoadPanel();
                BlocklyUI.WorkspaceView.CleanViews();
                Xml.ResetAllData(BlocklyUI.WorkspaceView.Workspace);
            });
            m_SaveOkBtn.onClick.AddListener(EditOrSaveXml);
            mErrorMsg = m_SaveErrorObject.GetComponent<TMP_Text>();
        }

        protected virtual void ShowSavePanel()
        {
			m_SavePanel.SetActive(true);
            m_SaveNameInput.text = null;
            mErrorMsg.text = "";
            DestroyAllLoadObjects();
            m_LoadPanel.SetActive(false);
        }

        protected virtual void HideSavePanel()
        {
            DestroyAllLoadObjects();
            m_SavePanel.SetActive(false);
        }

        protected virtual void ShowLoadPanel()
        {
            DestroyAllLoadObjects();

			m_LoadPanel.SetActive(true);
            m_SavePanel.SetActive(false);

            string[] xmlFiles = Directory.GetFiles(GetSavePath());
            for (int i = 0; i < xmlFiles.Length; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(xmlFiles[i]);
                GameObject optionXml = GameObject.Instantiate(m_XmlOptionPrefab, m_ScrollContent, false);

                GameObject btnEdit = optionXml.transform.GetChild(0).gameObject;
                GameObject btnXml = optionXml.transform.GetChild(1).gameObject;
                GameObject btnDelete = optionXml.transform.GetChild(2).gameObject;

                optionXml.SetActive(true);

                //Open file feature
                btnXml.GetComponentInChildren<Text>().text = fileName;
                btnXml.GetComponent<Button>().onClick.AddListener(() => LoadXml(fileName));

                //Edit feature
                btnEdit.GetComponent<Button>().onClick.AddListener(() =>
                {
                    mIsEdit = true;
                    mOriginalName = fileName;
                    if (!mIsSavePanelShow) ShowSavePanel();
                    else HideSavePanel();
                } );

                //Delete feature
                btnDelete.GetComponent<Button>().onClick.AddListener(() =>
                {
                    HideLoadPanel();
                    GameObject confirmDialog = GameObject.Instantiate(m_ConfirmDialogPrefab, this.transform, false);
                    confirmDialog.transform.SetAsLastSibling();
                    ConfirmActionDialog confirmDialogClass = confirmDialog.GetComponent<ConfirmActionDialog>();

                    if (confirmDialog != null)
                    {
                        confirmDialogClass.Question = "Are you sure you want to delete this xml file?";
                        confirmDialogClass.OnConfirm = () =>
                        {
                            DeleteXml(fileName);
                            RemoveConfirmDialog(confirmDialog);
                        };
                        confirmDialogClass.OnCancel = () =>
                        {
                            RemoveConfirmDialog(confirmDialog);
                        };
                    }
                });
            }
        }

        protected virtual void RemoveConfirmDialog(GameObject confirmDialog)
        {
            Destroy(confirmDialog);
            ShowLoadPanel();
        }

        protected virtual void HideLoadPanel()
        {
            DestroyAllLoadObjects();
			m_LoadPanel.SetActive(false);
            DestroyAllLoadObjects();
		}

        protected virtual void DestroyAllLoadObjects()
		{
			for (int i = 1; i < m_ScrollContent.childCount; i++)
			{
				GameObject.Destroy(m_ScrollContent.GetChild(i).gameObject);
			}
		}

        protected virtual void DeleteXml(string fileName)
        {
            string filePath = Path.Combine(GetSavePath(), fileName + ".xml");

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    Debug.Log($"File {fileName}.xml deleted successfully.");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to delete: {fileName}.xml: {ex.Message}");
                }
            }
            else
            {
                Debug.LogError($"File {fileName}.xml does not exist.");
                return;
            }
        }
        protected virtual void EditXml(string originalFileName)
        {
            string newFileName = m_SaveNameInput.text;
            if (string.IsNullOrEmpty(newFileName))
            {
                ShowErrorMessage("File name cannot be empty.");
                return;
            }

            string originalPath = Path.Combine(GetSavePath(), originalFileName + ".xml");
            string newPath = Path.Combine(GetSavePath(), newFileName + ".xml");

            if (File.Exists(newPath))
            {
                ShowErrorMessage($"A file named {newFileName}.xml already exists.");
                return;
            }
            if (!FilenameRegex.IsMatch(newFileName))
            {
                ShowErrorMessage("Filename contains invalid characters. Use only alphanumeric characters and underscores.");
                return; 
            }
            File.Move(originalPath, newPath);//move content of the file to a new one 

            HideSavePanel();
            ShowLoadPanel(); 
        }
        protected virtual void SaveNewXml() 
        {
            var dom = UBlockly.Xml.WorkspaceToDom(BlocklyUI.WorkspaceView.Workspace);
            string text = UBlockly.Xml.DomToText(dom);
            string path = GetSavePath();

            if (!FilenameRegex.IsMatch(m_SaveNameInput.text))
            {
                ShowErrorMessage("Filename contains invalid characters. Use only alphanumeric characters and underscores.");
                return;
            }

            if (!string.IsNullOrEmpty(m_SaveNameInput.text))
            {
                path = System.IO.Path.Combine(path, m_SaveNameInput.text + ".xml");
                if (File.Exists(path))
                {
                    ShowErrorMessage($"A file named {m_SaveNameInput.text}.xml already exists.");
                    return;
                }
            }
            else
            {
                ShowErrorMessage("File name cannot be empty.");
                return;
            }

            System.IO.File.WriteAllText(path, text);
            Debug.Log($"Saved workspace successfully.");
            HideSavePanel();
        }
        protected virtual void EditOrSaveXml()
        {
            if (mIsEdit)
            {
                EditXml(mOriginalName);
            }
            else
            {
                SaveNewXml();
            }
        }
        protected virtual void LoadXml(string fileName)
        {
           StartCoroutine(AsyncLoadXml(fileName));
        }
        
        IEnumerator AsyncLoadXml(string fileName)
        {
            BlocklyUI.WorkspaceView.CleanViews();

            string path = System.IO.Path.Combine(GetSavePath(), fileName + ".xml");
            string inputXml;
            if (path.Contains("://"))
            {
                using (UnityWebRequest webRequest = UnityWebRequest.Get(path))
                {
                    yield return webRequest.SendWebRequest();
                    if (webRequest.result != UnityWebRequest.Result.Success) {
                        throw new Exception(webRequest.error + ": " + path);
                    }
                    inputXml = webRequest.downloadHandler.text;
                }
            }
            else
                inputXml = System.IO.File.ReadAllText(path);

            var dom = UBlockly.Xml.TextToDom(inputXml);
            UBlockly.Xml.DomToWorkspace(dom, BlocklyUI.WorkspaceView.Workspace);
            BlocklyUI.WorkspaceView.BuildViews();
            
            HideLoadPanel();
        }
        protected virtual void ShowErrorMessage(string msg)
        {
            Debug.LogError(msg);
            mErrorMsg.text = msg;
        }
    }
}
