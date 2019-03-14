module tests
    open logic
    open FsUnit
    open NUnit.Framework

    [<Test>]
    let ``Сalculate 2 + 2`` () =
        eval (Addition(Number 2, Number 2)) |> should equal 4

    [<Test>]
    let ``Сalculate (7 + 5) * 2 / 3 - 2`` () =
        eval (Subtraction(Division(Multiplications(Addition(Number 7, Number 5),Number 2), Number 3), Number 2)) |> should equal 6

    [<Test>]
    let ``Output 0`` () =
        eval (Number 0) |> should equal 0
