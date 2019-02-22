let x = System.Console.ReadLine() |>int

let rec factorial n acc =
    try
        match n with
        | n when (n < 0) || (n > 33) -> failwith "Incorrect nomber"
        | 0 -> acc
        | _ -> factorial (n - 1) (acc * n)
    with :? System.Exception as ex-> printfn "Exception %s" (ex.Message); -1
let fact n = factorial n 1
printf "%A" (fact x)
    

