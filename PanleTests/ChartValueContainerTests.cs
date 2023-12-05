using Panel16.Util;
using Xunit.Abstractions;

namespace PanleTests;

public class ChartValueContainerTests
{
    #region Ctor

    // private readonly ITestOutput _output;
    private readonly ITestOutputHelper _outputHelper;

    public ChartValueContainerTests(/*ITestOutput output ,*/ ITestOutputHelper  outputHelper)
    {
        // _output = output;
        _outputHelper = outputHelper;
    }
    

    #endregion

    [Fact]
    public void LimitedInsertions()
    {
        var chc = new ChartValueContainer();
        
        chc.AddPoint(33.4 );
        chc.AddPoint(50);
        chc.AddPoint(44.6 );

        var vals = chc.Values;
        var indexes = chc.Indexes;
        var ix = 0;
        for (var index = 0; index < vals.Length; index++)
        {
            var val = vals[index];
            var ix_str = indexes[index];
            _outputHelper.WriteLine($"VAL-> ix:{ix} -> {val}");
            _outputHelper.WriteLine($"INX-> ix:{ix} -> {ix_str}");
            ix++;
        }

        Assert.Equal(3 , vals.Length);
    }
    
    
    
    [Fact]
    public void MoreThanSizeInsertion()
    {
        var chc = new ChartValueContainer(maxLength:10);

        var rand = new Random();
        for (var i = 0; i < 10; i++)
        {
            var point = rand.NextDouble() * 100;
            chc.AddPoint(point);
        }

        var values = chc.Values;
        Assert.Equal(10 , values.Length);
        
        for (var i = 0; i < 10; i++)
        {
            var point = rand.NextDouble() * 100;
            chc.AddPoint(point);
        }
        
        values = chc.Values;
        Assert.Equal(10 , values.Length);
    }
}