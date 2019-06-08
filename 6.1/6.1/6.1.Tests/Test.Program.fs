module Tests
    open Logic
    open NUnit.Framework
    open FsUnit

    // тестовые данные: матрица смежности и типы ос
    let testDataMatrix = array2D [ [0;1;1;0;0;0]; [1;0;1;0;1;1]; [1;1;0;1;1;0]; [0;0;1;0;0;0]; [0;1;1;0;0;0]; [0;1;0;0;0;0] ]
    let Windows probability = OperationSystem("Windows", probability)
    let Linux probability = OperationSystem("Linux", probability)
    let Ubuntu probability = OperationSystem("Ubuntu", probability)

    // массив компьютеров с возможностью выставления вероятности заражения для каждой ос
    let  testDataComputers pWin pLin pUbu = [|
        Computer(0, Windows pWin, true)
        Computer(1, Linux pLin, false)
        Computer(2, Windows pWin, false)
        Computer(3, Ubuntu pUbu, false)
        Computer(4, Ubuntu pUbu, false)
        Computer(5, Linux pLin, false) |]

    // функция, возвращающая массив из bool- ов ,соответствующим зараженности компов через заданное число ходов
    let stateAfterCoupleMoves computersData countOfMoves = 
        let rec makeStep computersData acc =
            match acc with
            | _ when acc = countOfMoves -> Array.map(fun (x : Computer) -> x.IsInfected) computersData
            | _ -> makeStep (StepMaker(testDataMatrix, computersData).Step acc) (acc + 1)
        makeStep computersData 0
    
    [<Test>]
    let ``check that with the probability of infection of 1 for all, the algorithm will behave like a detour in width``() =
        stateAfterCoupleMoves (testDataComputers 1.0 1.0 1.0) 1 |> should equal [| true; true; true; false; false; false |]
        stateAfterCoupleMoves (testDataComputers 1.0 1.0 1.0) 2 |> should equal [| true; true; true; true; true; true |]

    [<Test>]
    let ``check that, with a probability of infection of 1 for everyone except Linux, and 0 for Linux, the virus will behave correctly``() =
        stateAfterCoupleMoves (testDataComputers 1.0 0.0 1.0) 1 |> should equal [| true; false; true; false; false; false |]
        stateAfterCoupleMoves (testDataComputers 1.0 0.0 1.0) 2 |> should equal [| true; false; true; true; true; false |]

    [<Test>]
    let ``check that with the probability of infection at all, no one will be infected even through a large number of moves``() =
        stateAfterCoupleMoves (testDataComputers 0.0 0.0 0.0) 239 |> should equal [| true; false; false; false; false; false |]
        stateAfterCoupleMoves (testDataComputers 0.0 0.0 0.0) 2390 |> should equal [| true; false; false; false; false; false |]
           
       
    
   