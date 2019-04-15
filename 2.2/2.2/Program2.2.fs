module Logic

    let checkIsPalindrome str=
        let rec recChecking str n = 
            match str with
            | _ when n >= String.length(str) - n - 1 -> true
            | _ when str.[n] = str.[String.length(str) - n - 1] -> recChecking str (n+1)
            | _ when str.[n] <> str.[String.length(str) - n - 1] -> false
        recChecking str 0
