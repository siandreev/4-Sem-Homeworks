module Logic
    /// описание типа лямбда-выражение
    type LambdaTerm =
    | Variable of char
    | Application of  LambdaTerm * LambdaTerm
    | Abstraction of  char * LambdaTerm

    /// функция для получения списка свободных переменных в лямбда-выражении
    let rec  getFreeVariables (mainExpression : LambdaTerm) =
        let rec fV mainExpression listOfFV =
            match mainExpression with
            | Variable variable -> variable :: listOfFV
            | Application(expression1, expression2) -> listOfFV @ getFreeVariables(expression1) @ getFreeVariables(expression2)
            | Abstraction(variable, expression) -> listOfFV @ (List.filter(fun x -> x <> variable) <| getFreeVariables(expression))
        fV mainExpression []

    /// функция для получения нового имени для переменования связной переменной
    let getNewName (expression1 : LambdaTerm) (expression2 : LambdaTerm) =
        let listFV1 = getFreeVariables expression1
        let listFV2 = getFreeVariables expression2
        let listFV = listFV1 @ listFV2
        let allSymbolsList = ['a'..'z']
        let availableSymbolsList = List.filter(fun x -> not(List.contains(x) listFV)) allSymbolsList
        availableSymbolsList.Head
        
    /// функция подстановки в mainExpression терма term вместо вхождений переменной mainVariable
    /// (с поддержкой альфа-конверсии в случае, если переменная связана)
    let rec substitution mainExpression mainVariable term =
        match mainExpression with
        | Variable variable when variable = mainVariable -> term
        | Variable variable  -> LambdaTerm.Variable variable
        | Application(expression1, expression2) -> 
            LambdaTerm.Application(substitution expression1 mainVariable term, substitution expression2 mainVariable term)
        | Abstraction(variable, expression) when variable = mainVariable -> mainExpression
        | Abstraction(variable, expression) -> 
            if (not(List.contains(variable) (getFreeVariables term)) || 
                not(List.contains(mainVariable) (getFreeVariables expression))) then 
                let (newExpression : LambdaTerm) = substitution expression mainVariable term
                Abstraction(variable, newExpression)
            else 
                let newVariable = getNewName mainExpression term
                let firstSub = substitution expression variable (LambdaTerm.Variable newVariable)
                let secondSub = substitution firstSub mainVariable term
                Abstraction(newVariable, secondSub)
     
    /// бета-редукция лямбда-выражения по нормальной стратегии
    let rec betaReduction (mainExpression: LambdaTerm) =
        match mainExpression with
        | Variable variable -> LambdaTerm.Variable variable
        | Abstraction(variable, term) -> Abstraction(variable, betaReduction term)
        | Application(Abstraction(variable, expression), term) ->
            betaReduction (substitution expression variable term)
        | Application(expression1, expression2) ->
            let reductionExp1 = betaReduction expression1
            match reductionExp1 with 
            | Abstraction(variable, expression) -> 
                betaReduction(Application(reductionExp1, expression2))
            | _ ->
                let reductionExp2 = betaReduction expression2
                Application(reductionExp1, reductionExp2)

    /// нерекурсивная обертка для бета-редукции
    let reduction expression =
        betaReduction expression