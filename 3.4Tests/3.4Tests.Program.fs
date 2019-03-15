module tests
    open logic
    open FsUnit
    open NUnit.Framework

    [<Test>]
    let ``Check if seq cintains prime num 7`` () = 
        Seq.exists (fun x -> x = 7) (createPrimesSeq())  |> should equal true

    [<Test>]
    let ``Check if seq contains prime num 239`` () = 
        Seq.exists (fun x -> x = 239) (createPrimesSeq()) |> should equal true

    [<Test>]
    let ``Let's see 24th prime number`` () = 
        createPrimesSeq () |> Seq.skip 23 |> Seq.take 1 |> Seq.toArray |> should equal [|89|]