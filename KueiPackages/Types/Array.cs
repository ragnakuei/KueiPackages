using System.Collections;
using System.Collections.ObjectModel;

namespace KueiPackages.Types;

public class Array<T> : IEnumerable<T>
{
    private T[] _array;

    public Array(params T[] items)
    {
        _array = items;
    }

    public Array(int size)
    {
        _array = new T[size];
    }

    public Array(IEnumerable<T> items)
    {
        _array = items.ToArray();
    }

    public static implicit operator T[](Array<T> array)
    {
        return array._array;
    }
    
    public int Length => _array.Length;
    public int Count  => _array.Length;

    public IEnumerator<T> GetEnumerator()
    {
        return (_array as IEnumerable<T>).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _array.GetEnumerator();
    }

    public T this[int i]
    {
        get => _array[i];
        set => _array[i] = value;
    }

    public void Clear(int index = 0, int? length = null)
    {
        length ??= _array.Length - index;

        if (length > _array.Length - index)
        {
            length = _array.Length - index;
        }

        Array.Clear(_array, index, length.GetValueOrDefault());
    }

    public void Copy(Array<T> destination, int length)
    {
        Copy(destination._array, length);
    }

    public void Copy(T[] destination, int length)
    {
        Array.Copy(_array, destination, length);
    }

    public static Array<T> Empty()
    {
        return new Array<T>(Array.Empty<T>());
    }

    public bool Exists(Predicate<T> predicate)
    {
        return Array.Exists(_array, predicate);
    }

    public T Find(Predicate<T> predicate)
    {
        return Array.Find(_array, predicate);
    }

    public void Resize(int size)
    {
        Array.Resize(ref _array, size);
    }

    public void Reverse()
    {
        Array.Reverse(_array);
    }

    public void Sort()
    {
        Array.Sort(_array);
    }

    public int Sort(T item)
    {
        return Array.BinarySearch(_array, item);
    }

    public int IndexOf(T item)
    {
        return Array.IndexOf(_array, item);
    }

    public int LastIndexOf(T item)
    {
        return Array.LastIndexOf(_array, item);
    }

    public void ConstrainedCopy(int   sourceIndex,
                                Array targetArray,
                                int   targetIndex,
                                int   length)
    {
        Array.ConstrainedCopy(_array,
                              sourceIndex,
                              targetArray,
                              targetIndex,
                              length);
    }

    public void ConstrainedCopy(int      sourceIndex,
                                Array<T> targetArray,
                                int      targetIndex,
                                int      length)
    {
        ConstrainedCopy(sourceIndex,
                        targetArray._array,
                        targetIndex,
                        length);
    }

    public Array<TTarget> ConvertAll<TTarget>(Converter<T, TTarget> converter)
    {
        var targetArray = Array.ConvertAll(_array, converter);
        return new Array<TTarget>(targetArray);
    }

    public ReadOnlyCollection<T> AsReadOnly()
    {
        return Array.AsReadOnly(_array);
    }

    public bool TrueForAll(Predicate<T> predicate)
    {
        return Array.TrueForAll(_array, predicate);
    }

    public void Add(T item)
    {
        Array.Resize(ref _array, _array.Length + 1);
        _array[_array.Length - 1] = item;
    }
}
