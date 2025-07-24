

namespace FileSearch.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (convertToNumber == null)
                throw new ArgumentNullException(nameof(convertToNumber));

            using (var enumerator = collection.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException("Коллекция пуста");

                T maxItem = enumerator.Current ?? throw new InvalidOperationException("Первый элемент коллекции null");
                float maxValue = convertToNumber(maxItem);

                while (enumerator.MoveNext())
                {
                    var currentItem = enumerator.Current;
                    if (currentItem == null) continue;

                    float currentValue = convertToNumber(currentItem);
                    if (currentValue > maxValue)
                    {
                        maxValue = currentValue;
                        maxItem = currentItem;
                    }
                }

                return maxItem;
            }
        }
    }
}
