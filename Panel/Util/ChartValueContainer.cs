namespace Panel16.Util;

public class ChartValueContainer
{
    public string Name { get; }
    public double GetLastValue => _values?.Last?.Value ?? _defaultValue;

    public double[] Values => index < _maxLength
        ? _values.Select((x, i) => (x, i))
            .Where(z => z.i <= index)
            .OrderBy(x => x.i)
            .Select(x => x.x)
            .ToArray()
        : _values.ToArray();
    public string[] Indexes => index < _maxLength
        ? _indexes.Select((x, i) => (x, i))
            .Where(z => z.i <= index)
            .OrderBy(x => x.i)
            .Select(x => x.x)
            .ToArray()
        : _indexes.ToArray();
    
    private  int _maxLength;
    private readonly double _defaultValue;
    private LinkedList<double> _values;
    private LinkedList<string> _indexes;
    private int index;


    public ChartValueContainer(int maxLength = 30 , string name = "default" , double defaultValue = 0)
    {
        _maxLength = maxLength;
        _defaultValue = defaultValue;
        _values = new(new double[_maxLength + 1]);
        _indexes = new(new string[_maxLength + 1]);
        
        Name = name;
    }


    private static void AddOrReplace<T>(LinkedList<T> linkedList, int maxCount , T item)
    {
        linkedList.AddLast(item);
        if(linkedList.Count > maxCount)
            linkedList.RemoveFirst();
    }
    
    
    public void AddPoint (double point , string? id = null)
    {
        var identifier = string.IsNullOrEmpty(id) ? $"{index}" : id;
        
        AddOrReplace(_values , _maxLength , point);
        AddOrReplace(_indexes , _maxLength , identifier);
        index++;
    }
    
}