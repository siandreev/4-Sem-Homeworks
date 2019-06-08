module Tests

    open NUnit.Framework
    open FsUnit
    open Logic

    [<Test>]
    let ``check example from task``()=
        let res = RoundingBuilder 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        res |> should (equalWithin 0.00001)  0.048

    [<Test>]
    let ``check example from task with 5 signs``()=
        let res = RoundingBuilder 5 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        res |> should (equalWithin 0.0000001)  0.04762 