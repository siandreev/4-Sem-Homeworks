let n = System.Console.ReadLine() |> int
let m = System.Console.ReadLine() |> int

try
    if (n > m) || (m>32) then failwith "Incorrect nomber"
    else 
        let rec pow k value AccList =
            match k with
            | k when k < n -> pow (k + 1) (2 * value) AccList
            | k when k >= n && k <= m -> pow (k + 1) (2 * value) (value::AccList)
            | k when k > m -> List.rev(AccList)
        printf "%A" (pow 0 1 [])
 with :? System.Exception as ex-> printfn "Exception %s" (ex.Message); 
