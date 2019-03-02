let str1 = System.Console.ReadLine();

let ch str=
    let rec check str n = 
        match str with
        |_ when n >= String.length(str) - n - 1 -> true
        |_ when str.[n] = str.[String.length(str) - n - 1] -> check str (n+1)
        |_ when str.[n] <> str.[String.length(str) - n - 1] -> false
    check str 0
printf "%A" (ch str1);
System.Console.ReadKey();