using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawWithMouse : MonoBehaviour
{
    public static List<LineRenderer> drawLineRenders = new List<LineRenderer>();
    Coroutine drawing;
    public RectTransform area;
    [SerializeField]
    DrawRandomIdeo drawRandomIdeo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                StartLine();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            FinishLine();
        }

    }
    public void StartLine()
    {
        if (drawing != null)
        {
            StopCoroutine(drawing);
        }
        drawing = StartCoroutine(DrawLine());

    }
    public void FinishLine()
    {

        if (drawing != null)
        {
            StopCoroutine(drawing);
            // if (drawRandomIdeo.ToNextIdeosPartition())
            // {
            //     DestroyLines();
            // }
            
        }
    }
    public void DestroyLines()
    {
        if (drawLineRenders.Count != 0)
        {
            foreach (LineRenderer lr in drawLineRenders)
            {
                Destroy(lr.gameObject);
            }
            drawLineRenders.Clear();
        }
    }
    IEnumerator DrawLine()
    {
        LineRenderer line = null;
        bool wasInside = false;
        while (true)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector2 localMousePosition = area.InverseTransformPoint(Input.mousePosition);
            if (area.rect.Contains(localMousePosition))
            {
                if (!wasInside || line == null)
                {
                    GameObject newGameObject = Instantiate(Resources.Load("Line") as GameObject, Vector3.zero, Quaternion.identity);
                    line = newGameObject.GetComponent<LineRenderer>();
                    line.positionCount = 0;
                    drawLineRenders.Add(line);
                }
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, position);
            }
            wasInside = area.rect.Contains(localMousePosition);
            yield return null;
        }
    }
}
