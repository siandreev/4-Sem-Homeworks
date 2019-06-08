module LazyFactory

open LazyLogic

    /// создание объекта одного из классов разного (по поточности) выполнения вычислений  
    type LazyFactory() =
        static member CreateSingleThreadedLazy (supplier : unit -> 'a) =
            new LazyLogic.LazyMono<'a>(supplier) :> ILazy<'a>
        static member CreateAsyncThreadedLazy (supplier : unit -> 'a) =
            new LazyLogic.LazyAsync<'a>(supplier) :> ILazy<'a>
        static member CreateLockFreeThreadedLazy (supplier : unit -> 'a) =
            new LazyLogic.LazyLockFree<'a>(supplier) :> ILazy<'a>

