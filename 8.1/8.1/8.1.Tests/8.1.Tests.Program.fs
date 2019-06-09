module Tests
    open System.Threading
    open System.Threading.Tasks
    open FSharp.Collections.Array.Parallel
    open LazyFactory
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``lets check one thread mode``() =
        let lazyCalc = LazyFactory.CreateSingleThreadedLazy(fun () -> 239)
        lazyCalc.Get () |> should equal 239

    [<Test>]
    let ``check that the value is calculated once in second mode``() =
        let mutable count = (int64) 0
        let lazyCalc = LazyFactory.CreateAsyncThreadedLazy(fun () -> 
            Interlocked.Increment &count |> ignore 
            (Interlocked.Read &count) |> should equal 1)             
        for i in 1..10 do
            Task.Run(fun () -> lazyCalc.Get ()) |> ignore

    [<Test>]
    let ``check that all values match in async mode``() =
        let lazyCalc = LazyFactory.CreateAsyncThreadedLazy(fun () -> new obj())
        let exp = lazyCalc.Get()
        map(fun obj -> exp |> (lazyCalc.Get ()).Equals |> should be True) [|1..10|] |> ignore

    [<Test>]
    let ``lets check lock-free mode``() =
        let lazyCalc = LazyFactory.CreateLockFreeThreadedLazy(fun () -> 239)
        lazyCalc.Get () |> should equal 239

    [<Test>]
    let ``check that all values match in lock-free mode``() =
        let lazyCalc = LazyFactory.CreateLockFreeThreadedLazy(fun () -> new obj())
        let exp = lazyCalc.Get()
        map(fun obj -> exp |> (lazyCalc.Get ()).Equals |> should be True) [|1..10|] |> ignore

    