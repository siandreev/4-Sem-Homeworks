module Logic
    // делаем размеченное объединение дерева разбора арифметического выражения: в нем могут быть числа, и операции +, -, *, /
    type Operation =
    | Number of int
    | Addition of Operation * Operation
    | Subtraction of Operation * Operation
    | Multiplications of Operation * Operation
    | Division of Operation * Operation

    // функция eval последовательно раскрывает арифметическое выражение
    let rec eval (o : Operation) = 
        match o with
        | Number n -> n
        | Addition(n1, n2) -> (eval n1) + (eval n2)
        | Subtraction(n1,n2) -> (eval n1) - (eval n2)
        | Multiplications(n1, n2) -> (eval n1) * (eval n2)
        | Division(n1, n2) -> 
            try (eval n1) / (eval n2)
            with :? System.Exception as ex-> printfn "Exception %s" (ex.Message); 0
     



