using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

public class PatronSearcherTests
{
    private readonly PatronSearcher _patronSearcher;
    private readonly PatronManager _patronManager;

    public PatronSearcherTests()
    {
        _patronSearcher = new PatronSearcher();
        _patronManager = new PatronManager();
    }

    [Fact]
    public void SearchMultiple_FindsMatchingPatrons()
    {
        var patrons = new List<Patron>
        {
            new Patron("Patron 1", "M001", "patron1@example.com"),
            new Patron("Patron 2", "M002", "patron2@example.com"),
            new Patron("Patron 3", "M003", "patron3@example.com")
        };

        var result = _patronSearcher.SearchMultiple(patrons, p => p.Name.Contains("Patron"));

        Assert.Equal(3, result.Count);
        Assert.Contains(result, p => p.Name == "Patron 1");
        Assert.Contains(result, p => p.Name == "Patron 2");
        Assert.Contains(result, p => p.Name == "Patron 3");
    }

    [Fact]
    public void SearchMultiple_NoMatches_ReturnsEmptyList()
    {
        var patrons = new List<Patron>
        {
            new Patron("Patron 1", "M001", "patron1@example.com"),
            new Patron("Patron 2", "M002", "patron2@example.com"),
        };

        var result = _patronSearcher.SearchMultiple(patrons, p => p.Name.Contains("NonExistent"));

        Assert.Empty(result);
    }

    [Fact]
    public void SearchSingle_FindsMatchingPatron()
    {
        var patrons = new List<Patron>
        {
            new Patron("Patron 1", "M001", "patron1@example.com"),
            new Patron("Patron 2", "M002", "patron2@example.com"),
        };

        var result = _patronSearcher.SearchSingle(patrons, p => p.MemberShipNumber == "M002");

        Assert.NotNull(result);
        Assert.Equal("Patron 2", result.Name);
    }

    [Fact]
    public void SearchSingle_NoMatch_ReturnsNull()
    {
        var patrons = new List<Patron>
        {
            new Patron("Patron 1", "M001", "patron1@example.com"),
            new Patron("Patron 2", "M002", "patron2@example.com"),
        };

        var result = _patronSearcher.SearchSingle(patrons, p => p.MemberShipNumber == "M003");

        Assert.Null(result);
    }
}
