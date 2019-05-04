module Logic

    type RoundingBuilder (precision : int) =
        member this.Bind(x, f) =
            f x
        member this.Return (x : float) =
            System.Math.Round(x, precision)

   


