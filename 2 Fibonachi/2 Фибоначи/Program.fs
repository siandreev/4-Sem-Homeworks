let x = System.Console.ReadLine() |> int 

let fibonachi n =
    let rec fib n acc1 acc2 = 
            match n with
            | n when n < 0 -> failwith "Incorrect nomber"
            | 0 -> acc2
            | _ -> fib (n - 1) acc2 (acc1 + acc2)
    fib n 0 1 
printfn "%A"(fibonachi x)


       

