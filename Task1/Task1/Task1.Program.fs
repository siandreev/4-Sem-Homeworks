module Logic

    open System

    let sumOfEvenFibonacci() =
        let rec fib acc1 acc2 sum =
            match acc2 with 
            | _ when acc2 >= 1000000 -> sum
            | _ -> 
                match acc2 with 
                | _ when acc2 % 2 = 0 ->fib acc2 (acc1 + acc2) sum + acc2
                | _ -> fib acc2 (acc1 + acc2) sum
        fib 1 1 0

    printf "%A" sumOfEvenFibonacci
            
