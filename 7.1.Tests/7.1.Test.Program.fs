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
        res |> should equal  0.048

    [<Test>]
    let ``check example from task with 15 signs``()=
        let res = RoundingBuilder 15 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        res |> should equal  0.047619047619047998 