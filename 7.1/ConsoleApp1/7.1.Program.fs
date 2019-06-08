module Logic

    /// округление результата математических вычислений с заданной точностью
    type RoundingBuilder (precision : int) =
        member this.Bind(x : float, f : float -> float) =
            f (System.Math.Round(x, precision))
        member this.Return (x : float) =
            System.Math.Round(x, precision)