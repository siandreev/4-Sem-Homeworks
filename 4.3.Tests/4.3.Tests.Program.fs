module Tests

    open Logic
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``check the correctness of the conversion in the case of only one variable``() =
        reduction(Variable('x')) |> should equal (Variable('x'))

    [<Test>]
    let ``check the correctness of the transformation in the case of the simplest case``() =
        let expression = (Combination(Abstraction('x', Variable('x')), Variable('y')))
        reduction expression |> should equal (Variable('y')) 

    [<Test>]
    let ``check the alpha-conversion``() =
        let K = Abstraction('x', Abstraction('y', Variable('x')))
        let Ky =Combination(K, Variable('y'))
        let expression = Combination(Abstraction('x', Abstraction('y', Variable('x'))), Variable('y'))
        reduction Ky |> should equal (Abstraction('a', Variable('y')))

    [<Test>]
    let ``check the second task in homework``() =
        let S = 
            Abstraction(
                'x', Abstraction(
                    'y', Abstraction(
                        'z', Combination(
                            Combination(Variable('x'), (Variable('z'))), 
                            Combination(Variable('y'), (Variable('z'))))))) 
        let K = Abstraction('x', Abstraction('y', Variable('x')))
        let SKK = Combination(Combination(S, K), K)
        let I = Abstraction('z',Variable('z'))
        reduction SKK |> should equal I
