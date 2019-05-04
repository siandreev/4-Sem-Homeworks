module Logic

    type StringCalcBuilder() =
        member this.Bind(s : string, f) =
            try
                f (int s)
            with
                :? System.FormatException -> printf "Wrong data"; None
        member this.Return(s) =
            Some(s)
       
           
