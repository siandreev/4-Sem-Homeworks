module tests
    open FsUnit
    open NUnit.Framework
    open logic

    [<Test>]
    let ``Map count of even num in list [-10..10] should be equal 11`` () =
        evenCountMap [-10..10] |> should equal 11

    [<Test>]
    let ``Map count of even num in list [0..10] should be equal 6`` () =
        evenCountMap [0..10] |> should equal 6

    [<Test>]
    let ``Map count of even num in empty list should be equal 0`` () =
        evenCountMap [] |> should equal 0

    [<Test>]
    let ``Filter count of even num in list [-10..10] should be equal 11`` () =
        evenCountFilter [-10..10] |> should equal 11

    [<Test>]
    let ``Filter count of even num in list [0..10] should be equal 6`` () =
        evenCountFilter [0..10] |> should equal 6

    [<Test>]
    let ``Filter count of even num in empty list should be equal 0`` () =
        evenCountFilter [] |> should equal 0

    [<Test>]
    let ``Fold count of even num in list [-10..10] should be equal 11`` () =
        evenCountFold [-10..10] |> should equal 11

    [<Test>]
    let ``Fold count of even num in list [0..10] should be equal 6`` () =
        evenCountFold [0..10] |> should equal 6

    [<Test>]
    let ``Fold count of even num in empty list should be equal 0`` () =
        evenCountFold [] |> should equal 0

