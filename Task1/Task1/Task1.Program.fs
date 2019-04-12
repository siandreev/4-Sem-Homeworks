module Logic

    open System
    // необходимая функция 
    let sumOfEvenFibonacci() =
        // возвращаем сумму когда очередное число acc2 >=1000000
        let rec fib acc1 acc2 sum =
            match acc2 with 
            | _ when acc2 >= 1000000 -> sum
            | _ -> 
                // если очередное число делится на 2 то добавляем его в сумму и вызываем рекурентно fib. Иначе просто вызывем fib
                match acc2 with 
                | _ when acc2 % 2 = 0 ->fib acc2 (acc1 + acc2) sum + acc2
                | _ -> fib acc2 (acc1 + acc2) sum
        fib 1 1 0

    printf "%A" sumOfEvenFibonacci
            
