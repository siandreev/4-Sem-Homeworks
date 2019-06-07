module Tests

    open Logic  
    open FsUnit
    open NUnit.Framework

    [<Test>]
    let ``check simple string`` ()=
        (checkString "a()b()c{}d[]e()") |> should equal true
   
    [<Test>]
    let ``check string with nested brackets`` ()=
        (checkString "[a(b({fff})c{x[x]x}d[]e())j]") |> should equal true
    
    [<Test>]
    let ``check incoreect string`` ()=
        (checkString "[a]()b[c{de}f()g{[h]}") |> should equal false

    [<Test>]
    let ``check string with incoreect nested brackets`` ()=
        (checkString "[a(b{(fff})c{x[x]x}d[]e())j]") |> should equal false

    [<Test>]
    let ``check the string without brackets`` ()=
        (checkString "abcdef") |> should equal true

    [<Test>]
    let ``check empty string`` ()=
        (checkString "") |> should equal true


    

