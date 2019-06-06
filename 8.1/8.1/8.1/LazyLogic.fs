module LazyLogic 

    open System.Threading
  
    /// итерфейс Lazy вычислений
    type ILazy<'a> =
        abstract member Get: unit -> 'a

    /// однопоточный режим
    type LazyMono<'a> (supplier : unit -> 'a) =
        let mutable calcResult = None

        interface ILazy<'a> with
            member this.Get() =
                match calcResult with 
                /// считаем, только если до этого не считали
                | Some value -> value
                | None -> 
                    let createResult = supplier ()
                    calcResult <- Some createResult
                    calcResult.Value

    /// многопоточный простой режим
    type LazyAsync<'a> (supplier : unit -> 'a) =
        let mutable calcResult = None
        let lockobj = obj()
        interface ILazy<'a> with
            member this.Get() =
                Monitor.Enter lockobj
                try
                    match calcResult with 
                    /// считаем, только если до этого не считали
                    | Some value -> value
                    | None -> 
                        let createResult = supplier ()
                        calcResult <- Some createResult
                        calcResult.Value
                finally
                    Monitor.Exit lockobj

    /// lock-free режим
     type LazyLockFree<'a> (supplier : unit -> 'a) =
        let mutable calcResult = None
        let checkValue = calcResult
        interface ILazy<'a> with
            member this.Get() =
                let rec repeat () =
                    match calcResult with                      
                        | None -> 
                            let createResult = supplier ()
                            Interlocked.CompareExchange(&calcResult, Some createResult, checkValue) |> ignore
                            repeat ()
                         | Some value -> value
                repeat () 

    


    
        