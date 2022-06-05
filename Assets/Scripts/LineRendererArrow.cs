using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class LineRendererArrow : MonoBehaviour
{
    [Tooltip("The percent of the line that is consumed by the arrowhead")]
    [Range(0, 1)]
    public float PercentHead = 0.4f;
    public Vector3 ArrowOrigin;
    public Vector3 ArrowTarget;
    private LineRenderer cachedLineRenderer;
    void Start()
    {
        UpdateArrow();
    }
    private void OnValidate()
    {
        UpdateArrow();
    }
    [ContextMenu("UpdateArrow")]
    public void UpdateArrow() //Code sourced from https://answers.unity.com/questions/1100566/making-a-arrow-instead-of-linerenderer.html?childToView=1330338#answer-1330338
    {
        if (cachedLineRenderer == null)
            cachedLineRenderer = this.GetComponent<LineRenderer>();

        cachedLineRenderer.SetPositions(new Vector3[] { ArrowOrigin, ArrowTarget });


    }

	public void Reset()
	{
        ArrowOrigin = new Vector3(0, 0, 0);
        ArrowTarget = new Vector3(0, 0, 0);
        UpdateArrow();
	}
}