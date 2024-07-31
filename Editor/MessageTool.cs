using DT.Scripts.Messages;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace DT.Editor.MessageTool
{
    public class MessageTool : EditorWindow
    {
        private static MessageTool _window;

        private static MessageHandler _messageHandler;

        private Vector2 _scrollPosition;

        private string _messageContent;

        [MenuItem("Tools/Message Tool")]
        private static void ShowWindow()
        {
            _window = GetWindow<MessageTool>();
            _window.titleContent = new GUIContent("Message Tool");
            _window.Show();

            _messageHandler = FindObjectOfType<MessageHandler>();
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnStop();
                    break;

                case PlayModeStateChange.EnteredPlayMode:
                    OnPlay();
                    break;
            }
        }

        private void OnPlay()
        {
            _messageContent = "";
            RunBehaviour();
            Debug.Log($"<b><color=#3498db>[{nameof(MessageTool)}]: Run</color></b>");
        }

        private void OnStop()
        {
            StopBehaviour();
            Debug.Log($"<b><color=#3498db>[{nameof(MessageTool)}]: Stop</color></b>");
        }

        private void OnGUI()
        {
            _messageHandler = FindObjectOfType<MessageHandler>();

            if (!_messageHandler)
            {
                var errorLabelStyle = new GUIStyle(EditorStyles.label);
                errorLabelStyle.normal.textColor = Color.red;
                errorLabelStyle.fontStyle = FontStyle.Bold;

                GUILayout.Label($"Dependencies {nameof(MessageHandler)} not found!", errorLabelStyle);
                return;
            }

            GUILayout.Space(10);

            CustomGUI();

            GUILayout.Space(10);

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(300));
            GUILayout.TextArea(_messageContent, GUILayout.ExpandHeight(true));
            GUILayout.EndScrollView();
        }

        private void CustomGUI()
        {
            //TO DEFINE
        }

        private void RunBehaviour()
        {
            //TO DEFINE
        }

        private void StopBehaviour()
        {
            //TO DEFINE
        }

        private void ExecuteTask(string messageKey, string messageData)
        {
            if (!_messageHandler)
            {
                Debug.LogError($"{nameof(MessageHandler)} must be not null");
                return;
            }

            var task = new
            {
                key = messageKey,
                data = messageData
            };

            string taskJSON = JsonConvert.SerializeObject(task, Formatting.Indented);

            _messageContent = taskJSON;
            _messageHandler.SendNewEvent(taskJSON);
        }
    }
}