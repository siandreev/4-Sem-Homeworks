let x = System.Console.ReadLine() |> int 



let rec fibonachi n acc1 acc2 = 
    try
        match n with
        | n when n < 0 -> failwith "Incorrect nomber"
        | 0 -> acc2
        | _ -> fibonachi (n - 1) acc2 (acc1 + acc2)
    with :? System.Exception as ex-> printfn "Exception %s" (ex.Message); -1
let fib n = fibonachi n 0 1 
printfn "%A"(fib x)


       

