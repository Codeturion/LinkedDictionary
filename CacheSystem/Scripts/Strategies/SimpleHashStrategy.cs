namespace Codeturion.Scripts.Strategies;

public class SimpleHashStrategy<TKey> : IHashStrategy<TKey>
{
    public int GetHashCode(TKey key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        int hash = key.GetHashCode();
        return hash;
    }

    public int GetBucketIndex(int hashCode, int bucketCount)
    {
        return Math.Abs(hashCode % bucketCount);
    }
}