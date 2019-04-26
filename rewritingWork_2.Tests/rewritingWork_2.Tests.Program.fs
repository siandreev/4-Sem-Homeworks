module Tests
    open Logic
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let `` check that the palindrome, which is issued by the program, coincides with the palindrome, which was calculated manually``() =
        searchPalindrome() |> should equal 906609