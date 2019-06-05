module Tests
    open Logic
    open FsUnit
    open NUnit.Framework

    [<Test>]
    let ``check count of references in some site``() =
        let url = "https://stackoverflow.com/questions/20714492/antlr4-listeners-and-visitors-which-to-implement"
        (webFunction url).Length |> should equal 3

    [<Test>]
    let ``check another site``() =
        let url = "https://www.anekdot.ru/tags/%D0%A3%D0%BA%D1%80%D0%B0%D0%B8%D0%BD%D0%B0"
        (webFunction url).Length |> should equal 2 
