using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Quests))]
public class QuestsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Quests questsData = (Quests)target;

        if (GUILayout.Button("Assegna QuestID Automatici"))
        {
            for (int i = 0; i < questsData.quests.Count; i++)
            {
                questsData.quests[i].questID = i;
            }

            EditorUtility.SetDirty(questsData);
            Debug.Log("QuestID aggiornati automaticamente.");
        }
    }
}
