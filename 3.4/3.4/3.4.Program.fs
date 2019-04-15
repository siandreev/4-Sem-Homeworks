module Logic
    // вспомогательная функция для проверки числа на простоту: рекурсивно пробегаем все числа до корня из него и проверяем, не являются ли они делителями
    let isPrime n =
        let rec check i =
            i > n/2 || (n % i <> 0 && check (i + 1))
        check 2

    // бесконечно рекурсивно добавляем простые числа в последовательность
    let createPrimesSeq () =
        let rec create n =
            seq { if (isPrime n) then yield n; yield! create (n + 1)
                  else yield! create (n + 1)}
        create 2



   

    
