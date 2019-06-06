module Tests
    open System.Threading
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
        let mutable count =  0
        let lazyCalc = LazyFactory.CreateAsyncThreadedLazy(fun () -> 
            Interlocked.Increment &count )              
        Array.sum (map(fun obj -> lazyCalc.Get()) [|1..10|]) |> should equal 10

    [<Test>]
    let ``check that all values match``() =
        let lazyCalc = LazyFactory.CreateAsyncThreadedLazy(fun () -> 12 + 21 )              
        Array.filter(fun x -> x=33) (map(fun obj -> lazyCalc.Get()) [|1..10|]) |> Array.length|> should equal 10

    [<Test>]
        let ``lets check lock-free mode``() =
            let lazyCalc = LazyFactory.CreateLockFreeThreadedLazy(fun () -> 239)
            lazyCalc.Get () |> should equal 239
    
          


