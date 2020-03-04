using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCheck : MonoBehaviour
{
    public PuzzelBase[] puzzels = new PuzzelBase[0];
    public InteractEvent matchEvent;

    public void CheckAllMatch(Transform transform)
    {
        int matchCount = 0;
        for (int i = 0; i < puzzels.Length; i++)
        {
            if (puzzels[i].Match)
                matchCount++;
        }
        if (matchCount == puzzels.Length)
            matchEvent?.Invoke(transform);
    }
}
