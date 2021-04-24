using UnityEngine;

[CreateAssetMenu]
public class LootDescription : ScriptableObject
{
    [SerializeField] private DropProbabilityPair[] drops;

    public void SetDrops(params DropProbabilityPair[] drops)
    {
        this.drops = drops;
    }

    public Drop SelectDropRandomly()
    {
        float rnd = Random.value;

        for (int i = 0; i < drops.Length; i++)
        {
            if (rnd > (i == 0 ? 0 : drops[i - 1].Probability) && rnd <= (i == drops.Length -1 ? 1 : drops[i].Probability + (i == 0 ? 0 : drops[i - 1].Probability)))
            {
                return drops[i].Drop;
            }
        }

        return null;
    }
}

[System.Serializable]
public struct DropProbabilityPair
{
    public Drop Drop;

    [Range(0, 1)]
    public float Probability;
}
