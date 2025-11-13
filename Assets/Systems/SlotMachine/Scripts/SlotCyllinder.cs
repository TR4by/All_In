using UnityEngine;

public class SlotCyllinder : MonoBehaviour
{
    [SerializeField] private float speed = -15f;
    [SerializeField] private float segmentScale = 20f;

    private bool isSpinning;
    private int segmentCount;

    private void Awake()
    {
        segmentCount = transform.childCount;
    }

    private void Update()
    {
        if (isSpinning)
        {
            Spin();
        }
    }
    
    private void Spin()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed * Random.Range(1f, 1.5f));
        Loop();
    }

    private void Loop()
    {
        if (transform.localPosition.y >= (segmentCount - 1) * segmentScale)
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
    }

    public void StartSpinning()
    {
        isSpinning = true;
    }

    [ContextMenu("Stop")]
    public void StopSpinning()
    {
        var roundedYPosition = Mathf.Round(transform.localPosition.y / segmentScale) * segmentScale;
        transform.localPosition = new Vector3(transform.localPosition.x, roundedYPosition, transform.localPosition.z);
        isSpinning = false;

        var segmentIndex = (int)(roundedYPosition / segmentScale);
        Debug.Log("Stopped at segment: " + segmentIndex);
        
        var symbol = transform.GetChild(segmentIndex).GetComponent<SlotCyllinderSymbol>();
        Debug.Log(symbol.symbolData.symbolType);
    }
}
