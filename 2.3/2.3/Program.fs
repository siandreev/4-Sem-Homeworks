let rec sort list1 =
    match list1 with
    | [ ] -> [ ] 
    | x::xs -> sort(List.filter(fun n -> n < x) xs) @ [x] @ sort(List.filter (fun n -> n >= x) xs) 
printf "%A" (sort [3;2;1;4])
System.Console.ReadKey()
