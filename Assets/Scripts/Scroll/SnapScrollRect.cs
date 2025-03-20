using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(ScrollRect))]
public class SnapScrollRect : MonoBehaviour, IEndDragHandler
{
    [Header("Settings")]
    public int itemsPerPage = 5;         // Mỗi trang chứa bao nhiêu item
    public int totalItems = 15;          // Tổng số item (map)
    public float snapDuration = 0.3f;    // Thời gian "snap" về trang

    private ScrollRect scrollRect;
    private int totalPages;
    private float[] pagePositions;       // Vị trí (normalized) cho mỗi trang

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        // Tính số trang (mỗi trang 5 item)
        totalPages = Mathf.CeilToInt((float)totalItems / itemsPerPage);

        // Tạo mảng pagePositions để lưu vị trí normalized của từng trang
        // Ví dụ: 2 trang => [0, 1], 3 trang => [0, 0.5, 1] (tùy ý)
        pagePositions = new float[totalPages];

        if (totalPages > 1)
        {
            // Khi có từ 2 trang trở lên
            for (int i = 0; i < totalPages; i++)
            {
                // Ta muốn trải đều từ 0 -> 1
                // Nếu có (totalPages - 1) "khoảng" giữa, mỗi khoảng = 1 / (totalPages - 1)
                // Nếu totalPages = 2 => i = 0 => 0f, i = 1 => 1f
                // totalPages = 3 => i = 0 => 0f, i = 1 => 0.5f, i = 2 => 1f
                pagePositions[i] = (totalPages == 1) ? 0 : (float)i / (totalPages - 1);
            }
        }
        else
        {
            // Nếu chỉ có 1 trang => không cần snap
            pagePositions[0] = 0f;
        }
    }

    // Khi người dùng thả chuột / ngón tay (kết thúc drag)
    public void OnEndDrag(PointerEventData eventData)
    {
        // Tìm vị trí normalized gần nhất
        float currentPos = scrollRect.horizontalNormalizedPosition;
        float closestDistance = Mathf.Infinity;
        int closestPageIndex = 0;

        for (int i = 0; i < totalPages; i++)
        {
            float dist = Mathf.Abs(pagePositions[i] - currentPos);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestPageIndex = i;
            }
        }

        // Bắt đầu coroutine để "snap" mượt về trang gần nhất
        StopAllCoroutines();
        StartCoroutine(SmoothSnap(pagePositions[closestPageIndex]));
    }

    private IEnumerator SmoothSnap(float targetPos)
    {
        float startPos = scrollRect.horizontalNormalizedPosition;
        float time = 0f;

        while (time < snapDuration)
        {
            time += Time.deltaTime;
            float t = time / snapDuration;
            // Lerp từ startPos -> targetPos
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPos, targetPos, t);
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = targetPos;
    }
}
