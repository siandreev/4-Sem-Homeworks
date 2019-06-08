module Logic
    open System

    // класс, описывающий операционные системы : имя и вероятность заражения
    type OperationSystem (name : string, probabilityOfInfection : double) =
        member this.Name = name
        member this.ProbabilityOfInfection = probabilityOfInfection
    
    // класс, описывающий компьтеры : уникальный номер, ОС, заражен или нет.
    type Computer (id : int, system : OperationSystem, isInfected : bool) =
        let mutable isInfected = isInfected
        member this.Id = id
        member this.System = system
        member this.IsInfected = isInfected
        member this.ChangeInfectedStatus = isInfected <- true

    // класс, описывающий процедуру заражения : берем 2 компа (1 заражет 2) если 1 заражен, а 2- нет, то начинаем заражение путем умножения вероятности заражения
    // второго на рандомное число от 1 до 100, сравнивая потом это с 50 и выводя результат на консоль.  
    type Infector(comp1 : Computer, comp2 : Computer) =
        let rand = System.Random()
        member this.LetsTry =
            if (comp1.IsInfected && not comp2.IsInfected) then            
               let randomNumber = (double) <| rand.Next(1, 101)
               if  randomNumber <= (double) (comp2.System.ProbabilityOfInfection * 100.0)  then
                   comp2.ChangeInfectedStatus
                   Console.WriteLine("Computer " + string comp2.Id + " was infected from computer " + string comp1.Id + " succesfully")
               else
                   Console.WriteLine("Computer " + string comp2.Id + " was NOT infected from computer " + string comp1.Id)
    
    // класс, который отвечает за вывод информации на консоль
    type Drawer(matrix : int[,], computersArray : Computer[]) =
        // метод вывода состояния заражения: зараженных красим в красный, здоровых- в белый
        member this.DrawStage =
            for i in 0..(computersArray.Length - 1) do
                if computersArray.[i].IsInfected then 
                    Console.ForegroundColor <- ConsoleColor.Red
                    Console.Write(computersArray.[i].Id)
                    Console.Write(" ")
                else
                    Console.ForegroundColor <- ConsoleColor.White
                    Console.Write(computersArray.[i].Id)
                    Console.Write(" ")
            Console.ForegroundColor <- ConsoleColor.White
            Console.WriteLine()
            Console.Write("- - - - - - - - - - - - - - - - - - - - - - - - - - -")

        // метод вывода на экран стартовой информации: исходной конфигурации заражения и матрицы смежности
        member this.Start =
            Console.WriteLine("Start")
            for i in 0..(computersArray.Length - 1) do
                if computersArray.[i].IsInfected then 
                    Console.ForegroundColor <- ConsoleColor.Red
                    Console.Write(computersArray.[i].Id)
                    Console.Write(" ")
                else
                    Console.ForegroundColor <- ConsoleColor.White
                    Console.Write(computersArray.[i].Id)
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
    type StepMaker(matrix : int[,], computersArray : Computer[]) = 

        // метод который делает ход. computersData- массив-копия copmutersArray с добавленным полем для пометки при обходе в ширину
        member this.Step turn =     
            let mutable computersData = [| for i in 0..5 -> (new Computer(i, computersArray.[i].System, computersArray.[i].IsInfected) , false)|]
            Console.WriteLine("Step " + string turn)

            // рекурсивная функция для обхода в ширину. queue- очередь обхода в ширину
            let rec recStep  (queue : list<int>) =              
                match queue with 
                | x :: xs ->
                    let id = x
                    computersData.[id] <- (fst computersData.[id], true) // помечаем очередную вершину
                    let limit =  (int <| Math.Sqrt( (float) matrix.Length) ) - 1
                    for i in 0 .. limit do // заражаем смежные с просматриваемым компы и в случае чего меняем статус в computersArry, чтобы избежать влияния новозараженного в этом ходе
                        if (matrix.[id, i] = 1) then
                            Infector(fst computersData.[id], computersArray.[i]).LetsTry

                    // добавляем всех еще не помеченных и связанных с рассматриваемым компов
                    let newQueue = xs @ (List.filter(fun x -> x <> 0 && snd computersData.[x] = false)
                        <| List.mapi(fun i x -> i * x)  (matrix.[id, *] |> Array.toList))     
                    recStep newQueue 
                | [] -> None
            recStep [0] |> ignore 
            Drawer(matrix, computersArray).DrawStage // рисуем состояние
            computersArray

    // весь код далее- тестовые данные для просмотра работы алгоритма вручную
    type TestData() =
        member this.Start =
            let testDataMatrix = array2D [ 
                [0; 1; 1; 0; 0; 0]; 
                [1; 0; 1; 0; 1; 1]; 
                [1; 1; 0; 1; 1; 0]; 
                [0; 0; 1; 0; 0; 0]; 
                [0; 1; 1; 0; 0; 0]; 
                [0; 1; 0; 0; 0; 0] ]
            let windows probability = OperationSystem("Windows", probability)
            let linux probability = OperationSystem("Linux", probability)
            let ubuntu probability = OperationSystem("Ubuntu", probability)

            let mutable testDataComputers = [|
                Computer(0, windows 0.75, true)
                Computer(1, linux 0.25, false)
                Computer(2, windows 0.75, false)
                Computer(3, ubuntu 0.5, false)
                Computer(4, ubuntu 0.5, false)
                Computer(5, linux 0.25, false) |]

            let mutable turn = 0

            Drawer(testDataMatrix, testDataComputers).Start

            // пока не все заражены делаем ходы по нажатию любой клавиши
            while  (Array.filter(fun x -> x = false) <| (Array.map(fun (x : Computer) -> x.IsInfected) <| testDataComputers) <> [||])  do      
                testDataComputers <- StepMaker(testDataMatrix, testDataComputers).Step turn
                turn <- turn + 1
                Console.ReadLine() |> ignore
    
    TestData().Start
    
       
