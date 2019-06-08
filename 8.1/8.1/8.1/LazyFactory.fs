module LazyFactory

open LazyLogic

    /// создание объекта одного из классов разного (по поточности) выполнения вычислений  
    type LazyFactory() =
        /// создание объкта для однопоточного вычисления
        static member CreateSingleThreadedLazy (supplier : unit -> 'a) =
            new LazyLogic.LazyMono<'a>(supplier) :> ILazy<'a>
        /// создание объкта для многопоточного вычисления
        static member CreateAsyncThreadedLazy (supplier : unit -> 'a) =
            new LazyLogic.LazyAsync<'a>(supplier) :> ILazy<'a>
        /// создание объкта для lock-free вычисления
        static member CreateLockFreeThreadedLazy (supplier : unit -> 'a) =
            new LazyLogic.LazyLockFree<'a>(supplier) :> ILazy<'a>

