module Logic
 
    type Stack<'T>  (userStackList : list<'T>) =
        let mutable stackList = userStackList;

        // добваляем введенный элемент в голову стека
        member this.Push elem =
            stackList <- (elem :: stackList)

        // проверяем пустой ли стек
        member this.IsEmpty =
            if (stackList.IsEmpty) then
                true
            else
                false

        // если стек пуст- бросаем исключение. Иначе сохраняем голову, чтобы потом ее вернуть и обрезаем список
        member this.Pop =
            if (stackList.IsEmpty) then
                failwith "Can't pop an emty stack"
            else
                let result = stackList.Head
                stackList <- stackList.Tail
                result

    let myStack = new Stack<int>([1; 2; 3])
    myStack.Push 4
    myStack.Push 5
    System.Console.ReadKey()
            

      