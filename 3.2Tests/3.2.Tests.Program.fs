module tests
    open FsUnit
    open NUnit.Framework
    open logic

    [<Test>]
    let ``double every element of the tree`` () =
        mapTree (fun x -> 2 * x) <| Tree(1, Tree(2, Tree(3, Tip 4, Tip 5), Tip 6), Tree(7, Tree(8, Tip 9, Tip 10), Tip 11)) |> should equal ( Tree(2, Tree(4, Tree(6, Tip 8, Tip 10), Tip 12), Tree(14, Tree(16, Tip 18, Tip 20), Tip 22)))

    [<Test>]
    let ``square each element of the tree`` () =
        mapTree (fun x -> x * x) <| Tree(1, Tree(2, Tree(3, Tip 4, Tip 5), Tip 6), Tree(7, Tree(8, Tip 9, Tip 10), Tip 11)) |> should equal ( Tree(1, Tree(4, Tree(9, Tip 16, Tip 25), Tip 36), Tree(49, Tree(64, Tip 81, Tip 100), Tip 121)))

    [<Test>]
    let ``reset each element of the tree`` () =
        mapTree (fun x -> 0) <| Tree(1, Tree(2, Tree(3, Tip 4, Tip 5), Tip 6), Tree(7, Tree(8, Tip 9, Tip 10), Tip 11)) |> should equal ( Tree(0, Tree(0, Tree(0, Tip 0, Tip 0), Tip 0), Tree(0, Tree(0, Tip 0, Tip 0), Tip 0)))

