module logic

    let checkPalindrome str=
        let rec ch str n = 
            match str with
            |_ when n >= String.length(str) - n - 1 -> true
            |_ when str.[n] = str.[String.length(str) - n - 1] -> ch str (n+1)
            |_ when str.[n] <> str.[String.length(str) - n - 1] -> false
        ch str 0
