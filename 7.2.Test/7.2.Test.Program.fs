module Tests

    open NUnit.Framework
    open FsUnit
    open Logic

    [<Test>]
    let ``check example from task``() =
        let result = StringCalcBuilder() {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        }
        result |> should equal <| Some(3) 

    [<Test>]
    let ``check wrong example from task``() =
        let result = StringCalcBuilder() {
            let! x = "1"
            let! y = "Ъ"
            let z = x + y
            return z
        }
        result |> should equal <| None 

