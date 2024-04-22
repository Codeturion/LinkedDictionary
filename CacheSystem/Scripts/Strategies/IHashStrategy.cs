namespace Codeturion.Scripts.Strategies;

public interface IHashStrategy<TKey>
{
    int GetHashCode(TKey key);
    int GetBucketIndex(int hashCode, int bucketCount);
}