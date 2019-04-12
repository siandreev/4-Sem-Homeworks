module Logic    
    open System
    //создание полной строки
    let rec printFullLine n acc stringAcc =
            match acc with
            | _ when acc = n + 1 -> stringAcc
            | _ ->  
                printFullLine n (acc + 1) (stringAcc + "*")
    // создание дырявой строки
    let rec printHoleLine n acc stringAcc=
           match acc with
           | _ when acc = 1 -> printHoleLine n (acc + 1) (stringAcc + "*")
           | _ when acc = n -> printHoleLine n (acc + 1) (stringAcc + "*")
           | _ when acc = n + 1 -> stringAcc
           | _ -> printHoleLine n (acc + 1) (stringAcc + " ")
    // создание произвольной строки по номеру
    let rec printString n acc =
           match acc with
           | _ when acc = 1 -> (printFullLine n 1 "") 
           | _ when acc = n -> (printFullLine n 1 "") 
           | _ when acc = n + 1 -> ""
           | _ -> (printHoleLine n 1 "") 
    // печать всей картинки
    let printSquare n =      
        let rec printAllPicture n acc =
            match acc with
            | _ when acc <= n ->
                Console.WriteLine(printString n acc)
                printAllPicture n (acc + 1)
            | _ when acc = n + 1 -> Console.Write("")
        printAllPicture n 1
            