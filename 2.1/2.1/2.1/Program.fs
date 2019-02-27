
let li = [1;2;3;4;5]
let N = System.Console.ReadLine() |> int

let compare list1 template =
    let rec check list1 template count =
        let ch a b =
            if a=b then true
            else false
        match list1 with
        | x::xs -> ch x template ; if not (ch x template) then check xs template (count + 1) else Some(count)
        | [] -> None
    check list1 template 1
printf "%A" (compare li N )
System.Console.ReadKey();