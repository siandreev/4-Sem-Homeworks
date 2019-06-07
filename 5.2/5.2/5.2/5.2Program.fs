module Logic

     let func x l = List.map (fun y -> y * x) l

     let func'1 x : (int) list -> (int) list =
        List.map(fun y -> y * x)
    
     let func'2 x : (int) list -> (int) list =
        List.map((*) x )

     let func'3 : int -> int list -> int list = 
         List.map << (*) 
         
     printf "%A" <| func -1 [-1; 1]
     printf "%A" <| func'3 -1 [-1; 1]
     System.Console.ReadKey() |> ignore