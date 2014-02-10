namespace RedditDailyProgrammer.C001_H
module Tests =
    open Xunit

    [<Fact>]
    let ParseDate_ReturnsValidDateTime() =
        let str = "2014-01-01T12:13:14"
        let wasSuccessful, datetime = System.DateTimeOffset.TryParse(str)
        let properTime = new System.DateTimeOffset(2014,01,01,12,13,14,new System.TimeSpan(-5,0,0))
        Assert.Equal(properTime,datetime)