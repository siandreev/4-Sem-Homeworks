module Logic    
    let palindromeCheck (x : int)=
        let rec check str n = 
            if n >= String.length(str) - n - 1 then true
            else
                if str.[n] = str.[String.length(str) - n - 1] then check str (n + 1)
                else false
        check (string x) 0

    let searchPalindrome() =
        let rec multplyiAndCheck first second maxPalindrome =
            match first with
            | _ when palindromeCheck (first*second) ->
                if first*second > maxPalindrome then multplyiAndCheck first (second - 1) (first*second)
                else multplyiAndCheck first (second - 1) maxPalindrome
            | _ when (second = 100) -> multplyiAndCheck (first - 1) 999 maxPalindrome
            | _ when (first = 100) -> maxPalindrome
            | _ -> multplyiAndCheck first (second - 1) maxPalindrome
        multplyiAndCheck 999 999 0


 