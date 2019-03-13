module tests
    
    open NUnit.Framework
    open FsUnit
    open logic

    [<Test>]
    let ``ListRevers 1 with list [1; 2; 3; 4; 5; 6; 7; 8; 9; 10] should give out [10; 9; 8; 7; 6; 5; 4; 3; 2; 1]`` () =
        reverse [1; 2; 3; 4; 5; 6; 7; 8; 9; 10] |> should equal  [10; 9; 8; 7; 6; 5; 4; 3; 2; 1]

    [<Test>]
    let ``ListReverse 2 with list [-3; -1; -2] should give out [-2; -1; -3]`` ()=
        reverse [-3; -1; -2] |> should equal [-2; -1; -3]

    [<Test>]
    let ``ListReverse 3 with list [] should give out []`` ()=
        reverse [] |> should equal []

