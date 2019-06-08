module LazyLogic 

    open System.Threading
  
    /// интерфейс Lazy вычислений
    type ILazy<'a> =
        abstract member Get: unit -> 'a

    /// однопоточный режим
    type LazyMono<'a> (supplier : unit -> 'a) =
        let mutable calcResult = None

        interface ILazy<'a> with
            /// запускаем вычисление (только при первом вызове), выдаем результат вычисления
            member this.Get() =
                match calcResult with 
                | Some value -> value
                | None -> 
                    let createResult = supplier ()
                    calcResult <- Some createResult
                    calcResult.Value

    /// многопоточный простой режим
    type LazyAsync<'a> (supplier : unit -> 'a) =
        let monitor = obj;
        let mutable calcResult = None
        interface ILazy<'a> with
            /// запускаем вычисление (только при первом вызове), выдаем результат вычисления
            member this.Get() =
                match calcResult with 
                | Some value -> value
                | None -> 
                    lock monitor (fun () -> 
                    match calcResult with
                    | None ->
                        let createResult = supplier ()
                        calcResult <- Some createResult
                        createResult
                    | Some value -> value)

    /// lock-free режим
     type LazyLockFree<'a> (supplier : unit -> 'a) =
        let mutable calcResult = None
        let checkValue = calcResult
        interface ILazy<'a> with
            /// запускаем вычисление и выдаем результат
            member this.Get() =
                    match calcResult with                      
                    | None -> 
                        let createResult = supplier ()
                        Interlocked.CompareExchange(&calcResult, Some createResult, checkValue) |> ignore
                        match calcResult with
                        | None -> createResult
                        | Some value -> value
                    | Some value -> value
         

    


    
        