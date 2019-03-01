module tests

open NUnit.Framework
open FsUnit
open logic



[<Test>]
let ``First appereance of 5 in list should be equal 4.`` () =
    check [1..10] 5 |> should equal (Some(5))

[<Test>]
let ``First appereance of 4 in list should be equal None.`` () =
    check [1..3] 4 |> should equal (None)

[<Test>]
let ``First appereance of 5 in empty list should be equal None.`` () =
    check [] 5 |> should equal None 
