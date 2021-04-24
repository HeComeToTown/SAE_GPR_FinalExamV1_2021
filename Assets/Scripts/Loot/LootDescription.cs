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
        for (int i = 0; i < drops.Length; i++)
        {
            float lootIteration = (float)i / (float)drops.Length;
            float rnd = Random.Range(lootIteration, 1f);

            DropProbabilityPair pair = drops[i];

            if (rnd >= pair.Probability)
            {
                return pair.Drop;
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
