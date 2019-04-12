module Tests
    open Logic
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``check 3 line in 4 by 4 square``() =
        printString 4 3 |> should equal "*  *"

    [<Test>]
    let ``check 2 line in 5 by 5 square``() =
        printString 5 2 |> should equal "*   *"

    [<Test>]
    let ``check 1 line in 3 by 3 square``() =
        printString 3 1 |> should equal "***"

    [<Test>]
    let ``check 6 line in 6 by 6 square``() =
        printString 6 6 |> should equal "******"
