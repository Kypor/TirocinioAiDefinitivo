using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    private QuestManager questManager;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questManager = FindAnyObjectByType<QuestManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (questManager.currentQuest.questID == 1)
        {
            animator.SetBool("isOpen", true);
        }
    }
}
