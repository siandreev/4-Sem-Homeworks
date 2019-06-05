module Logic
    /// описание типа лямбда-выражение
    type LambdaTherm =
    | Variable of char
    | Combination of  LambdaTherm * LambdaTherm
    | Abstraction of  char * LambdaTherm

    /// функция для получения списка свободных переменных в лямбда-выражении
    let rec  getFreeVariables (mainExpression : LambdaTherm) =
        let rec fV mainExpression listOfFV =
            match mainExpression with
            | Variable variable-> variable::listOfFV
            | Combination(expression1, expression2) -> listOfFV @ getFreeVariables(expression1) @ getFreeVariables(expression2)
            | Abstraction(variable, expression) -> listOfFV @ (List.filter(fun x -> x <> variable) <| getFreeVariables(expression))
        fV mainExpression []

    /// функция для получения нового имени для переменования связной переменной
    let getNewName (expression1 : LambdaTherm) (expression2 : LambdaTherm) =
        let listFV1 = getFreeVariables expression1
        let listFV2 = getFreeVariables expression2
        let listFV = listFV1 @ listFV2
        let allSymbolsList = ['a'..'z']
        let availableSymbilsList = List.filter(fun x -> not(List.contains(x) listFV)) allSymbolsList
        availableSymbilsList.Head
        
    /// функция подстановки в mainExpression терма term вместо вхождений переменной mainVariable
    /// (с поддержкой альфа-конверсии в случае, если переменная связана)
    let rec substitution mainExpression mainVariable term =
        match mainExpression with
        | Variable variable when variable = mainVariable -> term
        | Variable variable  -> LambdaTherm.Variable variable
        | Combination(expression1, expression2) -> 
            LambdaTherm.Combination(substitution expression1 mainVariable term, substitution expression2 mainVariable term)
        | Abstraction(variable,expression) when variable = mainVariable -> mainExpression
        | Abstraction(variable,expression)  -> 
            if (not(List.contains(variable) (getFreeVariables term)) || 
                not(List.contains(mainVariable) (getFreeVariables expression))) then 
                let (newExpression : LambdaTherm) = substitution expression mainVariable term
                Abstraction(variable, newExpression)
            else 
                let newVariable = getNewName mainExpression term
                let firstSub = substitution expression variable (LambdaTherm.Variable newVariable)
                let SecondSub = substitution firstSub mainVariable term
                Abstraction(newVariable, SecondSub)
     
    /// бета-редукция лямбда-выражения по нормальной стратегии
    let rec betaReduction (mainExpression: LambdaTherm) =
        match mainExpression with
        | Variable variable -> LambdaTherm.Variable variable
        | Abstraction(variable, term) -> Abstraction(variable, betaReduction term)
        | Combination(Abstraction(variable, expression), term) ->
            betaReduction (substitution expression variable term)
        | Combination(expression1, expression2) ->
            let reductionExp1 = betaReduction expression1
            match reductionExp1 with 
            | Abstraction(variable, expression) -> 
                betaReduction(Combination(reductionExp1, expression2))
            | _ ->
                let reductionExp2 = betaReduction expression2
                Combination(reductionExp1, reductionExp2)

    /// нерекурсивная обертка для бета-редукции
    let reduction expression =
        betaReduction expression





      
         
            
        
  