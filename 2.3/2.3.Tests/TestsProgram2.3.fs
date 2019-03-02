module tests 

    open NUnit.Framework
    open FsUnit
    open logic 

    [<Test>]
    let ``sort 1 with list [3; 1; 2; 4] should give out [1; 2; 3; 4]`` () =
        sort [3; 1; 2; 4] |> should equal [1; 2; 3; 4]

    [<Test>]
    let ``sort 2 with list [-2; -3; -2] should give out [-3; -2; -2]`` () =
        sort [-2; -3; -2] |> should equal [-3; -2; -2]

    [<Test>]
    let ``sort 3 with empty list should give out []`` () =
        sort [] |> should equal [] 