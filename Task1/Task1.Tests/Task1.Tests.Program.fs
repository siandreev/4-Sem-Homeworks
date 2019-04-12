module Tests
    open Logic
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``check that the sum of even numbers of fibonacci, not exceeding a million, is 1089154. This result was obtained using the service Wolfram Alpha``() =
        sumOfEvenFibonacci() |> should equal 1089154
