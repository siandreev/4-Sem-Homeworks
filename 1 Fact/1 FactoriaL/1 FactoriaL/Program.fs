let x = System.Console.ReadLine() |>int

let fact n =
    let rec factorial n acc =
        try
            match n with
            | n when (n < 0) || (n > 33) -> failwith "Incorrect nomber"
            | 0 -> acc
            | _ -> factorial (n - 1) (acc * n)
        with :? System.Exception as ex-> printfn "Exception %s" (ex.Message); -1       
    factorial n 1
printf "%A" (fact x)
    

