let x = System.Console.ReadLine() |>int

let factorial n =
    let rec fact n acc =
        match n with
        | n when (n < 0) || (n > 33) -> failwith "Incorrect nomber"
        | 0 -> acc
        | _ -> fact (n - 1) (acc * n)       
    fact n 1
printf "%A" (factorial x)
    

