module Tests

    open NUnit.Framework
    open FsUnit
    open Logic

    [<Test>]
    let ``check function IsEmpty in empty list``() =
        let myStack1 = new Stack<int>([])
        myStack1.IsEmpty |> should equal true

    [<Test>]
    let ``check function IsEmpty in nonempty list``() =
        let myStack2 = new Stack<int>([1])
        myStack2.IsEmpty |> should equal false

    [<Test>]
    let ``check function Pop on nonempty list``() =
        let myStack3 = new Stack<int>([1; 2])
        myStack3.Pop |> should equal 1

    [<Test>]
    let ``check function Pop on empty list``() =
        let myStack4 = new Stack<int>([])
        (fun () -> myStack4.Pop |> ignore) |> should throw typeof<System.Exception>

    [<Test>]
    let ``check function Push``() =
        let myStack5 = new Stack<int>([1; 2; 3])
        myStack5.Push 4
        myStack5.Pop |> should equal 4

    

    