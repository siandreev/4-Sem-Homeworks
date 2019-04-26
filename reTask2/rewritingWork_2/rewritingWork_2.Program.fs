module Logic    
    // проверка, является ли строка палиндромом
    let palindromeCheck (x : int)=
        let rec check str n = 
            if n >= String.length(str) - n - 1 then true
            else
                if str.[n] = str.[String.length(str) - n - 1] then check str (n + 1)
                else false
        check (string x) 0

    // перебираем все пары трехзначных чисел, храня максимальный на данный момент палиндром. Если находим больше- меняем. Когда переберем все пары- возвращаем максимум.
    let searchPalindrome() =
        let rec multplyiAndCheck first second maxPalindrome =
            match first with
            | _ when (first = 99) -> maxPalindrome
            | _ when palindromeCheck (first*second) ->
                if first*second > maxPalindrome then multplyiAndCheck first (second - 1) (first*second)
                else multplyiAndCheck first (second - 1) maxPalindrome
            | _ when (second = 100) -> multplyiAndCheck (first - 1) 999 maxPalindrome
            | _ -> multplyiAndCheck first (second - 1) maxPalindrome
        multplyiAndCheck 999 999 0


 