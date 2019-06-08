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
    let ``check that all values match in second mode``() =
        let lazyCalc = LazyFactory.CreateAsyncThreadedLazy(fun () -> 12 + 21 )              
        Array.filter(fun x -> x = 33) (map(fun obj -> lazyCalc.Get()) [|1..10|]) |> Array.length |> should equal 10

    [<Test>]
    let ``lets check lock-free mode``() =
        let lazyCalc = LazyFactory.CreateLockFreeThreadedLazy(fun () -> 239)
        lazyCalc.Get () |> should equal 239

    [<Test>]
    let ``check that all values match in lock-free mode``() =
        let lazyCalc = LazyFactory.CreateLockFreeThreadedLazy(fun () -> 12 + 21 )              
        Array.filter(fun x -> x = 33) (map(fun obj -> lazyCalc.Get()) [|1..10|]) |> Array.length|> should equal 10


    
          


