using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueDisplayer : MonoBehaviour
{
    public TMP_Text textMesh;

    public delegate void DialogueEvent();

    private Queue<DialogueSegment> segments;

    private DialogueSegment curSegment;

    // Start is called before the first frame update
    void Start()
    {
        segments = new Queue<DialogueSegment>();
        WallBoundary.wallEvent += Wall;
    }

    // Update is called once per frame
    void Update()
    {
        if (curSegment == null) {
            if (segments.Count > 0) {
                curSegment = segments.Dequeue();
                StartCoroutine(ProcessText());
            }
        }
    }

    private void Wall() {
        segments.Enqueue(new DialogueSegment("YOU HIT THE WALL", 0.02f, 0.1f, true));
    }

    IEnumerator ProcessText() {
        string seg = curSegment.GetSegment();
        for (int i = 0; i < seg.Length; i++) {
            textMesh.text += seg[i];
            yield return new WaitForSeconds(curSegment.GetTextPause());
        }

        curSegment.RunEvents();

        yield return new WaitForSeconds(curSegment.GetPauseTime());

        if (curSegment.GetClearAfter()) {
            textMesh.text = "";
        }

        curSegment = null;

        yield return null;
    }

    public class DialogueSegment {
        private string segment;
        private float timeBetweenText;
        private float pauseTime;
        private bool clearAfter;
        private List<DialogueEvent> eventsAfterDialogue;

        public DialogueSegment(string segment, float textPause, float pauseTime, bool clearAfter){
            this.segment = segment;
            this.timeBetweenText = textPause;
            this.pauseTime = pauseTime;
            this.clearAfter = clearAfter;
            this.eventsAfterDialogue = new List<DialogueEvent>();
        }

        public void AddDialogueEvent(DialogueEvent diaEvent) {
            eventsAfterDialogue.Add(diaEvent);
        }

        public string GetSegment() {
            return segment;
        }

        public float GetTextPause() {
            return timeBetweenText;
        }

        public float GetPauseTime() {
            return pauseTime;
        }

        public bool GetClearAfter() {
            return clearAfter;
        }

        public void RunEvents() {
            foreach(DialogueEvent e in eventsAfterDialogue) {
                e.Invoke();
            }
        }
    }
}
