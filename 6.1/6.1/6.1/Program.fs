module Logic
    open System

    // класс, описывающий операционные системы : имя и вероятность заражения
    type OperationSystem (name : string, probabilityOfInfection : double) =
        member this.name = name
        member this.probabilityOfInfection = probabilityOfInfection
    
    // класс, описывающий компьтеры : уникальный номер, ОС, заражен или нет.
    type Computer (id : int, system : OperationSystem, isInfected : bool) =
        let mutable IsInfected = isInfected
        member this.id = id
        member this.system = system
        member this.isInfected = IsInfected
        member this.chengeInfectedStatus = IsInfected <- true

    // класс, описывающий процедуру заражения : берем 2 компа (1 заражет 2) если 1 заражен, а 2- нет, то начинаем заражение путем умножения вероятности заражения
    // второго на рандомное число от 1 до 100, сравнивая потом это с 50 и выводя результат на консоль.  
    type TryToInfect(comp1 : Computer, comp2 : Computer) =
        member this.LetsTry =
            if (comp1.isInfected && not comp2.isInfected) then 
               let rand = System.Random()
               let randomNumber = (double) <|rand.Next(1,101)
               if  randomNumber <= (double) (comp2.system.probabilityOfInfection * 100.0)  then
                   comp2.chengeInfectedStatus
                   Console.WriteLine("Computer " + string comp2.id + " was infected from computer " + string comp1.id + " succesfully")
               else
                   Console.WriteLine("Computer " + string comp2.id + " was NOT infected from computer " + string comp1.id)
    
    // класс, который отвечает за вывод информации на консоль
    type Draw (matrix : int[,], computersArray : Computer[]) =
        // метод вывода состояния заражения: зараженных красим в красный, здоровых- в белый
        member this.DarwStage =
            for i in 0..(computersArray.Length - 1) do
                if computersArray.[i].isInfected then 
                    Console.ForegroundColor <- ConsoleColor.Red
                    Console.Write(computersArray.[i].id)
                    Console.Write(" ")
                else
                    Console.ForegroundColor <- ConsoleColor.White
                    Console.Write(computersArray.[i].id)
                    Console.Write(" ")
            Console.ForegroundColor <- ConsoleColor.White
            Console.WriteLine()
            Console.Write("- - - - - - - - - - - - - - - - - - - - - - - - - - -")

        // метод вывода на экран стартовой информации: исходной конфигурации заражения и матрицы смежности
        member this.Start =
            Console.WriteLine("Start")
            for i in 0..(computersArray.Length - 1) do
                if computersArray.[i].isInfected then 
                    Console.ForegroundColor <- ConsoleColor.Red
                    Console.Write(computersArray.[i].id)
                    Console.Write(" ")
                else
                    Console.ForegroundColor <- ConsoleColor.White
                    Console.Write(computersArray.[i].id)
                    Console.Write(" ")
            Console.ForegroundColor <- ConsoleColor.White
            Console.WriteLine()
            let limit =  (int <| Math.Sqrt( (float) matrix.Length) ) - 1
            for i in 0..limit do
                Console.WriteLine()
                for j in 0..limit do
                    Console.Write(string matrix.[i,j] + " ")
            Console.WriteLine()
            Console.Write("- - - - - - - - - - - - - - - - - - - - - - - - - - -")
            Console.WriteLine()

    //основная логика работы алгоритма. matrix -матрица смежности, computersArray- массив компов
    type MakeSteps(matrix : int[,], computersArray : Computer[]) = 

        // метод который делает ход. computersData- массив-копия copmutersArray с добавленным полем для пометки при обходе в ширину
        member this.Step turn =     
            let mutable computersData = [| for i in 0..5 -> (new Computer(i, computersArray.[i].system, computersArray.[i].isInfected) , false)|]
            Console.WriteLine("Step " + string turn)

            // рекурсивная функция для обхода в ширину. queue- очередь обхода в ширину
            let rec recStep  (queue : list<int>) =              
                match queue with 
                | x :: xs ->
                    let id = x
                    computersData.[id] <- (fst computersData.[id], true) // помечаем очередную вершину
                    let limit =  (int <| Math.Sqrt( (float) matrix.Length) ) - 1
                    for i in 0 .. limit do // заражаем смежные с просматриваемым компы и в случае чего меняем статус в computersArry, чтобы избежать влияния новозараженного в этом ходе
                        if (matrix.[id,i] = 1) then
                            TryToInfect(fst computersData.[id], computersArray.[i]).LetsTry

                    // добавляем всех еще не помеченных и связанных с рассматриваемым компов
                    let newQueue = xs @ (List.filter(fun x -> x <> 0 && snd computersData.[x] = false) <| List.mapi(fun i x -> i*x)  (matrix.[id,*] |> Array.toList))     
                    recStep newQueue 
                | [] -> None
            recStep <| [0] 
            Draw(matrix, computersArray).DarwStage // рисуем состояние
            computersArray

    // весь код далее- тестовые данные для просмотра работы алгоритма вручную
    type TestData() =
        member this.Start =
            let testDataMatrix = array2D [ [0;1;1;0;0;0]; [1;0;1;0;1;1]; [1;1;0;1;1;0]; [0;0;1;0;0;0]; [0;1;1;0;0;0]; [0;1;0;0;0;0] ]
            let Windows probability = OperationSystem("Windows", probability)
            let Linux probability = OperationSystem("Linux", probability)
            let Ubuntu probability = OperationSystem("Ubuntu", probability)

            let mutable testDataComputers = [|
                Computer(0, Windows 0.75, true)
                Computer(1, Linux 0.25, false)
                Computer(2, Windows 0.75, false)
                Computer(3, Ubuntu 0.5, false)
                Computer(4, Ubuntu 0.5, false)
                Computer(5, Linux 0.25, false) |]

            let mutable turn = 0

            Draw(testDataMatrix, testDataComputers).Start

            // пока не все заражены делаем ходы по нажатию любой клавиши
            while  (Array.filter(fun x -> x = false) <| (Array.map(fun (x : Computer) -> x.isInfected) <| testDataComputers) <> [||])  do      
                testDataComputers <- MakeSteps(testDataMatrix, testDataComputers).Step turn
                turn <- turn + 1
                Console.ReadLine()
    
    TestData().Start
    
       
